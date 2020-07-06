using ScoreMe.Business;
using ScoreMe.Business.Model;
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

    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        [HttpGet]
        [Route("GetUsers")]
        public List<tbl_User> GetUsers()
        {
            CRUDOperation operation = new CRUDOperation();
            var users = operation.GetUsers(); ;
            return users;
        }

        [HttpGet]
        [Route("GetUserByID/{id}")]
        public tbl_User GetUserByID(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();
            var user = operation.GetUserById(id); ;
            return user;
        }

        [HttpGet]
        [Route("GetUserByUserName/{username}")]
        public tbl_User GetUserByUserName(string username)
        {
            CRUDOperation operation = new CRUDOperation();
            var user = operation.GetUserByUserName(username); ;
            return user;
        }
        [HttpPost]
        [ResponseType(typeof(tbl_User))]
        [Route("AddUser")]
        public async Task<IHttpActionResult> AddUser(tbl_User item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CRUDOperation operation = new CRUDOperation();
            tbl_User dbitem = operation.AddUser(item);

            return Ok(dbitem);
        }

        [HttpPost]
        [ResponseType(typeof(tbl_User))]
        [Route("UpdateUser")]
        public async Task<IHttpActionResult> UpdateUser(tbl_User item)
        {
            CRUDOperation operation = new CRUDOperation();
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                var dbitem = operation.UpdateUser(item);
                return Ok(dbitem);
            }
        }

        [HttpPost]
        [ResponseType(typeof(tbl_User))]
        [Route("DeleteUser/{id}")]
        public async Task<IHttpActionResult> DeleteUser(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();

            var dbitem = operation.DeleteUser(id, 0);
            return Ok(dbitem);

        }

        [HttpPost]
        [ResponseType(typeof(tbl_User))]
        [Route("ChangePassword/{id}/{newpassword}")]
        public async Task<IHttpActionResult> ChangePassword(Int64 id, string newpassword)
        {
            CRUDOperation operation = new CRUDOperation();

            var dbitem = operation.ChangePassword(id, 0, newpassword);
            return Ok(dbitem);

        }
        [HttpPost]
        [ResponseType(typeof(tbl_User))]
        [Route("ChangePasswordByUserName")]
        public async Task<IHttpActionResult> ChangePasswordByUserName(UserInfo item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BusinessOperation businessOperation = new BusinessOperation();
            tbl_User itemOut = null;
            BaseOutput dbitem = businessOperation.ChangePasswordByUserName(item, 0, out itemOut);
            if (dbitem.ResultCode == 1)
            {
                return Ok(itemOut);
            }
            else
            {
                return BadRequest(dbitem.ResultCode + " : " + dbitem.ResultMessage);
            }

        }
    }

}

