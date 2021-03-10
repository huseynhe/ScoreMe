using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.DAL.DTO
{
    public class OperatorInformationDTO
    {
        public Int64 ID { get; set; }
        public int OperatorTypeEVID { get; set; }
        public string OperatorTypeDesc { get; set; }
        public string Name { get; set; }
        public int OperatorChanelTypeEVID { get; set; }
        public string OperatorChanelTypeDesc { get; set; }
        public int InOutTypeEVID { get; set; }
        public string InOutTypeDesc { get; set; }
        public decimal Price { get; set; }
        public decimal Point { get; set; }
    }
}
