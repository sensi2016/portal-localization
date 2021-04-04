using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO
{
    public class ApiResultDto
    {
        // ReSharper disable once InconsistentNaming
        public List<string> keys { get; set; }
        // ReSharper disable once InconsistentNaming
        public List<List<string>> values { get; set; }
    }

    public class IraqHospitalAllBedDto
    {
        public DateTime ReportData { get; set; }
        public int AllBedsAvailable { get; set; }
        public int AllBedsUsage { get; set; }
        public int IcuBedsAvailable { get; set; }
        public int IcuBedsUsage { get; set; }
    }

    public class IraqHospitalIcuBedDto
    {
        public new List<decimal[]> BedsAvailable { get; set; }
        public new List<decimal[]> BedsRange { get; set; }
        public new List<decimal[]> BedsMean { get; set; }
    }

    public class IraqHospitalBedRangeDto
    {
        public string Data { get; set; }
        public decimal BedsUpper { get; set; }
        public decimal BedsLower { get; set; }
    }

    public class IraqHospitalBedDateDto
    {
        public string Data { get; set; }
        public decimal Beds { get; set; }
    }
}
