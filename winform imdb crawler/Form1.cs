using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;
using CsvHelper;
using NCrawler;
using NCrawler.HtmlProcessor;

namespace winform_imdb_crawler
{
    public partial class MainForm : Form
    {
        private string _SaveFileName = "reviews.tsv";
        public string SaveFileName { get { return _SaveFileName; } }

        delegate void SetTextCallback(string text);

        static string cookieText = "";
        CookieContainer Cookies = new CookieContainer();

        private static DateTime NullDateTime = new DateTime(1, 1, 1);

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            tabControl1.SelectTab(2);

            webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(ShowCookie);
            webBrowser1.ScriptErrorsSuppressed = true;

            txtSaveFileName.Text = _SaveFileName;

            //no crawling until a cookie is set!
            this.btnCrawl.Enabled = false;


        }


        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://www.imdb.com");
        }

        static void ShowCookie(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser wb = ((WebBrowser)sender);
            if (wb.Document != null && wb.Document.Cookie != null)
            {
                cookieText = wb.Document.Cookie;
            }
            else
            {
                Console.WriteLine("wb document is null or cookie is null");
            }
        }

        private void btnSaveCookie_Click(object sender, EventArgs e)
        {
            try
            {
                cookieText = cookieText.Replace(';', ',');
                Cookies.SetCookies(new Uri("http://www.imdb.com"), cookieText);
                toolStripStatusLabel1.Text = "Cookie: " + cookieText.Substring(0, 20);

                appendLineToLog(cookieText);

                //assumes cookie is successfully set
                this.btnCrawl.Enabled = true;
            }
            catch (Exception ex)
            {
                appendLineToLog("Error setting cookie: " + ex.Message);
                toolStripStatusLabel1.Text = "Error setting cookie";
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            BackgroundWorker bgw = new BackgroundWorker();
            this.btnCrawl.Enabled = false;
            this.txtSaveFileName.Enabled = false;
            this.txtUrlsToCrawl.Enabled = false;

            bgw.DoWork += delegate
            {


                var urls = from u in txtUrlsToCrawl.Lines
                           where !string.IsNullOrWhiteSpace(u)
                           select u;

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < urls.Count(); i++)
                {
                    sb.Append(Environment.NewLine + (i + 1) + ". " + urls.ElementAt(i));
                }

                this.appendLineToLog("***************************************************");
                this.appendLineToLog("Crawling URLs:"
                    + sb.ToString());
                this.appendLineToLog("***************************************************");

                foreach (var u in urls)
                {
                    ReviewCrawler.Run(this, new Uri(u, UriKind.RelativeOrAbsolute), Cookies);
                }
            };

            bgw.RunWorkerCompleted += delegate
            {
                this.btnCrawl.Enabled = true;
                this.txtSaveFileName.Enabled = true;
                this.txtUrlsToCrawl.Enabled = true;
            };

            bgw.RunWorkerAsync();
        }


        public void appendLineToLog(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.txtCrawlOutput.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(appendLineToLog);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtCrawlOutput.AppendText("[" + System.DateTime.Now.ToString() + "] " + text + Environment.NewLine);
            }
        }




        private void txtSaveFileName_TextChanged(object sender, EventArgs e)
        {
            txtSaveFileName.Text = CrawlUtil.SanitiseFileName(txtSaveFileName.Text);
            this._SaveFileName = txtSaveFileName.Text;
        }

        private void btnSelectFiles_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();

            o.Multiselect = true;
            o.ValidateNames = true;

            o.ShowDialog();

            txtFileNames.Lines = o.FileNames;
        }

        private void btnLoadFiles_Click(object sender, EventArgs e)
        {
            btnLoadFiles.Enabled = false;

            StreamReader f = null;

            foreach (var file in txtFileNames.Lines.ToList<string>())
            {
                f = File.OpenText(file);

                try
                {
                    var csv = new CsvReader(f);
                    csv.Configuration.RegisterClassMap<UserMap>();
                    csv.Configuration.IgnoreReadingExceptions = false;

                    var records = csv.GetRecords<User>();

                    //derive string Id { get; set; }
                    //derive string Site { get; set; }
                    //string Username { get; set; }
                    //derive Nullable<System.DateTime> MemberSince { get; set; }
                    //string PageUrl { get; set; }
                    //get later from review string Location { get; set; }
                    //derive Nullable<int> NumOfRatings { get; set; }
                    //derive Nullable<int> NumOfReviews { get; set; }
                    //string RatingsAnalysisRaw { get; set; }

                    MoviesEntities db = new MoviesEntities();

                    foreach (var r in records)
                    {
                        r.Id = r.PageUrl.Substring("http://www.imdb.com".Length);
                        r.Site = "IMDB";

                        if (r.Id == null)
                        {
                        }

                        //check if user already exists in the db
                        if(db.Users.FirstOrDefault(u => u.Id == r.Id) != null)
                        {
                            continue;
                        }

                        //MemberSince
                        try
                        {
                            //if (r._member_since_utc != -1)
                            //{
                            //    DateTime d = new DateTime(1970, 1, 1, 0, 0, 0);
                            //    r.MemberSince = d.AddSeconds(r._member_since_utc/1000.0);
                            //}
                            if (!string.IsNullOrWhiteSpace(r._member_since_utc))
                            {
                                r.MemberSince = DateTime.Parse(r._member_since_utc.Substring("IMDB member since ".Length));
                            }

                        }
                        catch (Exception ex) { }

                        /**
                            total_ratings (two numbers)
                            19; 20 =	19 ratings; 20 reviews

                            total_ratings (one number)
                            24 =	IF user has ratings_analysis then 24 ratings (and less than 11 reviews)
		                            ELSE 24 reviews (and no ratings)
                         **/ 
                        //NumOfRatings (default is 0) and NumOfReviews (default is less than 11)
                        r.NumOfRatings = 0;
                        r.NumOfReviews = -11;

                        if (!string.IsNullOrWhiteSpace(r._total_ratings))
                        {
                            if (r._total_ratings.Contains(";")) //2 numbers
                            {
                                int n = 0;
                                var ratings = r._total_ratings.Split(';');
                                Int32.TryParse(ratings[0], out n);//first number is rating
                                if (n > 0)
                                {
                                    r.NumOfRatings = n;
                                }
                                Int32.TryParse(ratings[1], out n);//second number is reviews
                                if (n > 0)
                                {
                                    r.NumOfReviews = n;
                                }
                            }
                            else //1 number
                            {
                                if (!string.IsNullOrWhiteSpace(r.RatingsAnalysisRaw)) //ratings exist
                                {
                                    r.NumOfRatings = Int32.Parse(r._total_ratings);
                                }
                                else //no ratings exist
                                {
                                    r.NumOfReviews = Int32.Parse(r._total_ratings);
                                }
                            }
                        }
                        else //less than 6 ratings and/or less than 11 reviews
                        {
                            if (!string.IsNullOrWhiteSpace(r.RatingsAnalysisRaw)) //ratings exist
                            {
                                r.NumOfRatings = -6;
                            }
                            else //no ratings exist
                            {
                                r.NumOfRatings = 0;
                            }
                        }

                        txtToolsOutput.AppendText(Environment.NewLine);
                        txtToolsOutput.AppendText(
                            string.Format("{0}: || id: {1} ({2}) || url: {3}"
                             , csv.Row
                             , r.Id
                             , r.Username
                             , r.PageUrl
                             ));
                        txtToolsOutput.AppendText(Environment.NewLine);
                        txtToolsOutput.AppendText("\tRatings: " + r.NumOfRatings + Environment.NewLine);
                        txtToolsOutput.AppendText("\tReviews: " + r.NumOfReviews + Environment.NewLine);
                        txtToolsOutput.AppendText("\tSince: " + r.MemberSince + Environment.NewLine);


                        try
                        {
                            db.Users.Add(r);
                            db.SaveChanges();
                        }
                        catch (Exception ex) //PK or FK error
                        {
                            txtToolsOutput.AppendText(ex.Message + Environment.NewLine);
                            txtToolsOutput.AppendText(ex.StackTrace + Environment.NewLine);
                        }

                    }

                    f.Close();

                    toolStripStatusLabel1.Text = csv.Row + " rows read.";

                    csv.Dispose();
                }
                catch (Exception ex)
                {
                    //var detail = ex.Data["CsvHelper"];
                    txtToolsOutput.AppendText(ex.Message + Environment.NewLine);
                    txtToolsOutput.AppendText(ex.StackTrace + Environment.NewLine);
                }
                finally
                {
                    if (f != null)
                    {
                        f.Dispose();
                    }
                }
            }

            btnLoadFiles.Enabled = true;
        }



    }
}
