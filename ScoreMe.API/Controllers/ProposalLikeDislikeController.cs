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
    [RoutePrefix("api/proposal")]
    public class ProposalLikeDislikeController : ApiController
    {
        [HttpGet]
        [Route("GetProposalLikeDislikes")]
        public List<tbl_ProposalLikeDislike> GetProposalLikeDislikes()
        {
            CRUDOperation operation = new CRUDOperation();
            var items = operation.GetProposalLikeDislikes(); ;
            return items;
        }

        [HttpGet]
        [Route("GetProposalLikeDislikeByID/{id}")]
        public tbl_ProposalLikeDislike GetProposalLikeDislikeByID(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();
            var item = operation.GetProposalLikeDislikeById(id); ;
            return item;
        }
        [HttpGet]
        [Route("GetProposalLikeCountByProposalID/{proposalID}")]
        public Int64 GetProposalLikeCountByProposalID(Int64 proposalID)
        {
            CRUDOperation operation = new CRUDOperation();
            var items = operation.GetProposalLikeCountByProposalId(proposalID); ;
            return items;
        }
        [HttpGet]
        [Route("GetProposalLikeCountByUserID/{userID}")]
        public Int64 GetProposalLikeCountByUserID(Int64 userID)
        {
            CRUDOperation operation = new CRUDOperation();
            var items = operation.GetProposalLikeCountByUserId(userID); ;
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
            CRUDOperation operation = new CRUDOperation();
            tbl_ProposalLikeDislike dbitem = operation.AddProposalLikeDislike(item);

            return Ok(dbitem);
        }

        [HttpPost]
        [ResponseType(typeof(tbl_ProposalLikeDislike))]
        [Route("UpdateProposalLikeDislike")]
        public IHttpActionResult UpdateProposalLikeDislike(tbl_ProposalLikeDislike item)
        {
            CRUDOperation operation = new CRUDOperation();
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                var dbitem = operation.UpdateProposalLikeDislike(item);
                return Ok(dbitem);
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
