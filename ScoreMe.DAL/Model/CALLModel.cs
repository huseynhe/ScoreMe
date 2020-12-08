using ScoreMe.DAL.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.DAL.Model
{
    public class CALLModel
    {
        public Int64 ID { get; set; }
        public Int64 UserID { get; set; }
        public int? TotalCallCount { get; set; }
        public List<tbl_CALLDetail> CALLDetails { get; set; }
    }
}
