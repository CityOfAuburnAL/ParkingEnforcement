using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ParkingApp_Tablet;

namespace GetOnTabletTest
{
    public partial class OrdenanceForm : Form
    {
        Program pg;
        public OrdenanceForm(Program prog)
        {
            InitializeComponent();
            pg = prog;

            Activated += OrdenanceForm_Activated;
        }

        void OrdenanceForm_Activated(object sender, EventArgs e)
        {
            OkBtn.Focus();

            DBHelp dbh = new DBHelp();
            string[] ordInfo = dbh.OrdInfo(CurrentCharge.Charge);

            ticketCount.Text = dbh.TicketCount().ToString();

            OrdLabel.Text = "ORDENANCE: " + ordInfo[1];
            OrdInfoTxt.Text = ordInfo[0];
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            pg.offenceForm.Show();
            OrdInfoTxt.Text = "";
            OrdLabel.Text = "ORDENANCE: ";
            this.Hide();
        }

        private void KeyBrdBtn_Click(object sender, EventArgs e)
        {
            Process.Start(@"C:\Program Files\Common Files\Microsoft Shared\ink\TabTip.exe");
        }
    }
}
