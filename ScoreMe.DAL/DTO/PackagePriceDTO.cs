using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.DAL.DTO
{
    public class PackagePriceDTO
    {
        public Int64 PackageID { get; set; }
        public Int64 PackagePriceID { get; set; }
        public Int64 Source_EVID { get; set; }
        public string Source_EVDesc { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Price { get; set; }
        public decimal Point { get; set; }

    }
}
