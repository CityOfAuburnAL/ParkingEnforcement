using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;
using System.Security.Principal;
using ParkingApp_Tablet;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Diagnostics;

namespace GetOnTabletTest
{
    public partial class Form1 : Form
    {
        Program pg;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pg = new Program();
            pg.lForm.Show();

            DBHelp dbh = new DBHelp();

            string fulluser = WindowsIdentity.GetCurrent().Name.ToString();
            string user = fulluser.Split('\\')[1].ToString();

            int count = dbh.TicketCount();

            if (dbh.EmployeeLogin(user))
            {
                Attendant.username = user;
                Ticket.OfficerBadge = Attendant.OfficerBadge;

                if (count > 0)
                {
                    if (dbh.IsNetConnected())
                        dbh.PostTicket();
                    else
                    {
                        PopUpForm pf = new PopUpForm("Not Connected to Network. Tickets not posted.");
                        pf.ShowDialog();
                    }
                }
            }
            else
            {
                PopUpForm pf = new PopUpForm("Not Authorized User");
                pf.FormClosed += pf_FormClosed;
                pf.ShowDialog();
            }

            label1.Text = dbh.TicketCount().ToString();

            pg.lForm.Close();
        }

        void pf_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Process[] procs = Process.GetProcessesByName("TabTip");

                if (procs.Length > 0)
                    procs[0].Kill();
            }
            catch (Exception ex) { }
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pg.locationForm.Show();
            this.Hide();
        }
    }
}
