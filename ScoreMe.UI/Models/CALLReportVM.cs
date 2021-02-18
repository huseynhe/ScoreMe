using ScoreMe.DAL.DTO;
using ScoreMe.DAL.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScoreMe.UI.Models
{
    public class CALLReportVM
    {
        public Search Search;
        public PagedList.IPagedList<int> Paging { get; set; }
        public int ListCount { get; set; }
        public IList<CALLReportDTO> RCALLReportList { get; set; }

        public int UserID { get; set; }
        public string userIDName { get; set; }
        [Display(Name = "Istifadəçi adı")]
        public string UserName { get; set; }
        public IEnumerable<SelectListItem> UserList { get; set; }

        [Display(Name = "Ad")]
        public string Name { get; set; }
        [Display(Name = "İl")]
        public int Year { get; set; }
        [Display(Name = "1")]
        public int January { get; set; }
        [Display(Name = "2")]
        public int February { get; set; }
        [Display(Name = "3")]
        public int March { get; set; }
        [Display(Name = "4")]
        public int April { get; set; }
        [Display(Name = "5")]
        public int May { get; set; }
        [Display(Name = "6")]
        public int June { get; set; }
        [Display(Name = "7")]
        public int July { get; set; }
        [Display(Name = "8")]
        public int August { get; set; }
        [Display(Name = "9")]
        public int September { get; set; }
        [Display(Name = "10")]
        public int October { get; set; }
        [Display(Name = "11")]
        public int November { get; set; }
        [Display(Name = "12")]
        public int December { get; set; }
        [Display(Name = "Orta aylıq")]
        public decimal Average { get; set; }
    }
}