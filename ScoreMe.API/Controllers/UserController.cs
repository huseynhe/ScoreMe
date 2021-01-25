﻿using ScoreMe.Business;
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
using System.Web;
using System.IO;
using System.Net.Http.Headers;

namespace ScoreMe.API.Controllers
{

    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        static string ServerPath = @"h:\root\home\huseyn89-003\www\site1\document";
        [HttpGet]
        [Route("GetUsers")]
        public List<tbl_User> GetUsers()
        {
            CRUDOperation operation = new CRUDOperation();
            var users = operation.GetUsers(); ;
            return users;
        }

        [HttpGet]
        [Route("GetUserByID/{id}")]
        public tbl_User GetUserByID(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();
            var user = operation.GetUserById(id); ;
            return user;
        }

        [HttpGet]
        [Route("GetUserByUserName/{username}")]
        public tbl_User GetUserByUserName(string username)
        {
            CRUDOperation operation = new CRUDOperation();
            var user = operation.GetUserByUserName(username); ;
            return user;
        }
        [HttpPost]
        [ResponseType(typeof(tbl_User))]
        [Route("AddUser")]
        public IHttpActionResult AddUser(tbl_User item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CRUDOperation operation = new CRUDOperation();
            tbl_User dbitem = operation.AddUser(item);

            return Ok(dbitem);
        }

