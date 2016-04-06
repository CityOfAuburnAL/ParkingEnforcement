using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ParkingApp_Tablet
{
    static class Ticket
    {
        public static string ChargeCode { get; set; }
        public static DateTime DateIssued { get; set; }
        public static double Latitude { get; set; }
        public static double Longitude { get; set; }
        public static string MachineID { get; set; }
        public static string MeterNumber { get; set; }
        public static string Notes { get; set; }
        public static string OfficerBadge { get; set; }
        public static int PrintCount { get; set; }
        public static string ProfileID { get; set; }
        public static string StatusCode { get; set; }
        public static string TagNumber { get; set; }
        public static string TagState { get; set; }
        public static int TicketNumber { get; set; }
        public static string VehicleTagCategory { get; set; }
        public static string VIN { get; set; }
        public static string ViolationLocation { get; set; }

        public static bool IsOffline { get; set; }
        public static bool PrintError { get; set; }
    }

    static class CurrentCharge
    {
        public static string Charge { get; set; }
        public static string ChargeCode { get; set; }
        public static string LocalOrd { get; set; }
        public static decimal ViolationAmount { get; set; }
    }
}
