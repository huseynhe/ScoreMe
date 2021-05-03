using ScoreMe.DAL.DBModel;
using ScoreMe.DAL.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScoreMe.UI.Models
{
    public class AppInformationVM
    {
        public Search Search;
        public PagedList.IPagedList<int> Paging { get; set; }
        public int ListCount { get; set; }
        public IList<tbl_AppGroupInformation> RAppInformationList { get; set; }

        public Int64 ID { get; set; }

        [Display(Name = "Kateqori Type")]
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "Please select correct category id")]
        public int CategoryType { get; set; }
        [Required(ErrorMessage = "Please select correct category name")]
        [Display(Name = "Kateqori adı")]
        public string CategoryName { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }


        [Display(Name = "Tükətim Qiyməti")]
        public decimal PriceUsage { get; set; }
        [Display(Name = "Tükətim Balı")]
        public decimal PointUsage { get; set; }

        [Display(Name = "Sayın Qiyməti")]
        public decimal PriceCount { get; set; }
        [Display(Name = "Sayın Balı")]
        public decimal PointCount { get; set; }

    }
}