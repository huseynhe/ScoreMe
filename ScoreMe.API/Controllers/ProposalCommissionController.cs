using ScoreMe.Business;
using ScoreMe.DAL;
using ScoreMe.DAL.CodeObjects;
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
        ProposalBusinessOperation businessOperation = new ProposalBusinessOperation();
        [HttpGet]
        [Route("GetProposalCommissions")]
        public IHttpActionResult GetProposalCommissions()
        { 
            List<tbl_ProposalCommission> itemsOut = null;
            BaseOutput baseOutput = businessOperation.GetProposalCommissions(out itemsOut);
            if (baseOutput.ResultCode == 1)
            {
                return Ok(itemsOut);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, baseOutput);
            }
        }

        [HttpGet]
        [Route("GetProposalCommissionByID/{id}")]
        public IHttpActionResult GetProposalCommissionByID(Int64 id)
        {
            tbl_ProposalCommission itemOut = null;
            BaseOutput baseOutput = businessOperation.GetProposalCommissionByID(id,out itemOut);
            if (baseOutput.ResultCode == 1)
            {
                return Ok(itemOut);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, baseOutput);
            }
        }
        [HttpGet]
        [Route("GetProposalCommissionByProposalID/{proposalID}")]
        public IHttpActionResult GetProposalCommissionByProposalID(Int64 proposalID)
        {
            tbl_ProposalCommission itemOut = null;
            BaseOutput baseOutput = businessOperation.GetProposalCommissionByProposalID(proposalID, out itemOut);
            if (baseOutput.ResultCode == 1)
            {
                return Ok(itemOut);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, baseOutput);
            }
        }
        [HttpGet]
        [Route("GetProposalCommissionsByProviderID/{userID}")]
        public IHttpActionResult GetProposalCommissionsByProviderID(Int64 providerID)
        {
            List<tbl_ProposalCommission> itemsOut = null;
            BaseOutput baseOutput = businessOperation.GetProposalCommissionsByProviderID(providerID, out itemsOut);
            if (baseOutput.ResultCode == 1)
            {
                return Ok(itemsOut);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, baseOutput);
            }
        }

        [HttpPost]
        [ResponseType(typeof(tbl_ProposalCommission))]
        [Route("AddProposalCommission")]
        public IHttpActionResult AddProposalCommission(tbl_ProposalCommission item)
        {
            tbl_ProposalCommission itemOut = null;
            BaseOutput baseOutput = businessOperation.AddProposalCommission(item, out itemOut);
            if (baseOutput.ResultCode == 1)
            {
                return Ok(itemOut);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, baseOutput);
            }
        }

        [HttpPost]
        [ResponseType(typeof(tbl_ProposalCommission))]
        [Route("UpdateProposalCommission")]
        public IHttpActionResult UpdateProposalCommission(tbl_ProposalCommission item)
        {
            tbl_ProposalCommission itemOut = null;
            BaseOutput baseOutput = businessOperation.UpdateProposalCommission(item, out itemOut);
            if (baseOutput.ResultCode == 1)
            {
                return Ok(itemOut);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, baseOutput);
            }
        }

        [HttpPost]
        [ResponseType(typeof(tbl_ProposalCommission))]
        [Route("DeleteProposalCommission/{id}")]
        public IHttpActionResult DeleteProposalCommission(Int64 id)
        {
            tbl_ProposalCommission itemOut = null;
            BaseOutput baseOutput = businessOperation.DeleteProposalCommission(id, out itemOut);
            if (baseOutput.ResultCode == 1)
            {
                return Ok(itemOut);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, baseOutput);
            }

        }

    }
}
