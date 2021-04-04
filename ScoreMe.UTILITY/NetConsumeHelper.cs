using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.UTILITY
{
    public static class NetConsumeHelper
    {
        static IDictionary<string, int> operatorNames = new Dictionary<string, int>();
        static NetConsumeHelper()
        {
            operatorNames.Add("AZERCELL | Azercell", 1);
            operatorNames.Add("BAKCELL", 3);
        }
        public static int GetOperatorValueByKey(string key)
        {
            try
            {
                int value = operatorNames[key];
                return value;
            }
            catch (Exception ex)
            {

                return 0;
            }


        }


    }
}