        [HttpPost]
        [ResponseType(typeof(tbl_User))]
        [Route("UpdateUser")]
        public IHttpActionResult UpdateUser(tbl_User item)
        {
            CRUDOperation operation = new CRUDOperation();
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                var dbitem = operation.UpdateUser(item);
                return Ok(dbitem);
            }
        }

        [HttpPost]
        [ResponseType(typeof(tbl_User))]
        [Route("DeleteUser/{id}")]
        public IHttpActionResult DeleteUser(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();

            var dbitem = operation.DeleteUser(id, 0);
            return Ok(dbitem);

        }

        [HttpPost]
        [ResponseType(typeof(tbl_User))]
        [Route("ChangeUserActivateStatus/{id}/{activateStatus}")]
        public IHttpActionResult ChangeUserActivateStatus(Int64 id,int activateStatus)
        {
            CRUDOperation operation = new CRUDOperation();

            var dbitem = operation.ActivateUser(id, 0, activateStatus);
            return Ok(dbitem);

        }

        [HttpPost]
        [ResponseType(typeof(tbl_User))]
        [Route("ChangePassword/{id}/{newpassword}")]
        public IHttpActionResult ChangePassword(Int64 id, string newpassword)
        {
            CRUDOperation operation = new CRUDOperation();

            var dbitem = operation.ChangePassword(id, 0, newpassword);
            return Ok(dbitem);

        }
        [HttpPost]
        [ResponseType(typeof(tbl_User))]
        [Route("ChangePasswordByUserName")]
        public IHttpActionResult ChangePasswordByUserName(UserInfo item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BusinessOperation businessOperation = new BusinessOperation();
            tbl_User itemOut = null;
            BaseOutput dbitem = businessOperation.ChangePasswordByUserName(item, 0, out itemOut);
            if (dbitem.ResultCode == 1)
            {
                return Ok(itemOut);
            }
            else
            {
                return BadRequest(dbitem.ResultCode + " : " + dbitem.ResultMessage);
            }

        }

        #region UserDocument
        [HttpPost]
        [Route("AddUserDocument")]
        public HttpResponseMessage AddUserDocument()
        {
            try
            {
                Int64 userID = HttpContext.Current.Request.Form["userID"] == null ? 0 : Int64.Parse(HttpContext.Current.Request.Form["userID"]);
                int imageType_EVID = HttpContext.Current.Request.Form["imageType_EVID"] == null ? 0 : int.Parse(HttpContext.Current.Request.Form["imageType_EVID"]);
                var httpRequest = HttpContext.Current.Request;
                int count = 0;
                int i = 0;
                foreach (string fileItem in httpRequest.Files)
                {
                    var file = HttpContext.Current.Request.Files.Count > 0 ?
                  HttpContext.Current.Request.Files[i] : null;
                    i = i + 1;
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        string imagePath = ServerPath + @"\UserDocument";
                        if (!Directory.Exists(imagePath))
                        {
                            Directory.CreateDirectory(imagePath);
                        }
                        string fullPath = Path.Combine(imagePath, fileName);
                        file.SaveAs(fullPath);
                        tbl_UserDocument userDocument = new tbl_UserDocument()
                        {
                            ImageLinkName = fileName,
                            ImageLinkPath = fullPath,
                            UserID = userID,
                            ImageType_EVID=imageType_EVID,
                        };
                        CRUDOperation cRUDOperation = new CRUDOperation();
                        tbl_UserDocument userDocumentDB = cRUDOperation.AddUserDocument(userDocument);
                        if (userDocumentDB != null)
                        {
                            count++;
                        }

                    }

                }
                if (count > 0)
                {
                    var message1 = string.Format("{0} Image added successfully.", count);
                    return Request.CreateResponse(HttpStatusCode.Created, message1);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }

            }
            catch (Exception ex)
            {

                var message2 = ex.Message;
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, message2);
            }
        }
        [HttpPost]
        [Route("UpdateUserDocument")]
        public HttpResponseMessage UpdateUserDocument()
        {
            Int64 documentID = HttpContext.Current.Request.Form["documentID"] == null ? 0 : Int64.Parse(HttpContext.Current.Request.Form["documentID"]);
          
            var httpRequest = HttpContext.Current.Request;

            foreach (string fileItem in httpRequest.Files)
            {
                var file = HttpContext.Current.Request.Files.Count > 0 ?
              HttpContext.Current.Request.Files[0] : null;

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    string imagePath = ServerPath + @"\UserDocument";
                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }
                    string fullPath = Path.Combine(imagePath, fileName);
                    file.SaveAs(fullPath);
                    tbl_UserDocument userDocument = new tbl_UserDocument()
                    {
                        ImageLinkName = fileName,
                        ImageLinkPath = fullPath,
                        ID = documentID,
                        UpdateUser = 0
                    };
                    CRUDOperation cRUDOperation = new CRUDOperation();
                    tbl_UserDocument userDocumentDB = cRUDOperation.UpdateUserDocument(userDocument);
                    if (userDocumentDB != null)
                    {
                        var message1 = string.Format("Image Updated Successfully.");
                        return Request.CreateResponse(HttpStatusCode.Created, message1);
                    }

                }

            }

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
        [HttpPost]
        [ResponseType(typeof(tbl_UserDocument))]
        [Route("DeleteUserDocument/{documentID}")]
        public IHttpActionResult DeleteUserDocument(Int64 documentID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CRUDOperation cRUDOperation = new CRUDOperation();
            tbl_UserDocument dbitem = cRUDOperation.DeleteUserDocument(documentID, 0);
            if (dbitem != null)
            {
                return Ok(dbitem);
            }
            else
            {
                return BadRequest("404" + " : " + "An error occurred while deleting the record ");
            }


        }
        [HttpGet]
        [Route("GetUserDocumentByID/{documentID}")]
        public HttpResponseMessage GetUserDocumentByID(Int64 documentID)
        {
            var result =
                new HttpResponseMessage(HttpStatusCode.OK);
            CRUDOperation cRUDOperation = new CRUDOperation();
            tbl_UserDocument document = cRUDOperation.GetUserDocumentByID(documentID);
            // 1) Get file bytes
            var fileBytes = File.ReadAllBytes(document.ImageLinkPath);

            // 2) Add bytes to a memory stream
            var fileMemStream =
                new MemoryStream(fileBytes);

            // 3) Add memory stream to response
            result.Content = new StreamContent(fileMemStream);

            // 4) build response headers
            var headers = result.Content.Headers;

            headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment");
            headers.ContentDisposition.FileName = document.ImageLinkName;

            headers.ContentType =
                new MediaTypeHeaderValue("application/jpg");
            //new MediaTypeHeaderValue("application/octet-stream");

            headers.ContentLength = fileMemStream.Length;

            return result;
        }

        [HttpGet]
        [Route("GetUserProfileImageByUserID/{userID}")]
        public HttpResponseMessage GetUserProfileImageByUserID(Int64 userID)
        {
            var result =
                new HttpResponseMessage(HttpStatusCode.OK);
            CRUDOperation cRUDOperation = new CRUDOperation();
            tbl_UserDocument document = cRUDOperation.GetUserDocumentsByUserIDAndImageTypeEVID(userID,22).FirstOrDefault();
            // 1) Get file bytes
            var fileBytes = File.ReadAllBytes(document.ImageLinkPath);

            // 2) Add bytes to a memory stream
            var fileMemStream =
                new MemoryStream(fileBytes);

            // 3) Add memory stream to response
            result.Content = new StreamContent(fileMemStream);

            // 4) build response headers
            var headers = result.Content.Headers;

            headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment");
            headers.ContentDisposition.FileName = document.ImageLinkName;

            headers.ContentType =
                new MediaTypeHeaderValue("application/jpg");
            //new MediaTypeHeaderValue("application/octet-stream");

            headers.ContentLength = fileMemStream.Length;

            return result;
        }
        [Route("GetUserDocumentsByUserID/{userID}")]
        public List<tbl_UserDocument> GetUserDocumentsByUserID(Int64 userID)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            List<tbl_UserDocument> documents = new List<tbl_UserDocument>();
            documents = cRUDOperation.GetUserDocumentsByUserID(userID);
            return documents;
        }
        [Route("GetUserDocumentsByUserIDAndImageTypeEVID/{userID}/{imageType_EVID}")]
        public List<tbl_UserDocument> GetUserDocumentsByUserIDAndImageTypeEVID(Int64 userID,int imageType_EVID)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            List<tbl_UserDocument> documents = new List<tbl_UserDocument>();
            documents = cRUDOperation.GetUserDocumentsByUserIDAndImageTypeEVID(userID,imageType_EVID);
            return documents;
        }
        #endregion
    }

}

