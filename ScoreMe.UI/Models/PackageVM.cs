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
    public class PackageVM
    {
        public Search Search;
        public PagedList.IPagedList<int> Paging { get; set; }
        public int ListCount { get; set; }
        public IList<PackageDTO> RPackageList { get; set; }
        public IList<PackagePriceDTO> RPackagePriceList { get; set; }

        [Display(Name = "Paket ID")]
        public Int64 PackageID { get; set; }
        [Display(Name = "Mobil operator")]
        public Int64 Mobile_EVID { get; set; }
        public string Mobile_EVDesc { get; set; }
        [Display(Name = "Paket adı")]
        public string PackageName { get; set; }
        [Display(Name = "Paket ölçüsü")]
        public decimal PackageSize { get; set; }
        [Display(Name = "Keçərlilik müdəti")]
        public int Validity { get; set; }
        [Display(Name = "Keçərlilik tipi")]
        public string ValidityDesc { get; set; }
        public IEnumerable<SelectListItem> MobileEVList { get; set; }

        [Display(Name = "Paket Qiymət ID")]
        public Int64 PackagePriceID { get; set; }
        [Display(Name = "Mənbə")]
        public Int64 Source_EVID { get; set; }
        public string Source_EVDesc { get; set; }
        [Display(Name = "Başlanğıc tarixi")]
        public DateTime? BeginDate { get; set; }
        [Display(Name = "Bitiş tarixi")]
        public DateTime? EndDate { get; set; }
        [Display(Name = "Qiymət")]
        public decimal Price { get; set; }
        [Display(Name = "Bal")]
        public decimal Point { get; set; }
        public IEnumerable<SelectListItem> SourceEVList { get; set; }


    }
}