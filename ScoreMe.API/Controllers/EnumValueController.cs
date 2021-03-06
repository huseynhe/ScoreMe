﻿using ScoreMe.API.Attribute;
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
    [RoutePrefix("api/enumValue")]
    public class EnumValueController : ApiController
    {
        [HttpGet]
        [Route("GetEnumValues")]
        public List<tbl_EnumValue> GetEnumCategories()
        {
            CRUDOperation operation = new CRUDOperation();
            var items = operation.GetEnumValues(); ;
            return items;
        }

        [HttpGet]
        [Route("GetEnumValueByID/{id}")]
        public tbl_EnumValue GetEnumValueByID(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();
            var item = operation.GetEnumValueById(id); ;
            return item;
        }

        [HttpGet]
        [Route("GetEnumValuesByEnumCategoryID/{enumCategoryID}")]
        public List<tbl_EnumValue> GetEnumValuesByEnumCategoryID(Int64 enumCategoryID)
        {
            CRUDOperation operation = new CRUDOperation();
            var items = operation.GetEnumValuesByEnumCategoryID(enumCategoryID); ;
            return items;
        }

        [HttpGet]
        [Route("GetEnumValuesByEnumCategoryCode/{enumCategoryCode}")]
        public List<tbl_EnumValue> GetEnumValuesByEnumCategoryCode(string enumCategoryCode)
        {
            CRUDOperation operation = new CRUDOperation();
            var items = operation.GetEnumValuesByEnumCategoryCode(enumCategoryCode); ;
            return items;
        }

        [HttpPost]
        [ResponseType(typeof(tbl_EnumValue))]
        [Route("AddEnumValue")]
        public IHttpActionResult AddEnumValue(tbl_EnumValue item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CRUDOperation operation = new CRUDOperation();
            tbl_EnumValue dbitem = operation.AddEnumValue(item);

            return Ok(dbitem);
        }

        [HttpPost]
        [ResponseType(typeof(tbl_EnumValue))]
        [Route("UpdateEnumValue")]
        public IHttpActionResult UpdateEnumValue(tbl_EnumValue item)
        {
            CRUDOperation operation = new CRUDOperation();
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                var dbitem = operation.UpdateEnumValue(item);
                return Ok(dbitem);
            }
        }

        [HttpPost]
        [ResponseType(typeof(tbl_EnumValue))]
        [Route("DeleteEnumValue/{id}")]
        public IHttpActionResult DeleteEnumValue(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();

            var dbitem = operation.DeleteEnumValue(id, 0);
            return Ok(dbitem);

        }
    }
}
