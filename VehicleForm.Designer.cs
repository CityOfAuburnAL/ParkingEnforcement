namespace GetOnTabletTest
{
    partial class VehicleForm
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
            this.tagCatCombo = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.VINTxt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.LicenseTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.StateTxt = new System.Windows.Forms.TextBox();
            this.SubmitBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.KeyBrdBtn = new System.Windows.Forms.Button();
            this.ticketCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tagCatCombo
            // 
            this.tagCatCombo.DropDownHeight = 400;
            this.tagCatCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.tagCatCombo.IntegralHeight = false;
            this.tagCatCombo.Location = new System.Drawing.Point(122, 164);
            this.tagCatCombo.Name = "tagCatCombo";
            this.tagCatCombo.Size = new System.Drawing.Size(618, 45);
            this.tagCatCombo.TabIndex = 34;
            this.tagCatCombo.SelectedIndexChanged += new System.EventHandler(this.tagCatCombo_SelectedIndexChanged);
            this.tagCatCombo.Enter += new System.EventHandler(this.tagCatCombo_Enter);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label5.Location = new System.Drawing.Point(12, 176);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 27);
            this.label5.TabIndex = 35;
            this.label5.Text = "Category";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label3.Location = new System.Drawing.Point(200, 282);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(400, 36);
            this.label3.TabIndex = 36;
            this.label3.Text = "------------------------ OR -----------------------";
            // 
            // VINTxt
            // 
            this.VINTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.VINTxt.Location = new System.Drawing.Point(122, 377);
            this.VINTxt.Name = "VINTxt";
            this.VINTxt.Size = new System.Drawing.Size(618, 44);
            this.VINTxt.TabIndex = 37;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label4.Location = new System.Drawing.Point(12, 389);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 27);
            this.label4.TabIndex = 38;
            this.label4.Text = "VIN";
            // 
            // LicenseTxt
            // 
            this.LicenseTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.LicenseTxt.Location = new System.Drawing.Point(122, 52);
            this.LicenseTxt.MaxLength = 12;
            this.LicenseTxt.Name = "LicenseTxt";
            this.LicenseTxt.Size = new System.Drawing.Size(344, 44);
            this.LicenseTxt.TabIndex = 32;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label1.Location = new System.Drawing.Point(472, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 23);
            this.label1.TabIndex = 39;
            this.label1.Text = "State";
            // 
            // CancelBtn
            // 
            this.CancelBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.CancelBtn.Location = new System.Drawing.Point(12, 566);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(156, 109);
            this.CancelBtn.TabIndex = 40;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // StateTxt
            // 
            this.StateTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.StateTxt.Location = new System.Drawing.Point(516, 52);
            this.StateTxt.Name = "StateTxt";
            this.StateTxt.Size = new System.Drawing.Size(224, 44);
            this.StateTxt.TabIndex = 33;
            // 
            // SubmitBtn
            // 
            this.SubmitBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.SubmitBtn.Location = new System.Drawing.Point(584, 566);
            this.SubmitBtn.Name = "SubmitBtn";
            this.SubmitBtn.Size = new System.Drawing.Size(156, 109);
            this.SubmitBtn.TabIndex = 41;
            this.SubmitBtn.Text = "Submit";
            this.SubmitBtn.Click += new System.EventHandler(this.SubmitBtn_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label2.Location = new System.Drawing.Point(12, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 26);
            this.label2.TabIndex = 42;
            this.label2.Text = "License";
            // 
            // KeyBrdBtn
            // 
            this.KeyBrdBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.KeyBrdBtn.Location = new System.Drawing.Point(310, 566);
            this.KeyBrdBtn.Name = "KeyBrdBtn";
            this.KeyBrdBtn.Size = new System.Drawing.Size(156, 109);
            this.KeyBrdBtn.TabIndex = 43;
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
            // VehicleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 685);
            this.ControlBox = false;
            this.Controls.Add(this.ticketCount);
            this.Controls.Add(this.KeyBrdBtn);
            this.Controls.Add(this.tagCatCombo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.VINTxt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.LicenseTxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.StateTxt);
            this.Controls.Add(this.SubmitBtn);
            this.Controls.Add(this.label2);
            this.Name = "VehicleForm";
            this.Text = "VehicleForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox tagCatCombo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox VINTxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox LicenseTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.TextBox StateTxt;
        private System.Windows.Forms.Button SubmitBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button KeyBrdBtn;
        private System.Windows.Forms.Label ticketCount;
    }
}