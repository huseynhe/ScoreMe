using Microsoft.AspNetCore.Mvc;
using ScoreMe.API.Utility;
using ScoreMe.DAL;
using ScoreMe.DAL.DBModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ScoreMe.API.Controllers
{
    [RoutePrefix("api/DocumentOperation")]
    public class DocumentOperationController : ApiController
    {
        static string ServerPath = @"h:\root\home\huseyn89-003\www\site1\document";
        // 0) Action Method
        [HttpGet]
        [Route("GetLogo")]
        public HttpResponseMessage GetLogo(Int64 providerID)
        {
            var result =
                new HttpResponseMessage(HttpStatusCode.OK);
            CRUDOperation cRUDOperation = new CRUDOperation();
            tbl_Provider provider = cRUDOperation.GetProviderById(providerID);
            // 1) Get file bytes
            var fileBytes = File.ReadAllBytes(provider.LogoLinkPath);

            // 2) Add bytes to a memory stream
            var fileMemStream =
                new MemoryStream(fileBytes);

            // 3) Add memory stream to response
            result.Content = new StreamContent(fileMemStream);

            // 4) build response headers
            var headers = result.Content.Headers;

            headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment");
            headers.ContentDisposition.FileName = provider.LogoLinkName;

            headers.ContentType =
            //new MediaTypeHeaderValue("application/jpg");
            new MediaTypeHeaderValue("application/octet-stream");

            headers.ContentLength = fileMemStream.Length;

            return result;
        }
        [HttpGet]
        [Route("GetLogoByID")]
        public HttpResponseMessage GetLogoByID(Int64 providerID)
        {
            var result =
                new HttpResponseMessage(HttpStatusCode.OK);
            CRUDOperation cRUDOperation = new CRUDOperation();
            tbl_Provider provider = cRUDOperation.GetProviderById(providerID);
            // 1) Get file bytes
            var fileBytes = File.ReadAllBytes(provider.LogoLinkPath);

            // 2) Add bytes to a memory stream
            var fileMemStream =
                new MemoryStream(fileBytes);

            // 3) Add memory stream to response
            result.Content = new StreamContent(fileMemStream);

            // 4) build response headers
            var headers = result.Content.Headers;

            headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment");
            headers.ContentDisposition.FileName = provider.LogoLinkName;

            headers.ContentType =
                new MediaTypeHeaderValue("application/jpg");
            //new MediaTypeHeaderValue("application/octet-stream");

            headers.ContentLength = fileMemStream.Length;

            return result;
        }
        [HttpPost]
        [Route("UploadLogo")]
        public HttpResponseMessage UploadLogo()
        {
            Int64 providerID = HttpContext.Current.Request.Form["providerID"] == null ? 0 : Int64.Parse(HttpContext.Current.Request.Form["providerID"]);


            var file = HttpContext.Current.Request.Files.Count > 0 ?
                HttpContext.Current.Request.Files[0] : null;

            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                string logoPath = ServerPath + @"\logo";
                if (!Directory.Exists(logoPath))
                {
                    Directory.CreateDirectory(logoPath);
                }
                string fullPath = Path.Combine(logoPath, fileName);
                file.SaveAs(fullPath);
                tbl_Provider provider = new tbl_Provider()
                {
                    LogoLinkName = fileName,
                    LogoLinkPath = fullPath,
                    ID = providerID,
                    UpdateUser = 0
                };
                CRUDOperation cRUDOperation = new CRUDOperation();
                tbl_Provider providerDB = cRUDOperation.UpdateLogoPic(provider);
                if (providerDB != null)
                {
                    var message1 = string.Format("Image Updated Successfully.");
                    return Request.CreateResponse(HttpStatusCode.Created, message1);
                }

            }

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [Route("addFiles")]
        [AllowAnonymous]
        public HttpResponseMessage PostLogoImage()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {

                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {

                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {

                            var message = string.Format("Please Upload a file upto 1 mb.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {



                            var filePath = HttpContext.Current.Server.MapPath("~/Userimage/" + postedFile.FileName + extension);

                            postedFile.SaveAs(filePath);

                        }
                    }

                    var message1 = string.Format("Image Updated Successfully.");
                    return Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;
                }
                var res = string.Format("Please Upload a image.");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            catch (Exception ex)
            {
                var res = string.Format("some Message");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
        }


    }
}
