using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.DAL.Objects
{
    public class Search
    {
        public int? page { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }

        public Int64 Id { get; set; }
        public Int64 ProviderID{ get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool isCount { get; set; }


        public string ControllerName { get; set; }
        public string ActionName { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }

        public DateTime? FromtDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int Year { get; set; }
        public string Months { get; set; }
    }
}
