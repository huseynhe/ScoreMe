﻿using ScoreMe.API.Attribute;
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

        [HttpGet]
        [Route("GetProviderReportsByDatePeriod/{providerID}/{fromDate}/{toDate}")]
        public ProviderReportDTO GetProviderReportsByDatePeriod(Int64 providerID, DateTime fromDate, DateTime toDate)
        {
            Search search = new Search
            {
                ProviderID = providerID,
                FromtDate = fromDate,
                ToDate = toDate,
            };
            ProviderRepository repository = new ProviderRepository();
            var providerReportDTO = repository.SW_GetProviderReportsByDatePeriod(search);
            return providerReportDTO;
        }

        [HttpGet]
        [Route("GetProviderReportsByYearAndMonths{providerID}/{year}/{months}")]
        public ProviderReportDTO GetProviderReportsByYearAndMonths(Int64 providerID, int year, string months)
        {
           
            Search search = new Search
            {
                ProviderID = providerID,
                Year=year,
                Months = months,
            };
            ProviderRepository repository = new ProviderRepository();
            var providerReportDTO = repository.SW_GetProviderReportsByYearAndMonths(search);
            return providerReportDTO;
        }

        [HttpPost]
        [ResponseType(typeof(tbl_Provider))]
        [Route("AddProvider")]
        public IHttpActionResult AddProvider(tbl_Provider item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CRUDOperation cRUDOperation = new CRUDOperation();
            tbl_Provider provider = cRUDOperation.AddProvider(item);
            if (provider != null)
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
        public IHttpActionResult AddProviderWithUser(Provider item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ProviderBusinessOperation businessOperation = new ProviderBusinessOperation();
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
        [HttpGet]
        [ResponseType(typeof(Provider))]
        [Route("GetProviderWithUser/{providerID}")]
        public IHttpActionResult GetProviderWithUser(Int64 providerID)
        {

            ProviderBusinessOperation businessOperation = new ProviderBusinessOperation();
            Provider itemOut = null;
            BaseOutput dbitem = businessOperation.GetProviderByID(providerID, out itemOut);
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
            BaseOutput dbitem = businessOperation.DeleteProvider(id, out itemOut);
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
        [ResponseType(typeof(Provider))]
        [Route("GetProviderByUserName/{username}")]
        public IHttpActionResult GetProviderByUserName(string username)
        {

            ProviderBusinessOperation businessOperation = new ProviderBusinessOperation();
            Provider itemOut = null;
            BaseOutput dbitem = businessOperation.GetProviderByUserName(username, out itemOut);
            if (dbitem.ResultCode == 1)
            {
                return Ok(itemOut);
            }
            else
            {
                return BadRequest(dbitem.ResultCode + " : " + dbitem.ResultMessage);
            }

        }
    }
}
