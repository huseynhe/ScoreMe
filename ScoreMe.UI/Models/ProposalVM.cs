using ScoreMe.DAL.DTO;
using ScoreMe.DAL.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScoreMe.UI.Models
{
    public class ProposalVM
    {
        public Search Search;
        public PagedList.IPagedList<int> Paging { get; set; }
        public IList<ProposalDTO> RProposalList { get; set; }
        public int ListCount { get; set; }

        public Int64 ProviderID { get; set; }
        [Display(Name = "ID")]
        public Int64 ID { get; set; }
        [Display(Name = "Adı")]
        public string Name { get; set; }
        [Display(Name = "Açıqlama")]
        public string Description { get; set; }
        [Display(Name = "Hər kəsə açıqmı")]
        public bool IsPublic { get; set; }
        [Display(Name = "Başlanğıc tarix")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Bitiş tarix")]
        public DateTime EndDate { get; set; }
    }
}