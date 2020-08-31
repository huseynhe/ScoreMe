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
    [RoutePrefix("api/group")]
    public class GroupController : ApiController
    {
        [HttpGet]
        [Route("GetGroups")]
        public List<tbl_Group> GetGroups()
        {
            CRUDOperation operation = new CRUDOperation();
            var groups = operation.GetGroups(); ;
            return groups;
        }
        [HttpGet]
        [Route("GetGroupByID/{id}")]
        public tbl_Group GetGroupByID(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();
            var group = operation.GetGroupByID(id); ;
            return group;
        }
        [HttpGet]
        [Route("GetGroupByName/{name}")]
        public tbl_Group GetGroupByName(string name)
        {
            CRUDOperation operation = new CRUDOperation();
            var group = operation.GetGroupByName(name); ;
            return group;
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
            CRUDOperation operation = new CRUDOperation();
            tbl_Group dbitem = operation.AddGroup(item);

            return Ok(dbitem);
        }
        [HttpPost]
        [ResponseType(typeof(tbl_Group))]
        [Route("UpdateGroup")]
        public IHttpActionResult UpdateGroup(tbl_Group item)
        {
            CRUDOperation operation = new CRUDOperation();
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                var dbitem = operation.UpdateGroup(item);
                return Ok(dbitem);
            }
        }
        [HttpPost]
        [ResponseType(typeof(tbl_Group))]
        [Route("DeleteGroup/{id}")]
        public IHttpActionResult DeleteGroup(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();

            var dbitem = operation.DeleteGroup(id, 0);
            return Ok(dbitem);

        }

   
        [HttpGet]
        [Route("GetUsersByGroupID/{groupid}")]
        public List<tbl_User> GetUsersByGroupID(Int64 groupid)
        {
            CRUDOperation operation = new CRUDOperation();
            var users = operation.GetUsersByGroupID(groupid); ;
            return users;
        }
        [HttpGet]
        [Route("GetGroupsByUserID/{userid}")]
        public List<tbl_Group> GetGroupsByUserID(Int64 userid)
        {
            CRUDOperation operation = new CRUDOperation();
            var groups = operation.GetGroupsByUserID(userid); ;
            return groups;
        }
        [HttpGet]
        [Route("GetUserGroups")]
        public List<tbl_UserGroup> GetUserGroups()
        {
            CRUDOperation operation = new CRUDOperation();
            var usergroups = operation.GetUserGroups(); ;
            return usergroups;
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
            CRUDOperation operation = new CRUDOperation();
            tbl_UserGroup dbitem = operation.AddUserGroup(item);

            return Ok(dbitem);
        }
        [HttpPost]
        [ResponseType(typeof(tbl_UserGroup))]
        [Route("UpdateUserGroup")]
        public IHttpActionResult UpdateUserGroup(tbl_UserGroup item)
        {
            CRUDOperation operation = new CRUDOperation();
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                var dbitem = operation.UpdateUserGroup(item);
                return Ok(dbitem);
            }
        }
        [HttpPost]
        [ResponseType(typeof(tbl_UserGroup))]
        [Route("DeleteUserGroup/{id}")]
        public IHttpActionResult DeleteUserGroup(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();

            var dbitem = operation.DeleteUserGroup(id, 0);
            return Ok(dbitem);

        }
    }
}
