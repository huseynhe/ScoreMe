using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.DAL.DTO
{
    public class SMSReportDTO
    {
        public Int64 ID { get; set; }
        public int INOUT_EVType { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public int January { get; set; }
        public int February { get; set; }
        public int March { get; set; }
        public int April { get; set; }
        public int May { get; set; }
        public int June { get; set; }
        public int July { get; set; }
        public int August { get; set; }
        public int September { get; set; }
        public int October { get; set; }
        public int November { get; set; }
        public int December { get; set; }
        public decimal Average { get; set; }
        public decimal? AveragePrice { get; set; }
    }
}
