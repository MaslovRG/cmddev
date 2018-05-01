namespace FileSync
{
    partial class LoginForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.passwordEdt = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.usernameEdt = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.bunifuElipse2 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.loginBtn = new Bunifu.Framework.UI.BunifuThinButton2();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.serverPathEdt = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 550;
            this.bunifuElipse1.TargetControl = this;
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = null;
            this.bunifuDragControl1.Vertical = true;
            // 
            // passwordEdt
            // 
            this.passwordEdt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.passwordEdt.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.passwordEdt.ForeColor = System.Drawing.Color.White;
            this.passwordEdt.HintForeColor = System.Drawing.Color.Empty;
            this.passwordEdt.HintText = "";
            this.passwordEdt.isPassword = true;
            this.passwordEdt.LineFocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.passwordEdt.LineIdleColor = System.Drawing.Color.White;
            this.passwordEdt.LineMouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.passwordEdt.LineThickness = 3;
            this.passwordEdt.Location = new System.Drawing.Point(67, 187);
            this.passwordEdt.Margin = new System.Windows.Forms.Padding(4);
            this.passwordEdt.Name = "passwordEdt";
            this.passwordEdt.Size = new System.Drawing.Size(249, 30);
            this.passwordEdt.TabIndex = 5;
            this.passwordEdt.Text = "Password";
            this.passwordEdt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // usernameEdt
            // 
            this.usernameEdt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.usernameEdt.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.usernameEdt.ForeColor = System.Drawing.Color.White;
            this.usernameEdt.HintForeColor = System.Drawing.Color.Empty;
            this.usernameEdt.HintText = "";
            this.usernameEdt.isPassword = false;
            this.usernameEdt.LineFocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.usernameEdt.LineIdleColor = System.Drawing.Color.White;
            this.usernameEdt.LineMouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.usernameEdt.LineThickness = 3;
            this.usernameEdt.Location = new System.Drawing.Point(67, 149);
            this.usernameEdt.Margin = new System.Windows.Forms.Padding(4);
            this.usernameEdt.Name = "usernameEdt";
            this.usernameEdt.Size = new System.Drawing.Size(249, 30);
            this.usernameEdt.TabIndex = 6;
            this.usernameEdt.Text = "User Name";
            this.usernameEdt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // bunifuElipse2
            // 
            this.bunifuElipse2.ElipseRadius = 530;
            this.bunifuElipse2.TargetControl = this.pictureBox1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(118, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(141, 130);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // loginBtn
            // 
            this.loginBtn.ActiveBorderThickness = 1;
            this.loginBtn.ActiveCornerRadius = 20;
            this.loginBtn.ActiveFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(125)))), ((int)(((byte)(175)))));
            this.loginBtn.ActiveForecolor = System.Drawing.Color.White;
            this.loginBtn.ActiveLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(125)))), ((int)(((byte)(175)))));
            this.loginBtn.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.loginBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("loginBtn.BackgroundImage")));
            this.loginBtn.ButtonText = "Login";
            this.loginBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loginBtn.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(240)))));
            this.loginBtn.IdleBorderThickness = 1;
            this.loginBtn.IdleCornerRadius = 20;
            this.loginBtn.IdleFillColor = System.Drawing.Color.White;
            this.loginBtn.IdleForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(240)))));
            this.loginBtn.IdleLineColor = System.Drawing.Color.White;
            this.loginBtn.Location = new System.Drawing.Point(67, 282);
            this.loginBtn.Margin = new System.Windows.Forms.Padding(5);
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.Size = new System.Drawing.Size(249, 41);
            this.loginBtn.TabIndex = 10;
            this.loginBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.loginBtn.Click += new System.EventHandler(this.loginBtn_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.linkLabel1.Font = new System.Drawing.Font("Sitka Display", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
            this.linkLabel1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.linkLabel1.Image = ((System.Drawing.Image)(resources.GetObject("linkLabel1.Image")));
            this.linkLabel1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.linkLabel1.LinkColor = System.Drawing.Color.Chartreuse;
            this.linkLabel1.LinkVisited = true;
            this.linkLabel1.Location = new System.Drawing.Point(145, 338);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(103, 23);
            this.linkLabel1.TabIndex = 12;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = " DevSync v. 1.0";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.LimeGreen;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // serverPathEdt
            // 
            this.serverPathEdt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.serverPathEdt.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.serverPathEdt.ForeColor = System.Drawing.Color.White;
            this.serverPathEdt.HintForeColor = System.Drawing.Color.Empty;
            this.serverPathEdt.HintText = "";
            this.serverPathEdt.isPassword = false;
            this.serverPathEdt.LineFocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.serverPathEdt.LineIdleColor = System.Drawing.Color.White;
            this.serverPathEdt.LineMouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.serverPathEdt.LineThickness = 3;
            this.serverPathEdt.Location = new System.Drawing.Point(67, 225);
            this.serverPathEdt.Margin = new System.Windows.Forms.Padding(4);
            this.serverPathEdt.Name = "serverPathEdt";
            this.serverPathEdt.Size = new System.Drawing.Size(249, 30);
            this.serverPathEdt.TabIndex = 5;
            this.serverPathEdt.Text = "Server Path";
            this.serverPathEdt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(380, 380);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.loginBtn);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.serverPathEdt);
            this.Controls.Add(this.passwordEdt);
            this.Controls.Add(this.usernameEdt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoginForm";
            this.Opacity = 0.83D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoginForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private Bunifu.Framework.UI.BunifuThinButton2 loginBtn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Bunifu.Framework.UI.BunifuMaterialTextbox passwordEdt;
        private Bunifu.Framework.UI.BunifuMaterialTextbox usernameEdt;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private Bunifu.Framework.UI.BunifuMaterialTextbox serverPathEdt;
    }
}