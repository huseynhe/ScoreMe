using ScoreMe.Business;
using ScoreMe.DAL.CodeObjects;
using ScoreMe.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ScoreMe.API.Controllers
{
    [RoutePrefix("api/call")]
    public class CALLModelController : ApiController
    {

        [HttpGet]
        [Route("GetCALLModelWithDetails")]
        public List<CALLModel> GetCALLModelWithDetails()
        {
            BusinessOperation businessOperation = new BusinessOperation();
            List<CALLModel> itemsOut = null;
            BaseOutput dbitem = businessOperation.GetCALLModels(out itemsOut);
            if (dbitem.ResultCode == 1)
            {
                return itemsOut;
            }
            else
            {
                return null;
            }
        }
        [HttpGet]
        [ResponseType(typeof(CALLModel))]
        [Route("GetCALLModelWithDetailByID/{id}")]
        public IHttpActionResult GetCALLModelWithDetailByID(Int64 id)
        {
            BusinessOperation businessOperation = new BusinessOperation();
            CALLModel itemOut = null;
            BaseOutput dbitem = businessOperation.GetCALLModelsByID(id, out itemOut);
            if (dbitem.ResultCode == 1)
            {
                return Ok(itemOut);
            }
            else
            {
                return BadRequest(dbitem.ResultCode + " : " + dbitem.ResultMessage);
            }
        }
        [HttpGet]
        [ResponseType(typeof(CALLModel))]
        [Route("GetLastCALLModelByUserName/{userName}")]
        public IHttpActionResult GetLastCALLModelByUserName(string userName)
        {
            BusinessOperation businessOperation = new BusinessOperation();
            CALLModel itemOut = null;
            BaseOutput dbitem = businessOperation.GetLastCALLModelByUserName(userName, out itemOut);
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
        [ResponseType(typeof(CALLModel))]
        [Route("AddCALLModelWithDetail")]
        public IHttpActionResult AddCALLModelWithDetail(CALLModel item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BusinessOperation businessOperation = new BusinessOperation();

            BaseOutput dbitem = businessOperation.AddCALLModel(item);
            if (dbitem.ResultCode == 1)
            {
                return Ok(dbitem);
            }
            else
            {
                return BadRequest(dbitem.ResultCode + " : " + dbitem.ResultMessage);
            }


        }
        [HttpPost]
        [ResponseType(typeof(CALLModel))]
        [Route("UpdateCALLModelWithDetail")]
        public IHttpActionResult UpdateCALLModelWithDetail(CALLModel item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BusinessOperation businessOperation = new BusinessOperation();

            BaseOutput dbitem = businessOperation.UpdateCALLModel(item);
            if (dbitem.ResultCode == 1)
            {
                return Ok(dbitem);
            }
            else
            {
                return BadRequest(dbitem.ResultCode + " : " + dbitem.ResultMessage);
            }


        }

        [HttpPost]
        [ResponseType(typeof(Proposal))]
        [Route("DeleteCALLModelWithDetail")]
        public IHttpActionResult DeleteCALLModelWithDetail(Int64 id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BusinessOperation businessOperation = new BusinessOperation();

            BaseOutput dbitem = businessOperation.DeleteCALLModel(id);
            if (dbitem.ResultCode == 1)
            {
                return Ok(dbitem);
            }
            else
            {
                return BadRequest(dbitem.ResultCode + " : " + dbitem.ResultMessage);
            }


        }
    }
}
