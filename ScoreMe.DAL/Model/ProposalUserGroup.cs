using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.DAL.Model
{
    public class ProposalUserGroup
    {
        public Int64 ID { get; set; }
        public Int64 ProposalID { get; set; }
        public Int64 GroupID { get; set; }
    }
}
