using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using ParkingApp_Tablet;

namespace GetOnTabletTest
{
    public partial class LocationForm : Form
    {
        List<string> meter;
        Program pg;
        DBHelp dbh = new DBHelp();
        List<string> recentTicketStalls;

        public LocationForm(Program prog)
        {
            InitializeComponent();
            pg = prog;

            Activated += LocationForm_Activated;
        }

        void LocationForm_Activated(object sender, EventArgs e)
        {
            Reset();
            ticketCount.Text = dbh.TicketCount().ToString();

            if (flowLayoutPanel1.Controls.Count < 1)
            {
                GetRecentTickets();
                GetExpiredStalls();
            }
            else if (flowLayoutPanel2.Controls.Count < 1)
            {
                GetRecentTickets();
                GetExpiredStalls();
            }
        }

        void stallBtn_Click(object sender, EventArgs e)
        {
            Ticket.MeterNumber = (sender as Button).Text.ToString();

            if (tabControl1.SelectedIndex == 1)
                Ticket.ViolationLocation = "Gay St parking lot";
            else if (tabControl1.SelectedIndex == 2)
                Ticket.ViolationLocation = "Parking deck first floor";

            pg.vehicleForm.Show();
            this.Hide();
        }

        private void GetRecentTickets()
        {
            recentTicketStalls = new List<string>();
            recentTicketStalls.Clear();

            string compareDate = String.Format("{0:s}", DateTime.Now.AddHours(-2));
            string serviceURI = "http://www.auburnalabama.org/mvc/odata/lesnet/ParkingTicket?$select=Meter&$filter=Meter ne '' and Meter ne null and DateIssued gt datetime'" + compareDate + "'";

            HttpWebRequest TicketRequest = (HttpWebRequest)WebRequest.Create(serviceURI);
            TicketRequest.KeepAlive = false;
            TicketRequest.Method = "GET";
            TicketRequest.Timeout = 10000;
            TicketRequest.Credentials = CredentialCache.DefaultCredentials;

            HttpWebResponse response;
            Stream responseStream;
            StreamReader reader;

            try
            {
                response = TicketRequest.GetResponse() as HttpWebResponse;
                responseStream = response.GetResponseStream();
                reader = new StreamReader(responseStream);
                string responseText = reader.ReadToEnd();

                try
                {
                    JObject o = JObject.Parse(responseText);
                    JArray values = (JArray)o["value"];

                    for (int i = 0; i < values.Count(); i++)
                    {
                        JToken stall = values[i];
                        /////
                        string stallNum = stall.Value<string>("Meter");

                        int smartStall = 0;
                        Int32.TryParse(stallNum, out smartStall);

                        if (smartStall > 0)
                            recentTicketStalls.Add(smartStall.ToString());
                        else
                            recentTicketStalls.Add(stallNum);
                    }
                    TicketRequest.Abort();
                    response.Close();
                    responseStream.Close();
                    reader.Close();
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                }
            }
            catch (Exception ex)
            {
                TicketRequest.Abort();
            }
        }

