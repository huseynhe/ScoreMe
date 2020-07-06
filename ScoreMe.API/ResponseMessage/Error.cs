using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoreMe.API.ResponseMessage
{
    public class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}