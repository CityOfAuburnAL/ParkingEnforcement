using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;
using ParkingApp_Tablet;

namespace GetOnTabletTest
{
    public class Program
    {
        public CancelForm cancelForm;
        public LocationForm locationForm;
        public LoginForm loginForm;
        public OffenceForm offenceForm;
        public OrdenanceForm ordenanceForm;
        public VehicleForm vehicleForm;

        public LoadForm lForm;

        public Program()
        {
            cancelForm = new CancelForm(this) { FormBorderStyle = FormBorderStyle.None, WindowState = FormWindowState.Maximized };
            locationForm = new LocationForm(this) { FormBorderStyle = FormBorderStyle.None, WindowState = FormWindowState.Maximized };
            loginForm = new LoginForm(this) { FormBorderStyle = FormBorderStyle.None, WindowState = FormWindowState.Maximized };
            offenceForm = new OffenceForm(this) { FormBorderStyle = FormBorderStyle.None, WindowState = FormWindowState.Maximized };
            ordenanceForm = new OrdenanceForm(this) { FormBorderStyle = FormBorderStyle.None, WindowState = FormWindowState.Maximized };
            vehicleForm = new VehicleForm(this) { FormBorderStyle = FormBorderStyle.None, WindowState = FormWindowState.Maximized };

            lForm = new LoadForm();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Process.Start(@"C:\Program Files\Common Files\Microsoft Shared\ink\TabTip.exe");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new Form1());
        }
    }
}
