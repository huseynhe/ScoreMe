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
    [RoutePrefix("api/role")]
    public class RoleController : ApiController
    {

        [HttpGet]
        [Route("GetRoles")]
        public List<tbl_Role> GetRoles()
        {
            CRUDOperation operation = new CRUDOperation();
            var roles = operation.GetRoles(); ;
            return roles;
        }

        [HttpGet]
        [Route("GetRoleByID/{id}")]
        public tbl_Role GetRoleByID(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();
            var roles = operation.GetRoleById(id); ;
            return roles;
        }

        [HttpPost]
        [ResponseType(typeof(tbl_Role))]
        [Route("AddRole")]
        public async Task<IHttpActionResult> AddRole(tbl_Role item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CRUDOperation operation = new CRUDOperation();
            tbl_Role dbitem = operation.AddRole(item);

            return Ok(dbitem);
        }

        [HttpPost]
        [ResponseType(typeof(tbl_Role))]
        [Route("UpdateRole")]
        public async Task<IHttpActionResult> UpdateRole(tbl_Role item)
        {
            CRUDOperation operation = new CRUDOperation();
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                var dbitem = operation.UpdateRole(item);
                return Ok(dbitem);
            }
        }

        [HttpPost]
        [ResponseType(typeof(tbl_Role))]
        [Route("DeleteRole/{id}")]
        public async Task<IHttpActionResult> DeleteRole(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();

            var dbitem = operation.DeleteRole(id, 0);
            return Ok(dbitem);

        }
    }
}
