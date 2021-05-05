using ScoreMe.API.Attribute;
using ScoreMe.API.Models;
using ScoreMe.API.Utility;
using ScoreMe.Business;
using ScoreMe.DAL;
using ScoreMe.DAL.CodeObjects;
using ScoreMe.DAL.DBModel;
using ScoreMe.DAL.Enum;
using ScoreMe.DAL.ErrorManagment;
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

        //[HttpGet]
        //public HttpResponseMessage ValidLogin(string userName, string userPassword)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(userPassword))
        //        {

        //            return Request.CreateResponse(HttpStatusCode.BadRequest);
        //        }
        //        BusinessOperation businessOperation = new BusinessOperation();
        //        tbl_User itemOut = null;
        //        BaseOutput dbitem = businessOperation.ValidLogin(userName, userPassword, out itemOut);
        //        if (dbitem.ResultCode == 1)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.OK, value: TokenManager.GenerateToken(userName));

        //        }
        //        else
        //        {
        //            return Request.CreateResponse(HttpStatusCode.BadRequest, value: dbitem.ResultCode + " : " + dbitem.ResultMessage);
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        return Request.CreateResponse(HttpStatusCode.BadRequest);
        //    }

        //}
        public IHttpActionResult ValidLogin(string userName, string userPassword)
        {
            BaseOutput baseOutput = null;
            try
            {
                if (string.IsNullOrEmpty(userName))
                {
                    baseOutput = new BaseOutput()
                    {
                        Status = false,
                        ResultCode = CustomError.EmptyUserNameErrorCode,
                        ResultMessage = CustomError.EmptyUserNameErrorDesc,
                    };
                    return Content(HttpStatusCode.BadRequest, baseOutput);

                }
                if (string.IsNullOrEmpty(userPassword))
                {
                    baseOutput = new BaseOutput()
                    {
                        Status = false,
                        ResultCode = CustomError.EmptyUserPasswordErrorCode,
                        ResultMessage = CustomError.EmptyUserPasswordErrorDesc,
                    };
                    return Content(HttpStatusCode.BadRequest, baseOutput);
                }
                BusinessOperation businessOperation = new BusinessOperation();
                tbl_User itemOut = null;
                baseOutput = businessOperation.ValidLogin(userName, userPassword, out itemOut);
                if (baseOutput.ResultCode == 1)
                {
                    return Ok(TokenManager.GenerateToken(userName));


                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, baseOutput);

                }

            }
            catch (Exception ex)
            {

                baseOutput = new BaseOutput()
                {
                    Status = false,
                    ResultCode = BOResultTypes.Danger.GetHashCode(),
                    ResultMessage = BOBaseOutputResponse.DangerResponse,
                    ViewString = ex.Message
                };
                return Content(HttpStatusCode.BadRequest, baseOutput);
            }

        }

    }
}
