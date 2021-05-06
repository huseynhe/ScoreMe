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
using System.Web.Http.Description;

namespace ScoreMe.API.Controllers
{
    [RoutePrefix("api/netConsume")]
    public class NetConsumeController : ApiController
    {
        [HttpGet]
        [Route("GetNetConsumeModelWithDetails")]
        public IHttpActionResult GetNetConsumeModelWithDetails()
        {
            BusinessOperation businessOperation = new BusinessOperation();
            List<NetConsumeModel> itemsOut = null;
            BaseOutput baseOutput = businessOperation.GetNetConsumeModels(out itemsOut);
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
        [ResponseType(typeof(NetConsumeModel))]
        [Route("GetNetConsumeModelWithDetailByID/{id}")]
        public IHttpActionResult GetNetConsumeModelWithDetailByID(Int64 id)
        {
            BusinessOperation businessOperation = new BusinessOperation();
            NetConsumeModel itemOut = null;
            BaseOutput baseOutput = businessOperation.GetNetConsumeModelByID(id, out itemOut);
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
        [ResponseType(typeof(NetConsumeModel))]
        [Route("GetLastNetConsumeModelByUserName/{userName}")]
        public IHttpActionResult GetLastNetConsumeModelByUserName(string userName)
        {
            BusinessOperation businessOperation = new BusinessOperation();
            NetConsumeModel itemOut = null;
            BaseOutput baseOutput = businessOperation.GetLastNetConsumeModelByUserName(userName, out itemOut);
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
        [ResponseType(typeof(NetConsumeModel))]
        [Route("AddNetConsumeModelWithDetail")]
        public IHttpActionResult AddNetConsumeModelWithDetail(NetConsumeModel item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BusinessOperation businessOperation = new BusinessOperation();

            BaseOutput baseOutput = businessOperation.AddNetConsumeModel(item);
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
        [ResponseType(typeof(NetConsumeModel))]
        [Route("UpdateNetConsumeModelWithDetail")]
        public IHttpActionResult UpdateNetConsumeModelWithDetail(NetConsumeModel item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BusinessOperation businessOperation = new BusinessOperation();

            BaseOutput baseOutput = businessOperation.UpdateNetConsumeModel(item);
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
        [ResponseType(typeof(NetConsumeModel))]
        [Route("DeleteNetConsumeModelWithDetail")]
        public IHttpActionResult DeleteNetConsumeModelWithDetail(Int64 id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BusinessOperation businessOperation = new BusinessOperation();

            BaseOutput baseOutput = businessOperation.DeleteNetConsumeModel(id);
            if (baseOutput.ResultCode == 1)
            {
                return Ok();
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, baseOutput);
            }


        }
        [HttpGet]
        [Route("GetMontlyAverage")]
        public decimal GetMontlyAverage(Int64 userId, Int64 sourceEV, Int64 mobileEV, int year, int lastMontCount, int firstMountCount, int startMont, int endMonth)
        {
            decimal _monthlyAverage = 0;
            if (userId == 0 || sourceEV == 0 || mobileEV == 0)
            {
                return _monthlyAverage;
            }

            NetConsumeBOperation businessOperation = new NetConsumeBOperation();

            BaseOutput dbitem = businessOperation.GetMontlyAverage(userId, sourceEV, mobileEV, year, lastMontCount, firstMountCount, startMont, endMonth, out _monthlyAverage);

            return _monthlyAverage;

        }
    }
}
