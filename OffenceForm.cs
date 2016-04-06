using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using ParkingApp_Tablet;

namespace GetOnTabletTest
{
    public partial class OffenceForm : Form
    {
        Dictionary<string, decimal> charges;
        decimal fine;
        bool inserted = false;
        string sDate = "";
        Program pg;
        YesNoForm yn;
        string MachineID;
        string PrintLine;

        public OffenceForm(Program prog)
        {
            InitializeComponent();
            pg = prog;

            Activated += OffenceForm_Activated;
        }

        void OffenceForm_Activated(object sender, EventArgs e)
        {
            if (charges != null && charges.Count > 0)
            {
                charges.Clear();
            }

            DBHelp dbh = new DBHelp();
            charges = dbh.ChargesList();

            ticketCount.Text = dbh.TicketCount().ToString();

            Ticket.DateIssued = DateTime.Now;

            if (CurrentCharge.Charge != null)
            {
                ViolationCombo.Text = CurrentCharge.Charge;
            }
            else
            {
                ViolationCombo.Items.Clear();
                if (!Ticket.IsOffline)
                {
                    if (Ticket.MeterNumber == "")
                    {
                        //load all charges --> Messagebox if 2 hr eligibility is null
                        if (Vehicle.TwoHourViolationCharge != "")
                        {
                            foreach (string s in charges.Keys)
                            {
                                if (s.Contains(Vehicle.TwoHourViolationCharge))
                                    ViolationCombo.Items.Add(s);
                                else if (!s.Contains("TWO"))
                                    ViolationCombo.Items.Add(s);
                            }
                        }
                        else if (Vehicle.TwoHourViolationCharge == "")
                        {
                            foreach (string s in charges.Keys)
                            {
                                if (!s.Contains("TWO"))
                                    ViolationCombo.Items.Add(s);
                            }
                        }
                    }
                    else
                    {
                        foreach (string s in charges.Keys)
                        {
                            if (s.Contains(Vehicle.MeterViolationCharge))
                                ViolationCombo.Items.Add(s);
                            else if (s.Contains(Vehicle.TwoHourViolationCharge) && Vehicle.TwoHourViolationCharge != "")
                            {
                                ViolationCombo.Items.Add(s);
                            }
                        }
                    }
                }
                else
                {
                    //Offline/No Meter
                    if (Ticket.MeterNumber == "")
                    {
                        foreach (string s in charges.Keys)
                        {
                            ViolationCombo.Items.Add(s);
                        }
                    }
                    //Offline/Meter
                    else
                    {
                        foreach (string s in charges.Keys)
                        {
                            if (s.Contains("METER") || s.Contains("TWO"))
                                ViolationCombo.Items.Add(s);
                        }
                    }
                }
                if (ViolationCombo.Items.Count > 1)
                    ViolationCombo.SelectedIndex = -1;
                else if (ViolationCombo.Items.Count == 1)
                    ViolationCombo.SelectedIndex = 0;
            }
        }

        private void InfoBtn_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(Ticket.MeterNumber + "    " + Ticket.ViolationLocation);


            pg.ordenanceForm.Show();
            this.Hide();
        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            //ViolationCombo.Focus();
            if (ViolationCombo.SelectedIndex != -1)
            {
                Ticket.PrintCount = 1;
                Ticket.StatusCode = "O";

                if (!inserted)
                {
                    DBHelp dbh = new DBHelp();
                    dbh.InsertTicket();
                    inserted = true;
                }
                if (sDate == "")
                    sDate = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();

                PrintTicket();
            }
            else
            {
                PopUpForm pf = new PopUpForm("No Offense Selected");
                pf.ShowDialog();
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            //ViolationCombo.Focus();
            pg.cancelForm.Show();
            this.Hide();

            ViolationCombo.SelectedIndex = -1;
        }

        private void ViolationCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox cbx = (ComboBox)sender;
            if (cbx.Text != "")
            {
                fine = charges[cbx.Text];
                CurrentCharge.Charge = cbx.Text;

                InfoBtn.Enabled = true;

                DBHelp dbH = new DBHelp();
                string[] ordInfo = dbH.OrdInfo(CurrentCharge.Charge);
                Ticket.ChargeCode = ordInfo[1];

                if (PrintBtn.Enabled == false)
                    PrintBtn.Enabled = true;

                InfoBtn.Focus();
            }
        }

