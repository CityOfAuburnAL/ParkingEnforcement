using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using ParkingApp_Tablet;

namespace GetOnTabletTest
{
    public partial class VehicleForm : Form
    {
        Dictionary<string, string> categories;
        List<string> stateList = new List<string>();
        Program pg;
        bool reset = false;
        DBHelp dbh = new DBHelp();

        public VehicleForm(Program prog)
        {
            InitializeComponent();
            pg = prog;
            Activated += VehicleForm_Activated;

            DBHelp dbh = new DBHelp();
            categories = dbh.TagCategoryList();

            foreach (string s in categories.Values)
                tagCatCombo.Items.Add(s);

            tagCatCombo.SelectedIndex = 51;

            string[] states = { "AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "FL", "GA", "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY" };
            if (stateList.Count < 1)
                stateList = states.ToList();
        }

        void VehicleForm_Activated(object sender, EventArgs e)
        {
            //LicenseTxt.Text = "";
            //StateTxt.Text = "";
            //VINTxt.Text = "";
            Ticket.VehicleTagCategory = "GN";

            ticketCount.Text = dbh.TicketCount().ToString();
            
            if (tagCatCombo.SelectedIndex != 51)
                tagCatCombo.SelectedIndex = 51;
        }

        private void tagCatCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbx = (ComboBox)sender;
            if (cbx.Text != "")
            {
                Ticket.VehicleTagCategory = categories.FirstOrDefault(x => x.Value == cbx.Text).Key;
                SubmitBtn.Focus();
            }
        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            LicenseTxt.Focus();
            bool hasEdits = false;

            if (LicenseTxt.Text != "" && StateTxt.Text != "")
            {
                if (LicenseTxt.Text.Length < 13)
                {
                    Vehicle.TagNumber = LicenseTxt.Text.ToUpper();
                    Ticket.TagNumber = LicenseTxt.Text.ToUpper();
                }
                else
                {
                    LicenseTxt.Focus();
                    PopUpForm pf = new PopUpForm("Tag Number is too long");
                    pf.ShowDialog();
                    LicenseTxt.Text = "";
                    return;
                }

                if (stateList.Contains(StateTxt.Text.ToUpper()))
                {
                    Vehicle.TagState = StateTxt.Text.ToUpper();
                    Ticket.TagState = StateTxt.Text.ToUpper();
                }
                else
                {
                    StateTxt.Focus();
                    PopUpForm pf = new PopUpForm("Not valid state");
                    pf.ShowDialog();
                    StateTxt.Text = "";
                    return;
                }

                hasEdits = true;
            }
            else if (VINTxt.Text != "")
            {
                Vehicle.VIN = VINTxt.Text.ToUpper();
                Ticket.Notes = "VIN: " + VINTxt.Text.ToUpper();
                hasEdits = true;
            }

            if (hasEdits)
            {

                if (dbh.IsNetConnected())
                    TicketService();
                else
                    TicketOffline();


                //pg.locationForm.Show();
                this.Hide();
                LicenseTxt.Text = "";
                StateTxt.Text = "";
                VINTxt.Text = "";

                //reset is set to true if there a ticket is unable to be written due to constraints
                //If reset is false, go to Offence Form, else go to Location Form
                if (reset == false)
                    pg.offenceForm.Show();
                else
                {
                    reset = false;
                    pg.locationForm.Show();
                }
            }
            else
            {
                PopUpForm pf = new PopUpForm("Not Enough Information Entered");
                pf.ShowDialog();
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            LicenseTxt.Focus();
            if (LicenseTxt.Text == "" && StateTxt.Text == "" && VINTxt.Text == "")
            {
                pg.locationForm.Show();
                this.Hide();
            }
            else
            {
                LicenseTxt.Text = "";
                StateTxt.Text = "";
                VINTxt.Text = "";
            }
        }

        private void KeyBrdBtn_Click(object sender, EventArgs e)
        {
            Process.Start(@"C:\Program Files\Common Files\Microsoft Shared\ink\TabTip.exe");
        }

        private void tagCatCombo_Enter(object sender, EventArgs e)
        {
            //tagCatCombo.DroppedDown = true;
            
        }

        private void TicketService()
        {
            Ticket.IsOffline = false;
            List<Violation> vioList = new List<Violation>();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://coaworks/api/publicsafety/parkingticket/search?"
                + "tagState=" + Vehicle.TagState
                + "&tagNumber=" + Vehicle.TagNumber
                + "&meter=" + Ticket.MeterNumber);//MeterTxt.Text);

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://parking/api/tag/search/"
            //    + Vehicle.TagState + "/" + Vehicle.TagNumber
            //    + "?meter=" + MeterTxt.Text
            //    + "&tagCategory=" + Ticket.VehicleTagCategory
            //    + "&user=" + Attendant.username);

            request.KeepAlive = false;
            request.Method = "GET";
            request.Timeout = 10000;
            request.Credentials = CredentialCache.DefaultCredentials;

            HttpWebResponse response;
            Stream responseStream;
            StreamReader reader;

            try
            {
                response = request.GetResponse() as HttpWebResponse;
                responseStream = response.GetResponseStream();
                reader = new StreamReader(responseStream);
                string responseText = reader.ReadToEnd();

                #region JSON

                JObject o = JObject.Parse(responseText);

                #region Owner

                JContainer ownerContainer = null;
                if (o["owner"] != null && o["owner"].Type != JTokenType.Null)
                {
                    ownerContainer = (JContainer)o["owner"];
                    ParkingApp_Tablet.Owner.Name = (string)ownerContainer["name"];
                    ParkingApp_Tablet.Owner.ProfileID = (string)ownerContainer["profileID"];
                    Ticket.ProfileID = ParkingApp_Tablet.Owner.ProfileID;
                }
                else
                    ownerContainer = null;

                #endregion

                #region Vehicle

                JContainer vehicleContainer = null;
                if (o["vehicle"] != null && o["vehicle"].Type != JTokenType.Null)
                {
                    vehicleContainer = (JContainer)o["vehicle"];
                    Vehicle.Description = (string)vehicleContainer["description"];
                    Vehicle.TowEligible = (bool)vehicleContainer["towEligible"];

                    #region Meter Violation

                    JContainer eligibleMeterViolationContainer = null;
                    if (vehicleContainer["eligibleMeterViolation"] != null && vehicleContainer["eligibleMeterViolation"].Type != JTokenType.Null)
                    {
                        eligibleMeterViolationContainer = (JContainer)vehicleContainer["eligibleMeterViolation"];
                        Vehicle.MeterViolationCharge = (string)eligibleMeterViolationContainer["charge"];
                        Vehicle.MeterViolationChargeCode = (string)eligibleMeterViolationContainer["chargeCode"];
                        Vehicle.MeterViolationAmount = (string)eligibleMeterViolationContainer["amount"];
                    }
                    else
                    {
                        eligibleMeterViolationContainer = null;
                        Vehicle.MeterViolationCharge = "";
                    }

                    JContainer lastMeterViolationContainer = null;
                    if (vehicleContainer["lastMeterViolation"] != null && vehicleContainer["lastMeterViolation"].Type != JTokenType.Null)
                    {
                        lastMeterViolationContainer = (JContainer)vehicleContainer["lastMeterViolation"];
                        Vehicle.LastMeterViolationCharge = (string)lastMeterViolationContainer["charge"];
                        Vehicle.LastMeterViolationChargeCode = (string)lastMeterViolationContainer["chargeCode"];
                        //string lastMeterAmount = (string)lastMeterViolationContainer["amount"];
                    }
                    else
                    {
                        lastMeterViolationContainer = null;
                        Vehicle.LastMeterViolationCharge = "";
                        Vehicle.LastMeterViolationChargeCode = "";
                    }

                    #endregion

                    #region 2 Hour Violation

                    JContainer eligibleTwoHourViolationContainer = null;
                    if (vehicleContainer["eligibleTwoHourViolation"] != null && vehicleContainer["eligibleTwoHourViolation"].Type != JTokenType.Null)
                    {
                        eligibleTwoHourViolationContainer = (JContainer)vehicleContainer["eligibleTwoHourViolation"];
                        Vehicle.TwoHourViolationCharge = (string)eligibleTwoHourViolationContainer["charge"];
                        Vehicle.TwoHourViolationChargeCode = (string)eligibleTwoHourViolationContainer["chargeCode"];
                        Vehicle.TwoHourViolationAmount = (string)eligibleTwoHourViolationContainer["amount"];

                    }
                    else
                    {
                        eligibleTwoHourViolationContainer = null;
                        Vehicle.TwoHourViolationCharge = "";
                    }

                    JContainer lastTwoHourViolationContainer = null;
                    if (vehicleContainer["lastTwoHourViolation"] != null && vehicleContainer["lastTwoHourViolation"].Type != JTokenType.Null)
                    {
                        lastTwoHourViolationContainer = (JContainer)vehicleContainer["lastTwoHourViolation"];
                        Vehicle.LastTwoHourViolationCharge = (string)lastTwoHourViolationContainer["charge"];
                        Vehicle.LastTwoHourViolationChargeCode = (string)lastTwoHourViolationContainer["chargeCode"];
                        //string lastTwoHourAmount = (string)lastTwoHourViolationContainer["amount"];
                    }
                    else
                    {
                        lastTwoHourViolationContainer = null;
                        Vehicle.LastTwoHourViolationCharge = "";
                        Vehicle.LastTwoHourViolationChargeCode = "";
                    }

                    #endregion
                }
                else
                    vehicleContainer = null;

                #endregion

                #region Meter

                JContainer meterContainer;
                if (o["meter"] != null && o["meter"].Type != JTokenType.Null)
                    meterContainer = (JContainer)o["meter"];
                else
                    meterContainer = null;

                //if (MeterTxt.Text != "")
                //    Ticket.MeterNumber = MeterTxt.Text.ToUpper();
                //if (LocationTxt.Text != "")
                //    Ticket.ViolationLocation = LocationTxt.Text;
                //else
                //    Ticket.ViolationLocation = meter[0];

                #endregion

                #region Violation

                JContainer violationContainer;
                if (o["outstanding"] != null && o["outstanding"].Type != JTokenType.Null)
                {
                    violationContainer = (JContainer)o["outstanding"];
                    Violations.TotalBalance = (string)violationContainer["total"];
                }
                else
                    violationContainer = null;

                if (violationContainer != null)
                {
                    JContainer violations = (JContainer)violationContainer["violations"];
                    for (int i = 0; i < violations.Count(); i++)
                    {
                        JToken job = violations[i];
                        vioList.Add(new Violation()
                        {
                            Amount = (string)job["amount"],
                            Charge = (string)job["charge"],
                            Date = (string)job["date"],
                            Ticket = (string)job["ticketNumber"]
                        });
                    }
                    Violations.ViolationList = vioList;
                }

                #endregion

                //If Vehicle can be towed
                if (Vehicle.TowEligible)
                {
                    PopUpForm pf = new PopUpForm("This vehicle is eligible for towing.");
                    pf.ShowDialog();
                }

                if (Ticket.MeterNumber != "")
                {
                    if (dbh.CheckDate())
                    {
                        //If Vehicle has reached the limit of meter violations for today
                        if (Vehicle.MeterViolationCharge == "" && Vehicle.LastMeterViolationChargeCode == "03")
                        {
                            PopUpForm pf = new PopUpForm("Meter Violation Limit Reached.  \nNot Eligible For New Meter Violation");
                            pf.ShowDialog();

                            reset = true;
                        }
                        //if Vehicle has received a meter violation within the last two hours.
                        else if (Vehicle.MeterViolationCharge == "")
                        {
                            PopUpForm pf = new PopUpForm("Previous Meter Violation Issued Within Two Hours. \nNot Eligible For New Meter Violation");
                            pf.ShowDialog();

                            reset = true;
                        }
                    }
                    else
                    {
                        PopUpForm pf = new PopUpForm("Meter Violations Can Only Be Issued Between 8AM and 5PM Monday - Friday");
                        pf.ShowDialog();

                        reset = true;
                    }
                }
                else
                {
                    if (Vehicle.TwoHourViolationCharge == "" && Vehicle.LastTwoHourViolationChargeCode == "14")
                    {
                        PopUpForm pf = new PopUpForm("Two Hour Violation Limit Reached.  \nNot Eligible For New Two Hour Violation");
                        pf.ShowDialog();
                    }
                    else if (Vehicle.TwoHourViolationCharge == "")
                    {
                        PopUpForm pf = new PopUpForm("Previous Two Hour Violation Issued Within Two Hours. \nNot Eligible For New Two Hour Violation");
                        pf.ShowDialog();
                    }
                }

                #endregion

                try
                {
                    request.Abort();
                    response.Close();
                    responseStream.Close();
                    reader.Close();
                }
                catch (Exception ex) { }
            }
            catch (Exception execpt)
            {
                request.Abort();
                TicketOffline();
            }
        }

        private void TicketOffline()
        {
            Ticket.IsOffline = true;

            //if (meter.Count > 0)
            //    Ticket.MeterNumber = meter[1];
            //if (LocationTxt.Text != "")
            //    Ticket.ViolationLocation = LocationTxt.Text;
            //else
            //    Ticket.ViolationLocation = meter[0];
        }
    }
}
