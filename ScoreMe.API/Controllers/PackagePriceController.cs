using ScoreMe.API.Attribute;
using ScoreMe.DAL;
using ScoreMe.DAL.DBModel;
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
    [RoutePrefix("api/packagePrice")]
    public class PackagePriceController : ApiController
    {
        [HttpGet]
        [Route("GetPackagePrices")]
        public List<tbl_PackagePrice> GetPackagePrices()
        {
            CRUDOperation operation = new CRUDOperation();
            var items = operation.GetPackagePrices(); ;
            return items;
        }

        [HttpGet]
        [Route("GetPackagePriceByID/{id}")]
        public tbl_PackagePrice GetPackagePriceByID(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();
            var item = operation.GetPackagePriceByID(id); ;
            return item;
        }
        [HttpGet]
        [Route("GetPackagePriceByPackageID/{packageID}")]
        public List<tbl_PackagePrice> GetPackagePriceByPackageID(Int64 packageID)
        {
            CRUDOperation operation = new CRUDOperation();
            var items = operation.GetPackagePricesByPackageID(packageID); ;
            return items;
        }


        [HttpPost]
        [ResponseType(typeof(tbl_PackagePrice))]
        [Route("AddPackagePrice")]
        public async Task<IHttpActionResult> AddPackagePrice(tbl_PackagePrice item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CRUDOperation operation = new CRUDOperation();
            tbl_PackagePrice dbitem = operation.AddPackagePrice(item);

            return Ok(dbitem);
        }

        [HttpPost]
        [ResponseType(typeof(tbl_PackagePrice))]
        [Route("UpdatePackagePrice")]
        public async Task<IHttpActionResult> UpdatePackagePrice(tbl_PackagePrice item)
        {
            CRUDOperation operation = new CRUDOperation();
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                var dbitem = operation.UpdatePackagePrice(item);
                return Ok(dbitem);
            }
        }

        [HttpPost]
        [ResponseType(typeof(tbl_PackagePrice))]
        [Route("DeletePackagePrice/{id}")]
        public async Task<IHttpActionResult> DeletePackagePrice(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();

            var dbitem = operation.DeletePackagePrice(id, 0);
            return Ok(dbitem);

        }
    }
}
