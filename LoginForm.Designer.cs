namespace GetOnTabletTest
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
            this.CancelBtn = new System.Windows.Forms.Button();
            this.LoginBtn = new System.Windows.Forms.Button();
            this.PassTxtBx = new System.Windows.Forms.TextBox();
            this.UserTxtBx = new System.Windows.Forms.TextBox();
            this.PassLabel = new System.Windows.Forms.Label();
            this.UserLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(17, 163);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(72, 20);
            this.CancelBtn.TabIndex = 21;
            this.CancelBtn.Text = "Cancel";
            // 
            // LoginBtn
            // 
            this.LoginBtn.Location = new System.Drawing.Point(196, 163);
            this.LoginBtn.Name = "LoginBtn";
            this.LoginBtn.Size = new System.Drawing.Size(72, 20);
            this.LoginBtn.TabIndex = 22;
            this.LoginBtn.Text = "Login";
            this.LoginBtn.Click += new System.EventHandler(this.LoginBtn_Click);
            // 
            // PassTxtBx
            // 
            this.PassTxtBx.Location = new System.Drawing.Point(98, 110);
            this.PassTxtBx.Name = "PassTxtBx";
            this.PassTxtBx.Size = new System.Drawing.Size(150, 20);
            this.PassTxtBx.TabIndex = 20;
            this.PassTxtBx.Visible = false;
            // 
            // UserTxtBx
            // 
            this.UserTxtBx.Location = new System.Drawing.Point(98, 79);
            this.UserTxtBx.Name = "UserTxtBx";
            this.UserTxtBx.Size = new System.Drawing.Size(150, 20);
            this.UserTxtBx.TabIndex = 19;
            // 
            // PassLabel
            // 
            this.PassLabel.Location = new System.Drawing.Point(17, 110);
            this.PassLabel.Name = "PassLabel";
            this.PassLabel.Size = new System.Drawing.Size(75, 20);
            this.PassLabel.TabIndex = 23;
            this.PassLabel.Text = "Password";
            this.PassLabel.Visible = false;
            // 
            // UserLabel
            // 
            this.UserLabel.Location = new System.Drawing.Point(17, 79);
            this.UserLabel.Name = "UserLabel";
            this.UserLabel.Size = new System.Drawing.Size(75, 20);
            this.UserLabel.TabIndex = 24;
            this.UserLabel.Text = "Username";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 862);
            this.ControlBox = false;
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.LoginBtn);
            this.Controls.Add(this.PassTxtBx);
            this.Controls.Add(this.UserTxtBx);
            this.Controls.Add(this.PassLabel);
            this.Controls.Add(this.UserLabel);
            this.Name = "LoginForm";
            this.Text = "LoginForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button LoginBtn;
        private System.Windows.Forms.TextBox PassTxtBx;
        private System.Windows.Forms.TextBox UserTxtBx;
        private System.Windows.Forms.Label PassLabel;
        private System.Windows.Forms.Label UserLabel;
    }
}