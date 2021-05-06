using ScoreMe.Business;
using ScoreMe.DAL;
using ScoreMe.DAL.CodeObjects;
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
    [RoutePrefix("api/group")]
    public class GroupController : ApiController
    {
        GroupBusinessOperation businessOperation = new GroupBusinessOperation();

        #region tbl_Group   
        [HttpGet]
        [Route("GetGroups")]
        public IHttpActionResult GetGroups()
        {
            List<tbl_Group> itemsOut = null;
            BaseOutput baseOutput = businessOperation.GetGroups(out itemsOut);
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
        [Route("GetGroupByID/{id}")]
        public IHttpActionResult GetGroupByID(Int64 id)
        {
            tbl_Group itemOut = null;
            BaseOutput baseOutput = businessOperation.GetGroupByID(id, out itemOut);
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
        [Route("GetGroupByName/{name}")]
        public IHttpActionResult GetGroupByName(string name)
        {
            tbl_Group itemOut = null;
            BaseOutput baseOutput = businessOperation.GetGroupByName(name, out itemOut);
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
        [ResponseType(typeof(tbl_Group))]
        [Route("AddGroup")]
        public IHttpActionResult AddGroup(tbl_Group item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            tbl_Group itemOut = null;
            BaseOutput baseOutput = businessOperation.AddGroup(item, out itemOut);
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
        [ResponseType(typeof(tbl_Group))]
        [Route("UpdateGroup")]
        public IHttpActionResult UpdateGroup(tbl_Group item)
        {
            tbl_Group itemOut = null;
            BaseOutput baseOutput = businessOperation.UpdateGroup(item, out itemOut);
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
        [ResponseType(typeof(tbl_Group))]
        [Route("DeleteGroup/{id}")]
        public IHttpActionResult DeleteGroup(Int64 id)
        {
            tbl_Group itemOut = null;
            BaseOutput baseOutput = businessOperation.DeleteGroup(id, out itemOut);
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
        [Route("GetUsersByGroupID/{groupid}")]
        public IHttpActionResult GetUsersByGroupID(Int64 groupid)
        {
            List<tbl_User> itemsOut = null;
            BaseOutput baseOutput = businessOperation.GetUsersByGroupID(groupid, out itemsOut);
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
        [Route("GetGroupsByUserID/{userid}")]
        public IHttpActionResult GetGroupsByUserID(Int64 userid)
        {
            List<tbl_Group> itemsOut = null;
            BaseOutput baseOutput = businessOperation.GetGroupsByUserID(userid, out itemsOut);
            if (baseOutput.ResultCode == 1)
            {
                return Ok(itemsOut);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, baseOutput);
            }
        }
        #endregion

        [HttpGet]
        [Route("GetUserGroups")]
        public IHttpActionResult GetUserGroups()
        {
            List<tbl_UserGroup> itemsOut = null;
            BaseOutput baseOutput = businessOperation.GetUserGroups(out itemsOut);
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
        [ResponseType(typeof(tbl_UserGroup))]
        [Route("AddUserGroup")]
        public IHttpActionResult AddUserGroup(tbl_UserGroup item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            tbl_UserGroup itemOut = null;
            BaseOutput baseOutput = businessOperation.AddUserGroup(item, out itemOut);
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
        [ResponseType(typeof(tbl_UserGroup))]
        [Route("UpdateUserGroup")]
        public IHttpActionResult UpdateUserGroup(tbl_UserGroup item)
        {
            tbl_UserGroup itemOut = null;
            BaseOutput baseOutput = businessOperation.UpdateUserGroup(item, out itemOut);
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
        [ResponseType(typeof(tbl_UserGroup))]
        [Route("DeleteUserGroup/{id}")]
        public IHttpActionResult DeleteUserGroup(Int64 id)
        {
            tbl_UserGroup itemOut = null;
            BaseOutput baseOutput = businessOperation.DeleteUserGroup(id, out itemOut);
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
