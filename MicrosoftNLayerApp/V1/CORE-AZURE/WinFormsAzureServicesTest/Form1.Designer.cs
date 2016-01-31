namespace WinFormsAzureServicesTest
{
    partial class Form1
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
            this.btnCallAzureWCFTestService = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCallAzureWCFTestService
            // 
            this.btnCallAzureWCFTestService.Location = new System.Drawing.Point(12, 12);
            this.btnCallAzureWCFTestService.Name = "btnCallAzureWCFTestService";
            this.btnCallAzureWCFTestService.Size = new System.Drawing.Size(195, 28);
            this.btnCallAzureWCFTestService.TabIndex = 0;
            this.btnCallAzureWCFTestService.Text = "Call Azure WCF TestService";
            this.btnCallAzureWCFTestService.UseVisualStyleBackColor = true;
            this.btnCallAzureWCFTestService.Click += new System.EventHandler(this.btnCallAzureWCFTestService_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 365);
            this.Controls.Add(this.btnCallAzureWCFTestService);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCallAzureWCFTestService;
    }
}

