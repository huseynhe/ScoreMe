using ScoreMe.API.Models;
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

namespace ScoreMe.API.Controllers
{
    [RoutePrefix("api/customer")]
    public class CustomerController : ApiController
    {
        CustomerBusinessOperation businessOperation = new CustomerBusinessOperation();
        [HttpGet]
        [Route("GetCustomers")]
        public IHttpActionResult GetCustomers()
        {
            List<tbl_Customer> itemsOut = null;
            BaseOutput baseOutput = businessOperation.GetCustomers(out itemsOut);
            if (baseOutput.ResultCode == 1)
            {
                return Ok(itemsOut);
            }
            else if (baseOutput.ResultCode == 5)
            {
                return Content(HttpStatusCode.NotFound, baseOutput);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, baseOutput);
            }
        }

        [HttpGet]
        [Route("GetCustomerByID/{id}")]
        public IHttpActionResult GetCustomerByID(Int64 id)
        {

            tbl_Customer itemOut = null;
            BaseOutput baseOutput = businessOperation.GetCustomerByID(id, out itemOut);
            if (baseOutput.ResultCode == 1)
            {
                return Ok(itemOut);
            }
            else if (baseOutput.ResultCode == 5)
            {
                return Content(HttpStatusCode.NotFound, baseOutput);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, baseOutput);
            }

        }

        [HttpGet]
        [Route("GetCustomerByUserID/{userid}")]
        public IHttpActionResult GetCustomerByUserID(Int64 userid)
        {
            tbl_Customer itemOut = null;
            BaseOutput baseOutput = businessOperation.GetCustomerByUserID(userid, out itemOut);
            if (baseOutput.ResultCode == 1)
            {
                return Ok(itemOut);
            }
            else if (baseOutput.ResultCode == 5)
            {
                return Content(HttpStatusCode.NotFound, baseOutput);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, baseOutput);
            }
        }

        [HttpGet]
        [Route("GetCustomerByUserName/{username}")]
        public IHttpActionResult GetCustomerByUserName(string username)
        {
            tbl_Customer itemOut = null;
            BaseOutput baseOutput = businessOperation.GetCustomerByUserName(username, out itemOut);
            if (baseOutput.ResultCode == 1)
            {
                return Ok(itemOut);
            }
            else if (baseOutput.ResultCode == 5)
            {
                return Content(HttpStatusCode.NotFound, baseOutput);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, baseOutput);
            }
        }
        [HttpPost]
        [Route("AddCustomer")]
        public IHttpActionResult AddCustomer(tbl_Customer item)
        {
            tbl_Customer itemOut = null;
            BaseOutput baseOutput = businessOperation.AddCustomer(item, out itemOut);
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
        [ResponseType(typeof(Customer))]
        [Route("AddCustomerWithUser")]
        public IHttpActionResult AddCustomerWithUser(Customer item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Customer itemOut = null;
            BaseOutput baseOutput = businessOperation.AddCustomerWithUser(item, out itemOut);
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
        [ResponseType(typeof(tbl_Customer))]
        [Route("UpdateCustomer")]
        public IHttpActionResult UpdateCustomer(tbl_Customer item)
        {
            CRUDOperation operation = new CRUDOperation();
            tbl_Customer itemOut = null;
            BaseOutput baseOutput = businessOperation.UpdateCustomer(item, out itemOut);
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
        [ResponseType(typeof(tbl_Customer))]
        [Route("DeleteCustomer/{id}")]
        public IHttpActionResult DeleteCustomer(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();
            tbl_Customer itemOut = null;
            BaseOutput baseOutput = businessOperation.DeleteCustomer(id, out itemOut);
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
