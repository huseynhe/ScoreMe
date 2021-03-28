using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.DAL.DTO
{
    public class SMSReportShortDTO
    {
       
        public string SenderName { get; set; }
        public int IsExpense { get; set; }
        public string IsExpenseDesc { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CardNumber { get; set; }
        public DateTime? OperationDate { get; set; }
        public string MerchantName { get; set; }
        public decimal Balance { get; set; }
        public string BalanceCurrency { get; set; }
    }
}
