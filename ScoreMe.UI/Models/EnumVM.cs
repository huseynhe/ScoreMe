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
    public class EnumVM
    {
        public Search Search;
        public PagedList.IPagedList<int> Paging { get; set; }
        public int ListCount { get; set; }
        public IList<EnumDTO> REnumCategoryList { get; set; }
        public IList<EnumDTO> REnumValueList { get; set; }

        [Display(Name = "Enum kategori")]
        public Int64 EnumCategoryID { get; set; }
        [Display(Name = "Enum kategori kodu")]
        public string EnumCategoryCode { get; set; }
        [Display(Name = "Enum kategori adı")]
        public string EnumCategoryName { get; set; }
        [Display(Name = "Enum kategori açıqlama")]
        public string EnumCategoryDesc { get; set; }

        public Int64 EnumValueID { get; set; }
        [Display(Name = "Enum value kodu")]
        public string EnumValueCode { get; set; }
        [Display(Name = "Enum value adı")]
        public string EnumValueName { get; set; }
        [Display(Name = "Enum value açıqlama")]
        public string EnumValueDesc { get; set; }
        public IEnumerable<SelectListItem> EnumCategoryList { get; set; }
    }
}