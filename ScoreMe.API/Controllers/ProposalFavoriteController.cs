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
        [HttpGet]
        [Route("GetProposalFavoriteCountByProposalID/{proposalID}")]
        public Int64 GetProposalFavoriteCountByProposalID(Int64 proposalID)
        {
            CRUDOperation operation = new CRUDOperation();
            var items = operation.GetProposalFavoriteCountByPropsalId(proposalID); 
            return items;
        }
        [HttpGet]
        [Route("GetProposalFavoriteCountByUserId/{userID}")]
        public Int64 GetProposalFavoriteCountByUserId(Int64 userID)
        {
            CRUDOperation operation = new CRUDOperation();
          
            var items = operation.GetProposalFavoriteCountByUserID(userID);
            return items;
        }

        [HttpGet]
        [Route("GetProposalFavoriteByPropsalIdAndUserID/{proposalID}/{userID}")]
        public tbl_ProposalFavorite GetProposalFavoriteByPropsalIdAndUserID(Int64 proposalID, Int64 userID)
        {
            CRUDOperation operation = new CRUDOperation();
            var items = operation.GetProposalFavoriteByPropsalIdAndUserID(proposalID, userID); ;
            return items;
        }


        [HttpPost]
        [ResponseType(typeof(tbl_ProposalFavorite))]
        [Route("AddProposalFavorite")]
        public IHttpActionResult AddProposalFavorite(tbl_ProposalFavorite item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BusinessOperation businessOperation = new BusinessOperation();

            BaseOutput dbitem = businessOperation.AddProposalFavorite(item);
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
        [ResponseType(typeof(tbl_ProposalFavorite))]
        [Route("DeleteProposalFavorite/{id}")]
        public IHttpActionResult DeleteProposalFavorite(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();

            var dbitem = operation.DeleteProposalFavorite(id, 0);
            return Ok(dbitem);

        }
    }
}