        private void PrintTicket()
        {
            DBHelp dbh = new DBHelp();
            MachineID = dbh.GetMachineID();

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += pd_PrintPage;
            pd.EndPrint += pd_EndPrint;
            pd.PrintController = new StandardPrintController();

            int initialHeight = 550;
            //Add for every violation

            if (Violations.ViolationList != null)
            {
                for (int i = 0; i < Violations.ViolationList.Count(); i++)
                {
                    initialHeight += 30;
                }
            }

            pd.DefaultPageSettings.PaperSize = new PaperSize("Custon", 285, initialHeight);

            try
            {
                pd.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        void pd_EndPrint(object sender, PrintEventArgs e)
        {
            yn = new YesNoForm(Ticket.TicketNumber.ToString() + MachineID);
            yn.FormClosed += yn_FormClosed;
            yn.Deactivate += yn_Deactivate;
            yn.Show();
        }

        void yn_Deactivate(object sender, EventArgs e)
        {
            yn.TopMost = true;
        }

        void yn_FormClosed(object sender, FormClosedEventArgs e)
        {
            DBHelp dbh = new DBHelp();

            bool reprint = ((YesNoForm)sender).returnValue;

            if (reprint)
            {
                dbh.UpdatePrintCount(Ticket.TicketNumber);
                PrintTicket();
            }
            else
            {
                if (dbh.TicketCount() > 0)
                {
                    inserted = false;
                    if (dbh.IsNetConnected())
                    {
                        dbh.PostTicket();
                    }
                    else
                    {
                        PopUpForm pf = new PopUpForm("Not Connected to Network. Tickets not posted.");
                        pf.ShowDialog();
                    }
                }
                pg.locationForm.Show();
                this.Hide();

                Ticket.PrintError = false;
                ViolationCombo.SelectedIndex = -1;
                CurrentCharge.Charge = null;
                sDate = "";

                if (Ticket.Latitude > 0)
                    Ticket.Latitude = 0;
                if (Ticket.Longitude > 0)
                    Ticket.Longitude = 0;
            }
        }

        void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            #region Fonts

            Font font6 = new Font(FontFamily.GenericMonospace, 6);
            Font font6B = new Font(FontFamily.GenericMonospace, 6, FontStyle.Bold);
            Font font8 = new Font(FontFamily.GenericMonospace, 8);
            Font font8B = new Font(FontFamily.GenericMonospace, 8, FontStyle.Bold);
            Font font10 = new Font(FontFamily.GenericMonospace, 10);
            Font font10B = new Font(FontFamily.GenericMonospace, 10, FontStyle.Bold);
            Font font12 = new Font(FontFamily.GenericMonospace, 12);
            Font font12B = new Font(FontFamily.GenericMonospace, 12, FontStyle.Bold);
            Font font16 = new Font(FontFamily.GenericMonospace, 16);
            Font font16B = new Font(FontFamily.GenericMonospace, 16, FontStyle.Bold);

            float left = 0;
            float top = 0;

            #endregion

            #region Header

            e.Graphics.DrawString("  NOTICE OF PARKING VIOLATION", font10B, Brushes.Black, left, top, new StringFormat());
            top += font10B.GetHeight(e.Graphics);
            e.Graphics.DrawString("********************************************", font10, Brushes.Black, left, top, new StringFormat());
            top += font10.GetHeight(e.Graphics);
            e.Graphics.DrawString("Auburn Public Safety Department", font10, Brushes.Black, left, top, new StringFormat());
            top += font10.GetHeight(e.Graphics);
            e.Graphics.DrawString("       Police Division", font10, Brushes.Black, left, top, new StringFormat());
            top += font10.GetHeight(e.Graphics);
            e.Graphics.DrawString("141 N Ross St * Auburn, AL 36830", font10, Brushes.Black, left, top, new StringFormat());
            top += font10.GetHeight(e.Graphics);
            e.Graphics.DrawString("********************************************", font10, Brushes.Black, left, top, new StringFormat());
            top += font10.GetHeight(e.Graphics);

            #endregion

            #region Vehicle Information

            string vehicleID = "";
            if (Vehicle.TagNumber != "")
            { vehicleID = "License: " + Vehicle.TagState + " - " + Vehicle.TagNumber; }
            else
            { vehicleID = "VIN: " + Vehicle.VIN; }
            //////////////////
            e.Graphics.DrawString(vehicleID, font10, Brushes.Black, left, top, new StringFormat());
            top += font10.GetHeight(e.Graphics);
            //Date
            e.Graphics.DrawString("Date: " + sDate, font10, Brushes.Black, left, top, new StringFormat());
            top += font10.GetHeight(e.Graphics);
            //Officer Number
            e.Graphics.DrawString("Officer: " + Attendant.OfficerBadge, font10, Brushes.Black, left, top, new StringFormat());
            top += font10.GetHeight(e.Graphics);
            //Vehicle Location
            string locationInfo = "";
            if (Ticket.MeterNumber != "")
            { locationInfo = "Meter: " + Ticket.MeterNumber; }
            else
            { locationInfo = "Location: " + Ticket.ViolationLocation; }
            //////////////
            e.Graphics.DrawString(locationInfo, font10, Brushes.Black, left, top, new StringFormat());
            top += font10.GetHeight(e.Graphics) + 5;
            e.Graphics.DrawString("********************************************", font10, Brushes.Black, left, top, new StringFormat());
            top += font10.GetHeight(e.Graphics);
            top += font10.GetHeight(e.Graphics);

            #endregion

            /////////////////////////////////////////
            //THE TICKET NUMBER
            e.Graphics.DrawString("Ticket# " + Ticket.TicketNumber + MachineID, font10B, Brushes.Black, left, top, new StringFormat());
            top += font10B.GetHeight(e.Graphics);
            /////////////////////////////////////////

            //Current Violation
            e.Graphics.DrawString(ViolationCombo.Text, font10, Brushes.Black, left, top, new StringFormat());
            top += font10.GetHeight(e.Graphics) + 5;
            //Current Fine
            e.Graphics.DrawString(("Fine").PadRight(25, '.') + "$" + fine.ToString() + ".00", font10, Brushes.Black, left, top, new StringFormat());
            top += font10.GetHeight(e.Graphics);
            top += font10.GetHeight(e.Graphics);

            #region Past Violations

            if (Violations.ViolationList != null && Violations.ViolationList.Count() > 0)
            {
                for (int i = 0; i < Violations.ViolationList.Count; i++)
                {
                    string[] preAmt = (Violations.ViolationList[i].Amount).Split('.');
                    string amt = preAmt[0] + "." + preAmt[1].Substring(0, 2);
                    DateTime ticketDate = Convert.ToDateTime(Violations.ViolationList[i].Date);

                    //Past TicketNumber
                    e.Graphics.DrawString("#" + Violations.ViolationList[i].Ticket + " - " + ticketDate.ToShortDateString(), font8, Brushes.Black, left, top, new StringFormat());
                    top += font8.GetHeight(e.Graphics);
                    //Past Violation + Amount
                    e.Graphics.DrawString((Violations.ViolationList[i].Charge).PadRight(33, '.') + "$" + amt, font8, Brushes.Black, left, top, new StringFormat());
                    top += font8.GetHeight(e.Graphics);
                }
            }

            #endregion

            //Total Ticket Amount
            if (Violations.TotalBalance != "0" && Violations.TotalBalance != "" && Violations.TotalBalance != null)
            {
                decimal preFine = Convert.ToDecimal(Violations.TotalBalance) + fine;
                string[] totalFine = preFine.ToString().Split('.');
                string FinalAmt = totalFine[0] + "." + totalFine[1].Substring(0, 2);

                e.Graphics.DrawString(("Total:").PadRight(25, '.') + "$" + FinalAmt, font10B, Brushes.Black, left, top, new StringFormat());
                top += font10B.GetHeight(e.Graphics);
                top += font10B.GetHeight(e.Graphics);
            }
            else
            {
                e.Graphics.DrawString(("Total:").PadRight(25, '.') + "$" + fine.ToString() + ".00", font10B, Brushes.Black, left, top, new StringFormat());
                top += font10B.GetHeight(e.Graphics);
                top += font10B.GetHeight(e.Graphics);
            }

            if (Vehicle.TowEligible)
            {
                e.Graphics.DrawString("     ELIGIBLE FOR TOWING", font10B, Brushes.Black, left, top, new StringFormat());
                top += font10B.GetHeight(e.Graphics);
                top += font10B.GetHeight(e.Graphics);
            }

            #region Wall of Text

            e.Graphics.DrawString("TO PAY ONLINE:visit www.auburnalabama.org/pay and", font6, Brushes.Black, left, top, new StringFormat());
            top += font6.GetHeight(e.Graphics);
            e.Graphics.DrawString("select the link for parking ticket payments.", font6, Brushes.Black, left, top, new StringFormat());
            top += font6.GetHeight(e.Graphics);
            top += font6.GetHeight(e.Graphics);

            e.Graphics.DrawString("TO PAY BY MAIL:enclose payment and ticket in this", font6, Brushes.Black, left, top, new StringFormat());
            top += font6.GetHeight(e.Graphics);
            e.Graphics.DrawString("envelope. You may pay by check or money order payable", font6, Brushes.Black, left, top, new StringFormat());
            top += font6.GetHeight(e.Graphics);
            e.Graphics.DrawString("to the City of Auburn. Remember to affix appropriate", font6, Brushes.Black, left, top, new StringFormat());
            top += font6.GetHeight(e.Graphics);
            e.Graphics.DrawString("postage before mailing. DO NOT SEND CASH.", font6, Brushes.Black, left, top, new StringFormat());
            top += font6.GetHeight(e.Graphics);
            top += font6.GetHeight(e.Graphics);

            e.Graphics.DrawString("TO PAY IN PERSON:bring your ticket to the Municipal", font6, Brushes.Black, left, top, new StringFormat());
            top += font6.GetHeight(e.Graphics);
            e.Graphics.DrawString("Court offices located in the Douglas J Watson", font6, Brushes.Black, left, top, new StringFormat());
            top += font6.GetHeight(e.Graphics);
            e.Graphics.DrawString("Municipal Complex 141 North Ross Street. Office hours", font6, Brushes.Black, left, top, new StringFormat());
            top += font6.GetHeight(e.Graphics);
            e.Graphics.DrawString("are Monday through Friday 7:00 AM to 5:00 PM.", font6, Brushes.Black, left, top, new StringFormat());
            top += font6.GetHeight(e.Graphics);
            //e.Graphics.DrawString("Saturday & Sunday 7:00 AM to 11:00 AM.", font6, Brushes.Black, left, top, new StringFormat());
            //top += font6.GetHeight(e.Graphics);
            top += font6.GetHeight(e.Graphics);

            e.Graphics.DrawString("TO PAY AT CONVENIENTLY LOCATED DROP BOXES: enclose", font6, Brushes.Black, left, top, new StringFormat());
            top += font6.GetHeight(e.Graphics);
            e.Graphics.DrawString("payment and ticket in the envelope provided and drop", font6, Brushes.Black, left, top, new StringFormat());
            top += font6.GetHeight(e.Graphics);
            e.Graphics.DrawString("it off at one of the two conveniently located drop boxes:", font6, Brushes.Black, left, top, new StringFormat());
            top += font6.GetHeight(e.Graphics);
            e.Graphics.DrawString("the corner of Magnolia Avenue and College Street", font6, Brushes.Black, left, top, new StringFormat());
            top += font6.GetHeight(e.Graphics);
            e.Graphics.DrawString("(Toomer's Corner) or in fronst of the Douglas J ", font6, Brushes.Black, left, top, new StringFormat());
            top += font6.GetHeight(e.Graphics);
            e.Graphics.DrawString("Watson Municipal Complex at 141 North Ross Street.", font6, Brushes.Black, left, top, new StringFormat());
            top += font6.GetHeight(e.Graphics);
            top += font6.GetHeight(e.Graphics);

            e.Graphics.DrawString("Failure to pay within 7 days from the violation date", font6B, Brushes.Black, left, top, new StringFormat());
            top += font6B.GetHeight(e.Graphics);
            e.Graphics.DrawString("may result in a required court appearance.", font6B, Brushes.Black, left, top, new StringFormat());
            top += font6B.GetHeight(e.Graphics);
            top += font6B.GetHeight(e.Graphics);

            if (Ticket.IsOffline)
            {
                e.Graphics.DrawString("OFF", font6, Brushes.Black, left, top, new StringFormat());
                top += font6.GetHeight(e.Graphics);
            }

            e.Graphics.DrawString("********************************************", font10, Brushes.Black, left, top, new StringFormat());
            top += font10.GetHeight(e.Graphics);
            top += font10.GetHeight(e.Graphics);

            #endregion
        }

        private void KeyBrdBtn_Click(object sender, EventArgs e)
        {
            Process.Start(@"C:\Program Files\Common Files\Microsoft Shared\ink\TabTip.exe");
        }

        private void ViolationCombo_Enter(object sender, EventArgs e)
        {
            //ViolationCombo.DroppedDown = true;
        }
    }
}
