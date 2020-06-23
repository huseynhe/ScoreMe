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
    [RoutePrefix("api/providerservice")]
    public class ProviderServicesController : ApiController
    {
        [HttpGet]
        [Route("GetProviderServices")]
        public List<tbl_ProviderService> GetProviderServices()
        {
            CRUDOperation operation = new CRUDOperation();
            var providerservices = operation.GetProviderServices(); ;
            return providerservices;
        }

        [HttpGet]
        [Route("GetProviderServiceByID/{id}")]
        public tbl_ProviderService GetProviderServiceByID(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();
            var providerservice = operation.GetProviderServiceById(id); ;
            return providerservice;
        }

        [HttpPost]
        [ResponseType(typeof(tbl_ProviderService))]
        [Route("AddProviderService")]
        public async Task<IHttpActionResult> AddProviderService(tbl_ProviderService item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CRUDOperation operation = new CRUDOperation();
            tbl_ProviderService dbitem = operation.AddProviderService(item);

            return Ok(dbitem);
        }

        [HttpPost]
        [ResponseType(typeof(tbl_ProviderService))]
        [Route("UpdateProviderService")]
        public async Task<IHttpActionResult> UpdateProviderService(tbl_ProviderService item)
        {
            CRUDOperation operation = new CRUDOperation();
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                var dbitem = operation.UpdateProviderService(item);
                return Ok(dbitem);
            }
        }

        [HttpPost]
        [ResponseType(typeof(tbl_ProviderService))]
        [Route("DeleteProviderService/{id}")]
        public async Task<IHttpActionResult> DeleteProviderService(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();

            var dbitem = operation.DeleteProviderService(id, 0);
            return Ok(dbitem);

        }
    }
}

