using ScoreMe.API.Models;
using ScoreMe.Business;
using ScoreMe.Business.Model;
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
        [HttpGet]
        [Route("GetCustomers")]
        public List<tbl_Customer> GetCustomers()
        {
            CRUDOperation operation = new CRUDOperation();
            var customers = operation.GetCustomers(); ;
            return customers;
        }

        [HttpGet]
        [Route("GetCustomerByID/{id}")]
        public tbl_Customer GetCustomerByID(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();
            var customer = operation.GetCustomerById(id); ;
            return customer;
        }
        [HttpGet]
        [Route("GetCustomerByUserID/{userid}")]
        public tbl_Customer GetCustomerByUserID(Int64 userid)
        {
            CRUDOperation operation = new CRUDOperation();
            var customer = operation.GetCustomerByUserId(userid); ;
            return customer;
        }

        [HttpGet]
        [Route("GetCustomerByUserName/{username}")]
        public tbl_Customer GetCustomerByUserName(string username)
        {
            CRUDOperation operation = new CRUDOperation();
            var customer = operation.GetCustomerByUserName(username);
            return customer;
        }
        [HttpPost]
        [ResponseType(typeof(tbl_Customer))]
        [Route("AddCustomer")]
        public async Task<IHttpActionResult> AddCustomer(tbl_Customer item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CRUDOperation operation = new CRUDOperation();
            tbl_Customer dbitem = operation.AddCustomer(item);

            return Ok(dbitem);
        }

        [HttpPost]
        [ResponseType(typeof(Customer))]
        [Route("AddCustomerWithUser")]
        public async Task<IHttpActionResult> AddCustomerWithUser(Customer item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BusinessOperation businessOperation = new BusinessOperation();
            Customer itemOut = null;
            BaseOutput dbitem = businessOperation.AddCustomerWithUser(item, out itemOut);
            if (dbitem.ResultCode==1)
            {
                return Ok(itemOut);
            }
            else
            {
                return BadRequest(dbitem.ResultCode + " : " + dbitem.ResultMessage);
            }

      
        }

        [HttpPost]
        [ResponseType(typeof(tbl_Customer))]
        [Route("UpdateCustomer")]
        public async Task<IHttpActionResult> UpdateCustomer(tbl_Customer item)
        {
            CRUDOperation operation = new CRUDOperation();
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                var dbitem = operation.UpdateCustomer(item);
                return Ok(dbitem);
            }
        }

        [HttpPost]
        [ResponseType(typeof(tbl_Customer))]
        [Route("DeleteCustomer/{id}")]
        public async Task<IHttpActionResult> DeleteCustomer(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();

            var dbitem = operation.DeleteCustomer(id, 0);
            return Ok(dbitem);

        }
    }
}
