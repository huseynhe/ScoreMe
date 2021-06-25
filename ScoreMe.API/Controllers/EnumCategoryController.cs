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
    [RoutePrefix("api/enumCategory")]
    public class EnumCategoryController : ApiController
    {
        [HttpGet]
        [Route("GetEnumCategories")]
        public List<tbl_EnumCategory> GetEnumCategories()
        {
            CRUDOperation operation = new CRUDOperation();
            var items = operation.GetEnumCategorys(); ;
            return items;
        }

        [HttpGet]
        [Route("GetEnumCategoryByID/{id}")]
        public tbl_EnumCategory GetEnumCategoryByID(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();
            var item = operation.GetEnumCategoryById(id); ;
            return item;
        }

        [HttpPost]
        [ResponseType(typeof(tbl_EnumCategory))]
        [Route("AddEnumCategory")]
        public IHttpActionResult AddEnumCategory(tbl_EnumCategory item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CRUDOperation operation = new CRUDOperation();
            tbl_EnumCategory dbitem = operation.AddEnumCategory(item);

            return Ok(dbitem);
        }

        [HttpPost]
        [ResponseType(typeof(tbl_EnumCategory))]
        [Route("UpdateEnumCategory")]
        public IHttpActionResult UpdateEnumCategory(tbl_EnumCategory item)
        {
            CRUDOperation operation = new CRUDOperation();
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                var dbitem = operation.UpdateEnumCategory(item);
                return Ok(dbitem);
            }
        }

        [HttpPost]
        [ResponseType(typeof(tbl_EnumCategory))]
        [Route("DeleteEnumCategory/{id}")]
        public IHttpActionResult DeleteEnumCategory(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();

            var dbitem = operation.DeleteEnumCategory(id, 0);
            return Ok(dbitem);

        }
    }
}
