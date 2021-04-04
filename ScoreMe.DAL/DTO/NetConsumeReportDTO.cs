using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.DAL.DTO
{
    public class NetConsumeReportDTO
    {
        public Int64 ID { get; set; }
        public int INOUT_EVType { get; set; }
        public string SourceDesc { get; set; }
        public string OperatorName { get; set; }
        public int OperatorEVID { get; set; }
        public int Year { get; set; }
        public decimal? January { get; set; }
        public decimal? February { get; set; }
        public decimal? March { get; set; }
        public decimal? April { get; set; }
        public decimal? May { get; set; }
        public decimal? June { get; set; }
        public decimal? July { get; set; }
        public decimal? August { get; set; }
        public decimal? September { get; set; }
        public decimal? October { get; set; }
        public decimal? November { get; set; }
        public decimal? December { get; set; }
        public decimal? Average { get; set; }
        public decimal? AveragePrice { get; set; }
        public decimal? AveragePoint { get; set; }
    }
}
