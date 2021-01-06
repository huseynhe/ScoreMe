using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.DAL.DTO
{
    public class EnumDTO
    {
        public Int64 EnumCategoryID { get; set; }
        public string EnumCategoryCode { get; set; }
        public string EnumCategoryName { get; set; }
        public string EnumCategoryDesc { get; set; }

        public Int64 EnumValueID { get; set; }
        public string EnumValueCode { get; set; }
        public string EnumValueName { get; set; }
        public string EnumValueDesc { get; set; }
    }
}
