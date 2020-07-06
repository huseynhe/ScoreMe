using ScoreMe.API.Attribute;
using ScoreMe.API.ResponseMessage;
using ScoreMe.Business;
using ScoreMe.Business.Model;
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
    [RoutePrefix("api/provider")]
    public class ProviderController : ApiController
    {
     
        [HttpGet]
        [Route("GetProviders")]
        public List<tbl_Provider> GetProviders()
        {
            CRUDOperation operation = new CRUDOperation();
            var providers = operation.GetProviders(); ;
            return providers;
        }

        [HttpGet]
        [Route("GetProviderByID/{id}")]
        public tbl_Provider GetProviderByID(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();
            var providers = operation.GetProviderById(id); ;
            return providers;
        }

        [HttpPost]
        [ResponseType(typeof(tbl_Provider))]
        [Route("AddProvider")]
        public async Task<IHttpActionResult> AddProvider(tbl_Provider item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CRUDOperation cRUDOperation = new CRUDOperation();
            tbl_Provider provider = cRUDOperation.AddProvider(item);
            if (provider!= null)
            {
                return Ok(provider);
            }
            else
            {
              
                return BadRequest();
            }
   
        }
        [HttpPost]
        [ResponseType(typeof(Provider))]
        [Route("AddProviderWithUser")]
        public async Task<IHttpActionResult> AddProviderWithUser(Provider item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BusinessOperation businessOperation = new BusinessOperation();
            Provider itemOut = null;
            BaseOutput dbitem = businessOperation.AddProviderWithUser(item, out itemOut);
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
        [ResponseType(typeof(tbl_Provider))]
        [Route("UpdateProvider")]
        public async Task<IHttpActionResult> UpdateProvider(tbl_Provider item)
        {
            CRUDOperation operation = new CRUDOperation();
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                var dbitem = operation.UpdateProvider(item);
                return Ok(dbitem);
            }
        }

        [HttpPost]
        [ResponseType(typeof(tbl_Provider))]
        [Route("DeleteProvider/{id}")]
        public async Task<IHttpActionResult> DeleteProvider(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();

            var dbitem = operation.DeleteProvider(id, 0);
            return Ok(dbitem);

        }

        [HttpGet]
        [Route("GetProviderByUserName/{username}")]
        public tbl_Provider GetProviderByUserName(string username)
        {
            CRUDOperation operation = new CRUDOperation();
            var provider = operation.GetProviderByUserName(username);
            return provider;
        }
    }
}
