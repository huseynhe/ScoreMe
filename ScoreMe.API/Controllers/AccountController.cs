using ScoreMe.API.Attribute;
using ScoreMe.API.Models;
using ScoreMe.API.Utility;
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
        public string TestData() {
            return "Huseyn Hesenli";

        }

        [HttpGet]
        public HttpResponseMessage ValidLogin(string userName, string userPassword)
        {

            if (userName == "admin" && userPassword == "admin")
            {
                return Request.CreateResponse(HttpStatusCode.OK, value: TokenManager.GenerateToken(userName));
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, message: "Username and password is invalid");
            }
        }

        [HttpGet]
        [CustomAuthenticationFilter]
        public HttpResponseMessage GetEmployee()
        {
            return Request.CreateResponse(HttpStatusCode.OK, value: "Successfuly Valid");
        }

        [HttpPost]
        [CustomAuthenticationFilter]
        public HttpResponseMessage SaveUser(UserInfo userInfo)
        {
            if (userInfo.ID == 1)
            {
                return Request.CreateResponse(HttpStatusCode.OK, value: "Nəsib Həsənli");
            }
            else if (userInfo.ID == 2)
            {
                return Request.CreateResponse(HttpStatusCode.OK, value: "Yunus Həsənli");
            }
            return Request.CreateResponse(HttpStatusCode.OK, value: "Successfuly Valid");
        }
        [HttpPost]
        [CustomAuthenticationFilter]
        [ResponseType(typeof(UserInfo))]
        public async Task<IHttpActionResult> SaveUserInfo(UserInfo userInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            UserInfo user = null;
            if (userInfo.ID == 1)
            {
                user = new UserInfo()
                {
                    ID = 11,
                    UserName = "Huseyn ",
                    UserSurname = "Hesenli"
                };
                return Ok(user);
                // return CreatedAtRoute("DefaultApi", new { id = 11 }, user);
            }
            else
            {
                return NotFound();
            }



        }
    }
}
