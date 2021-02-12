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
        public Int64? OutMissedCallCount { get; set; }
        public decimal? OutCallMinute { get; set; }
        public Int64? InCallCount { get; set; }
        public Int64? InMissedCallCount { get; set; }
        public decimal? InCallMinute { get; set; }
        public Int64? OutCallForeignCount { get; set; }
        public decimal? OutCallForeignMinute { get; set; }
        public Int64? InCallForeignCount { get; set; }
        public decimal? InCallForeignMinute { get; set; }
        public Int64? OutCallRoamingCount { get; set; }
        public decimal? OutCallRoamingMinute { get; set; }
        public Int64? InCallRoamingCount { get; set; }
        public decimal? InCallRoamingMinute { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<tbl_CALLDetail> CALLDetails { get; set; }
    }
}
