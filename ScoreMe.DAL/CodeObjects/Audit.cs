using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.DAL.CodeObjects
{
    [DataContract]
    public class Audit
    {
        public Nullable<long> Status { get; set; }
        public Nullable<long> LastUpdatedStatus { get; set; }
        public string createdUser { get; set; }
        public Nullable<long> createdDate { get; set; }
        public string updatedUser { get; set; }
        public Nullable<long> updatedDate { get; set; }
    }
}
