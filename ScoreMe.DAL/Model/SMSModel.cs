using ScoreMe.DAL.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.DAL.Model
{
    public class SMSModel
    {
        public Int64 ID { get; set; }
        public Int64 UserID { get; set; }
        public Int64? TotalMessageCount { get; set; }
        public Int64? ShortMessageCount { get; set; }
        public Int64? OutMessageCount { get; set; }
        public Int64? InMessageCount { get; set; }
        public Int64? OutMessageForeignCount { get; set; }
        public Int64? InMessageForeigCount { get; set; }
        public Int64? OutMessageRoamingCount { get; set; }
        public Int64? InMessageRoamingCount { get; set; }
        public List<tbl_SMSDetail> SMSDetails { get; set; }



    }
}
