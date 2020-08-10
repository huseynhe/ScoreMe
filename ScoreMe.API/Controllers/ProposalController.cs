using ScoreMe.DAL;
using ScoreMe.DAL.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace ScoreMe.API.Controllers
{
    [RoutePrefix("api/proposal")]
    public class ProposalController : ApiController
    {
        [HttpGet]
        [Route("GetProposals")]
        public List<tbl_Proposal> GetProposals()
        {
            CRUDOperation operation = new CRUDOperation();
            var proposals = operation.GetProposals(); ;
            return proposals;
        }

        [HttpGet]
        [Route("GetProposalByID/{id}")]
        public tbl_Proposal GetProposalByID(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();
            var proposal = operation.GetProposalById(id); ;
            return proposal;
        }

        [HttpPost]
        [ResponseType(typeof(tbl_Proposal))]
        [Route("AddProposal")]
        public IHttpActionResult AddProposal(tbl_Proposal item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CRUDOperation operation = new CRUDOperation();
            tbl_Proposal dbitem = operation.AddProposal(item);

            return Ok(dbitem);
        }

        [HttpPost]
        [ResponseType(typeof(tbl_Proposal))]
        [Route("UpdateProposal")]
        public IHttpActionResult UpdateProposal(tbl_Proposal item)
        {
            CRUDOperation operation = new CRUDOperation();
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                var dbitem = operation.UpdateProposal(item);
                return Ok(dbitem);
            }
        }

        [HttpPost]
        [ResponseType(typeof(tbl_Proposal))]
        [Route("DeleteProposal/{id}")]
        public IHttpActionResult DeleteProposal(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();

            var dbitem = operation.DeleteProposal(id, 0);
            return Ok(dbitem);

        }

        [HttpGet]
        [Route("GetProposalDetails")]
        public List<tbl_ProposalDetail> GetProposalDetails()
        {
            CRUDOperation operation = new CRUDOperation();
            var proposalDetails = operation.GetProposalDetails(); ;
            return proposalDetails;
        }

        [HttpGet]
        [Route("GetProposalDetailByID/{id}")]
        public tbl_ProposalDetail GetProposalDetailByID(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();
            var proposalDetail = operation.GetProposalDetailByID(id); ;
            return proposalDetail;
        }

        [HttpPost]
        [ResponseType(typeof(tbl_ProposalDetail))]
        [Route("AddProposalDetail")]
        public IHttpActionResult AddProposalDetail(tbl_ProposalDetail item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CRUDOperation operation = new CRUDOperation();
            tbl_ProposalDetail dbitem = operation.AddProposalDetail(item);

            return Ok(dbitem);
        }

        [HttpPost]
        [ResponseType(typeof(tbl_ProposalDetail))]
        [Route("UpdateProposalDetail")]
        public IHttpActionResult UpdateProposalDetail(tbl_ProposalDetail item)
        {
            CRUDOperation operation = new CRUDOperation();
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                var dbitem = operation.UpdateProposalDetail(item);
                return Ok(dbitem);
            }
        }

        [HttpPost]
        [ResponseType(typeof(tbl_ProposalDetail))]
        [Route("DeleteProposalDetail/{id}")]
        public IHttpActionResult DeleteProposalDetail(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();

            var dbitem = operation.DeleteProposalDetail(id, 0);
            return Ok(dbitem);

        }
    }
}
