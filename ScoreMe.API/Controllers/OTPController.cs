
using ScoreMe.Business;
using ScoreMe.DAL;
using ScoreMe.DAL.CodeObjects;
using ScoreMe.DAL.DBModel;
using ScoreMe.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ScoreMe.API.Controllers
{
    [RoutePrefix("api/otp")]
    public class OTPController : ApiController
    {
        [HttpPost]
        [Route("GenarateOTP/{username}")]
        public IHttpActionResult GenarateOTP(string username)
        {            
            OTPOperation businessOperation = new OTPOperation();
            string itemOut = string.Empty;
            BaseOutput baseOutput = businessOperation.GenarateOTP(username, out itemOut);
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
        [Route("VerifyOTP/{username}/{otptext}")]
        public IHttpActionResult VerifyOTP(string username,string otptext)
        {

            OTPOperation businessOperation = new OTPOperation();
            bool itemOut = false;
            BaseOutput baseOutput = businessOperation.VerifyOTP(username, otptext, out itemOut);
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
        [Route("GenarateOTPByNumber/{phoneNumber}")]
        public IHttpActionResult GenarateOTPUseNumber(string phoneNumber)
        {


            OTPOperation businessOperation = new OTPOperation();
            string itemOut = string.Empty;
            BaseOutput baseOutput = businessOperation.GenarateOTPByNumber(phoneNumber, out itemOut);
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
        [Route("VerifyOTPByNumber/{phoneNumber}/{otptext}")]
        public IHttpActionResult VerifyOTPUseNumber(string phoneNumber, string otptext)
        {

            OTPOperation businessOperation = new OTPOperation();
            bool itemOut = false;
            BaseOutput baseOutput = businessOperation.VerifyOTPByNumber(phoneNumber, otptext, out itemOut);
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
