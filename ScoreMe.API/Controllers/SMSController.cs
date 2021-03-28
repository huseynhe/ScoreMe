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
    [RoutePrefix("api/sms")]
    public class SMSController : ApiController
    {

       
        [HttpGet]
        [Route("GetSMSModelWithDetails")]
        public List<SMSModel> GetSMSModelWithDetails()
        {
            BusinessOperation businessOperation = new BusinessOperation();
            List<SMSModel> itemsOut = null;
            BaseOutput dbitem = businessOperation.GetSMSModels(out itemsOut);
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
        [ResponseType(typeof(SMSModel))]
        [Route("GetSMSModelWithDetailByID/{id}")]
        public IHttpActionResult GetSMSModelWithDetailByID(Int64 id)
        {
            BusinessOperation businessOperation = new BusinessOperation();
            SMSModel itemOut = null;
            BaseOutput dbitem = businessOperation.GetSMSModelsByID(id, out itemOut);
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
        [ResponseType(typeof(SMSModel))]
        [Route("GetLastSMSModelByUserName/{userName}")]
        public IHttpActionResult GetLastSMSModelByUserName(string userName)
        {
            BusinessOperation businessOperation = new BusinessOperation();
            SMSModel itemOut = null;
            BaseOutput dbitem = businessOperation.GetLastSMSModelByUserName(userName, out itemOut);
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
        [ResponseType(typeof(SMSModel))]
        [Route("AddSMSModelWithDetail")]
        public IHttpActionResult AddSMSModelWithDetail(SMSModel item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BusinessOperation businessOperation = new BusinessOperation();

            BaseOutput dbitem = businessOperation.AddSMSModel(item);
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
        [ResponseType(typeof(SMSModel))]
        [Route("UpdateSMSModelWithDetail")]
        public IHttpActionResult UpdateSMSModelWithDetail(SMSModel item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BusinessOperation businessOperation = new BusinessOperation();

            BaseOutput dbitem = businessOperation.UpdateSMSModel(item);
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
        [ResponseType(typeof(SMSModel))]
        [Route("DeleteSMSModelWithDetail")]
        public IHttpActionResult DeleteSMSModelWithDetail(Int64 id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BusinessOperation businessOperation = new BusinessOperation();

            BaseOutput dbitem = businessOperation.DeleteSMSModel(id);
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
