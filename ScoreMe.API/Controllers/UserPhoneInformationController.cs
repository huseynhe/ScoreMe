using ScoreMe.Business;
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
    [RoutePrefix("api/userphone")]
    public class UserPhoneInformationController : ApiController
    {

        [HttpGet]
        [ResponseType(typeof(tbl_UserPhoneInforamtion))]
        [Route("GetUserPhoneInforamtionByID/{id}")]
        public IHttpActionResult GetUserPhoneInforamtionByID(Int64 id)
        {
            BusinessOperation businessOperation = new BusinessOperation();
            tbl_UserPhoneInforamtion itemOut = null;
            BaseOutput dbitem = businessOperation.GetUserPhoneInformationByID(id, out itemOut);
            if (dbitem.ResultCode == 1)
            {
                return Ok(itemOut);
            }
            else
            {
                return BadRequest(dbitem.ResultCode + " : " + dbitem.ResultMessage);
            }
        }
        [HttpGet]
        [Route("GetUserPhoneInforamtionsByUserName/{userName}")]
        public List<tbl_UserPhoneInforamtion> GetUserPhoneInforamtionsByUserName(string userName)
        {
            BusinessOperation businessOperation = new BusinessOperation();
            List<tbl_UserPhoneInforamtion> itemsOut = null;
            BaseOutput dbitem = businessOperation.GetUserPhoneInformationsByUserName(userName,out itemsOut);
            if (dbitem.ResultCode == 1)
            {
                return itemsOut;
            }
            else
            {
                return null;
            }
        }
        [HttpGet]
        [ResponseType(typeof(tbl_UserPhoneInforamtion))]
        [Route("GetLastUserPhoneInforamtionByUserName/{userName}")]
        public IHttpActionResult GetLastUserPhoneInforamtionByUserName(string userName)
        {
            BusinessOperation businessOperation = new BusinessOperation();
            tbl_UserPhoneInforamtion itemOut = null;
            BaseOutput dbitem = businessOperation.GetLastUserPhoneInformationByUserName(userName, out itemOut);
            if (dbitem.ResultCode == 1)
            {
                return Ok(itemOut);
            }
            else
            {
                return BadRequest(dbitem.ResultCode + " : " + dbitem.ResultMessage);
            }
        }
        [HttpPost]
        [ResponseType(typeof(tbl_UserPhoneInforamtion))]
        [Route("AddUserPhoneInforamtion")]
        public IHttpActionResult AddUserPhoneInforamtion(tbl_UserPhoneInforamtion item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BusinessOperation businessOperation = new BusinessOperation();

            BaseOutput dbitem = businessOperation.AddUserPhoneInformation(item);
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
        [ResponseType(typeof(tbl_UserPhoneInforamtion))]
        [Route("UpdateUserPhoneInforamtion")]
        public IHttpActionResult UpdateUserPhoneInforamtion(tbl_UserPhoneInforamtion item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BusinessOperation businessOperation = new BusinessOperation();

            BaseOutput dbitem = businessOperation.UpdateUserPhoneInformation(item);
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
        [ResponseType(typeof(tbl_UserPhoneInforamtion))]
        [Route("DeleteUserPhoneInforamtion")]
        public IHttpActionResult DeleteUserPhoneInforamtion(Int64 id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BusinessOperation businessOperation = new BusinessOperation();

            BaseOutput dbitem = businessOperation.DeleteUserPhoneInformation(id);
            if (dbitem.ResultCode == 1)
            {
                return Ok(dbitem);
            }
            else
            {
                return BadRequest(dbitem.ResultCode + " : " + dbitem.ResultMessage);
            }


        }
    }
}
