using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.DAL.DTO
{
    public class ProposalDTO
    {
        public Int64 ProposalID { get; set; }
        public string ProposalName { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public Int64 ProviderID { get; set; }
        public string ProviderName { get; set; }
        public Int64 OwnerUserID { get; set; }     
        public bool IsPublic { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Int64 ProviderType { get; set; }
        public string ProviderTypeCode { get; set; }
        public Int64 UserID { get; set; }

    }
}
