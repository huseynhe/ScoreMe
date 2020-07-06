using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.Business.Model
{
    public class Customer
    {

        public Int64 UserID { get; set; }
        public Int64 CustomerID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FatherName { get; set; }
        public string IdentityCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public long RegionId { get; set; }
        public string Address { get; set; }
    }
}
