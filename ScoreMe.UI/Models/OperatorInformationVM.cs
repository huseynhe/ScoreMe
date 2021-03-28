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
    public class OperatorInformationVM
    {
        public Search Search;
        public PagedList.IPagedList<int> Paging { get; set; }
        public int ListCount { get; set; }
        public IList<OperatorInformationDTO> ROperatorInformationList { get; set; }

        public Int64 ID { get; set; }
       
        [Display(Name = "Mobil operator")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select correct mobil operator")]
        public int OperatorTypeEVID { get; set; }
        public string OperatorTypeDesc { get; set; }
        public IEnumerable<SelectListItem> OperatorTypeList { get; set; }

        [Required]
        [Display(Name = "Prefix")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a Prefix")]
        public string Name { get; set; }
        public IEnumerable<SelectListItem> OperatorPrefixList { get; set; }

        [Required]
        [Display(Name = "Mobil operator kanalı")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a Operator Chanel Type")]
        public int OperatorChanelTypeEVID { get; set; }
        public string OperatorChanelTypeDesc { get; set; }
        public IEnumerable<SelectListItem> OperatorChanelTypeList { get; set; }

        [Required]
        [Display(Name = "Giriş/Çıxış tipi")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a IO Type")]
        public int InOutTypeEVID { get; set; }
        public string InOutTypeDesc { get; set; }
        public IEnumerable<SelectListItem> InOutTypeList { get; set; }

        [Display(Name = "Qiymət")]
        public decimal Price { get; set; }
        [Display(Name = "Bal")]
        public decimal Point { get; set; }

        public Int64? EnumCategoryID { get; set; }
    }
}