using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ParkingApp_Tablet;

namespace GetOnTabletTest
{
    public partial class LoginForm : Form
    {
        Program pg;
        DBHelp dbh = new DBHelp();
        public LoginForm(Program prog)
        {
            InitializeComponent();
            pg = prog;
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            UserTxtBx.Focus();

        }

    }
}
