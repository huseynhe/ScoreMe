using ScoreMe.DAL.DTO;
using ScoreMe.DAL.Objects;
using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScoreMe.UI.Models
{
    public class UserVM
    {
        public Search Search;
        public PagedList.IPagedList<int> Paging { get; set; }
        public IList<UserDTO> RUserList { get; set; }
        public int listCount { get; set; }

        public Int64 Id { get; set; }

        [Required(ErrorMessage = "Zəhmət olmasa istifadəçi adını daxil edin")]
        [Display(Name = "İstifadəçi adı")]
        [StringLength(20, MinimumLength = 1)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Zəhmət olmasa parolu daxil edin")]
        [Display(Name = "Parol")]
        [StringLength(20, MinimumLength = 1)]
        public string Password { get; set; }

        [Display(Name = "İstifadəçi tipi")]
        public Int64 UserTypeEvID { get; set; }
        public string UserTypeDesc { get; set; }
        public IEnumerable<SelectListItem> UserTypeList { get; set; }

        [Display(Name = "Kilid tipi")]
        public int? LockType { get; set; }
        public IEnumerable<SelectListItem> LockTypes { get; set; }

        [Display(Name = "Sonuncu giriş")]
        public DateTime? LastLoginDate { get; set; }

        [Display(Name = "Sonuncu cəhd")]
        public DateTime? LastAccessedDate { get; set; }

        [Display(Name = "Uğursuz cəhdlərin sayı")]
        public int? LoginFailedCount { get; set; }

        [Display(Name = "IP ünvanı")]
        public string LoginIPAddress { get; set; }


    }
}