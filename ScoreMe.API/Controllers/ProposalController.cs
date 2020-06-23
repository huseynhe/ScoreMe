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
        public async Task<IHttpActionResult> AddCustomer(tbl_Proposal item)
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
        public async Task<IHttpActionResult> UpdateProposal(tbl_Proposal item)
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
        public async Task<IHttpActionResult> DeleteProposal(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();

            var dbitem = operation.DeleteProposal(id, 0);
            return Ok(dbitem);

        }
    }
}
