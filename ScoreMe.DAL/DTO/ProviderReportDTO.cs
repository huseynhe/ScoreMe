using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.DAL.DTO
{
    public class ProviderReportDTO
    {
        public Int64 ProviderID { get; set; }
        public string ProviderName { get; set; }
        public Int64 DeclaredProposalCount { get; set; }
        public Int64 AppliedProposalCount { get; set; }
        public Int64 AccteptedProposalCount { get; set; }
        public Int64 RejectedProposalCount { get; set; }
        public Int64 WaitingProposalCount { get; set; }
    }
}
