using ScoreMe.API.Attribute;
using ScoreMe.DAL;
using ScoreMe.DAL.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace ScoreMe.API.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    [CustomAuthenticationFilter]
    [RoutePrefix("api/proposal")]
    public class ProposalUserSaveController : ApiController
    {
        [HttpGet]
        [Route("GetProposalUserSaves")]
        public List<tbl_ProposalUserSave> GetProposalUserSaves()
        {
            CRUDOperation operation = new CRUDOperation();
            var items = operation.GetProposalUserSaves(); ;
            return items;
        }

        [HttpGet]
        [Route("GetProposalUserSaveByID/{id}")]
        public tbl_ProposalUserSave GetProposalUserSaveByID(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();
            var item = operation.GetProposalUserSaveById(id); ;
            return item;
        }
        [HttpGet]
        [Route("GetProposalUserSaveByProposalID/{proposalID}")]
        public List<tbl_ProposalUserSave> GetProposalUserSaveByProposalID(Int64 proposalID)
        {
            CRUDOperation operation = new CRUDOperation();
            var items = operation.GetProposalUserSavesByProposalId(proposalID); ;
            return items;
        }
        [HttpGet]
        [Route("GetProposalUserSaveByUserID/{userID}")]
        public List<tbl_ProposalUserSave> GetProposalUserSaveByUserID(Int64 userID)
        {
            CRUDOperation operation = new CRUDOperation();
            var items = operation.GetProposalUserSavesByUserId(userID); ;
            return items;
        }

        [HttpPost]
        [ResponseType(typeof(tbl_ProposalUserSave))]
        [Route("AddProposalUserSave")]
        public IHttpActionResult AddProposalUserSave(tbl_ProposalUserSave item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CRUDOperation operation = new CRUDOperation();
            tbl_ProposalUserSave dbitem = operation.AddProposalUserSave(item);

            return Ok(dbitem);
        }

        [HttpPost]
        [ResponseType(typeof(tbl_ProposalUserSave))]
        [Route("UpdateProposalUserSave")]
        public IHttpActionResult UpdateProposalUserSave(tbl_ProposalUserSave item)
        {
            CRUDOperation operation = new CRUDOperation();
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                var dbitem = operation.UpdateProposalUserSave(item);
                return Ok(dbitem);
            }
        }

        [HttpPost]
        [ResponseType(typeof(tbl_ProposalUserSave))]
        [Route("DeleteProposalUserSave/{id}")]
        public IHttpActionResult DeleteProposalUserSave(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();

            var dbitem = operation.DeleteProposalUserSave(id, 0);
            return Ok(dbitem);

        }
    }
}
