using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoreMe.API.Models
{
    public class UserInfo
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
    }
}