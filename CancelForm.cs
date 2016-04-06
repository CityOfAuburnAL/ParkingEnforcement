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
    public partial class CancelForm : Form
    {
        Program pg;
        DBHelp dbh = new DBHelp();
        bool inserted = false;

        public CancelForm(Program prog)
        {
            InitializeComponent();
            pg = prog;
            this.Activated += CancelForm_Activated;
        }

        void CancelForm_Activated(object sender, EventArgs e)
        {
            CancelReasonTxt.Focus();
            ticketCount.Text = dbh.TicketCount().ToString();
        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            CancelReasonTxt.Focus();
            DBHelp dbh = new DBHelp();

            if (CancelReasonTxt.Text != "")
            {
                Ticket.Notes = "CANCELED: " + CancelReasonTxt.Text;
                Ticket.StatusCode = "C";

                try
                {
                    if (Ticket.PrintError)
                        dbh.CancelExistingTicket(Ticket.TicketNumber);
                    else
                    {
                        if (!inserted)
                        {
                            dbh.InsertTicket();
                            inserted = true;
                        }
                    }

                    if (dbh.IsNetConnected())
                        dbh.PostTicket();

                    pg.locationForm.Show();
                    this.Hide();
                    CancelReasonTxt.Text = "";
                    CurrentCharge.Charge = null;
                    inserted = false;
                }
                catch (Exception er) { }
            }
            else
            {
                CancelReasonTxt.Focus();
                

                PopUpForm pf = new PopUpForm("Cancel Reason not entered.");
                pf.ShowDialog();
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            CancelReasonTxt.Text = "";
            CancelReasonTxt.Focus();
            pg.offenceForm.Show();
            this.Hide();
        }

        private void KeyBrdBtn_Click(object sender, EventArgs e)
        {
            Process.Start(@"C:\Program Files\Common Files\Microsoft Shared\ink\TabTip.exe");
        }
    }
}
