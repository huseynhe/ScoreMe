using ScoreMe.DAL;
using ScoreMe.DAL.DBModel;
using ScoreMe.DAL.Repositories;
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
    [RoutePrefix("api/region")]
    public class RegionController : ApiController
    {
        [HttpGet]
        [Route("GetRegions")]
        public List<tbl_Region> GetRegions()
        {
            CRUDOperation operation = new CRUDOperation();
            var regions = operation.GetRegions(); ;
            return regions;
        }

        [HttpGet]
        [Route("GetRegionByID/{id}")]
        public tbl_Region GetRegionByID(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();
            var region = operation.GetRegionById(id); ;
            return region;
        }
        [HttpGet]
        [Route("GetRegionsByChildID/{childId}")]
        public List<tbl_Region> GetRegionsByChildID(Int64 childId)
        {
            RegionRepository regionRepository = new RegionRepository();
            var regions = regionRepository.SV_GetSQLRegionsByChild(childId); ;
            return regions;
        }
        [HttpGet]
        [Route("GetRegionsByParentID/{parentId}")]
        public List<tbl_Region> GetRegionsByParentID(Int64 parentId)
        {
            RegionRepository regionRepository = new RegionRepository();
            var regions = regionRepository.SV_GetSQLRegionsByParent(parentId); ;
            return regions;
        }
        [HttpPost]
        [ResponseType(typeof(tbl_Region))]
        [Route("AddRegion")]
        public async Task<IHttpActionResult> AddRegion(tbl_Region item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CRUDOperation operation = new CRUDOperation();
            tbl_Region dbitem = operation.AddRegion(item);

            return Ok(dbitem);
        }

        [HttpPost]
        [ResponseType(typeof(tbl_Region))]
        [Route("UpdateRegion")]
        public async Task<IHttpActionResult> UpdateRegion(tbl_Region item)
        {
            CRUDOperation operation = new CRUDOperation();
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                var dbitem = operation.UpdateRegion(item);
                return Ok(dbitem);
            }
        }

        [HttpPost]
        [ResponseType(typeof(tbl_Region))]
        [Route("DeleteRegion/{id}")]
        public async Task<IHttpActionResult> DeleteRegion(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();

            var dbitem = operation.DeleteRegion(id, 0);
            return Ok(dbitem);

        }
    }
}
