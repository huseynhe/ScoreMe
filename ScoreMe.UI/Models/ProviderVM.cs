using ScoreMe.DAL.Model;
using ScoreMe.DAL.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScoreMe.UI.Models
{
    public class ProviderVM
    {
        public Search Search;
        public PagedList.IPagedList<int> Paging { get; set; }
        public IList<Provider> RProviderList { get; set; }
        public int ListCount { get; set; }
        [Display(Name = "ID")]
        public Int64 ID { get; set; }
        [Display(Name = "Adı")]
        public string Name { get; set; }
        [Display(Name = "Açıqlama")]
        public string Description { get; set; }
        [Display(Name = "Üst adı")]
        public string ParentName { get; set; }
        [Display(Name = "İstifadəçi ID")]
        public Int64 UserID { get; set; }
        [Display(Name = "İstifadəçi adı")]
        public string UserName { get; set; }
        [Display(Name = "Növü")]
        public string ProvuderTypeDesc { get; set; }
        [Display(Name = "Əlaqəli Ş.A")]
        public string RelatedPersonName { get; set; }
        [Display(Name = "Əlaqəli Ş.T.N")]
        public string RelatedPersonPhone { get; set; }

        [Display(Name = "Əlaqəli Ş.M")]
        public string RelatedPersonProfession { get; set; }
        [Display(Name = "VOEN")]
        public string VOEN { get; set; }

    }
}