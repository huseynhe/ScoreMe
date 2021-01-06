
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
    public class PersonVM
    {
        public Search Search;
        public PagedList.IPagedList<int> Paging { get; set; }
        public int ListCount { get; set; }
        public IList<PersonDTO> RPersonList { get; set; }

        [Display(Name = "Şəxs")]
        public int ID { get; set; }

        public bool IsPrint { get; set; }

        [Display(Name = "FIN")]
        public string PIN { get; set; }

        [Display(Name = "Adı")]
        [Required(ErrorMessage = "Zəhmət olmazsa adı daxil edin")]
        public string FirstName { get; set; }

        [Display(Name = "Soyadı")]
        [Required(ErrorMessage = "Zəhmət olmazsa soyadı daxil edin")]
        public string LastName { get; set; }

        [Display(Name = "Ata adı")]
        [Required(ErrorMessage = "Zəhmət olmazsa ata adını daxil edin")]
        public string FatherName { get; set; }

        [Display(Name = "Cinsi")]
        [Required(ErrorMessage = "Zəhmət olmazsa cinsini seçin")]
        public int GenderType { get; set; }

        [Display(Name = "Açıqlama")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string GenderTypeDesc { get; set; }
        public IEnumerable<SelectListItem> GenderTypeList { get; set; }

        //[Display(Name = "Region")]
        //[Required(ErrorMessage = "Zəhmət olmazsa region seçin")]
        //public Int32 RegionID { get; set; }
        //public string RegionDesc { get; set; }
        //public List<tbl_Region> Regions { get; set; }
        //public IList<tbl_Region>[] RegionListArray { get; set; }
        //public List<tbl_Region> RegionList { get; set; }

        [Display(Name = "Ünvan")]
        public string Address { get; set; }

        [Display(Name = "Prfoil şəkili")]
        public string Photo { get; set; }

        [Display(Name = "Ölkə adı")]
        [Required(ErrorMessage = "Zəhmət olmazsa sənəd növünü seçin")]
        public Int32 CountryID { get; set; }
        public string CountryName { get; set; }
        public IEnumerable<SelectListItem> CountryList { get; set; }

        [Display(Name = "İş ünvan")]
        public string WorkAddress { get; set; }

        [Display(Name = "Xüsusi rabitə")]
        public string SpecialCommunication { get; set; }
        [Display(Name = "Şəhər telefonu")]
        public string CityPhone { get; set; }
        [Display(Name = "Daxili nömrə")]
        public string InternalPhone { get; set; }
        [Display(Name = "Ev telefonu")]
        public string HomePhone { get; set; }
        [Display(Name = "Kabinet N-si")]
        public string RoomNumber { get; set; }
        [Display(Name = "E-poçt")]
        public string Email { get; set; }

     

    }
}