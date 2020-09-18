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
        public int? TotalMessageCount { get; set; }
        public int? ShortMessageCount { get; set; }
        public List<tbl_SMSDetail> SMSDetails { get; set; }



    }
}
