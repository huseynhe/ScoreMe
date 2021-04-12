using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.DAL.DTO
{
    public class UserPhoneDTO
    {
        public Int64 ID { get; set; }
        public Int64 UserID { get; set; }
        public string UserName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string CompanyName { get; set; }
        public string ModelName { get; set; }
        public string ModelNumber { get; set; }
        public string SerialNumber { get; set; }
        public string IMEI1 { get; set; }
        public string IMEI2 { get; set; }
        public string OSName { get; set; }
        public string OSVersion { get; set; }
        public DateTime? InsertDate { get; set; }
    }
}
