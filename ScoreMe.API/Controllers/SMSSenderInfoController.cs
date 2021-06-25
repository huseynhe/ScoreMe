using ScoreMe.API.Attribute;
using ScoreMe.DAL;
using ScoreMe.DAL.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace ScoreMe.API.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    [CustomAuthenticationFilter]
    [RoutePrefix("api/smssenderinfo")]
    public class SMSSenderInfoController : ApiController
    {

        [HttpGet]
        [Route("GetSMSSenderInfos")]
        public List<tbl_SMSSenderInfo> GetSMSSenderInfos()
        {
            CRUDOperation operation = new CRUDOperation();
            var items = operation.GetSMSSenderInfos(); ;
            return items;
        }

        [HttpGet]
        [Route("GetSMSSenderInfoByID/{id}")]
        public tbl_SMSSenderInfo GetSMSSenderInfoByID(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();
            var item = operation.GetSMSSenderInfoByID(id); ;
            return item;
        }
        [HttpGet]
        [Route("GetSMSSenderInfoByName/{senderName}")]
        public tbl_SMSSenderInfo GetPackagesByMobileEVID(string senderName)
        {
            CRUDOperation operation = new CRUDOperation();
            var items = operation.GetSMSSenderInfoByName(senderName); ;
            return items;
        }


        [HttpPost]
        [ResponseType(typeof(tbl_SMSSenderInfo))]
        [Route("AddSMSSenderInfo")]
        public IHttpActionResult AddSMSSenderInfo(tbl_SMSSenderInfo item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CRUDOperation operation = new CRUDOperation();
            tbl_SMSSenderInfo dbitem = operation.AddSMSSenderInfo(item);

            return Ok(dbitem);
        }

        [HttpPost]
        [ResponseType(typeof(tbl_SMSSenderInfo))]
        [Route("UpdateSMSSenderInfo")]
        public IHttpActionResult UpdateSMSSenderInfo(tbl_SMSSenderInfo item)
        {
            CRUDOperation operation = new CRUDOperation();
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                var dbitem = operation.UpdateSMSSenderInfo(item);
                return Ok(dbitem);
            }
        }

        [HttpPost]
        [ResponseType(typeof(tbl_SMSSenderInfo))]
        [Route("DeleteSMSSenderInfo/{id}")]
        public IHttpActionResult DeleteSMSSenderInfo(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();

            var dbitem = operation.DeleteSMSSenderInfo(id, 0);
            return Ok(dbitem);

        }

    }
}
