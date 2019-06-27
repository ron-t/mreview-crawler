using System;
using System.Net;
using System.Text;
using Autofac;
using NCrawler;
using NCrawler.HtmlProcessor;
using NCrawler.Interfaces;
using System.IO;
using System.Web;
using System.Text.RegularExpressions;

namespace winform_imdb_crawler
{
    class ReviewCrawler
    {
        public static MainForm MainForm;

        public static void Run(MainForm form, Uri uri, CookieContainer cc)
        {
            MainForm = form;
            var modules = new Module[] {
                new CustomDownloaderModule(cc)
                //,new FileStorageModule(".", true)
            };
            NCrawlerModule.Setup(modules);
            using (Crawler c = new Crawler(
                uri,
                new HtmlDocumentProcessor(),
                new ReviewStep(),
                new DumpStep()))
            {
                c.AfterDownload += c_AfterDownload;

                c.MaximumCrawlDepth = 1;
                c.MaximumThreadCount = 1;
                c.Crawl();
            }
        }

        //sleep between 1ms and 3 seconds after each page visit
        static void c_AfterDownload(object sender, NCrawler.Events.AfterDownloadEventArgs e)
        {
            System.Threading.Thread.Sleep((new Random()).Next(1, 3000));
        }
    }

    class ReviewStep : IPipelineStep
    {
        public void Process(Crawler crawler, PropertyBag propertyBag)
        {
            HtmlAgilityPack.HtmlDocument doc = propertyBag["HtmlDoc"].Value as HtmlAgilityPack.HtmlDocument;

            if (doc == null) { return; }

            var reviews = doc.DocumentNode.SelectNodes("//h2");

            if (reviews == null) { return; }

            //On first page for each gender-age-group queue up other pages for the same group
            //First page is where ?start=<blank>
            if (CrawlUtil.getQueryValueFromUrl(propertyBag.ResponseUri.AbsoluteUri, "start") == null)
            {
                var matchingReviewsNode = doc.DocumentNode.SelectSingleNode("//td[@align='right']");
                if (matchingReviewsNode != null)
                {
                    //89 matching reviews (334 reviews in total)&nbsp;
                    Regex r = new Regex(@"(\d+) (matching reviews|reviews in total)");
                    Match m = r.Match(matchingReviewsNode.InnerText);

                    if (m != null)
                    {
                        int matchingReviews = 0;

                        int.TryParse(m.Groups[1].Value, out matchingReviews);

                        if (matchingReviews > 10)
                        {
                            for (int i = 10; i < matchingReviews; i+=10)
                            {
                                Uri add = new Uri(propertyBag.ResponseUri.AbsoluteUri + "&start=" + i);
                                crawler.AddStep(add, 0);
                                ReviewCrawler.MainForm.appendLineToLog(add.AbsoluteUri);
                            }
                        }
                    }
                }
            }

            foreach (var r in reviews)
            {
                //title (may be null?)
                var review_title = r.InnerText;
                if (!string.IsNullOrWhiteSpace(review_title))
                {
                    review_title = HttpUtility.HtmlDecode(review_title);
                }
                else
                {
                    review_title = "";
                }

                //Console.WriteLine("TITLE : " + HttpUtility.HtmlDecode(title));
                ReviewCrawler.MainForm.appendLineToLog("TITLE : " + review_title);

                //rating img (can definitely be null)
                string ratingString = "";
                var ratingNode = r.ParentNode.SelectSingleNode("./img");
                if (ratingNode != null)
                {
                    ratingString = HttpUtility.HtmlDecode(ratingNode.GetAttributeValue("alt", ""));
                    ratingString += " stars";
                }
                //Console.WriteLine("RATING (text) : " + HttpUtility.HtmlDecode(ratingString));
                ReviewCrawler.MainForm.appendLineToLog("RATING : " + ratingString);

                //author name and url (may be null?)
                string authorName = "";
                string authorUrl = "";
                var authorNode = r.ParentNode.SelectSingleNode("./a[2]");
                if (authorNode != null)
                {
                    authorName = HttpUtility.HtmlDecode(authorNode.InnerText);
                    authorUrl = HttpUtility.HtmlDecode(authorNode.GetAttributeValue("href", ""));
                }
                //Console.WriteLine("AUTHOR NAME : " + HttpUtility.HtmlDecode(authorName));
                //Console.WriteLine("AUTHOR URL : " + HttpUtility.HtmlDecode(authorUrl));
                ReviewCrawler.MainForm.appendLineToLog("AUTHOR NAME : " + authorName);
                ReviewCrawler.MainForm.appendLineToLog("AUTHOR URL : " + authorUrl);

                //review date (may be null)
                //location (may be null)
                string dateString = "";
                string locationString = "";
                var dateNode = r.ParentNode.SelectSingleNode("./small[3]");
                var location = r.ParentNode.SelectSingleNode("./small[2]");

                if (dateNode == null) //this happens if the author does not have a location
                {
                    dateNode = r.ParentNode.SelectSingleNode("./small[2]");
                    location = null;
                }
                if (dateNode != null)
                {
                    DateTime date;
                    try
                    {
                        DateTime.TryParse(HttpUtility.HtmlDecode(dateNode.InnerText), out date);
                        dateString = date.ToShortDateString();
                    }
                    catch (Exception) { /* ignore :( */ }
                }

                if (location != null)
                {
                    locationString = HttpUtility.HtmlDecode(location.InnerText);
                }

                //Console.WriteLine("DATE : " + HttpUtility.HtmlDecode(dateString));
                //Console.WriteLine("LOCATION : " + HttpUtility.HtmlDecode(locationString));
                ReviewCrawler.MainForm.appendLineToLog("DATE : " + dateString);
                ReviewCrawler.MainForm.appendLineToLog("LOCATION : " + locationString);

                //usefulness (may be null)
                string usefulness = "";
                var usefulnessNode = r.ParentNode.SelectSingleNode("./small");
                if (usefulnessNode != null && usefulnessNode.InnerText.EndsWith("following review useful:"))
                {
                    usefulness = HttpUtility.HtmlDecode(usefulnessNode.InnerText);
                }
                //Console.WriteLine("USEFULNESS : " + HttpUtility.HtmlDecode(usefulness));
                ReviewCrawler.MainForm.appendLineToLog("USEFULNESS : " + usefulness);

                //Review text
                var reviewText = r.ParentNode.NextSibling.NextSibling.InnerText;
                if (!String.IsNullOrWhiteSpace(reviewText))
                {
                    //Console.WriteLine("REVIEW TEXT : " + HttpUtility.HtmlDecode(reviewText.Replace("\n", " ")).Substring(0, 200) + " ...");
                    reviewText = HttpUtility.HtmlDecode(reviewText.Replace("\n", " ").Replace("\r", " ").Replace("\t", " "));
                    ReviewCrawler.MainForm.appendLineToLog("REVIEW TEXT : " + reviewText.Substring(0, reviewText.Length/10) + " ...");
                }
                else reviewText = "";

                string gender = "gender";
                string age_min = "age_min";
                string age_max = "age_max";

                gender = CrawlUtil.getQueryValueFromUrl(propertyBag.ResponseUri.AbsoluteUri, gender);
                age_min = CrawlUtil.getQueryValueFromUrl(propertyBag.ResponseUri.AbsoluteUri, age_min);
                age_max = CrawlUtil.getQueryValueFromUrl(propertyBag.ResponseUri.AbsoluteUri, age_max);

                ReviewCrawler.MainForm.appendLineToLog("GENDER : " + gender);
                ReviewCrawler.MainForm.appendLineToLog("AGE MIN : " + age_min);
                ReviewCrawler.MainForm.appendLineToLog("AGE MAX : " + age_max);

                string movie_title = CrawlUtil.getMovieNameFromTitle(HttpUtility.HtmlDecode(propertyBag.Title));

                var tsv = new StringBuilder();

                tsv.AppendFormat("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}" + Environment.NewLine,
                    movie_title,    //0
                    review_title,   //1
                    ratingString,   //2
                    dateString,     //3
                    authorName,     //4
                    authorUrl,      //5
                    locationString, //6
                    usefulness,     //7
                    reviewText,     //8
                    gender,         //9
                    age_min,        //10
                    age_max         //11
                    );
                try
                {
                    File.AppendAllText(ReviewCrawler.MainForm.SaveFileName, tsv.ToString());
                }
                catch (Exception ex)
                {
                    ReviewCrawler.MainForm.appendLineToLog(ex.Message);
                    ReviewCrawler.MainForm.appendLineToLog(ex.StackTrace);
                }
            }
        }
    }

