using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.DAL.DTO
{
    public class UserDTO
    {
        public Int64 UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Int64 UserType_EVID { get; set; }
        public string UserTypeDesc { get; set; }
        public int AccountLocked { get; set; }

    }
}
