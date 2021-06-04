using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.DAL.Model
{
   public class ProposalUserGroupModel
    {
        public Int64 ProposalID { get; set; }
        public List<Int64> GroupIDs { get; set; }
    }
}
