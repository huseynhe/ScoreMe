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
            BaseOutput baseOutput = businessOperation.GetUserPhoneInformationByID(id, out itemOut);
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
        [Route("GetUserPhoneInforamtionsByUserName/{userName}")]
        public IHttpActionResult GetUserPhoneInforamtionsByUserName(string userName)
        {
            BusinessOperation businessOperation = new BusinessOperation();
            List<tbl_UserPhoneInforamtion> itemsOut = null;
            BaseOutput baseOutput = businessOperation.GetUserPhoneInformationsByUserName(userName,out itemsOut);
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
        [ResponseType(typeof(tbl_UserPhoneInforamtion))]
        [Route("GetLastUserPhoneInforamtionByUserName/{userName}")]
        public IHttpActionResult GetLastUserPhoneInforamtionByUserName(string userName)
        {
            BusinessOperation businessOperation = new BusinessOperation();
            tbl_UserPhoneInforamtion itemOut = null;
            BaseOutput baseOutput = businessOperation.GetLastUserPhoneInformationByUserName(userName, out itemOut);
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
        [ResponseType(typeof(tbl_UserPhoneInforamtion))]
        [Route("AddUserPhoneInforamtion")]
        public IHttpActionResult AddUserPhoneInforamtion(tbl_UserPhoneInforamtion item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BusinessOperation businessOperation = new BusinessOperation();

            BaseOutput baseOutput = businessOperation.AddUserPhoneInformation(item);
            if (baseOutput.ResultCode == 1)
            {
                return Ok();
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, baseOutput);
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

            BaseOutput baseOutput = businessOperation.UpdateUserPhoneInformation(item);
            if (baseOutput.ResultCode == 1)
            {
                return Ok();
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, baseOutput);
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

            BaseOutput baseOutput = businessOperation.DeleteUserPhoneInformation(id);
            if (baseOutput.ResultCode == 1)
            {
                return Ok();
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, baseOutput);
            }


        }
    }
}
