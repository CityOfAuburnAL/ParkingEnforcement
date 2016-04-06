namespace GetOnTabletTest
{
    partial class OffenceForm
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
            this.ViolationCombo = new System.Windows.Forms.ComboBox();
            this.PrintBtn = new System.Windows.Forms.Button();
            this.InfoBtn = new System.Windows.Forms.Button();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.KeyBrdBtn = new System.Windows.Forms.Button();
            this.ticketCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CancelBtn
            // 
            this.CancelBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.CancelBtn.Location = new System.Drawing.Point(10, 578);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(156, 109);
            this.CancelBtn.TabIndex = 33;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // ViolationCombo
            // 
            this.ViolationCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.ViolationCombo.Location = new System.Drawing.Point(10, 162);
            this.ViolationCombo.Name = "ViolationCombo";
            this.ViolationCombo.Size = new System.Drawing.Size(730, 45);
            this.ViolationCombo.TabIndex = 30;
            this.ViolationCombo.SelectedValueChanged += new System.EventHandler(this.ViolationCombo_SelectedValueChanged);
            this.ViolationCombo.Enter += new System.EventHandler(this.ViolationCombo_Enter);
            // 
            // PrintBtn
            // 
            this.PrintBtn.Enabled = false;
            this.PrintBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.PrintBtn.Location = new System.Drawing.Point(584, 578);
            this.PrintBtn.Name = "PrintBtn";
            this.PrintBtn.Size = new System.Drawing.Size(156, 109);
            this.PrintBtn.TabIndex = 34;
            this.PrintBtn.Text = "Print";
            this.PrintBtn.Click += new System.EventHandler(this.PrintBtn_Click);
            // 
            // InfoBtn
            // 
            this.InfoBtn.Enabled = false;
            this.InfoBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.InfoBtn.Location = new System.Drawing.Point(584, 222);
            this.InfoBtn.Name = "InfoBtn";
            this.InfoBtn.Size = new System.Drawing.Size(156, 109);
            this.InfoBtn.TabIndex = 31;
            this.InfoBtn.Text = "More Info";
            this.InfoBtn.Click += new System.EventHandler(this.InfoBtn_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.txtStatus.Location = new System.Drawing.Point(10, 465);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(730, 44);
            this.txtStatus.TabIndex = 32;
            this.txtStatus.Visible = false;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label2.Location = new System.Drawing.Point(12, 432);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 30);
            this.label2.TabIndex = 35;
            this.label2.Text = "COMMENT:";
            this.label2.Visible = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label1.Location = new System.Drawing.Point(12, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 33);
            this.label1.TabIndex = 36;
            this.label1.Text = "OFFENCE:";
            // 
            // KeyBrdBtn
            // 
            this.KeyBrdBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.KeyBrdBtn.Location = new System.Drawing.Point(293, 578);
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
            // OffenceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 701);
            this.ControlBox = false;
            this.Controls.Add(this.ticketCount);
            this.Controls.Add(this.KeyBrdBtn);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.ViolationCombo);
            this.Controls.Add(this.PrintBtn);
            this.Controls.Add(this.InfoBtn);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "OffenceForm";
            this.Text = "OffenceForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.ComboBox ViolationCombo;
        private System.Windows.Forms.Button PrintBtn;
        private System.Windows.Forms.Button InfoBtn;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button KeyBrdBtn;
        private System.Windows.Forms.Label ticketCount;
    }
}