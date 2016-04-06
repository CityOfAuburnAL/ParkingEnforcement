namespace GetOnTabletTest
{
    partial class OrdenanceForm
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
            this.OrdInfoTxt = new System.Windows.Forms.TextBox();
            this.OkBtn = new System.Windows.Forms.Button();
            this.OrdLabel = new System.Windows.Forms.Label();
            this.KeyBrdBtn = new System.Windows.Forms.Button();
            this.ticketCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // OrdInfoTxt
            // 
            this.OrdInfoTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.OrdInfoTxt.Location = new System.Drawing.Point(12, 123);
            this.OrdInfoTxt.Multiline = true;
            this.OrdInfoTxt.Name = "OrdInfoTxt";
            this.OrdInfoTxt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.OrdInfoTxt.Size = new System.Drawing.Size(728, 382);
            this.OrdInfoTxt.TabIndex = 15;
            // 
            // OkBtn
            // 
            this.OkBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.OkBtn.Location = new System.Drawing.Point(584, 556);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(156, 109);
            this.OkBtn.TabIndex = 16;
            this.OkBtn.Text = "Continue";
            this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // OrdLabel
            // 
            this.OrdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.OrdLabel.Location = new System.Drawing.Point(279, 65);
            this.OrdLabel.Name = "OrdLabel";
            this.OrdLabel.Size = new System.Drawing.Size(188, 38);
            this.OrdLabel.TabIndex = 17;
            this.OrdLabel.Text = "ORDENANCE:";
            this.OrdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // KeyBrdBtn
            // 
            this.KeyBrdBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.KeyBrdBtn.Location = new System.Drawing.Point(311, 556);
            this.KeyBrdBtn.Name = "KeyBrdBtn";
            this.KeyBrdBtn.Size = new System.Drawing.Size(156, 109);
            this.KeyBrdBtn.TabIndex = 44;
            this.KeyBrdBtn.Text = "Keyboard";
            this.KeyBrdBtn.Click += new System.EventHandler(this.KeyBrdBtn_Click);
            // 
            // ticketCount
            // 
            this.ticketCount.AutoSize = true;
            this.ticketCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ticketCount.Location = new System.Drawing.Point(700, 10);
            this.ticketCount.Name = "ticketCount";
            this.ticketCount.Size = new System.Drawing.Size(0, 25);
            this.ticketCount.TabIndex = 47;
            // 
            // OrdenanceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 680);
            this.ControlBox = false;
            this.Controls.Add(this.ticketCount);
            this.Controls.Add(this.KeyBrdBtn);
            this.Controls.Add(this.OrdInfoTxt);
            this.Controls.Add(this.OkBtn);
            this.Controls.Add(this.OrdLabel);
            this.Name = "OrdenanceForm";
            this.Text = "OrdenanceForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox OrdInfoTxt;
        private System.Windows.Forms.Button OkBtn;
        private System.Windows.Forms.Label OrdLabel;
        private System.Windows.Forms.Button KeyBrdBtn;
        private System.Windows.Forms.Label ticketCount;
    }
}