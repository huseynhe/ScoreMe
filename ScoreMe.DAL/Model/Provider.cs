using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.DAL.Model
{
    public class Provider
    {
        public Int64 UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Int64 UserType_EVID { get; set; }
        public long ProviderID { get; set; }
        public string Name { get; set; }
        public long ParentID { get; set; }
        public long Type { get; set; }
        public string Description { get; set; }
        public long RegionId { get; set; }
        public string Address { get; set; }
        public string RelatedPersonName { get; set; }
        public string RelatedPersonProfession { get; set; }
        public string RelatedPersonPhone { get; set; }
        public string RP_HomePhone { get; set; }
        public string VOEN { get; set; }
    }
}
