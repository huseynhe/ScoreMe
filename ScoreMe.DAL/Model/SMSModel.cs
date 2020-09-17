using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.DAL.Model
{
    public class SMSModel
    {
        public string SenderName { get; set; }
        public string SenderPhoneNumber { get; set; }
        public string RecievedDate { get; set; }
        public string SendDate { get; set; }
        public string Message { get; set; }


    }
}
