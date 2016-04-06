using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;
using GetOnTabletTest;
using Newtonsoft.Json.Linq;

namespace ParkingApp_Tablet
{
    public class DBHelp
    {
        string fileName = @"C:\Users\Public\Documents\parking2.sdf";
        string testFilename = @"C:\Users\wkimrey\Desktop\parkingDB_backup\Test\parking2.sdf";
        string password = "";
        public string ConnectionString;
        public string postString;
        public int TicketNumToDelete;

        public DBHelp()
        {
            if (File.Exists(testFilename))
                ConnectionString = string.Format("DataSource='{0}'; Password='{1}'", testFilename, password);
            else
                ConnectionString = string.Format("DataSource='{0}'; Password='{1}'", fileName, password);
        }

        public bool IsNetConnected()
        {
            //bool IsConnect = false;
            //try
            //{
            //    string hostName = Dns.GetHostName();
            //    IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
            //    string hostIPAdd = hostEntry.AddressList[0].ToString();

            //    IsConnect = hostIPAdd != IPAddress.Parse("127.0.0.1").ToString();
            //}
            //catch
            //{
            //    IsConnect = false;
            //}
            return NetworkInterface.GetIsNetworkAvailable(); //IsConnect;
        }

        public bool EmployeeLogin(string username)
        {
            bool IsUser = false;

            SqlCeConnection cn = new SqlCeConnection(ConnectionString);
            cn.Open();

            if (cn.State == ConnectionState.Open)
            {
                try
                {
                    string cmdTxt = "Select OfficerBadge, EmployeeName as OfficerName From Employees E join Officers O on E.EmployeeID = O.EmployeeID Where UserName = '" + username + "'";
                    SqlCeCommand cmd = new SqlCeCommand(cmdTxt, cn);

                    SqlCeDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Attendant.OfficerBadge = reader["OfficerBadge"].ToString();
                        Attendant.OfficerName = reader["OfficerName"].ToString();

                        if (Attendant.OfficerName != null)
                            IsUser = true;
                    }
                }
                catch (Exception ex)
                {
                    PopUpForm pf = new PopUpForm(ex.ToString());
                    pf.ShowDialog();
                }
                finally
                {
                    cn.Close();
                }
            }
            if (!IsUser)
                return false;
            else
                return true;
        }

