using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.DAL.Model
{
   public class ProposalDetail
    {
        public Int64 ID { get; set; }
        public Int64 ProposalID { get; set; }
        public string ProposolKey { get; set; }
        public string ProposolValue { get; set; }
 
    }
}
