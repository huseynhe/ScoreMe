using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.DAL.Objects
{
    [Serializable]
    public class UserProfileSessionData
    {
        public Int64 UserId { get; set; }
        public Int64 EmployeeID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
