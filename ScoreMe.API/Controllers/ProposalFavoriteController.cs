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
    [RoutePrefix("api/proposal")]
    public class ProposalFavoriteController : ApiController
    {
        ProposalBusinessOperation businessOperation = new ProposalBusinessOperation();

        [HttpGet]
        [Route("GetProposalFavoriteCountByProposalID/{proposalID}")]
        public IHttpActionResult GetProposalFavoriteCountByProposalID(Int64 proposalID)
        {
         
            Int64 valueOut = 0;
            BaseOutput baseOutput = businessOperation.GetProposalFavoriteCountByPropsalId(proposalID,out valueOut);
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
        [Route("GetProposalFavoriteCountByUserId/{userID}")]
        public IHttpActionResult GetProposalFavoriteCountByUserId(Int64 userID)
        {
            Int64 valueOut = 0;
            BaseOutput baseOutput = businessOperation.GetProposalFavoriteCountByUserId(userID, out valueOut);
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
        [Route("GetProposalFavoriteByPropsalIdAndUserID/{proposalID}/{userID}")]
        public IHttpActionResult GetProposalFavoriteByPropsalIdAndUserID(Int64 proposalID, Int64 userID)
        {
            ProposalBusinessOperation businessOperation = new ProposalBusinessOperation();
            tbl_ProposalFavorite itemOut = null;
            BaseOutput baseOutput = businessOperation.GetProposalFavoriteByPropsalIdAndUserID(proposalID, userID, out itemOut);
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
        [ResponseType(typeof(tbl_ProposalFavorite))]
        [Route("AddProposalFavorite")]
        public IHttpActionResult AddProposalFavorite(tbl_ProposalFavorite item)
        {
           
            ProposalBusinessOperation businessOperation = new ProposalBusinessOperation();
            tbl_ProposalFavorite itemOut = null;
            BaseOutput baseOutput = businessOperation.AddProposalFavorite(item, out itemOut);
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
        [ResponseType(typeof(tbl_ProposalFavorite))]
        [Route("DeleteProposalFavorite/{id}")]
        public IHttpActionResult DeleteProposalFavorite(Int64 id)
        {
            ProposalBusinessOperation businessOperation = new ProposalBusinessOperation();
            tbl_ProposalFavorite itemOut = null;
            BaseOutput baseOutput = businessOperation.DeleteProposalFavorite(id, out itemOut);
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
