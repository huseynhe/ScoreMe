using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.DAL.Util
{
    public class SMSUtil
    {
        public static decimal OutSamePrice = 0.02M;
        public static decimal OutSamePoint = 0.01M;
        public static decimal OutOtherPrice = 0.02M;
        public static decimal OutOtherPoint = 0.01M;
        public static decimal OutForeignPrice = 0.02M;
        public static decimal OutForeignPoint = 0.01M;
        public static decimal INPrice = 0;
        public static decimal INPoint = 0.01M;
        public static decimal INForeignPrice = 0;
        public static decimal INForeignPoint = 0.01M;
        public static decimal OutRoamingPrice { get; set; }
        public static decimal OutRoamingPoint { get; set; }
        public static decimal INRoamingPrice { get; set; }
        public static decimal INRoamingPoint { get; set; }
    }
}
