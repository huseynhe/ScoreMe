using ScoreMe.DAL.DTO;
using ScoreMe.DAL.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScoreMe.UI.Models
{
    public class UserPhoneVM
    {
        public Search Search;
        public PagedList.IPagedList<int> Paging { get; set; }
        public int ListCount { get; set; }
        public IList<UserPhoneDTO> RUserPhoneList { get; set; }
        public Int64 ID { get; set; }
        [Display(Name = "İstifadəçi ID")]
        public Int64 UserID { get; set; }
        [Display(Name = "İstifadəçi adı")]
        [Required(ErrorMessage = "Zəhmət olmazsa adı daxil edin")]
        public string UserName { get; set; }
        [Display(Name = "Müştərinin adı")]
        public string CustomerName { get; set; }
        [Display(Name = "Müştərinin soyadı")]
        public string CustomerSurname { get; set; }
        [Display(Name = "Firma adı")]
        public string CompanyName { get; set; }
        [Display(Name = "Model adı")]
        public string ModelName { get; set; }
        [Display(Name = "Model Nömrəsi")]
        public string ModelNumber { get; set; }
        [Display(Name = "Seriya Nömrəsi")]
        public string SerialNumber { get; set; }
        [Display(Name = "IMEI-1")]
        public string IMEI1 { get; set; }
        [Display(Name = "IMEI-1")]
        public string IMEI2 { get; set; }
        [Display(Name = "OS adı")]
        public string OSName { get; set; }
        [Display(Name = "OS version")]
        public string OSVersion { get; set; }
        [Display(Name = "Daxil olma tarixi")]
        public DateTime? InsertDate { get; set; }
    }
}