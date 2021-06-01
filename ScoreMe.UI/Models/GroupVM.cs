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
    public class GroupVM
    {
        public Search Search;
        public PagedList.IPagedList<int> Paging { get; set; }
        public int ListCount { get; set; }
        public IList<GroupDTO> RGroupList { get; set; }

        [Display(Name = "Group ID")]
        public Int64 GroupID { get; set; }

        [Display(Name = "Group tip kod")]
        public int GroupType { get; set; }
        [Display(Name = "Group tip adı")]
        public string GroupTypeDesc { get; set; }

        public IEnumerable<SelectListItem> GroupTypeList { get; set; }

        [Display(Name = "Group adı")]
        public string GroupName { get; set; }

        [Display(Name = "Açıqlama")]
        public string Description { get; set; }

        [Display(Name = "Başlanğıc limit")]
        public decimal StartLimit { get; set; }

        [Display(Name = "Son limit")]
        public decimal EndLimit { get; set; }

    }
}