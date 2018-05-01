namespace FileSync
{
    partial class LogOutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogOutForm));
            this.yesBtn = new System.Windows.Forms.Button();
            this.pathBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.noBtn = new System.Windows.Forms.Button();
            this.folderPath = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // yesBtn
            // 
            this.yesBtn.Font = new System.Drawing.Font("Sitka Display", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yesBtn.Location = new System.Drawing.Point(4, 41);
            this.yesBtn.Name = "yesBtn";
            this.yesBtn.Size = new System.Drawing.Size(125, 28);
            this.yesBtn.TabIndex = 1;
            this.yesBtn.Text = " Да";
            this.yesBtn.UseVisualStyleBackColor = true;
            this.yesBtn.Click += new System.EventHandler(this.yesBtn_Click);
            // 
            // noBtn
            // 
            this.noBtn.Font = new System.Drawing.Font("Sitka Display", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noBtn.Location = new System.Drawing.Point(128, 41);
            this.noBtn.Name = "noBtn";
            this.noBtn.Size = new System.Drawing.Size(122, 28);
            this.noBtn.TabIndex = 1;
            this.noBtn.Text = "Нет";
            this.noBtn.UseVisualStyleBackColor = true;
            this.noBtn.Click += new System.EventHandler(this.noBtn_Click);
            // 
            // folderPath
            // 
            this.folderPath.Font = new System.Drawing.Font("Sitka Display", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.folderPath.Location = new System.Drawing.Point(4, 7);
            this.folderPath.Name = "folderPath";
            this.folderPath.ReadOnly = true;
            this.folderPath.Size = new System.Drawing.Size(246, 28);
            this.folderPath.TabIndex = 0;
            this.folderPath.Text = "Вы действительно хотите выйти?";
            // 
            // LogOutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(255, 73);
            this.ControlBox = false;
            this.Controls.Add(this.noBtn);
            this.Controls.Add(this.yesBtn);
            this.Controls.Add(this.folderPath);
            this.Name = "LogOutForm";
            this.Opacity = 0.75D;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выход";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button yesBtn;
        private System.Windows.Forms.FolderBrowserDialog pathBrowser;
        private System.Windows.Forms.Button noBtn;
        private System.Windows.Forms.TextBox folderPath;
    }
}