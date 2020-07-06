using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.DAL.Model
{
   public class UserInfo
    {
        public Int64 UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Newpassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
