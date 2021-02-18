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
        public Int64? TotalCallCount { get; set; }
        public Int64? OutCallCount { get; set; }
        public decimal? OutCallSecond { get; set; }
        public Int64? InCallCount { get; set; }
        public decimal? InCallSecond { get; set; }
        public Int64? MissedCallCount { get; set; }
        public Int64? OutCallForeignCount { get; set; }
        public decimal? OutCallForeignSecond { get; set; }
        public Int64? InCallForeignCount { get; set; }
        public decimal? InCallForeignSecond { get; set; }
        public Int64? OutCallRoamingCount { get; set; }
        public decimal? OutCallRoamingSecond { get; set; }
        public Int64? InCallRoamingCount { get; set; }
        public decimal? InCallRoamingSecond { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<tbl_CALLDetail> CALLDetails { get; set; }
    }
}
