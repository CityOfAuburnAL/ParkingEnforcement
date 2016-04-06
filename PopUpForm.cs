using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetOnTabletTest
{
    public partial class PopUpForm : Form
    {
        string messageTxt;

        public PopUpForm(String message)
        {
            InitializeComponent();

            messageTxt = message;
        }

        private void PopUpForm_Load(object sender, EventArgs e)
        {
            label1.Text = messageTxt;
            int textWidth = label1.Width;

            this.Width = textWidth + 200;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
