using ScoreMe.API.Attribute;
using ScoreMe.API.Models;
using ScoreMe.API.Utility;
using ScoreMe.DAL;
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
    public class AccountController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage ValidLogin(string userName, string userPassword)
        {
            try
            {
                CRUDOperation cRUDOperation = new CRUDOperation();
                tbl_User validUser = cRUDOperation.ValidLogin(userName, userPassword);

                if (validUser != null)
                {

                    return Request.CreateResponse(HttpStatusCode.OK, value: TokenManager.GenerateToken(userName));
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, message: "Username and password is invalid");
                }
            }
            catch (Exception)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, message: "Username and password is invalid");
            }

        }

    }
}
