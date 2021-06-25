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
    [RoutePrefix("api/permission")]
    public class PermissionController : ApiController
    {
        [HttpGet]
        [Route("GetPermissions")]
        public List<tbl_Permission> GetPermissions()
        {
            CRUDOperation operation = new CRUDOperation();
            var permissions = operation.GetPermissons(); ;
            return permissions;
        }

        [HttpGet]
        [Route("GetPermissionByID/{id}")]
        public tbl_Permission GetPermissionByID(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();
            var permission = operation.GetPermissionById(id); ;
            return permission;
        }

        [HttpPost]
        [ResponseType(typeof(tbl_Permission))]
        [Route("AddPermission")]
        public async Task<IHttpActionResult> AddPermission(tbl_Permission item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CRUDOperation operation = new CRUDOperation();
            tbl_Permission dbitem = operation.AddPermission(item);

            return Ok(dbitem);
        }

        [HttpPost]
        [ResponseType(typeof(tbl_Permission))]
        [Route("UpdatePermission")]
        public async Task<IHttpActionResult> UpdatePermission(tbl_Permission item)
        {
            CRUDOperation operation = new CRUDOperation();
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                var dbitem = operation.UpdatePermission(item);
                return Ok(dbitem);
            }
        }

        [HttpPost]
        [ResponseType(typeof(tbl_Permission))]
        [Route("DeletePermission/{id}")]
        public async Task<IHttpActionResult> DeletePermission(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();

            var dbitem = operation.DeletePermission(id, 0);
            return Ok(dbitem);

        }

    }
}
