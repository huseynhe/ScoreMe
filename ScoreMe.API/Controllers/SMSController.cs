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
    [RoutePrefix("api/sms")]
    public class SMSController : ApiController
    {

        [HttpGet]
        [Route("GetSMSModels")]
        public List<tbl_SMSModel> GetSMSModels()
        {
            CRUDOperation operation = new CRUDOperation();
            var items = operation.GetSMSModels(); ;
            return items;
        }

        [HttpGet]
        [Route("GetSMSModelByID/{id}")]
        public tbl_SMSModel GetSMSModelByID(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();
            var item = operation.GetSMSModelByID(id); ;
            return item;
        }



        [HttpPost]
        [ResponseType(typeof(tbl_SMSModel))]
        [Route("AddSMSModel")]
        public IHttpActionResult AddSMSModel(tbl_SMSModel item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CRUDOperation operation = new CRUDOperation();
            tbl_SMSModel dbitem = operation.AddSMSModel(item);

            return Ok(dbitem);
        }

        [HttpPost]
        [ResponseType(typeof(tbl_SMSModel))]
        [Route("UpdateSMSModel")]
        public IHttpActionResult UpdateSMSModel(tbl_SMSModel item)
        {
            CRUDOperation operation = new CRUDOperation();
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                var dbitem = operation.UpdateSMSModel(item);
                return Ok(dbitem);
            }
        }

        [HttpPost]
        [ResponseType(typeof(tbl_SMSModel))]
        [Route("DeleteSMSModel/{id}")]
        public IHttpActionResult DeleteSMSModel(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();

            var dbitem = operation.DeleteSMSModel(id, 0);
            return Ok(dbitem);

        }
    }
}
