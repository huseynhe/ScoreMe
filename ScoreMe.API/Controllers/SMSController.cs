using ScoreMe.API.Attribute;
using ScoreMe.Business;
using ScoreMe.DAL;
using ScoreMe.DAL.CodeObjects;
using ScoreMe.DAL.DBModel;
using ScoreMe.DAL.Model;
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
    [RoutePrefix("api/sms")]
    public class SMSController : ApiController
    {
        [HttpGet]
        [Route("GetSMSModelWithDetails")]
        public IHttpActionResult GetSMSModelWithDetails()
        {
            BusinessOperation businessOperation = new BusinessOperation();
            List<SMSModel> itemsOut = null;
            BaseOutput baseOutput = businessOperation.GetSMSModels(out itemsOut);
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
        [ResponseType(typeof(SMSModel))]
        [Route("GetSMSModelWithDetailByID/{id}")]
        public IHttpActionResult GetSMSModelWithDetailByID(Int64 id)
        {
            BusinessOperation businessOperation = new BusinessOperation();
            SMSModel itemOut = null;
            BaseOutput baseOutput = businessOperation.GetSMSModelsByID(id, out itemOut);
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
        [ResponseType(typeof(SMSModel))]
        [Route("GetLastSMSModelByUserName/{userName}")]
        public IHttpActionResult GetLastSMSModelByUserName(string userName)
        {
            BusinessOperation businessOperation = new BusinessOperation();
            SMSModel itemOut = null;
            BaseOutput baseOutput = businessOperation.GetLastSMSModelByUserName(userName, out itemOut);
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
        [ResponseType(typeof(SMSModel))]
        [Route("AddSMSModelWithDetail")]
        public IHttpActionResult AddSMSModelWithDetail(SMSModel item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BusinessOperation businessOperation = new BusinessOperation();

            BaseOutput baseOutput = businessOperation.AddSMSModel(item);
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
        [ResponseType(typeof(SMSModel))]
        [Route("UpdateSMSModelWithDetail")]
        public IHttpActionResult UpdateSMSModelWithDetail(SMSModel item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BusinessOperation businessOperation = new BusinessOperation();

            BaseOutput baseOutput = businessOperation.UpdateSMSModel(item);
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
        [ResponseType(typeof(SMSModel))]
        [Route("DeleteSMSModelWithDetail")]
        public IHttpActionResult DeleteSMSModelWithDetail(Int64 id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BusinessOperation businessOperation = new BusinessOperation();

            BaseOutput baseOutput = businessOperation.DeleteSMSModel(id);
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