        private void GetExpiredStalls()
        {
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel2.Controls.Clear();

            string currentDate = String.Format("{0:s}", DateTime.Now);
            string serviceURI = "http://www.auburnalabama.org/mvc/odata/ventek/stall?$select=StallNumber,StallType,ExpirationTime,OccupiedTime,Occupied&$orderby=StallNumber&$filter=Occupied eq true and ExpirationTime lt datetime'" + currentDate + "'";

            HttpWebRequest LotRequest = (HttpWebRequest)WebRequest.Create(serviceURI);
            LotRequest.KeepAlive = false;
            LotRequest.Method = "GET";
            LotRequest.Timeout = 10000;
            LotRequest.Credentials = CredentialCache.DefaultCredentials;

            HttpWebResponse response;
            Stream responseStream;
            StreamReader reader;

            try
            {
                response = LotRequest.GetResponse() as HttpWebResponse;
                responseStream = response.GetResponseStream();
                reader = new StreamReader(responseStream);
                string responseText = reader.ReadToEnd();

                try
                {
                    JObject o = JObject.Parse(responseText);
                    JArray values = (JArray)o["value"];

                    for (int i = 0; i < values.Count(); i++)
                    {
                        JToken stall = values[i];

                        string stallNum = stall.Value<string>("StallNumber");
                        ////////////
                        ///////////
                        if (!recentTicketStalls.Contains(stallNum))
                        {
                            //GET OCCUPIED TIME!!!!
                            DateTime occupiedTime = stall.Value<DateTime>("OccupiedTime");
                            //COMPARE TO TICKETS WRITTEN IN LAST 2 HOURS
                            //FILTER OUT MATCHES
                            string stallType = stall.Value<string>("StallType");
                            ////////////
                            ///////////

                            Button stallBtn = new Button()
                            {
                                Text = stallNum,  //This text will show on the button, it's the Stall Number
                                Tag = occupiedTime, //This is the time the stall was occupied, it will be used for filtering
                                Size = new Size(120, 100),
                                Padding = new Padding(10)
                            };
                            stallBtn.Font = new System.Drawing.Font(stallBtn.Font.FontFamily, 30);
                            stallBtn.Click += stallBtn_Click;

                            int num = Convert.ToInt32(stallNum);

                            if (stallType == "Metered")
                            {
                                if (num > 100)
                                    flowLayoutPanel1.Controls.Add(stallBtn);
                                else
                                    flowLayoutPanel2.Controls.Add(stallBtn);
                            }
                        }
                    }

                    LotRequest.Abort();
                    response.Close();
                    responseStream.Close();
                    reader.Close();
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                }
            }
            catch (Exception ex)
            {
                LotRequest.Abort();
            }
        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                MeterTxt.Focus();
                meter = new List<string>();

                if (MeterTxt.Text == "" && LocationTxt.Text == "")
                {
                    PopUpForm pf = new PopUpForm("No Information Entered");
                    pf.ShowDialog();
                }
                else
                {
                    Cursor.Current = Cursors.WaitCursor;

                    if (MeterTxt.Text != "")
                    {
                        meter = dbh.MeterInfo(MeterTxt.Text.ToUpper());  //This is a list of meter info ("Meter Location","Meter ID")

                        if (meter[1] == "")
                        {
                            PopUpForm pf = new PopUpForm("Not Valid Meter");
                            pf.ShowDialog();

                            MeterTxt.Text = "";

                            if (Cursor.Current == Cursors.WaitCursor)
                                Cursor.Current = Cursors.Default;

                            return;
                        }
                        else
                        {
                            Ticket.MeterNumber = meter[1];
                            Ticket.ViolationLocation = meter[0];
                        }
                    }
                    else if (LocationTxt.Text != "")
                    {
                        Ticket.MeterNumber = "";
                        Ticket.ViolationLocation = LocationTxt.Text;
                    }

                    Cursor.Current = Cursors.Default;
                    MeterTxt.Text = "";
                    LocationTxt.Text = "";

                    pg.vehicleForm.Show();
                    this.Hide();
                }
            }
            else if (tabControl1.SelectedIndex == 1)
            {

            }
            else if (tabControl1.SelectedIndex == 2)
            {

            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                MeterTxt.Focus();
                if (MeterTxt.Text == "" && LocationTxt.Text == "")
                {
                    this.Hide();

                    int count = dbh.TicketCount();
                    if (count > 0)
                    {
                        if (dbh.IsNetConnected() && Attendant.username != null)
                            dbh.PostTicket();
                        else
                        {
                            PopUpForm pf = new PopUpForm(count + " tickets not posted.");
                            pf.ShowDialog();
                        }
                    }

                    if (dbh.IsNetConnected())
                        dbh.TableCheck();
                    try
                    {
                        Process[] procs = Process.GetProcessesByName("TabTip");

                        if (procs.Count() > 0)
                            procs[0].Kill();
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                    }
                    Application.Exit();
                }
                else
                {
                    MeterTxt.Text = "";
                    LocationTxt.Text = "";
                }
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                if (flowLayoutPanel1.Controls.Count > 0)
                    flowLayoutPanel1.Controls.Clear();
                else
                {
                    int count = dbh.TicketCount();
                    if (count > 0)
                    {
                        if (dbh.IsNetConnected() && Attendant.username != null)
                            dbh.PostTicket();
                        else
                        {
                            PopUpForm pf = new PopUpForm(count + " tickets not posted.");
                            pf.ShowDialog();
                        }
                    }

                    if (dbh.IsNetConnected())
                        dbh.TableCheck();
                    try
                    {
                        Process[] procs = Process.GetProcessesByName("TabTip");

                        if (procs.Count() > 0)
                            procs[0].Kill();
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                    }
                    Application.Exit();
                }
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                if (flowLayoutPanel2.Controls.Count > 0)
                    flowLayoutPanel2.Controls.Clear();
                else
                {
                    int count = dbh.TicketCount();
                    if (count > 0)
                    {
                        if (dbh.IsNetConnected() && Attendant.username != null)
                            dbh.PostTicket();
                        else
                        {
                            PopUpForm pf = new PopUpForm(count + " tickets not posted.");
                            pf.ShowDialog();
                        }
                    }

                    if (dbh.IsNetConnected())
                        dbh.TableCheck();
                    try
                    {
                        Process[] procs = Process.GetProcessesByName("TabTip");

                        if (procs.Count() > 0)
                            procs[0].Kill();
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                    }
                    Application.Exit();
                }
            }
        }