    class DumpStep : IPipelineStep
    {
        public void Process(Crawler crawler, PropertyBag propertyBag)
        {
            if (propertyBag["HtmlDoc"].Value == null)
            {
                return;
            }

            string gender = "gender";
            string age_min = "age_min";
            string age_max = "age_max";
            string start = "start";

            gender += "=" + CrawlUtil.getQueryValueFromUrl(propertyBag.ResponseUri.AbsoluteUri, gender);
            age_min += "=" + CrawlUtil.getQueryValueFromUrl(propertyBag.ResponseUri.AbsoluteUri, age_min);
            age_max += "=" + CrawlUtil.getQueryValueFromUrl(propertyBag.ResponseUri.AbsoluteUri, age_max);
            start += "=" + CrawlUtil.getQueryValueFromUrl(propertyBag.ResponseUri.AbsoluteUri, start);

            ReviewCrawler.MainForm.appendLineToLog(propertyBag.Title);
            ReviewCrawler.MainForm.appendLineToLog(gender);
            ReviewCrawler.MainForm.appendLineToLog(age_min);
            ReviewCrawler.MainForm.appendLineToLog(age_max);
            ReviewCrawler.MainForm.appendLineToLog(start);

            HtmlAgilityPack.HtmlDocument doc = propertyBag["HtmlDoc"].Value as HtmlAgilityPack.HtmlDocument;

            doc.Save("HtmlDump/"
                + CrawlUtil.SanitiseFileName(CrawlUtil.getMovieNameFromTitle(HttpUtility.HtmlDecode(propertyBag.Title)))
                + "#" + gender
                + "#" + age_min
                + "#" + age_max
                + "#" + start
                + ".html");
            
        }

        
        
    }
}
