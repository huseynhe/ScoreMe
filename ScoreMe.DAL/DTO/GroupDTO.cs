using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.DAL.DTO
{
    public class GroupDTO
    {
        
        public Int64 ID { get; set; }
        public int GroupType { get; set; }
        public string GroupTypeDesc { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal StartLimit { get; set; }
        public decimal EndLimit { get; set; }
    }
}
