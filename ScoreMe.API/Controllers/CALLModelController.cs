using ScoreMe.Business;
using ScoreMe.DAL.CodeObjects;
using ScoreMe.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ScoreMe.API.Controllers
{
    [RoutePrefix("api/call")]
    public class CALLModelController : ApiController
    {

        [HttpGet]
        [Route("GetCALLModelWithDetails")]
        public IHttpActionResult GetCALLModelWithDetails()
        {
            BusinessOperation businessOperation = new BusinessOperation();
            List<CALLModel> itemsOut = null;
            BaseOutput baseOutput = businessOperation.GetCALLModels(out itemsOut);
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
        [ResponseType(typeof(CALLModel))]
        [Route("GetCALLModelWithDetailByID/{id}")]
        public IHttpActionResult GetCALLModelWithDetailByID(Int64 id)
        {
            BusinessOperation businessOperation = new BusinessOperation();
            CALLModel itemOut = null;
            BaseOutput baseOutput = businessOperation.GetCALLModelsByID(id, out itemOut);
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
        [ResponseType(typeof(CALLModel))]
        [Route("GetLastCALLModelByUserName/{userName}")]
        public IHttpActionResult GetLastCALLModelByUserName(string userName)
        {
            BusinessOperation businessOperation = new BusinessOperation();
            CALLModel itemOut = null;
            BaseOutput baseOutput = businessOperation.GetLastCALLModelByUserName(userName, out itemOut);
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
        [Route("AddCALLModelWithDetail")]
        public IHttpActionResult AddCALLModelWithDetail(CALLModel item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BusinessOperation businessOperation = new BusinessOperation();

            BaseOutput baseOutput = businessOperation.AddCALLModel(item);
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
        [Route("UpdateCALLModelWithDetail")]
        public IHttpActionResult UpdateCALLModelWithDetail(CALLModel item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BusinessOperation businessOperation = new BusinessOperation();

            BaseOutput baseOutput = businessOperation.UpdateCALLModel(item);
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
        [Route("DeleteCALLModelWithDetail")]
        public IHttpActionResult DeleteCALLModelWithDetail(Int64 id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BusinessOperation businessOperation = new BusinessOperation();

            BaseOutput baseOutput = businessOperation.DeleteCALLModel(id);
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
