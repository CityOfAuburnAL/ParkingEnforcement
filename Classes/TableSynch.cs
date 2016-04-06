using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ParkingApp_Tablet
{
    class TableSynch
    {
    }

    public class ChargeRecord
    {
        public string Charge { get; set; }
        public string LocalOrd { get; set; }
        public string ChargeCode { get; set; }
        public decimal ViolationAmount { get; set; }
    }

    public class ChargeTable
    {
        public List<ChargeRecord> ChargeList { get; set; }
    }

    public class EmployeeRecord
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string UserName { get; set; }
        public string API { get; set; }
        public int Status { get; set; }
    }

    public class EmployeeTable
    {
        public List<EmployeeRecord> EmployeeList { get; set; }
    }

    public class MeterFreeDateRecord
    {
        public string Date { get; set; }
    }

    public class MeterFreeDateTable
    {
        public List<MeterFreeDateRecord> MeterFreeDateList { get; set; }
    }

    public class OfficerRecord
    {
        public string OfficerBadge { get; set; }
        public string EmployeeID { get; set; }
        public string Rank { get; set; }
    }

    public class OfficerTable
    {
        public List<OfficerRecord> OfficerList { get; set; }
    }

    public class TagCategoryRecord
    {
        public string TagCategory { get; set; }
        public string Description { get; set; }
    }

    public class TagCategoryTable
    {
        public List<TagCategoryRecord> TagCategoryList { get; set; }
    }

    public class ParkingMeter
    {
        public string Location { get; set; }
        public string FacilityID { get; set; }
    }

    public class ParkingMeterTable
    {
        public List<ParkingMeter> ParkingMeterList { get; set; }
    }
}
