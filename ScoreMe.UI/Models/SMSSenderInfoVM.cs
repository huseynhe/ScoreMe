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
    public class SMSSenderInfoVM
    {
        
        public Search Search;
        public PagedList.IPagedList<int> Paging { get; set; }
        public int ListCount { get; set; }
        public IList<SMSSenderInfoDTO> RSMSSenderInfoList { get; set; }

        [Display(Name = "Fəaliyyət növü")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select correct activity type")]
        public int ActivityTypeEVID { get; set; }
        public string ActivityTypeDesc { get; set; }
        public IEnumerable<SelectListItem> ActivityTypeList { get; set; }

        public Int64 ID { get; set; }

        [Required(ErrorMessage = "Please enter a sender name")]
        public string SenderName { get; set; }
        public string Number { get; set; }

        [Display(Name = "Qiymət")]
        public decimal? Price { get; set; }
        [Display(Name = "Bal")]
        public decimal? Point { get; set; }
        [Display(Name = "Çek")]
        public decimal? Cheque { get; set; }
        [Display(Name = "Açıqlama")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Parse mümkünmü")]
        public int IsParse { get; set; }
        public string IsParseDesc { get; set; }
        public IEnumerable<SelectListItem> IsParseList { get; set; }

    }
}