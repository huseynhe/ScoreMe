using ScoreMe.API.Attribute;
using ScoreMe.API.ResponseMessage;
using ScoreMe.Business;
using ScoreMe.DAL.Model;
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
using ScoreMe.DAL.DTO;
using ScoreMe.DAL.Objects;
using ScoreMe.DAL.Repositories;

namespace ScoreMe.API.Controllers
{
    [RoutePrefix("api/provider")]
    public class ProviderController : ApiController
    {

        [HttpGet]
        [Route("GetProviders")]
        public IHttpActionResult GetProviders()
        {
            ProviderBusinessOperation businessOperation = new ProviderBusinessOperation();
            List<tbl_Provider> itemsOut = null;
            BaseOutput baseOutput = businessOperation.GetProviders(out itemsOut);
            if (baseOutput.ResultCode == 1)
            {
                return Ok(itemsOut);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, baseOutput);
            }
        }

        [HttpGet]
        [Route("GetProviderByID/{id}")]
        public IHttpActionResult GetProviderByID(Int64 id)
        {
            ProviderBusinessOperation businessOperation = new ProviderBusinessOperation();
            tbl_Provider itemOut = null;
            BaseOutput baseOutput = businessOperation.GetProviderByID(id, out itemOut);
            if (baseOutput.ResultCode == 1)
            {
                return Ok(itemOut);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, baseOutput);
            }
        }

        [HttpGet]
        [ResponseType(typeof(Provider))]
        [Route("GetProviderWithUser/{providerID}")]
        public IHttpActionResult GetProviderWithUser(Int64 providerID)
        {

            ProviderBusinessOperation businessOperation = new ProviderBusinessOperation();
            Provider itemOut = null;
            BaseOutput baseOutput = businessOperation.GetProviderByID(providerID, out itemOut);
            if (baseOutput.ResultCode == 1)
            {
                return Ok(itemOut);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, baseOutput);
            }


        }

        [HttpGet]
        [ResponseType(typeof(Provider))]
        [Route("GetProviderByUserName/{username}")]
        public IHttpActionResult GetProviderByUserName(string username)
        {

            ProviderBusinessOperation businessOperation = new ProviderBusinessOperation();
            Provider itemOut = null;
            BaseOutput baseOutput = businessOperation.GetProviderByUserName(username, out itemOut);
            if (baseOutput.ResultCode == 1)
            {
                return Ok(itemOut);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, baseOutput);
            }

        }

        [HttpGet]
        [Route("GetProviderReportsByDatePeriod/{providerID}/{fromDate}/{toDate}")]
        public IHttpActionResult GetProviderReportsByDatePeriod(Int64 providerID, DateTime fromDate, DateTime toDate)
        {
            ProviderBusinessOperation businessOperation = new ProviderBusinessOperation();
            Search search = new Search
            {
                ProviderID = providerID,
                FromtDate = fromDate,
                ToDate = toDate,
            };
            ProviderReportDTO itemOut = null;
            BaseOutput baseOutput = businessOperation.GetProviderReportsByDatePeriod(search, out itemOut);

            if (baseOutput.ResultCode == 1)
            {
                return Ok(itemOut);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, baseOutput);
            }

        }

        [HttpGet]
        [Route("GetProviderReportsByYearAndMonths{providerID}/{year}")]
        public IHttpActionResult GetProviderReportsByYearAndMonths( Int64 providerID, int year, string months="")
        {
            ProviderBusinessOperation businessOperation = new ProviderBusinessOperation();
            Search search = new Search
            {
                ProviderID = providerID,
                Year = year,
                Months = months,
            };
            ProviderReportDTO itemOut = null;
            BaseOutput baseOutput = businessOperation.GetProviderReportsByYearAndMonths(search, out itemOut);

            if (baseOutput.ResultCode == 1)
            {
                return Ok(itemOut);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, baseOutput);
            }
        }

        [HttpPost]
        [ResponseType(typeof(Provider))]
        [Route("AddProviderWithUser")]
        public IHttpActionResult AddProviderWithUser(Provider item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ProviderBusinessOperation businessOperation = new ProviderBusinessOperation();
            Provider itemOut = null;
            BaseOutput baseOutput = businessOperation.AddProviderWithUser(item, out itemOut);
            if (baseOutput.ResultCode == 1)
            {
                return Ok(itemOut);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, baseOutput);
            }


        }

        [HttpPost]
        [ResponseType(typeof(tbl_Provider))]
        [Route("UpdateProvider")]
        public IHttpActionResult UpdateProvider(tbl_Provider item)
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
        public IHttpActionResult DeleteProvider(Int64 id)
        {

            ProviderBusinessOperation businessOperation = new ProviderBusinessOperation();
            tbl_Provider itemOut = null;
            BaseOutput baseOutput = businessOperation.DeleteProvider(id, out itemOut);
            if (baseOutput.ResultCode == 1)
            {
                return Ok(itemOut);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, baseOutput);
            }


        }


    }
}
