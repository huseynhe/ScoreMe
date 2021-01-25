using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.DAL.DTO
{
    public class OTPResult
    {
        public Int64 TimeStepMatched { get; set; }
        public int RemainingSecond { get; set; }
        public bool Verify { get; set; }
    }
}
