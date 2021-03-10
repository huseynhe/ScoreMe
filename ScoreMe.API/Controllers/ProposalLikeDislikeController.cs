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
    public class ProposalLikeDislikeController : ApiController
    {
        
        [HttpGet]
        [Route("GetProposalLikeCountByProposalID/{proposalID}")]
        public Int64 GetProposalLikeCountByProposalID(Int64 proposalID)
        {
            CRUDOperation operation = new CRUDOperation();
            int dislikecount = 0;
            var items = operation.GetProposalLikeDislikeCountByProposalId(proposalID,out dislikecount); ;
            return items;
        }
        [HttpGet]
        [Route("GetProposalDislikeCountByProposalID/{proposalID}")]
        public Int64 GetProposalDislikeCountByProposalID(Int64 proposalID)
        {
            CRUDOperation operation = new CRUDOperation();
            int dislikecount = 0;
            var items = operation.GetProposalLikeDislikeCountByProposalId(proposalID,out dislikecount); ;
            return dislikecount;
        }

        [HttpGet]
        [Route("GetProposalLikeDislikeByPropsalIdAndUserID/{proposalID}/{userID}")]
        public tbl_ProposalLikeDislike GetProposalLikeDislikeByPropsalIdAndUserID(Int64 proposalID, Int64 userID)
        {
            CRUDOperation operation = new CRUDOperation();
            var items = operation.GetProposalLikeDislikeByPropsalIdAndUserID(proposalID,userID); ;
            return items;
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
            BusinessOperation businessOperation = new BusinessOperation();

            BaseOutput dbitem = businessOperation.AddProposalLikeDislike(item);
            if (dbitem.ResultCode == 1)
            {
                return Ok(dbitem);
            }
            else
            {
                return BadRequest(dbitem.ResultCode + " : " + dbitem.ResultMessage);
            }


        }

        [HttpPost]
        [ResponseType(typeof(tbl_ProposalLikeDislike))]
        [Route("DeleteProposalLikeDislike/{id}")]
        public IHttpActionResult DeleteProposalLikeDislike(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();

            var dbitem = operation.DeleteProposalLikeDislike(id, 0);
            return Ok(dbitem);

        }
    }
}
