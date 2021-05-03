using ScoreMe.DAL.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.DAL.Model
{
    public class Proposal
    {

        public Int64 ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public Int64 ProviderID { get; set; }
        public Int64 UserID { get; set; }
        public string ProviderName { get; set; }
        public bool IsPublic { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Int64 ProviderType { get; set; }
        public string ProviderTypeCode { get; set; }
        public List<ProposalDetail> ProposalDetails  { get; set; }
        public List<ProposalUserGroup> ProposalUserGroups { get; set; }
        public ProposalUserState ProposalUserState { get; set; }
        public List<ProposalUserState> ProposalUserStateList { get; set; }
        public List<Int64> ProposalDocumentIds { get; set; }
        public bool IsLike  { get; set; } = false;
        public bool IsDislike { get; set; } = false;
        public bool IsFavorite { get; set; } = false;
    }
}
