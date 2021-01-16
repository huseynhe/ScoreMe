using ScoreMe.DAL;
using ScoreMe.DAL.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ScoreMe.API.Controllers
{
    public class ProposalCommissionController : ApiController
    {
        [HttpGet]
        [Route("GetProposalCommissions")]
        public List<tbl_ProposalCommission> GetProposalCommissions()
        {
            CRUDOperation operation = new CRUDOperation();
            var items = operation.GetProposalCommissions(); ;
            return items;
        }

        [HttpGet]
        [Route("GetProposalCommissionByID/{id}")]
        public tbl_ProposalCommission GetProposalCommissionByID(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();
            var item = operation.GetProposalCommissionById(id); 
            return item;
        }
        [HttpGet]
        [Route("GetProposalUserSaveByProposalID/{proposalID}")]
        public tbl_ProposalCommission GetProposalCommissionByProposalID(Int64 proposalID)
        {
            CRUDOperation operation = new CRUDOperation();
            var items = operation.GetProposalCommissionByProposalId(proposalID); ;
            return items;
        }
        [HttpGet]
        [Route("GetProposalUserSaveByUserID/{userID}")]
        public List<tbl_ProposalCommission> GetProposalCommissionsByProviderID(Int64 providerID)
        {
            CRUDOperation operation = new CRUDOperation();
            var items = operation.GetProposalCommissionByProviderId(providerID); ;
            return items;
        }

        [HttpPost]
        [ResponseType(typeof(tbl_ProposalCommission))]
        [Route("AddProposalCommission")]
        public IHttpActionResult AddProposalCommission(tbl_ProposalCommission item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CRUDOperation operation = new CRUDOperation();
            tbl_ProposalCommission dbitem = operation.AddProposalCommission(item);

            return Ok(dbitem);
        }

        [HttpPost]
        [ResponseType(typeof(tbl_ProposalCommission))]
        [Route("UpdateProposalCommission")]
        public IHttpActionResult UpdateProposalCommission(tbl_ProposalCommission item)
        {
            CRUDOperation operation = new CRUDOperation();
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                var dbitem = operation.UpdateProposalCommission(item);
                return Ok(dbitem);
            }
        }

        [HttpPost]
        [ResponseType(typeof(tbl_ProposalCommission))]
        [Route("DeleteProposalCommission/{id}")]
        public IHttpActionResult DeleteProposalCommission(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();

            var dbitem = operation.DeleteProposalCommission(id, 0);
            return Ok(dbitem);

        }

    }
}
