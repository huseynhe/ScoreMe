using ScoreMe.Business;
using ScoreMe.DAL;
using ScoreMe.DAL.CodeObjects;
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
    [RoutePrefix("api/netConsume")]
    public class NetConsumeController : ApiController
    {

        [HttpGet]
        [Route("GetNetConsumeByID/{id}")]
        public tbl_NetConsume GetNetConsumeByID(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();
            var item = operation.GetNetConsumeByID(id); ;
            return item;
        }

        [HttpGet]
        [Route("GetNetConsumesByUserID/{userID}")]
        public List<tbl_NetConsume> GetNetConsumesByUserID(Int64 userID)
        {
            CRUDOperation operation = new CRUDOperation();
            var items = operation.GetNetConsumesByUserID(userID); ;
            return items;
        }


        [HttpPost]
        [ResponseType(typeof(tbl_NetConsume))]
        [Route("AddNetConsume")]
        public IHttpActionResult AddNetConsume(tbl_NetConsume item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CRUDOperation operation = new CRUDOperation();
            tbl_NetConsume dbitem = operation.AddNetConsume(item);

            return Ok(dbitem);
        }

        [HttpPost]
        [ResponseType(typeof(tbl_NetConsume))]
        [Route("UpdateNetConsume")]
        public IHttpActionResult UpdateNetConsume(tbl_NetConsume item)
        {
            CRUDOperation operation = new CRUDOperation();
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                var dbitem = operation.UpdateNetConsume(item);
                return Ok(dbitem);
            }
        }

        [HttpPost]
        [ResponseType(typeof(tbl_NetConsume))]
        [Route("DeleteNetConsume/{id}")]
        public IHttpActionResult DeleteNetConsume(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();

            var dbitem = operation.DeleteNetConsume(id, 0);
            return Ok(dbitem);

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
