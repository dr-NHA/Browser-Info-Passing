namespace NHA_Browser_Info_Passing
{
    partial class BrowserInfPassing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrowserInfPassing));
            this.GetPasswordsFromChrome = new System.Windows.Forms.Button();
            this.InfoBox = new System.Windows.Forms.TextBox();
            this.KnownDomains = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // GetPasswordsFromChrome
            // 
            this.GetPasswordsFromChrome.Dock = System.Windows.Forms.DockStyle.Top;
            this.GetPasswordsFromChrome.Location = new System.Drawing.Point(0, 0);
            this.GetPasswordsFromChrome.Name = "GetPasswordsFromChrome";
            this.GetPasswordsFromChrome.Size = new System.Drawing.Size(742, 54);
            this.GetPasswordsFromChrome.TabIndex = 0;
            this.GetPasswordsFromChrome.Text = "Get User Infomation From Web Browsers";
            this.GetPasswordsFromChrome.UseVisualStyleBackColor = true;
            this.GetPasswordsFromChrome.Click += new System.EventHandler(this.GetPasswordsFromChrome_Click);
            // 
            // InfoBox
            // 
            this.InfoBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InfoBox.Location = new System.Drawing.Point(0, 54);
            this.InfoBox.Multiline = true;
            this.InfoBox.Name = "InfoBox";
            this.InfoBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.InfoBox.Size = new System.Drawing.Size(742, 396);
            this.InfoBox.TabIndex = 1;
            this.InfoBox.WordWrap = false;
            // 
            // KnownDomains
            // 
            this.KnownDomains.Dock = System.Windows.Forms.DockStyle.Right;
            this.KnownDomains.Location = new System.Drawing.Point(742, 0);
            this.KnownDomains.Multiline = true;
            this.KnownDomains.Name = "KnownDomains";
            this.KnownDomains.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.KnownDomains.Size = new System.Drawing.Size(182, 450);
            this.KnownDomains.TabIndex = 2;
            this.KnownDomains.WordWrap = false;
            // 
            // BrowserInfPassing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 450);
            this.Controls.Add(this.InfoBox);
            this.Controls.Add(this.GetPasswordsFromChrome);
            this.Controls.Add(this.KnownDomains);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(200, 200);
            this.Name = "BrowserInfPassing";
            this.Text = "Web Browser Personal Infomation Passing";
            this.Load += new System.EventHandler(this.UserInfoPassing_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button GetPasswordsFromChrome;
        private System.Windows.Forms.TextBox InfoBox;
        private System.Windows.Forms.TextBox KnownDomains;
    }
}

