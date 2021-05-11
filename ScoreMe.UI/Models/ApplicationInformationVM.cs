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
    public class ApplicationInformationVM
    {
        public Search Search;
        public PagedList.IPagedList<int> Paging { get; set; }
        public int ListCount { get; set; }
        public IList<tbl_ApplicationInformation> RApplicationInformationList { get; set; }

        public Int64 ID { get; set; }

        [Display(Name = "Platforma")]
        [Required(ErrorMessage = "Zəhmət olmazsa platforma adını giriniz")]
        public string Platform { get; set; }

        [Display(Name = "Qurup adı")]
        [Required(ErrorMessage = "Zəhmət olmazsa grup  adını giriniz")]
        public string GroupName { get; set; }


        [Display(Name = "Tətbiqin adı")]
        [Required(ErrorMessage = "Zəhmət olmazsa tətbiq adını giriniz")]
        public string AppName { get; set; }

        [Display(Name = "Tətbiqin qısa adı")]
        [Required(ErrorMessage = "Zəhmət olmazsa qısa adını giriniz")]
        public string ShortName { get; set; }

        [Display(Name = "Yazar")]
        public string Author { get; set; }

        [Display(Name = "Qiymət")]
        public decimal? Price { get; set; }

        [Display(Name = "Bal")]
        public decimal? Point { get; set; }

        [Display(Name = "İnternet istifadəsi")]
        public string NetUsage { get; set; }
    }
}