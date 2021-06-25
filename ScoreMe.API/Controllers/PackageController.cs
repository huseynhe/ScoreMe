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
    [RoutePrefix("api/package")]
    public class PackageController : ApiController
    {

        [HttpGet]
        [Route("GetPackages")]
        public List<tbl_Package> GetPackages()
        {
            CRUDOperation operation = new CRUDOperation();
            var items = operation.GetPackages(); ;
            return items;
        }

        [HttpGet]
        [Route("GetPackageByID/{id}")]
        public tbl_Package GetPackageByID(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();
            var item = operation.GetPackageByID(id); ;
            return item;
        }
        [HttpGet]
        [Route("GetPackagesByMobileEVID/{moebileEVID}")]
        public List<tbl_Package> GetPackagesByMobileEVID(Int64 moebileEVID)
        {
            CRUDOperation operation = new CRUDOperation();
            var items = operation.GetPackagesByMobileEVID(moebileEVID); ;
            return items;
        }


        [HttpPost]
        [ResponseType(typeof(tbl_Package))]
        [Route("AddPackage")]
        public IHttpActionResult AddPackage(tbl_Package item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CRUDOperation operation = new CRUDOperation();
            tbl_Package dbitem = operation.AddPackage(item);

            return Ok(dbitem);
        }

        [HttpPost]
        [ResponseType(typeof(tbl_Package))]
        [Route("UpdatePackage")]
        public IHttpActionResult UpdatePackage(tbl_Package item)
        {
            CRUDOperation operation = new CRUDOperation();
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                var dbitem = operation.UpdatePackage(item);
                return Ok(dbitem);
            }
        }

        [HttpPost]
        [ResponseType(typeof(tbl_Package))]
        [Route("DeletePackage/{id}")]
        public IHttpActionResult DeletePackage(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();

            var dbitem = operation.DeletePackage(id, 0);
            return Ok(dbitem);

        }
    }
}