        public Dictionary<string, decimal> ChargesList()
        {
            Dictionary<string, decimal> charges = new Dictionary<string, decimal>();

            SqlCeConnection cn = new SqlCeConnection(ConnectionString);
            cn.Open();

            if (cn.State == ConnectionState.Open)
            {
                try
                {
                    if (CheckDate())
                    {
                        string cmdTxt = "Select * From Charges";
                        SqlCeCommand cmd = new SqlCeCommand(cmdTxt, cn);
                        SqlCeDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            charges.Add(reader["Charge"].ToString(), (decimal)reader["ViolationAmount"]);
                        }
                    }
                    else
                    {
                        string cmdTxt = "Select * From Charges Where Charge NOT LIKE '%METER%' ";
                        SqlCeCommand cmd = new SqlCeCommand(cmdTxt, cn);
                        SqlCeDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            charges.Add(reader["Charge"].ToString(), (decimal)reader["ViolationAmount"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    PopUpForm pf = new PopUpForm(ex.ToString());
                    pf.ShowDialog();
                }
                finally
                {
                    cn.Close();
                }
            }
            return charges;
        }

        public string[] OrdInfo(string Charge)
        {
            string Ord = "";
            string CCode = "";
            SqlCeConnection cn = new SqlCeConnection(ConnectionString);
            cn.Open();

            if (cn.State == ConnectionState.Open)
            {
                try
                {
                    string cmdTxt = "Select * From Charges Where Charge = '" + Charge + "'";
                    SqlCeCommand cmd = new SqlCeCommand(cmdTxt, cn);
                    SqlCeDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Ord = reader["LocalOrd"].ToString();
                        CCode = reader["ChargeCode"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    PopUpForm pf = new PopUpForm(ex.ToString());
                    pf.ShowDialog();
                }
                finally
                {
                    cn.Close();
                }
            }
            string[] Info = new string[] { Ord, CCode };
            return Info;
        }

        public List<string> MeterInfo(string meter)
        {
            string mLoc = "";
            string mID = "";

            SqlCeConnection cn = new SqlCeConnection(ConnectionString);
            cn.Open();

            if (cn.State == ConnectionState.Open)
            {
                try
                {
                    string cmdTxt = "Select * From ParkingMeters Where FacilityID = '" + meter + "'";
                    SqlCeCommand cmd = new SqlCeCommand(cmdTxt, cn);
                    SqlCeDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        mLoc = reader["Location"].ToString();
                        mID = reader["FacilityID"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    PopUpForm pf = new PopUpForm(ex.ToString());
                    pf.ShowDialog();
                }
                finally
                {
                    cn.Close();
                }
            }
            string[] Info = new string[] { mLoc, mID };
            return Info.ToList();
        }

        public void InsertTicket()
        {
            SqlCeConnection cn = new SqlCeConnection(ConnectionString);
            cn.Open();

            if (cn.State == ConnectionState.Open)
            {
                try
                {
                    string cmdTxt = "Insert into ParkingTickets "
                        + "(DateIssued, ChargeCode, MeterNumber, VehicleTagCategory, TagState, TagNumber, VIN, PrintCount, ProfileID, OfficerBadge, ViolationLocation, Notes, Latitude, Longitude, StatusCode)"
                        + "values (@dateissued, @chargecode, @meternumber, @vehicletagcategory, @tagstate, @tagnumber, @vin, @printcount, @profileid, @officerbadge, @violationlocation, @notes, @latitude,@longitude, @statuscode)";
                    SqlCeCommand cmd = new SqlCeCommand(cmdTxt, cn);
                    //cmd.Parameters.AddWithValue("@ticketnumber", Ticket.TicketNumber);
                    //cmd.Parameters.AddWithValue("@machineid", Ticket.MachineID);
                    cmd.Parameters.AddWithValue("@dateissued", Ticket.DateIssued);
                    cmd.Parameters.AddWithValue("@chargecode", Ticket.ChargeCode);
                    cmd.Parameters.AddWithValue("@meternumber", Ticket.MeterNumber);
                    cmd.Parameters.AddWithValue("@vehicletagcategory", Ticket.VehicleTagCategory);
                    cmd.Parameters.AddWithValue("@tagstate", Ticket.TagState);
                    cmd.Parameters.AddWithValue("@tagnumber", Ticket.TagNumber);
                    cmd.Parameters.AddWithValue("@vin", Ticket.VIN);
                    cmd.Parameters.AddWithValue("@printcount", Ticket.PrintCount);
                    cmd.Parameters.AddWithValue("@profileid", Ticket.ProfileID);
                    cmd.Parameters.AddWithValue("@officerbadge", Ticket.OfficerBadge);
                    cmd.Parameters.AddWithValue("@violationlocation", Ticket.ViolationLocation);
                    cmd.Parameters.AddWithValue("@notes", Ticket.Notes);
                    cmd.Parameters.AddWithValue("@latitude", Ticket.Latitude);
                    cmd.Parameters.AddWithValue("@longitude", Ticket.Longitude);
                    cmd.Parameters.AddWithValue("@statuscode", Ticket.StatusCode);
                    cmd.ExecuteNonQuery();

                    string cmdTxt2 = "Select @@IDENTITY";
                    SqlCeCommand cmd2 = new SqlCeCommand(cmdTxt2, cn);
                    Ticket.TicketNumber = Convert.ToInt32(cmd2.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    PopUpForm pf = new PopUpForm(ex.ToString());
                    pf.ShowDialog();
                }
                finally
                {
                    cn.Close();
                }
            }
        }

        public void UpdatePrintCount(int TicketNum)
        {
            SqlCeConnection cn = new SqlCeConnection(ConnectionString);
            cn.Open();

            if (cn.State == ConnectionState.Open)
            {
                try
                {
                    string cmdTxt = "UPDATE ParkingTickets " +
                        "SET PrintCount = PrintCount + 1" +
                        "WHERE ParkingTickets.TicketNumber = " + TicketNum;
                    SqlCeCommand cmd = new SqlCeCommand(cmdTxt, cn);
                    cmd.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    PopUpForm pf = new PopUpForm(ex.ToString());
                    pf.ShowDialog();
                }
                finally
                {
                    cn.Close();
                }
            }
        }

        public void CancelExistingTicket(int TicketNum)
        {
            SqlCeConnection cn = new SqlCeConnection(ConnectionString);
            cn.Open();

            if (cn.State == ConnectionState.Open)
            {
                try
                {
                    string cmdTxt = "UPDATE ParkingTickets " +
                        "SET Notes = '" + Ticket.Notes + "'" +
                        "WHERE ParkingTickets.TicketNumber = " + TicketNum;
                    SqlCeCommand cmd = new SqlCeCommand(cmdTxt, cn);
                    cmd.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    PopUpForm pf = new PopUpForm(ex.ToString());
                    pf.ShowDialog();
                }
                finally
                {
                    cn.Close();
                }
            }
        }

        public void DeleteTicket(int TicketNum)
        {
            SqlCeConnection cn = new SqlCeConnection(ConnectionString);
            cn.Open();

            if (cn.State == ConnectionState.Open)
            {
                try
                {
                    string cmdTxt = "DELETE FROM ParkingTickets " +
                        "WHERE ParkingTickets.TicketNumber = " + TicketNum;
                    SqlCeCommand cmd = new SqlCeCommand(cmdTxt, cn);
                    cmd.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    PopUpForm pf = new PopUpForm(ex.ToString());
                    pf.ShowDialog();
                }
                finally
                {
                    cn.Close();
                }
            }
        }

        public bool CheckDate()
        {
            bool CanTicket = true;
            DateTime today = DateTime.Now;
            if (today.DayOfWeek == DayOfWeek.Saturday || today.DayOfWeek == DayOfWeek.Sunday)
            {
                CanTicket = false;
            }
            else if ((today.TimeOfDay < (new TimeSpan(8, 0, 0)) || (today.TimeOfDay > (new TimeSpan(17, 0, 0)))))
            {
                CanTicket = false;
            }
            else
            {
                SqlCeConnection cn = new SqlCeConnection(ConnectionString);
                cn.Open();

                if (cn.State == ConnectionState.Open)
                {
                    try
                    {
                        string cmdTxt = "Select Date From MeterFreeDates Where Date = GETDATE()";
                        SqlCeCommand cmd = new SqlCeCommand(cmdTxt, cn);
                        object ob = cmd.ExecuteScalar();
                        if (ob == null)
                            CanTicket = true;
                        else
                            CanTicket = false;
                    }

                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        cn.Close();
                    }
                }
            }

            return CanTicket;
        }

        public void PostTicket()
        {
            Cursor.Current = Cursors.WaitCursor;

            DataSet toPost = GetPostTickets();
            foreach (DataRow dr in toPost.Tables[0].Rows)
            {
                Dictionary<string, object> postVariables = new Dictionary<string, object>();
                postVariables.Add("TicketNumber", dr["TicketNumber"].ToString());
                postVariables.Add("MachineID", dr["MachineID"].ToString());
                postVariables.Add("DateIssued", dr["DateIssued"].ToString());

                if (dr["ChargeCode"].ToString() != String.Empty)
                    postVariables.Add("ChargeCode", dr["ChargeCode"].ToString());
                else
                    postVariables.Add("ChargeCode", DBNull.Value);

                postVariables.Add("MeterNumber", dr["MeterNumber"].ToString());
                postVariables.Add("VehicleTagCategory", dr["VehicleTagCategory"].ToString());
                postVariables.Add("TagState", dr["TagState"].ToString());
                postVariables.Add("TagNumber", dr["TagNumber"].ToString());
                postVariables.Add("ProfileID", dr["ProfileID"].ToString());
                postVariables.Add("OfficerBadge", dr["OfficerBadge"].ToString());
                postVariables.Add("ViolationLocation", dr["ViolationLocation"].ToString());
                postVariables.Add("Notes", dr["Notes"].ToString());
                postVariables.Add("Latitude", dr["Latitude"].ToString());
                postVariables.Add("Longitude", dr["Longitude"].ToString());
                postVariables.Add("PrintCount", dr["PrintCount"].ToString());
                postVariables.Add("StatusCode", dr["StatusCode"].ToString());

                postString = "";
                foreach (KeyValuePair<string, object> pair in postVariables)
                {
                    postString += pair.Key + "=" + pair.Value + "&";
                }
                postString = postString.Substring(0, postString.Length - 1);
                byte[] postBytes = Encoding.UTF8.GetBytes(postString);

                TicketNumToDelete = Convert.ToInt32(dr["TicketNumber"]);
                //MessageBox.Show(postString);
                HttpWebRequest poster = (HttpWebRequest)WebRequest.Create("http://coaworks/api/publicsafety/parkingticket");
                //HttpWebRequest poster = (HttpWebRequest)WebRequest.Create("http://parking/api/parkingticketapi/?user=" + Attendant.username);
                poster.KeepAlive = false;
                poster.Method = "POST";
                poster.ContentType = "application/x-www-form-urlencoded";
                poster.Timeout = 10000; //This is in milliseconds.  Equals 10 seconds
                poster.ContentLength = postBytes.Length;
                poster.Credentials = CredentialCache.DefaultCredentials;
                //poster.Accept = "*/*";
                //poster.UserAgent = ".NET Framework Test Client";

                Stream dataStream;
                HttpWebResponse response;

                try
                {
                    //poster.BeginGetRequestStream(new AsyncCallback(RequestCallback), poster);

                    dataStream = poster.GetRequestStream();
                    dataStream.Write(postBytes, 0, postBytes.Length);
                    dataStream.Close();

                    response = (HttpWebResponse)poster.GetResponse();
                    //MessageBox.Show(response.StatusCode.ToString());
                    //Look for Status 409 for Duplicate entries
                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    {
                        DeleteTicket(Convert.ToInt32(dr["TicketNumber"]));
                    }
                }
                catch (WebException tex)
                {
                    //MessageBox.Show(tex.Message);
                    if (((HttpWebResponse)tex.Response).StatusCode == HttpStatusCode.Conflict)
                    {
                        DeleteTicket(Convert.ToInt32(dr["TicketNumber"]));
                    }
                    else
                    {
                        Cursor.Current = Cursors.Default;
                    }
                    continue;
                }
                catch (Exception exxx)
                {
                    //MessageBox.Show(exxx.Message);
                    Cursor.Current = Cursors.Default;

                    continue;
                }

                try
                {
                    poster.Abort();
                    response.Close();
                }
                catch (Exception ex) { }

            }

            Cursor.Current = Cursors.Default;
        }

        private void RequestCallback(IAsyncResult asynchResult)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)asynchResult.AsyncState;

                // End the operation
                Stream postStream = request.EndGetRequestStream(asynchResult);

                //Console.WriteLine("Please enter the input data to be posted:");
                string postData = postString;//Console.ReadLine();

                // Convert the string into a byte array.
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                // Write to the request stream.
                postStream.Write(byteArray, 0, postData.Length);
                postStream.Close();

                // Start the asynchronous operation to get the response
                request.BeginGetResponse(new AsyncCallback(GetResponseCallback), request);
            }
            catch (Exception ex) { }
        }

        private void GetResponseCallback(IAsyncResult asynchronousResult)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;

                // End the operation
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);
                Stream streamResponse = response.GetResponseStream();
                StreamReader streamRead = new StreamReader(streamResponse);
                string responseString = streamRead.ReadToEnd();
                //Console.WriteLine(responseString);
                // Close the stream object
                streamResponse.Close();
                streamRead.Close();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    DeleteTicket(TicketNumToDelete);
                }
                // Release the HttpWebResponse
                response.Close();
                //allDone.Set();
            }
            catch (Exception ex) { }
        }

        public int TicketCount()
        {
            int count = 0;

            SqlCeConnection cn = new SqlCeConnection(ConnectionString);
            cn.Open();

            if (cn.State == ConnectionState.Open)
            {
                try
                {
                    string cmdTxt = "Select Count(*) From ParkingTickets";
                    SqlCeCommand cmd = new SqlCeCommand(cmdTxt, cn);
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }

                catch (Exception ex)
                {
                    PopUpForm pf = new PopUpForm(ex.ToString());
                    pf.ShowDialog();
                }
                finally
                {
                    cn.Close();
                }
            }

            return count;
        }

        public string GetMachineID()
        {
            string MachineID = "";

            SqlCeConnection cn = new SqlCeConnection(ConnectionString);
            cn.Open();

            if (cn.State == ConnectionState.Open)
            {
                try
                {
                    string cmdTxt = "Select Top (1) MachineID From ParkingTickets";
                    SqlCeCommand cmd = new SqlCeCommand(cmdTxt, cn);
                    MachineID = cmd.ExecuteScalar().ToString();
                }

                catch (Exception ex)
                {
                    PopUpForm pf = new PopUpForm(ex.ToString());
                    pf.ShowDialog();
                }
                finally
                {
                    cn.Close();
                }
            }

            return MachineID;
        }

        public Dictionary<string, string> TagCategoryList()
        {
            Dictionary<string, string> categories = new Dictionary<string, string>();

            SqlCeConnection cn = new SqlCeConnection(ConnectionString);
            cn.Open();

            if (cn.State == ConnectionState.Open)
            {
                try
                {
                    string cmdTxt = "Select * From TagCategory";
                    SqlCeCommand cmd = new SqlCeCommand(cmdTxt, cn);
                    SqlCeDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        categories.Add(reader["TagCategory"].ToString(), reader["Description"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    PopUpForm pf = new PopUpForm(ex.ToString());
                    pf.ShowDialog();
                }
                finally
                {
                    cn.Close();
                }
            }

            Dictionary<string, string> sorted = categories.OrderBy(k => k.Value).ToDictionary(k => k.Key, k => k.Value);

            return sorted;
        }

        public DataSet GetPostTickets()
        {
            DataSet ds = new DataSet();

            SqlCeConnection cn = new SqlCeConnection(ConnectionString);
            cn.Open();

            if (cn.State == ConnectionState.Open)
            {
                try
                {
                    SqlCeDataAdapter sda = new SqlCeDataAdapter();
                    string cmdTxt = "Select * From ParkingTickets";
                    SqlCeCommand cmd = new SqlCeCommand(cmdTxt, cn);
                    sda.SelectCommand = cmd;

                    sda.Fill(ds);
                }
                catch (Exception ex)
                {
                    PopUpForm pf = new PopUpForm(ex.ToString());
                    pf.ShowDialog();
                }
                finally
                {
                    cn.Close();
                }
            }

            return ds;
        }

        public DateTime LastUpdateDate()
        {
            DateTime updateDate = DateTime.Now;

            SqlCeConnection cn = new SqlCeConnection(ConnectionString);
            cn.Open();

            if (cn.State == ConnectionState.Open)
            {
                try
                {
                    string cmdTxt = "Select * From LastUpdate";
                    SqlCeCommand cmd = new SqlCeCommand(cmdTxt, cn);
                    updateDate = (DateTime)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    PopUpForm pf = new PopUpForm(ex.ToString());
                    pf.ShowDialog();
                }
                finally
                {
                    cn.Close();
                }
            }

            //MessageBox.Show("Last Update is: " + updateDate.ToShortDateString());

            return updateDate;
        }

        public void UpdateLastUpdateDate()
        {
            SqlCeConnection cn = new SqlCeConnection(ConnectionString);
            cn.Open();

            if (cn.State == ConnectionState.Open)
            {
                try
                {
                    string cmdTxt = "UPDATE LastUpdate " +
                        "SET [Update] = '" + DateTime.Now.ToShortDateString() + "'";
                    SqlCeCommand cmd = new SqlCeCommand(cmdTxt, cn);
                    cmd.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    PopUpForm pf = new PopUpForm(ex.ToString());
                    pf.ShowDialog();
                }
                finally
                {
                    cn.Close();
                }
            }
        }

        public void TableCheck()
        {
            //MessageBox.Show("Checking Table");

            ChargeTable chargeTbl = new ChargeTable();
            chargeTbl.ChargeList = new List<ChargeRecord>();
            EmployeeTable employeeTbl = new EmployeeTable();
            employeeTbl.EmployeeList = new List<EmployeeRecord>();
            MeterFreeDateTable meterDateTbl = new MeterFreeDateTable();
            meterDateTbl.MeterFreeDateList = new List<MeterFreeDateRecord>();
            OfficerTable officerTbl = new OfficerTable();
            officerTbl.OfficerList = new List<OfficerRecord>();
            TagCategoryTable tagCatTbl = new TagCategoryTable();
            tagCatTbl.TagCategoryList = new List<TagCategoryRecord>();
            ParkingMeterTable parkMeterTbl = new ParkingMeterTable();
            parkMeterTbl.ParkingMeterList = new List<ParkingMeter>();

            DateTime LastUpdated = LastUpdateDate();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://coaworks/api/publicsafety/parkingmetersync/table?"
                + "date=" + LastUpdated.ToShortDateString());

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://parking/api/syncapi/"
            //    + "?date=" + LastUpdated.ToShortDateString()
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

                JObject o = JObject.Parse(responseText);

                #region Charges

                JContainer chargesContainer = null;
                if (o["charges"] != null && o["charges"].Type != JTokenType.Null)
                {
                    chargesContainer = (JContainer)o["charges"];
                    List<ChargeRecord> chrgList = new List<ChargeRecord>();
                    for (int i = 0; i < chargesContainer.Count(); i++)
                    {
                        JToken chrg = chargesContainer[i];
                        ChargeRecord cr = new ChargeRecord();

                        if (chrg["Charge1"] != null && chrg["Charge1"].Type != JTokenType.Null)
                            cr.Charge = (string)chrg["Charge1"];
                        if (chrg["ChargeCode"] != null && chrg["ChargeCode"].Type != JTokenType.Null)
                            cr.ChargeCode = (string)chrg["ChargeCode"];
                        if (chrg["LocalOrd"] != null && chrg["LocalOrd"].Type != JTokenType.Null)
                            cr.LocalOrd = (string)chrg["LocalOrd"];
                        if (chrg["ViolationAmount"] != null && chrg["ViolationAmount"].Type != JTokenType.Null)
                            cr.ViolationAmount = (decimal)chrg["ViolationAmount"];

                        chrgList.Add(cr);
                    }

                    chargeTbl.ChargeList = chrgList;
                }
                else
                    chargesContainer = null;

                #endregion

                #region Employees

                JContainer employeesContainer = null;
                if (o["employees"] != null && o["employees"].Type != JTokenType.Null)
                {
                    employeesContainer = (JContainer)o["employees"];
                    List<EmployeeRecord> empList = new List<EmployeeRecord>();
                    for (int i = 0; i < employeesContainer.Count(); i++)
                    {
                        JToken emp = employeesContainer[i];
                        EmployeeRecord er = new EmployeeRecord();

                        if (emp["EmployeeID"] != null && emp["EmployeeID"].Type != JTokenType.Null)
                            er.EmployeeID = (string)emp["EmployeeID"];
                        if (emp["EmployeeName"] != null && emp["EmployeeName"].Type != JTokenType.Null)
                            er.EmployeeName = (string)emp["EmployeeName"];
                        if (emp["UserName"] != null && emp["UserName"].Type != JTokenType.Null)
                            er.UserName = (string)emp["UserName"];
                        if (emp["Status"] != null && emp["Status"].Type != JTokenType.Null)
                            er.Status = (int)emp["Status"];

                        if (er.Status == 1)
                            empList.Add(er);
                    }
                    employeeTbl.EmployeeList = empList;
                }
                else
                    employeesContainer = null;

                #endregion

                #region MeterFreeDates

                JContainer meterFreeDatesContainer = null;
                if (o["meterFreeDates"] != null && o["meterFreeDates"].Type != JTokenType.Null)
                {
                    meterFreeDatesContainer = (JContainer)o["meterFreeDates"];
                    List<MeterFreeDateRecord> mfDateList = new List<MeterFreeDateRecord>();
                    for (int i = 0; i < meterFreeDatesContainer.Count(); i++)
                    {
                        JToken freeDate = meterFreeDatesContainer[i];
                        MeterFreeDateRecord mfdr = new MeterFreeDateRecord();

                        if (freeDate["Date"] != null && freeDate["Date"].Type != JTokenType.Null)
                            mfdr.Date = (string)freeDate["Date"];

                        mfDateList.Add(mfdr);
                    }

                    meterDateTbl.MeterFreeDateList = mfDateList;
                }
                else
                    meterFreeDatesContainer = null;

                #endregion

                #region Officers

                JContainer officersContainer = null;
                if (o["officers"] != null && o["officers"].Type != JTokenType.Null)
                {
                    officersContainer = (JContainer)o["officers"];
                    List<OfficerRecord> offRecList = new List<OfficerRecord>();
                    for (int i = 0; i < officersContainer.Count(); i++)
                    {
                        JToken officer = officersContainer[i];
                        OfficerRecord offR = new OfficerRecord();

                        if (officer["OfficerBadge"] != null && officer["OfficerBadge"].Type != JTokenType.Null)
                            offR.OfficerBadge = (string)officer["OfficerBadge"];
                        if (officer["EmployeeID"] != null && officer["EmployeeID"].Type != JTokenType.Null)
                            offR.EmployeeID = (string)officer["EmployeeID"];
                        if (officer["Rank"] != null && officer["Rank"].Type != JTokenType.Null)
                            offR.Rank = (string)officer["Rank"];

                        offRecList.Add(offR);
                    }
                    officerTbl.OfficerList = offRecList;
                }
                else
                    officersContainer = null;

                #endregion

                #region TagCategories

                JContainer tagCategoriesContainer = null;
                if (o["tagCategories"] != null && o["tagCategories"].Type != JTokenType.Null)
                {
                    tagCategoriesContainer = (JContainer)o["tagCategories"];
                    List<TagCategoryRecord> tCatList = new List<TagCategoryRecord>();
                    for (int i = 0; i < tagCategoriesContainer.Count(); i++)
                    {
                        JToken tagCat = tagCategoriesContainer[i];
                        TagCategoryRecord tcr = new TagCategoryRecord();

                        if (tagCat["Description"] != null && tagCat["Description"].Type != JTokenType.Null)
                            tcr.Description = (string)tagCat["Description"];
                        if (tagCat["TagCategory1"] != null && tagCat["TagCategory1"].Type != JTokenType.Null)
                            tcr.TagCategory = (string)tagCat["TagCategory1"];

                        tCatList.Add(tcr);
                    }
                    tagCatTbl.TagCategoryList = tCatList;
                }
                else
                    tagCategoriesContainer = null;

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
            catch (Exception ex)
            {
                request.Abort();
            }

            //This is away from the rest of the table downloading, mainly due to not wanting
            //to interfere with the HttpWebResponse and HttpWebRequest
            #region ParkingMeters

            string connStr = "Data Source=atlas;Initial Catalog=Edits;";

            using (SqlConnection sqlconn = new SqlConnection(connStr))
            {
                string queryStr = "SELECT * FROM Edits.dataloader.PARKINGMETERS_EVW WHERE LifecycleStatus = 'ACTIVE'";
                SqlCommand comm = new SqlCommand(queryStr, sqlconn);

                sqlconn.Open();

                SqlDataReader sqlreader = comm.ExecuteReader();
                try
                {
                    while (sqlreader.Read())
                    {
                        string Location = sqlreader[2].ToString();
                        string facilityID = sqlreader[3].ToString();
                        parkMeterTbl.ParkingMeterList.Add(new ParkingMeter { Location = Location, FacilityID = facilityID });
                    }
                }
                finally
                {
                    sqlreader.Close();
                }
            }

            #endregion

            SqlCeConnection cn = new SqlCeConnection(ConnectionString);
            cn.Open();

            if (cn.State == ConnectionState.Open)
            {
                try
                {
                    bool NeedsUpdate = false;

                    #region Insert Charges

                    if (chargeTbl.ChargeList.Count > 0)
                    {
                        NeedsUpdate = true;
                        string cmdTxt = "DELETE FROM Charges ";
                        SqlCeCommand cmd = new SqlCeCommand(cmdTxt, cn);
                        cmd.ExecuteNonQuery();

                        foreach (ChargeRecord CR in chargeTbl.ChargeList)
                        {
                            try
                            {
                                string cmdTxt2 = "Insert into Charges "
                                    + "(ChargeCode, Charge, LocalOrd, ViolationAmount)"
                                    + "values (@chargecode, @charge, @localord, @violationamount)";
                                SqlCeCommand cmd2 = new SqlCeCommand(cmdTxt2, cn);
                                cmd2.Parameters.AddWithValue("@chargecode", CR.ChargeCode);
                                cmd2.Parameters.AddWithValue("@charge", CR.Charge);
                                cmd2.Parameters.AddWithValue("@localord", CR.LocalOrd);
                                cmd2.Parameters.AddWithValue("@violationamount", CR.ViolationAmount);
                                cmd2.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }

                    #endregion

                    #region Insert Employees

                    if (employeeTbl.EmployeeList.Count > 0)
                    {
                        NeedsUpdate = true;
                        string cmdTxt = "DELETE FROM Employees ";
                        SqlCeCommand cmd = new SqlCeCommand(cmdTxt, cn);
                        cmd.ExecuteNonQuery();

                        foreach (EmployeeRecord ER in employeeTbl.EmployeeList)
                        {
                            try
                            {
                                string cmdTxt2 = "Insert into Employees "
                                    + "(EmployeeID, EmployeeName, UserName, Status)"
                                    + "values (@employeeid, @employeename, @username, @status)";
                                SqlCeCommand cmd2 = new SqlCeCommand(cmdTxt2, cn);
                                cmd2.Parameters.AddWithValue("@employeeid", ER.EmployeeID);
                                cmd2.Parameters.AddWithValue("@employeename", ER.EmployeeName);
                                cmd2.Parameters.AddWithValue("@username", ER.UserName);
                                cmd2.Parameters.AddWithValue("@status", ER.Status);
                                cmd2.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }

                    #endregion

                    #region Insert MeterFreeDates

                    if (meterDateTbl.MeterFreeDateList.Count > 0)
                    {
                        NeedsUpdate = true;
                        string cmdTxt = "DELETE FROM MeterFreeDates ";
                        SqlCeCommand cmd = new SqlCeCommand(cmdTxt, cn);
                        cmd.ExecuteNonQuery();

                        foreach (MeterFreeDateRecord MFDR in meterDateTbl.MeterFreeDateList)
                        {
                            try
                            {
                                string cmdTxt2 = "Insert into MeterFreeDates "
                                    + "(Date)"
                                    + "values (@date)";
                                SqlCeCommand cmd2 = new SqlCeCommand(cmdTxt2, cn);
                                cmd2.Parameters.AddWithValue("@date", MFDR.Date);
                                cmd2.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }

                    #endregion

                    #region Insert Officers

                    if (officerTbl.OfficerList.Count > 0)
                    {
                        NeedsUpdate = true;
                        string cmdTxt = "DELETE FROM Officers ";
                        SqlCeCommand cmd = new SqlCeCommand(cmdTxt, cn);
                        cmd.ExecuteNonQuery();

                        foreach (OfficerRecord OR in officerTbl.OfficerList)
                        {
                            try
                            {
                                string cmdTxt2 = "Insert into Officers "
                                    + "(OfficerBadge, EmployeeID, Rank)"
                                    + "values (@officerbadge, @employeeid, @rank)";
                                SqlCeCommand cmd2 = new SqlCeCommand(cmdTxt2, cn);
                                cmd2.Parameters.AddWithValue("@officerbadge", OR.OfficerBadge);
                                cmd2.Parameters.AddWithValue("@employeeid", OR.EmployeeID);
                                if (OR.Rank == null)
                                    cmd2.Parameters.AddWithValue("@rank", DBNull.Value);
                                else
                                    cmd2.Parameters.AddWithValue("@rank", OR.Rank);
                                cmd2.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }

                    #endregion

                    #region Insert TagCategories

                    if (tagCatTbl.TagCategoryList.Count > 0)
                    {
                        NeedsUpdate = true;
                        string cmdTxt = "DELETE FROM TagCategory ";
                        SqlCeCommand cmd = new SqlCeCommand(cmdTxt, cn);
                        cmd.ExecuteNonQuery();

                        foreach (TagCategoryRecord TCR in tagCatTbl.TagCategoryList)
                        {
                            string cmdTxt2 = "Insert into TagCategory "
                                + "(TagCategory, Description)"
                                + "values (@tagcategory, @description)";
                            SqlCeCommand cmd2 = new SqlCeCommand(cmdTxt2, cn);
                            cmd2.Parameters.AddWithValue("@tagcategory", TCR.TagCategory);
                            cmd2.Parameters.AddWithValue("@description", TCR.Description);
                            cmd2.ExecuteNonQuery();
                        }
                    }

                    #endregion

                    #region Insert Parking Meters

                    if (parkMeterTbl.ParkingMeterList.Count > 0)
                    {
                        NeedsUpdate = true;
                        string cmdTxt = "DELETE FROM ParkingMeters ";
                        SqlCeCommand cmd = new SqlCeCommand(cmdTxt, cn);
                        cmd.ExecuteNonQuery();

                        foreach (ParkingMeter prkMeter in parkMeterTbl.ParkingMeterList)
                        {
                            string cmdTxt2 = "Insert into ParkingMeters "
                                + "(Location, FacilityID)"
                                + "values (@location, @facilityid)";
                            SqlCeCommand cmd2 = new SqlCeCommand(cmdTxt2, cn);
                            cmd2.Parameters.AddWithValue("@location", prkMeter.Location);
                            cmd2.Parameters.AddWithValue("@facilityid", prkMeter.FacilityID);
                            cmd2.ExecuteNonQuery();
                        }
                    }

                    #endregion

                    //MessageBox.Show("Needs Update is " + NeedsUpdate.ToString());

                    if (NeedsUpdate)
                        UpdateLastUpdateDate();
                }
                catch (Exception ex)
                {
                    PopUpForm pf = new PopUpForm(ex.ToString());
                    pf.ShowDialog();
                }
                finally
                {
                    cn.Close();
                }
            }
        }
    }
}
