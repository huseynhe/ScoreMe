using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.DAL.Model
{
    public class ProposalUserState
    {
        
        public Int64 ID { get; set; }
        public Int64 ProposalID { get; set; }
        public Int64 UserID { get; set; }
        public decimal ProviderOfferAmount { get; set; }
        public decimal UserDemandAmount { get; set; }
        public Int64 ProviderStateType { get; set; }
        public string ProviderStateTypeDesc { get; set; }
        public Int64 UserStateType { get; set; }
        public string UserStateTypeDesc { get; set; }
        public string CustomerFullName { get; set; }
        public int ProviderOfferMonth { get; set; }
        public int UserDemandMonth { get; set; }
    }
}
