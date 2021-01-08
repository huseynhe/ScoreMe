using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.DAL.DTO
{
    public class PackageDTO
    {
        public Int64 PackageID { get; set; }
        public Int64 Mobile_EVID { get; set; }
        public string Mobile_EVDesc { get; set; }
        public string PackageName { get; set; }
        public decimal PackageSize { get; set; }
        public int Validity { get; set; }
        public string ValidityDesc { get; set; }
    }
}
