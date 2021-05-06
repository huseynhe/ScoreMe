using ScoreMe.Business;
using ScoreMe.DAL;
using ScoreMe.DAL.CodeObjects;
using ScoreMe.DAL.DBModel;
using ScoreMe.DAL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace ScoreMe.API.Controllers
{
    [RoutePrefix("api/proposal")]
    public class ProposalController : ApiController
    {
        static string ServerPath = @"h:\root\home\huseyn89-003\www\site1\document";
        //static string ServerPath = @"D:\GitProject\ScoreMe\images";

        #region ProposalGroup

        [HttpGet]
        [Route("GetProposalUserGroups")]
        public IHttpActionResult GetProposalUserGroups()
        {
            ProposalBusinessOperation businessOperation = new ProposalBusinessOperation();
            List<tbl_ProposalUserGroup> itemsOut = null;
            BaseOutput baseOutput = businessOperation.GetProposalUserGroups(out itemsOut);
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
        [Route("GetProposalsByGroupID/{groupid}")]
        public IHttpActionResult GetProposalsByGroupID(Int64 groupid)
        {
            ProposalBusinessOperation businessOperation = new ProposalBusinessOperation();
            List<tbl_Proposal> itemsOut = null;
            BaseOutput baseOutput = businessOperation.GetProposalsByGroupID(groupid, out itemsOut);
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
        [Route("GetGroupsByPropsalID/{propsalid}")]
        public IHttpActionResult GetGroupsByPropsalID(Int64 propsalid)
        {
            ProposalBusinessOperation businessOperation = new ProposalBusinessOperation();
            List<tbl_Group> itemsOut = null;
            BaseOutput baseOutput = businessOperation.GetGroupsByPropsalID(propsalid, out itemsOut);
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
        [HttpPost]
        [ResponseType(typeof(tbl_ProposalUserGroup))]
        [Route("AddProposalUserGroup")]
        public IHttpActionResult AddProposalUserGroup(tbl_ProposalUserGroup item)
        {
            ProposalBusinessOperation businessOperation = new ProposalBusinessOperation();
            tbl_ProposalUserGroup itemOut = null;
            BaseOutput baseOutput = businessOperation.AddProposalUserGroup(item, out itemOut);
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
        [ResponseType(typeof(tbl_ProposalUserGroup))]
        [Route("UpdateProposalUserGroup")]
        public IHttpActionResult UpdateProposalUserGroup(tbl_ProposalUserGroup item)
        {
            ProposalBusinessOperation businessOperation = new ProposalBusinessOperation();
            tbl_ProposalUserGroup itemOut = null;
            BaseOutput baseOutput = businessOperation.UpdateProposalUserGroup(item, out itemOut);
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
        [ResponseType(typeof(tbl_ProposalUserGroup))]
        [Route("DeleteProposalUserGroup/{id}")]
        public IHttpActionResult DeleteProposalUserGroup(Int64 id)
        {
            ProposalBusinessOperation businessOperation = new ProposalBusinessOperation();
            tbl_ProposalUserGroup itemOut = null;
            BaseOutput baseOutput = businessOperation.DeleteProposalUserGroup(id, out itemOut);
            if (baseOutput.ResultCode == 1)
            {
                return Ok(itemOut);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, baseOutput);
            }

        }
        #endregion

        [HttpPost]
        [ResponseType(typeof(Proposal))]
        [Route("AddProposalWithDetail")]
        public IHttpActionResult AddProposalWithDetail(Proposal item)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ProposalBusinessOperation businessOperation = new ProposalBusinessOperation();
            Proposal itemOut = null;
            BaseOutput dbitem = businessOperation.AddProposalWithDetail(item, out itemOut);

            if (dbitem.ResultCode == 1)
            {
                return Ok(itemOut);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, dbitem);
            }


        }
        [HttpPost]
        [ResponseType(typeof(Proposal))]
        [Route("AddProposalWithDetailNew")]
        public IHttpActionResult AddProposalWithDetailNew(Proposal item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ProposalBusinessOperation businessOperation = new ProposalBusinessOperation();

            BaseOutput dbitem = businessOperation.AddProposalWithDetailNew(item);
            if (dbitem.ResultCode == 1)
            {
                return Ok(dbitem);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, dbitem);
            }


        }
        [HttpGet]
        [ResponseType(typeof(Proposal))]
        [Route("GetProposalWithDetailsByID/{id}")]
        public IHttpActionResult GetProposalWithDetailsByID(Int64 id)
        {
            ProposalBusinessOperation businessOperation = new ProposalBusinessOperation();
            Proposal itemOut = null;
            BaseOutput dbitem = businessOperation.GetProposalByID(id, out itemOut);
            if (dbitem.ResultCode == 1)
            {
                return Ok(itemOut);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, dbitem);
            }
        }

        [HttpGet]
        [Route("GetProposalWithDetails")]
        public IHttpActionResult GetProposalWithDetails()
        {
            ProposalBusinessOperation businessOperation = new ProposalBusinessOperation();
            List<Proposal> itemsOut = null;
            BaseOutput dbitem = businessOperation.GetProposals(out itemsOut);
            if (dbitem.ResultCode == 1)
            {
                return Ok(itemsOut);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, dbitem);
            }
        }
        [HttpGet]
        [Route("GetProposalWithDetailsByProviderID/{providerid}")]
        public IHttpActionResult GetProposalWithDetailsByProviderID(Int64 providerid)
        {
            ProposalBusinessOperation businessOperation = new ProposalBusinessOperation();
            List<Proposal> itemsOut = null;
            BaseOutput dbitem = businessOperation.GetProposalsByProviderID(providerid, out itemsOut);
            if (dbitem.ResultCode == 1)
            {

                return Ok(itemsOut);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, dbitem);

            }
        }
        [HttpGet]
        [Route("GetProposalWithDetailAndStatesByProviderID/{providerid}")]
        public IHttpActionResult GetProposalWithDetailAndStatesByProviderID(Int64 providerid)
        {
            ProposalBusinessOperation businessOperation = new ProposalBusinessOperation();
            List<Proposal> itemsOut = null;
            BaseOutput dbitem = businessOperation.GetProposalsWithStateByProviderID(providerid, out itemsOut);
            if (dbitem.ResultCode == 1)
            {
                return Ok(itemsOut);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, dbitem);
            }
        }
        [HttpGet]
        [Route("GetProposalWithDetailsByUserName/{username}")]
        public IHttpActionResult GetProposalWithDetailsByUserName(string username)
        {
            ProposalBusinessOperation businessOperation = new ProposalBusinessOperation();
            List<Proposal> itemsOut = null;
            BaseOutput dbitem = businessOperation.GetProposalsByUserName(username, out itemsOut);
            if (dbitem.ResultCode == 1)
            {
                return Ok(itemsOut);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, dbitem);
            }
        }
        [HttpGet]
        [Route("GetFavoriteProposalWithDetailsByUserName/{username}")]
        public IHttpActionResult GetFavoriteProposalWithDetailsByUserName(string username)
        {
            ProposalBusinessOperation businessOperation = new ProposalBusinessOperation();
            List<Proposal> itemsOut = null;
            BaseOutput dbitem = businessOperation.GetFavoriteProposalsByUserName(username, out itemsOut);
            if (dbitem.ResultCode == 1)
            {
                return Ok(itemsOut);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, dbitem);
            }
        }
        [HttpGet]
        [Route("GetProposalWithDetailsByIsPublic/{username}")]
        public IHttpActionResult GetProposalWithDetailsByIsPublic(string username)
        {
            ProposalBusinessOperation businessOperation = new ProposalBusinessOperation();
            List<Proposal> itemsOut = null;
            BaseOutput dbitem = businessOperation.GetProposalWithDetailsByIsPublic(username, out itemsOut);
            if (dbitem.ResultCode == 1)
            {
                return Ok(itemsOut);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, dbitem);
            }
        }
        [HttpPost]
        [ResponseType(typeof(Proposal))]
        [Route("UpdateProposalWithDetail")]
        public IHttpActionResult UpdateProposalWithDetail(Proposal item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ProposalBusinessOperation businessOperation = new ProposalBusinessOperation();

            BaseOutput dbitem = businessOperation.UpdateProposalWithDetail(item);
            if (dbitem.ResultCode == 1)
            {
                return Ok(dbitem);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, dbitem);
            }


        }

        [HttpPost]
        [ResponseType(typeof(Proposal))]
        [Route("DeleteProposalWithDetail")]
        public IHttpActionResult DeleteProposalWithDetail(Int64 id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ProposalBusinessOperation businessOperation = new ProposalBusinessOperation();

            BaseOutput dbitem = businessOperation.DeleteProposalWithDetail(id);
            if (dbitem.ResultCode == 1)
            {
                return Ok(dbitem);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, dbitem);
            }


        }

        #region ProposalUserState
        [HttpGet]
        [Route("GetProposalUserStates")]
        public IHttpActionResult GetProposalUserStates()
        {
            ProposalBusinessOperation businessOperation = new ProposalBusinessOperation();
            List<tbl_ProposalUserState> itemsOut = null;
            BaseOutput baseOutput = businessOperation.GetProposalUserStates(out itemsOut);
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
        [Route("GetProposalUserStateByID/{id}")]
        public IHttpActionResult GetProposalUserStateByID(Int64 id)
        {
            ProposalBusinessOperation businessOperation = new ProposalBusinessOperation();
            tbl_ProposalUserState itemOut = null;
            BaseOutput baseOutput = businessOperation.GetProposalUserStateByID(id, out itemOut);
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
        [ResponseType(typeof(tbl_ProposalUserState))]
        [Route("AddProposalUserState")]
        public IHttpActionResult AddProposalUserState(tbl_ProposalUserState item)
        {
            ProposalBusinessOperation businessOperation = new ProposalBusinessOperation();
            tbl_ProposalUserState itemOut = null;
            BaseOutput baseOutput = businessOperation.AddProposalUserState(item, out itemOut);
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
        [ResponseType(typeof(tbl_ProposalUserState))]
        [Route("UpdateProposalUserState")]
        public IHttpActionResult UpdateProposalUserState(tbl_ProposalUserState item)
        {
            ProposalBusinessOperation businessOperation = new ProposalBusinessOperation();
            tbl_ProposalUserState itemOut = null;
            BaseOutput baseOutput = businessOperation.UpdateProposalUserState(item, out itemOut);
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
        [ResponseType(typeof(tbl_ProposalUserState))]
        [Route("DeleteProposalUserState/{id}")]
        public IHttpActionResult DeleteProposalUserState(Int64 id)
        {
            ProposalBusinessOperation businessOperation = new ProposalBusinessOperation();
            tbl_ProposalUserState itemOut = null;
            BaseOutput baseOutput = businessOperation.DeleteProposalUserState(id, out itemOut);
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
        [Route("GetProposalUserStatesByProposalID/{proposalID}")]
        public IHttpActionResult GetProposalUserStatesByProposalID(Int64 proposalID)
        {
            ProposalBusinessOperation businessOperation = new ProposalBusinessOperation();
            List<tbl_ProposalUserState> itemsOut = null;
            BaseOutput dbitem = businessOperation.GetProposalUserStatesByProposalID(proposalID, out itemsOut);
            if (dbitem.ResultCode == 1)
            {
                return Ok(itemsOut);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, dbitem);
            }
        }
        [HttpGet]
        [Route("GetProposalUserStatesByUserID/{userID}")]
        public IHttpActionResult GetProposalUserStatesByUserID(Int64 userID)
        {
            ProposalBusinessOperation businessOperation = new ProposalBusinessOperation();
            List<tbl_ProposalUserState> itemsOut = null;
            BaseOutput dbitem = businessOperation.GetProposalUserStatesByUserID(userID, out itemsOut);
            if (dbitem.ResultCode == 1)
            {
                return Ok(itemsOut);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, dbitem);
            }
        }
        [HttpGet]
        [Route("GetProposalUserStatesByProviderStateType/{providerStateType}")]
        public IHttpActionResult GetProposalUserStatesByProviderStateType(Int64 providerStateType)
        {
            ProposalBusinessOperation businessOperation = new ProposalBusinessOperation();
            List<tbl_ProposalUserState> itemsOut = null;
            BaseOutput dbitem = businessOperation.GetProposalUserStatesByProviderStateType(providerStateType, out itemsOut);
            if (dbitem.ResultCode == 1)
            {
                return Ok(itemsOut);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, dbitem);
            }
        }
        [HttpGet]
        [Route("GetProposalUserStatesByUserStateType/{userStateType}")]
        public IHttpActionResult GetProposalUserStatesByUserStateType(Int64 userStateType)
        {
            ProposalBusinessOperation businessOperation = new ProposalBusinessOperation();
            List<tbl_ProposalUserState> itemsOut = null;
            BaseOutput dbitem = businessOperation.GetProposalUserStatesByUserStateType(userStateType, out itemsOut);
            if (dbitem.ResultCode == 1)
            {
                return Ok(itemsOut);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, dbitem);
            }
        }

        #endregion

        #region ProposalDocument
        [HttpPost]
        [Route("AddProposalDocument")]
        public HttpResponseMessage AddProposalDocument()
        {
            try
            {
                Int64 proposalID = HttpContext.Current.Request.Form["proposalID"] == null ? 0 : Int64.Parse(HttpContext.Current.Request.Form["proposalID"]);
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
                        string imagePath = ServerPath + @"\ProposalDocument";
                        if (!Directory.Exists(imagePath))
                        {
                            Directory.CreateDirectory(imagePath);
                        }
                        string fullPath = Path.Combine(imagePath, fileName);
                        file.SaveAs(fullPath);
                        tbl_ProposalDocument proposalDocument = new tbl_ProposalDocument()
                        {
                            ImageLinkName = fileName,
                            ImageLinkPath = fullPath,
                            ProposalID = proposalID,
                        };
                        CRUDOperation cRUDOperation = new CRUDOperation();
                        tbl_ProposalDocument proposalDocumentDB = cRUDOperation.AddProposalDocument(proposalDocument);
                        if (proposalDocumentDB != null)
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
        [Route("UpdateProposalDocument")]
        public HttpResponseMessage UpdateProposalDocument()
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
                    string imagePath = ServerPath + @"\ProposalDocument";
                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }
                    string fullPath = Path.Combine(imagePath, fileName);
                    file.SaveAs(fullPath);
                    tbl_ProposalDocument proposalDocument = new tbl_ProposalDocument()
                    {
                        ImageLinkName = fileName,
                        ImageLinkPath = fullPath,
                        ID = documentID,
                        UpdateUser = 0
                    };
                    CRUDOperation cRUDOperation = new CRUDOperation();
                    tbl_ProposalDocument proposalDocumentDB = cRUDOperation.UpdateProposalDocument(proposalDocument);
                    if (proposalDocumentDB != null)
                    {
                        var message1 = string.Format("Image Updated Successfully.");
                        return Request.CreateResponse(HttpStatusCode.Created, message1);
                    }

                }

            }

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
        [HttpPost]
        [ResponseType(typeof(tbl_ProposalDocument))]
        [Route("DeleteProposalDocument/{documentID}")]
        public IHttpActionResult DeleteProposalDocument(Int64 documentID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CRUDOperation cRUDOperation = new CRUDOperation();
            tbl_ProposalDocument dbitem = cRUDOperation.DeleteProposalDocument(documentID, 0);
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
        [Route("GetProposalDocumentByID/{documentID}")]
        public HttpResponseMessage GetProposalDocumentByID(Int64 documentID)
        {
            var result =
                new HttpResponseMessage(HttpStatusCode.OK);
            CRUDOperation cRUDOperation = new CRUDOperation();
            tbl_ProposalDocument document = cRUDOperation.GetProposalDocumentByID(documentID);
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
        [Route("GetProposalDocumentsByProposalID/{proposalID}")]
        public List<tbl_ProposalDocument> GetProposalDocumentsByProposalID(Int64 proposalID)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            List<tbl_ProposalDocument> documents = new List<tbl_ProposalDocument>();
            documents = cRUDOperation.GetProposalDocumentsByProposalID(proposalID);
            return documents;
        }
        #endregion
    }
}
