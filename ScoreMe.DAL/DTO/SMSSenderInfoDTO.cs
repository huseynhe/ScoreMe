using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.DAL.DTO
{
    public class SMSSenderInfoDTO
    {
        public Int64 ID { get; set; }
        public int ActivityType { get; set; }
        public string ActivityTypeDesc { get; set; }
        public string SenderName { get; set; }
        public string Number { get; set; }
        public decimal Price { get; set; }
        public decimal Point { get; set; }
        public decimal Cheque { get; set; }
        public int IsParse { get; set; }
        public string IsParseDesc { get; set; }
        public string Description { get; set; }
    }
}
