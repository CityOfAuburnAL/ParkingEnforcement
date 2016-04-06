using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ParkingApp_Tablet
{
    static class Vehicle
    {
        public static string VIN { get; set; }
        public static string Description { get; set; }        
        public static string TagNumber { get; set; }
        public static string TagState { get; set; }
        public static bool TowEligible { get; set; }

        public static string MeterViolationCharge { get; set; }
        public static string MeterViolationChargeCode { get; set; }
        public static string MeterViolationAmount { get; set; }
        public static string LastMeterViolationCharge { get; set; }
        public static string LastMeterViolationChargeCode { get; set; }

        public static string TwoHourViolationCharge { get; set; }
        public static string TwoHourViolationChargeCode { get; set; }
        public static string TwoHourViolationAmount { get; set; }
        public static string LastTwoHourViolationCharge { get; set; }
        public static string LastTwoHourViolationChargeCode { get; set; }

        //public static bool MeterViolationEligible { get; set; }
        //public static bool twoHourViolationEligible { get; set; }
    }
}
