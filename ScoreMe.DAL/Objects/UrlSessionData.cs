using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.DAL.Objects
{
    [Serializable]
    public class UrlSessionData
    {
        public string Controller { get; set; }
        public string Action { get; set; }
    }
}
