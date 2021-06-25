using ScoreMe.API.Attribute;
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
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace ScoreMe.API.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    [CustomAuthenticationFilter]
    [RoutePrefix("api/proposal")]
    public class ProposalLikeDislikeController : ApiController
    {
        ProposalBusinessOperation businessOperation = new ProposalBusinessOperation();
        [HttpGet]
        [Route("GetProposalLikeCountByProposalID/{proposalID}")]
        public IHttpActionResult GetProposalLikeCountByProposalID(Int64 proposalID)
        {
            int valueOut = 0;
            BaseOutput baseOutput = businessOperation.GetProposalLikeCountByProposalID(proposalID, out valueOut);
            if (baseOutput.ResultCode == 1)
            {
                return Ok(valueOut);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, baseOutput);
            }
        }
        [HttpGet]
        [Route("GetProposalDislikeCountByProposalID/{proposalID}")]
        public IHttpActionResult GetProposalDislikeCountByProposalID(Int64 proposalID)
        {
            int valueOut = 0;
            BaseOutput baseOutput = businessOperation.GetProposalDislikeCountByProposalID(proposalID, out valueOut);
            if (baseOutput.ResultCode == 1)
            {
                return Ok(valueOut);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, baseOutput);
            }
        }

        [HttpGet]
        [Route("GetProposalLikeDislikeByPropsalIdAndUserID/{proposalID}/{userID}")]
        public IHttpActionResult GetProposalLikeDislikeByPropsalIdAndUserID(Int64 proposalID, Int64 userID)
        {
            
            tbl_ProposalLikeDislike itemOut = null;
            BaseOutput baseOutput = businessOperation.GetProposalLikeDislikeByPropsalIdAndUserID(proposalID, userID, out itemOut);
            if (baseOutput.ResultCode == 1)
            {
                return Ok(itemOut);
            }
            else if (baseOutput.ResultCode == 5)
            {
                return Content(HttpStatusCode.NotFound, baseOutput);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, baseOutput);
            }
        }
       
        [HttpPost]
        [ResponseType(typeof(tbl_ProposalLikeDislike))]
        [Route("AddProposalLikeDislike")]
        public IHttpActionResult AddProposalLikeDislike(tbl_ProposalLikeDislike item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ProposalBusinessOperation businessOperation = new ProposalBusinessOperation();
            tbl_ProposalLikeDislike itemOut = null;
            BaseOutput baseOutput = businessOperation.AddProposalLikeDislike(item, out itemOut);
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
        [ResponseType(typeof(tbl_ProposalLikeDislike))]
        [Route("DeleteProposalLikeDislike/{id}")]
        public IHttpActionResult DeleteProposalLikeDislike(Int64 id)
        {
            ProposalBusinessOperation businessOperation = new ProposalBusinessOperation();
            tbl_ProposalLikeDislike itemOut = null;
            BaseOutput baseOutput = businessOperation.DeleteProposalLikeDislike(id, out itemOut);
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
