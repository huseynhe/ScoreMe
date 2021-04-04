using ScoreMe.DAL.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.DAL.Model
{
    public class AppConsumeModel
    {
        public Int64 ID { get; set; }
        public Int64 UserID { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<tbl_AppConsumeDetail> AppConsumeDetails { get; set; }
    }
}