        private void KeyBrdBtn_Click(object sender, EventArgs e)
        {
            Process.Start(@"C:\Program Files\Common Files\Microsoft Shared\ink\TabTip.exe");
        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            GetRecentTickets();

            GetExpiredStalls();

            if (Cursor.Current == Cursors.WaitCursor)
                Cursor.Current = Cursors.Default;
        }

        private void Reset()
        {
            //reset Vehicle
            Vehicle.Description = "";
            Vehicle.TagNumber = "";
            Vehicle.TagState = "";
            Vehicle.TowEligible = false;
            Vehicle.VIN = "";
            Vehicle.TwoHourViolationAmount = "";
            Vehicle.TwoHourViolationCharge = "";
            Vehicle.TwoHourViolationChargeCode = "";
            Vehicle.MeterViolationAmount = "";
            Vehicle.MeterViolationCharge = "";
            Vehicle.MeterViolationChargeCode = "";
            Vehicle.LastMeterViolationCharge = "";
            Vehicle.LastMeterViolationChargeCode = "";
            Vehicle.LastTwoHourViolationCharge = "";
            Vehicle.LastTwoHourViolationChargeCode = "";

            //reset Violations
            Violations.TotalBalance = "";
            if (Violations.ViolationList != null)
                Violations.ViolationList.Clear();

            //reset Owner
            ParkingApp_Tablet.Owner.Name = "";
            ParkingApp_Tablet.Owner.ProfileID = "";

            //reset Ticket
            Ticket.ChargeCode = "";
            Ticket.Latitude = 0.0;
            Ticket.Longitude = 0.0;
            Ticket.TagState = "";
            Ticket.VehicleTagCategory = "";
            //Ticket.MeterNumber = "";
            Ticket.Notes = "";
            Ticket.PrintCount = 0;
            Ticket.PrintError = false;
            Ticket.ProfileID = "";
            Ticket.StatusCode = "O";
            Ticket.TagNumber = "";
            Ticket.VIN = "";
            //Ticket.ViolationLocation = "";
        }
    }
}
