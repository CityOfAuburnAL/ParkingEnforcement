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
    public partial class LoadForm : Form
    {
        public LoadForm()
        {
            InitializeComponent();
        }

        private void LoadForm_Shown(object sender, EventArgs e)
        {
            label1.Refresh();
        }

        private void LoadForm_Load(object sender, EventArgs e)
        {
            label1.Refresh();
        }

        private void LoadForm_Activated(object sender, EventArgs e)
        {
            label1.Refresh();
        }
    }
}
