using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.UTILITY
{
  public  class WebServiceUtil
    {
        public static String SMSUserName
        {
            get
            {
                string SMSUserName = ConfigurationManager.AppSettings["SMSUserName"];
                return SMSUserName;
            }
        }
        public static String SMSPassword
        {
            get
            {
                string SMSPassword = ConfigurationManager.AppSettings["SMSPassword"];
                return SMSPassword;
            }
        }
    }
}
