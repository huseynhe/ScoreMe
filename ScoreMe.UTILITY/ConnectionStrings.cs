using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.UTILITY
{
    public class ConnectionStrings
    {
        public static String ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["ScoreMeConnectionString"].ConnectionString;
            }
        }

        public static String ConnectionString_Log
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["SCUPRA_LOGConnectionString"].ConnectionString;
            }
        }
    }
}
