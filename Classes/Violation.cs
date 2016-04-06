using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ParkingApp_Tablet
{
    public class Violation
    {
        public string Amount { get; set; }
        public string Charge { get; set; }
        public string Date { get; set; }
        public string Ticket { get; set; }
    }

    static class Violations
    {
        public static string TotalBalance { get; set; }
        public static List<Violation> ViolationList { get; set; }
    }
}
