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
    [RoutePrefix("api/appConsume")]
    public class AppConsumeController : ApiController
    {
        [HttpGet]
        [Route("GetAppConsumeModelWithDetails")]
        public List<AppConsumeModel> GetAppConsumeModelWithDetails()
        {
            BusinessOperation businessOperation = new BusinessOperation();
            List<AppConsumeModel> itemsOut = null;
            BaseOutput dbitem = businessOperation.GetAppConsumeModels(out itemsOut);
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
        [ResponseType(typeof(AppConsumeModel))]
        [Route("GetAppConsumeModelWithDetailByID/{id}")]
        public IHttpActionResult GetAppConsumeModelWithDetailByID(Int64 id)
        {
            BusinessOperation businessOperation = new BusinessOperation();
            AppConsumeModel itemOut = null;
            BaseOutput dbitem = businessOperation.GetAppConsumeModelByID(id, out itemOut);
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
        [ResponseType(typeof(AppConsumeModel))]
        [Route("GetLastAppConsumeModelByUserName/{userName}")]
        public IHttpActionResult GetLastAppConsumeModelByUserName(string userName)
        {
            BusinessOperation businessOperation = new BusinessOperation();
            AppConsumeModel itemOut = null;
            BaseOutput dbitem = businessOperation.GetLastAppConsumeModelByUserName(userName, out itemOut);
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
        [ResponseType(typeof(AppConsumeModel))]
        [Route("AddAppConsumeModelWithDetail")]
        public IHttpActionResult AddAppConsumeModelWithDetail(AppConsumeModel item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BusinessOperation businessOperation = new BusinessOperation();

            BaseOutput dbitem = businessOperation.AddAppConsumeModel(item);
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
        [ResponseType(typeof(AppConsumeModel))]
        [Route("UpdateAppConsumeModelWithDetail")]
        public IHttpActionResult UpdateAppConsumeModelWithDetail(AppConsumeModel item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BusinessOperation businessOperation = new BusinessOperation();

            BaseOutput dbitem = businessOperation.UpdateAppConsumeModel(item);
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
        [ResponseType(typeof(AppConsumeModel))]
        [Route("DeleteAppConsumeModelWithDetail")]
        public IHttpActionResult DeleteAppConsumeModelWithDetail(Int64 id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BusinessOperation businessOperation = new BusinessOperation();

            BaseOutput dbitem = businessOperation.DeleteAppConsumeModel(id);
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
