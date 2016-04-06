namespace GetOnTabletTest
{
    partial class CancelForm
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
            this.CancelBtn = new System.Windows.Forms.Button();
            this.SubmitBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CancelReasonTxt = new System.Windows.Forms.TextBox();
            this.KeyBrdBtn = new System.Windows.Forms.Button();
            this.ticketCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CancelBtn
            // 
            this.CancelBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.CancelBtn.Location = new System.Drawing.Point(12, 573);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(156, 109);
            this.CancelBtn.TabIndex = 19;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // SubmitBtn
            // 
            this.SubmitBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.SubmitBtn.Location = new System.Drawing.Point(584, 573);
            this.SubmitBtn.Name = "SubmitBtn";
            this.SubmitBtn.Size = new System.Drawing.Size(156, 109);
            this.SubmitBtn.TabIndex = 20;
            this.SubmitBtn.Text = "Submit";
            this.SubmitBtn.Click += new System.EventHandler(this.SubmitBtn_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label2.Location = new System.Drawing.Point(300, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 32);
            this.label2.TabIndex = 21;
            this.label2.Text = "Cancel Ticket?";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label1.Location = new System.Drawing.Point(12, 243);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 32);
            this.label1.TabIndex = 22;
            this.label1.Text = "Reason:";
            // 
            // CancelReasonTxt
            // 
            this.CancelReasonTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.CancelReasonTxt.Location = new System.Drawing.Point(124, 231);
            this.CancelReasonTxt.Name = "CancelReasonTxt";
            this.CancelReasonTxt.Size = new System.Drawing.Size(616, 44);
            this.CancelReasonTxt.TabIndex = 18;
            // 
            // KeyBrdBtn
            // 
            this.KeyBrdBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.KeyBrdBtn.Location = new System.Drawing.Point(305, 573);
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
            // CancelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 692);
            this.ControlBox = false;
            this.Controls.Add(this.ticketCount);
            this.Controls.Add(this.KeyBrdBtn);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.SubmitBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CancelReasonTxt);
            this.Name = "CancelForm";
            this.Text = "CancelForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button SubmitBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CancelReasonTxt;
        private System.Windows.Forms.Button KeyBrdBtn;
        private System.Windows.Forms.Label ticketCount;
    }
}