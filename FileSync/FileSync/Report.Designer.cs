namespace FileSync
{
    partial class Report
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
            this.reportBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // reportBox
            // 
            this.reportBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportBox.Font = new System.Drawing.Font("Sitka Display", 9.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reportBox.Location = new System.Drawing.Point(0, 0);
            this.reportBox.Name = "reportBox";
            this.reportBox.ReadOnly = true;
            this.reportBox.Size = new System.Drawing.Size(384, 172);
            this.reportBox.TabIndex = 0;
            this.reportBox.Text = "Process ...";
            // 
            // Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 172);
            this.Controls.Add(this.reportBox);
            this.Name = "Report";
            this.Text = "Report";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox reportBox;
    }
}