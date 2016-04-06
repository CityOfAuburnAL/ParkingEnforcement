using System;
using System.Windows.Forms;

namespace GetOnTabletTest
{
    public partial class YesNoForm : Form
    {
        public bool returnValue;
        string messageText;

        public YesNoForm(String message)
        {
            InitializeComponent();
            
            messageText = message;
        }

        private void YesBtn_Click(object sender, EventArgs e)
        {
            returnValue = true;

            this.Close();
        }

        private void NoBtn_Click(object sender, EventArgs e)
        {
            returnValue = false;

            this.Close();
        }

        private void YesNoForm_Load(object sender, EventArgs e)
        {
            label1.Text = "Reprint Ticket # "+ messageText;
        }
    }
}
