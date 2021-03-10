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
        public int OperatorTypeEVID { get; set; }
        public string OperatorTypeDesc { get; set; }
        public IEnumerable<SelectListItem> OperatorTypeList { get; set; }
        [Display(Name = "Prefix")]
        public string Name { get; set; }
        public IEnumerable<SelectListItem> OperatorPrefixList { get; set; }
        [Display(Name = "Mobil operator kanalı")]
        public int OperatorChanelTypeEVID { get; set; }
        public string OperatorChanelTypeDesc { get; set; }
        public IEnumerable<SelectListItem> OperatorChanelTypeList { get; set; }
        [Display(Name = "Giriş/Çıxış tipi")]
        public int InOutTypeEVID { get; set; }
        public string InOutTypeDesc { get; set; }
        public IEnumerable<SelectListItem> InOutTypeList { get; set; }
        [Display(Name = "Qiymət")]
        public decimal Price { get; set; }
        [Display(Name = "Bal")]
        public decimal Point { get; set; }
    }
}