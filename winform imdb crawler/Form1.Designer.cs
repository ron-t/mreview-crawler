namespace winform_imdb_crawler
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageLogin = new System.Windows.Forms.TabPage();
            this.btnSaveCookie = new System.Windows.Forms.Button();
            this.btnNavToImdb = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.tabPageCrawl = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSaveFileName = new System.Windows.Forms.TextBox();
            this.txtUrlsToCrawl = new System.Windows.Forms.TextBox();
            this.btnCrawl = new System.Windows.Forms.Button();
            this.txtCrawlOutput = new System.Windows.Forms.TextBox();
            this.tabPageTools = new System.Windows.Forms.TabPage();
            this.btnSelectFiles = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtFileNames = new System.Windows.Forms.TextBox();
            this.btnLoadFiles = new System.Windows.Forms.Button();
            this.txtToolsOutput = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPageLogin.SuspendLayout();
            this.tabPageCrawl.SuspendLayout();
            this.tabPageTools.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageLogin);
            this.tabControl1.Controls.Add(this.tabPageCrawl);
            this.tabControl1.Controls.Add(this.tabPageTools);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(757, 425);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPageLogin
            // 
            this.tabPageLogin.Controls.Add(this.btnSaveCookie);
            this.tabPageLogin.Controls.Add(this.btnNavToImdb);
            this.tabPageLogin.Controls.Add(this.webBrowser1);
            this.tabPageLogin.Location = new System.Drawing.Point(4, 22);
            this.tabPageLogin.Name = "tabPageLogin";
            this.tabPageLogin.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLogin.Size = new System.Drawing.Size(749, 399);
            this.tabPageLogin.TabIndex = 0;
            this.tabPageLogin.Text = "Login";
            this.tabPageLogin.UseVisualStyleBackColor = true;
            // 
            // btnSaveCookie
            // 
            this.btnSaveCookie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveCookie.Location = new System.Drawing.Point(100, 6);
            this.btnSaveCookie.Name = "btnSaveCookie";
            this.btnSaveCookie.Size = new System.Drawing.Size(75, 23);
            this.btnSaveCookie.TabIndex = 2;
            this.btnSaveCookie.Text = "SaveCookie";
            this.btnSaveCookie.UseVisualStyleBackColor = true;
            this.btnSaveCookie.Click += new System.EventHandler(this.btnSaveCookie_Click);
            // 
            // btnNavToImdb
            // 
            this.btnNavToImdb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavToImdb.Location = new System.Drawing.Point(6, 6);
            this.btnNavToImdb.Name = "btnNavToImdb";
            this.btnNavToImdb.Size = new System.Drawing.Size(87, 23);
            this.btnNavToImdb.TabIndex = 0;
            this.btnNavToImdb.Text = "NavToIMDB";
            this.btnNavToImdb.UseVisualStyleBackColor = true;
            this.btnNavToImdb.Click += new System.EventHandler(this.button1_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(3, 35);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(727, 343);
            this.webBrowser1.TabIndex = 1;
            // 
            // tabPageCrawl
            // 
            this.tabPageCrawl.Controls.Add(this.label1);
            this.tabPageCrawl.Controls.Add(this.txtSaveFileName);
            this.tabPageCrawl.Controls.Add(this.txtUrlsToCrawl);
            this.tabPageCrawl.Controls.Add(this.btnCrawl);
            this.tabPageCrawl.Controls.Add(this.txtCrawlOutput);
            this.tabPageCrawl.Location = new System.Drawing.Point(4, 22);
            this.tabPageCrawl.Name = "tabPageCrawl";
            this.tabPageCrawl.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCrawl.Size = new System.Drawing.Size(749, 399);
            this.tabPageCrawl.TabIndex = 1;
            this.tabPageCrawl.Text = "Crawl";
            this.tabPageCrawl.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Save reviews to:";
            // 
            // txtSaveFileName
            // 
            this.txtSaveFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSaveFileName.Location = new System.Drawing.Point(94, 126);
            this.txtSaveFileName.Name = "txtSaveFileName";
            this.txtSaveFileName.Size = new System.Drawing.Size(248, 20);
            this.txtSaveFileName.TabIndex = 3;
            this.txtSaveFileName.TextChanged += new System.EventHandler(this.txtSaveFileName_TextChanged);
            // 
            // txtUrlsToCrawl
            // 
            this.txtUrlsToCrawl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUrlsToCrawl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUrlsToCrawl.Location = new System.Drawing.Point(88, 7);
            this.txtUrlsToCrawl.Multiline = true;
            this.txtUrlsToCrawl.Name = "txtUrlsToCrawl";
            this.txtUrlsToCrawl.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtUrlsToCrawl.Size = new System.Drawing.Size(653, 112);
            this.txtUrlsToCrawl.TabIndex = 2;
            this.txtUrlsToCrawl.WordWrap = false;
            // 
            // btnCrawl
            // 
            this.btnCrawl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCrawl.Location = new System.Drawing.Point(6, 6);
            this.btnCrawl.Name = "btnCrawl";
            this.btnCrawl.Size = new System.Drawing.Size(75, 113);
            this.btnCrawl.TabIndex = 1;
            this.btnCrawl.Text = "Crawl URLs";
            this.btnCrawl.UseVisualStyleBackColor = true;
            this.btnCrawl.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // txtCrawlOutput
            // 
            this.txtCrawlOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCrawlOutput.BackColor = System.Drawing.SystemColors.Window;
            this.txtCrawlOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCrawlOutput.Location = new System.Drawing.Point(8, 152);
            this.txtCrawlOutput.Multiline = true;
            this.txtCrawlOutput.Name = "txtCrawlOutput";
            this.txtCrawlOutput.ReadOnly = true;
            this.txtCrawlOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCrawlOutput.Size = new System.Drawing.Size(733, 226);
            this.txtCrawlOutput.TabIndex = 0;
            this.txtCrawlOutput.WordWrap = false;
            // 
            // tabPageTools
            // 
            this.tabPageTools.Controls.Add(this.txtToolsOutput);
            this.tabPageTools.Controls.Add(this.btnLoadFiles);
            this.tabPageTools.Controls.Add(this.txtFileNames);
            this.tabPageTools.Controls.Add(this.btnSelectFiles);
            this.tabPageTools.Location = new System.Drawing.Point(4, 22);
            this.tabPageTools.Name = "tabPageTools";
            this.tabPageTools.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTools.Size = new System.Drawing.Size(749, 399);
            this.tabPageTools.TabIndex = 2;
            this.tabPageTools.Text = "Tools";
            this.tabPageTools.UseVisualStyleBackColor = true;
            // 
            // btnSelectFiles
            // 
            this.btnSelectFiles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectFiles.Location = new System.Drawing.Point(6, 6);
            this.btnSelectFiles.Name = "btnSelectFiles";
            this.btnSelectFiles.Size = new System.Drawing.Size(75, 28);
            this.btnSelectFiles.TabIndex = 0;
            this.btnSelectFiles.Text = "Select Files";
            this.btnSelectFiles.UseVisualStyleBackColor = true;
            this.btnSelectFiles.Click += new System.EventHandler(this.btnSelectFiles_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 403);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(757, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabel1.Text = "Status:";
            // 
            // txtFileNames
            // 
            this.txtFileNames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileNames.BackColor = System.Drawing.SystemColors.Window;
            this.txtFileNames.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFileNames.Location = new System.Drawing.Point(88, 7);
            this.txtFileNames.Multiline = true;
            this.txtFileNames.Name = "txtFileNames";
            this.txtFileNames.ReadOnly = true;
            this.txtFileNames.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtFileNames.Size = new System.Drawing.Size(653, 127);
            this.txtFileNames.TabIndex = 1;
            this.txtFileNames.WordWrap = false;
            // 
            // btnLoadFiles
            // 
            this.btnLoadFiles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadFiles.Location = new System.Drawing.Point(6, 40);
            this.btnLoadFiles.Name = "btnLoadFiles";
            this.btnLoadFiles.Size = new System.Drawing.Size(75, 94);
            this.btnLoadFiles.TabIndex = 2;
            this.btnLoadFiles.Text = "Load Files";
            this.btnLoadFiles.UseVisualStyleBackColor = true;
            this.btnLoadFiles.Click += new System.EventHandler(this.btnLoadFiles_Click);
            // 
            // txtToolsOutput
            // 
            this.txtToolsOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtToolsOutput.BackColor = System.Drawing.SystemColors.Window;
            this.txtToolsOutput.Location = new System.Drawing.Point(9, 164);
            this.txtToolsOutput.MaxLength = 3276700;
            this.txtToolsOutput.Multiline = true;
            this.txtToolsOutput.Name = "txtToolsOutput";
            this.txtToolsOutput.ReadOnly = true;
            this.txtToolsOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtToolsOutput.Size = new System.Drawing.Size(732, 201);
            this.txtToolsOutput.TabIndex = 3;
            this.txtToolsOutput.Text = "***load output***";
            this.txtToolsOutput.WordWrap = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 425);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "IMDB Crawler";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageLogin.ResumeLayout(false);
            this.tabPageCrawl.ResumeLayout(false);
            this.tabPageCrawl.PerformLayout();
            this.tabPageTools.ResumeLayout(false);
            this.tabPageTools.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageLogin;
        private System.Windows.Forms.Button btnNavToImdb;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.TabPage tabPageCrawl;
        private System.Windows.Forms.TabPage tabPageTools;
        private System.Windows.Forms.Button btnSaveCookie;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button btnCrawl;
        private System.Windows.Forms.TextBox txtCrawlOutput;
        private System.Windows.Forms.TextBox txtUrlsToCrawl;
        private System.Windows.Forms.Button btnSelectFiles;
        private System.Windows.Forms.TextBox txtSaveFileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFileNames;
        private System.Windows.Forms.Button btnLoadFiles;
        private System.Windows.Forms.TextBox txtToolsOutput;

    }
}

