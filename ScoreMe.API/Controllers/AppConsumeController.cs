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
    [RoutePrefix("api/appConsume")]
    public class AppConsumeController : ApiController
    {
        [HttpGet]
        [Route("GetAppConsumeModelWithDetails")]
        public IHttpActionResult GetAppConsumeModelWithDetails()
        {
            BusinessOperation businessOperation = new BusinessOperation();
            List<AppConsumeModel> itemsOut = null;
            BaseOutput baseOutput = businessOperation.GetAppConsumeModels(out itemsOut);
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
        [ResponseType(typeof(AppConsumeModel))]
        [Route("GetAppConsumeModelWithDetailByID/{id}")]
        public IHttpActionResult GetAppConsumeModelWithDetailByID(Int64 id)
        {
            BusinessOperation businessOperation = new BusinessOperation();
            AppConsumeModel itemOut = null;
            BaseOutput baseOutput = businessOperation.GetAppConsumeModelByID(id, out itemOut);
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
        [ResponseType(typeof(AppConsumeModel))]
        [Route("GetLastAppConsumeModelByUserName/{userName}")]
        public IHttpActionResult GetLastAppConsumeModelByUserName(string userName)
        {
            BusinessOperation businessOperation = new BusinessOperation();
            AppConsumeModel itemOut = null;
            BaseOutput baseOutput = businessOperation.GetLastAppConsumeModelByUserName(userName, out itemOut);
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
        [Route("AddAppConsumeModelWithDetail")]
        public IHttpActionResult AddAppConsumeModelWithDetail(AppConsumeModel item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BusinessOperation businessOperation = new BusinessOperation();
            AppConsumeModel itemOut = null;
            BaseOutput baseOutput = businessOperation.AddAppConsumeModel(item, out itemOut);
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
        [Route("UpdateAppConsumeModelWithDetail")]
        public IHttpActionResult UpdateAppConsumeModelWithDetail(AppConsumeModel item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BusinessOperation businessOperation = new BusinessOperation();

            BaseOutput baseOutput = businessOperation.UpdateAppConsumeModel(item);

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
        [Route("DeleteAppConsumeModelWithDetail")]
        public IHttpActionResult DeleteAppConsumeModelWithDetail(Int64 id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BusinessOperation businessOperation = new BusinessOperation();

            BaseOutput baseOutput = businessOperation.DeleteAppConsumeModel(id);
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
