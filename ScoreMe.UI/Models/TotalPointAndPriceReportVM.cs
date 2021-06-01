﻿using ScoreMe.DAL.DTO;
using ScoreMe.DAL.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScoreMe.UI.Models
{
    public class TotalPointAndPriceReportVM
    {
        public Search Search;
        public PagedList.IPagedList<int> Paging { get; set; }
        public int ListCount { get; set; }
        public IList<TotalPointAndPriceDTO> RTotalPointAndPriceReportList { get; set; }

        public int UserID { get; set; }
     
        public int ReportType { get; set; }
        [Display(Name = "Rapor növü")]
        public string ReportTypeDesc { get; set; }
        public IEnumerable<SelectListItem> ReportTypeList { get; set; }
        [Display(Name = "Istifadəçi adı")]
        public string UserName { get; set; }

        [Display(Name = "Müştəro adı")]
        public string CustomerName { get; set; }
        [Display(Name = "Müştəro soyadı")]
        public string CustomerSurname { get; set; }

        [Display(Name = "İl")]
        public int Year { get; set; }
        [Display(Name = "1")]
        public decimal January { get; set; }
        [Display(Name = "2")]
        public decimal February { get; set; }
        [Display(Name = "3")]
        public decimal March { get; set; }
        [Display(Name = "4")]
        public decimal April { get; set; }
        [Display(Name = "5")]
        public decimal May { get; set; }
        [Display(Name = "6")]
        public decimal June { get; set; }
        [Display(Name = "7")]
        public decimal July { get; set; }
        [Display(Name = "8")]
        public decimal August { get; set; }
        [Display(Name = "9")]
        public decimal September { get; set; }
        [Display(Name = "10")]
        public decimal October { get; set; }
        [Display(Name = "11")]
        public decimal November { get; set; }
        [Display(Name = "12")]
        public decimal December { get; set; }
        [Display(Name = "Orta aylıq")]
        public decimal Average { get; set; }
    }
}