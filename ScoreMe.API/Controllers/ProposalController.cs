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
        [HttpGet]
        [Route("GetProposals")]
        public List<tbl_Proposal> GetProposals()
        {
            CRUDOperation operation = new CRUDOperation();
            var proposals = operation.GetProposals(); ;
            return proposals;
        }

        [HttpGet]
        [Route("GetProposalByID/{id}")]
        public tbl_Proposal GetProposalByID(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();
            var proposal = operation.GetProposalById(id); ;
            return proposal;
        }

        [HttpPost]
        [ResponseType(typeof(tbl_Proposal))]
        [Route("AddProposal")]
        public IHttpActionResult AddProposal(tbl_Proposal item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CRUDOperation operation = new CRUDOperation();
            tbl_Proposal dbitem = operation.AddProposal(item);

            return Ok(dbitem);
        }

        [HttpPost]
        [ResponseType(typeof(tbl_Proposal))]
        [Route("UpdateProposal")]
        public IHttpActionResult UpdateProposal(tbl_Proposal item)
        {
            CRUDOperation operation = new CRUDOperation();
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                var dbitem = operation.UpdateProposal(item);
                return Ok(dbitem);
            }
        }

        [HttpPost]
        [ResponseType(typeof(tbl_Proposal))]
        [Route("DeleteProposal/{id}")]
        public IHttpActionResult DeleteProposal(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();

            var dbitem = operation.DeleteProposal(id, 0);
            return Ok(dbitem);

        }

        [HttpGet]
        [Route("GetProposalDetails")]
        public List<tbl_ProposalDetail> GetProposalDetails()
        {
            CRUDOperation operation = new CRUDOperation();
            var proposalDetails = operation.GetProposalDetails(); ;
            return proposalDetails;
        }

        [HttpGet]
        [Route("GetProposalDetailByID/{id}")]
        public tbl_ProposalDetail GetProposalDetailByID(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();
            var proposalDetail = operation.GetProposalDetailByID(id); ;
            return proposalDetail;
        }

        [HttpPost]
        [ResponseType(typeof(tbl_ProposalDetail))]
        [Route("AddProposalDetail")]
        public IHttpActionResult AddProposalDetail(tbl_ProposalDetail item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CRUDOperation operation = new CRUDOperation();
            tbl_ProposalDetail dbitem = operation.AddProposalDetail(item);

            return Ok(dbitem);
        }

        [HttpPost]
        [ResponseType(typeof(tbl_ProposalDetail))]
        [Route("UpdateProposalDetail")]
        public IHttpActionResult UpdateProposalDetail(tbl_ProposalDetail item)
        {
            CRUDOperation operation = new CRUDOperation();
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                var dbitem = operation.UpdateProposalDetail(item);
                return Ok(dbitem);
            }
        }

        [HttpPost]
        [ResponseType(typeof(tbl_ProposalDetail))]
        [Route("DeleteProposalDetail/{id}")]
        public IHttpActionResult DeleteProposalDetail(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();

            var dbitem = operation.DeleteProposalDetail(id, 0);
            return Ok(dbitem);

        }

        #region ProposalGroup

        [HttpGet]
        [Route("GetProposalUserGroups")]
        public List<tbl_ProposalUserGroup> GetProposalUserGroups()
        {
            CRUDOperation operation = new CRUDOperation();
            var proposalusergroups = operation.GetProposalUserGroups(); ;
            return proposalusergroups;
        }
        [HttpGet]
        [Route("GetProposalsByGroupID/{groupid}")]
        public List<tbl_Proposal> GetProposalsByGroupID(Int64 groupid)
        {
            CRUDOperation operation = new CRUDOperation();
            var proposals = operation.GetProposalsByGroupID(groupid); ;
            return proposals;
        }
        [HttpGet]
        [Route("GetGroupsByPropsalID/{propsalid}")]
        public List<tbl_Group> GetGroupsByPropsalID(Int64 propsalid)
        {
            CRUDOperation operation = new CRUDOperation();
            var groups = operation.GetGroupsByPropsalID(propsalid); ;
            return groups;
        }
        [HttpPost]
        [ResponseType(typeof(tbl_ProposalUserGroup))]
        [Route("AddProposalUserGroup")]
        public IHttpActionResult AddProposalUserGroup(tbl_ProposalUserGroup item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CRUDOperation operation = new CRUDOperation();
            tbl_ProposalUserGroup dbitem = operation.AddProposalUserGroup(item);

            return Ok(dbitem);
        }
        [HttpPost]
        [ResponseType(typeof(tbl_ProposalUserGroup))]
        [Route("UpdateProposalUserGroup")]
        public IHttpActionResult UpdateUserGroup(tbl_ProposalUserGroup item)
        {
            CRUDOperation operation = new CRUDOperation();
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                var dbitem = operation.UpdateProposalUserGroup(item);
                return Ok(dbitem);
            }
        }

        [HttpPost]
        [ResponseType(typeof(tbl_ProposalUserGroup))]
        [Route("DeleteProposalUserGroup/{id}")]
        public IHttpActionResult DeleteUserGroup(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();

            var dbitem = operation.DeleteProposalUserGroup(id, 0);
            return Ok(dbitem);

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
            BusinessOperation businessOperation = new BusinessOperation();
            Proposal itemOut = null;
            BaseOutput dbitem = businessOperation.AddProposalWithDetail(item,out itemOut);
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
        [ResponseType(typeof(Proposal))]
        [Route("AddProposalWithDetailNew")]
        public IHttpActionResult AddProposalWithDetailNew(Proposal item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BusinessOperation businessOperation = new BusinessOperation();

            BaseOutput dbitem = businessOperation.AddProposalWithDetailNew(item);
            if (dbitem.ResultCode == 1)
            {
                return Ok(dbitem);
            }
            else
            {
                return BadRequest(dbitem.ResultCode + " : " + dbitem.ResultMessage);
            }


        }
        [HttpGet]
        [ResponseType(typeof(Proposal))]
        [Route("GetProposalWithDetailsByID/{id}")]
        public IHttpActionResult GetProposalWithDetailsByID(Int64 id)
        {
            BusinessOperation businessOperation = new BusinessOperation();
            Proposal itemOut = null;
            BaseOutput dbitem = businessOperation.GetProposalByID(id, out itemOut);
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
        [Route("GetProposalWithDetails")]
        public List<Proposal> GetProposalWithDetails()
        {
            BusinessOperation businessOperation = new BusinessOperation();
            List<Proposal> itemsOut = null;
            BaseOutput dbitem = businessOperation.GetProposals(out itemsOut);
            if (dbitem.ResultCode == 1)
            {
                return itemsOut;
            }
            else
            {
                return null;
            }
        }
        [HttpGet]
        [Route("GetProposalWithDetailsByProviderID/{providerid}")]
        public List<Proposal> GetProposalWithDetailsByProviderID(Int64 providerid)
        {
            BusinessOperation businessOperation = new BusinessOperation();
            List<Proposal> itemsOut = null;
            BaseOutput dbitem = businessOperation.GetProposalsByProviderID(providerid, out itemsOut);
            if (dbitem.ResultCode == 1)
            {
                return itemsOut;
            }
            else
            {
                return null;
            }
        }
        [HttpGet]
        [Route("GetProposalWithDetailsByUserName/{username}")]
        public List<Proposal> GetProposalWithDetailsByUserName(string username)
        {
            BusinessOperation businessOperation = new BusinessOperation();
            List<Proposal> itemsOut = null;
            BaseOutput dbitem = businessOperation.GetProposalsByUserName(username, out itemsOut);
            if (dbitem.ResultCode == 1)
            {
                return itemsOut;
            }
            else
            {
                return null;
            }
        }
        [HttpGet]
        [Route("GetProposalWithDetailsByIsPublic/{username}")]
        public List<Proposal> GetProposalWithDetailsByIsPublic(string username)
        {
            BusinessOperation businessOperation = new BusinessOperation();
            List<Proposal> itemsOut = null;
            BaseOutput dbitem = businessOperation.GetProposalWithDetailsByIsPublic(username, out itemsOut);
            if (dbitem.ResultCode == 1)
            {
                return itemsOut;
            }
            else
            {
                return null;
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
            BusinessOperation businessOperation = new BusinessOperation();

            BaseOutput dbitem = businessOperation.UpdateProposalWithDetail(item);
            if (dbitem.ResultCode == 1)
            {
                return Ok(dbitem);
            }
            else
            {
                return BadRequest(dbitem.ResultCode + " : " + dbitem.ResultMessage);
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
            BusinessOperation businessOperation = new BusinessOperation();

            BaseOutput dbitem = businessOperation.DeleteProposalWithDetail(id);
            if (dbitem.ResultCode == 1)
            {
                return Ok(dbitem);
            }
            else
            {
                return BadRequest(dbitem.ResultCode + " : " + dbitem.ResultMessage);
            }


        }

        #region ProposalUserState
        [HttpGet]
        [Route("GetProposalUserStates")]
        public List<tbl_ProposalUserState> GetProposalUserStates()
        {
            CRUDOperation operation = new CRUDOperation();
            var proposaluserstates = operation.GetProposalUserStates(); ;
            return proposaluserstates;
        }
        [HttpGet]
        [Route("GetProposalUserStateByID/{id}")]
        public tbl_ProposalUserState GetProposalUserStateByID(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();
            var item = operation.GetProposalUserStateByID(id); ;
            return item;
        }

        [HttpPost]
        [ResponseType(typeof(tbl_ProposalUserState))]
        [Route("AddProposalUserState")]
        public IHttpActionResult AddProposalUserState(tbl_ProposalUserState item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CRUDOperation operation = new CRUDOperation();
            tbl_ProposalUserState dbitem = operation.AddProposalUserState(item);

            return Ok(dbitem);
        }

        [HttpPost]
        [ResponseType(typeof(tbl_ProposalUserState))]
        [Route("UpdateProposalUserState")]
        public IHttpActionResult UpdateProposalUserState(tbl_ProposalUserState item)
        {
            CRUDOperation operation = new CRUDOperation();
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                var dbitem = operation.UpdateProposalUserState(item);
                return Ok(dbitem);
            }
        }

        [HttpPost]
        [ResponseType(typeof(tbl_ProposalUserState))]
        [Route("DeleteProposalUserState/{id}")]
        public IHttpActionResult DeleteProposalUserState(Int64 id)
        {
            CRUDOperation operation = new CRUDOperation();

            var dbitem = operation.DeleteProposalUserState(id, 0);
            return Ok(dbitem);

        }

        [HttpGet]
        [Route("GetProposalUserStatesByProposalID/{proposalID}")]
        public List<tbl_ProposalUserState> GetProposalUserStatesByProposalID(Int64 proposalID)
        {
            CRUDOperation operation = new CRUDOperation();
            var proposaluserstates = operation.GetProposalUserStatesByProposalID(proposalID);
            return proposaluserstates;
        }
        [HttpGet]
        [Route("GetProposalUserStatesByUserID/{userID}")]
        public List<tbl_ProposalUserState> GetProposalUserStatesByUserID(Int64 userID)
        {
            CRUDOperation operation = new CRUDOperation();
            var proposaluserstates = operation.GetProposalUserStatesByUserID(userID);
            return proposaluserstates;
        }
        [HttpGet]
        [Route("GetProposalUserStatesByProviderStateType/{providerStateType}")]
        public List<tbl_ProposalUserState> GetProposalUserStatesByProviderStateType(Int64 providerStateType)
        {
            CRUDOperation operation = new CRUDOperation();
            var proposaluserstates = operation.GetProposalUserStatesByProviderStateType(providerStateType);
            return proposaluserstates;
        }
        [HttpGet]
        [Route("GetProposalUserStatesByUserStateType/{userStateType}")]
        public List<tbl_ProposalUserState> GetProposalUserStatesByUserStateType(Int64 userStateType)
        {
            CRUDOperation operation = new CRUDOperation();
            var proposaluserstates = operation.GetProposalUserStatesByUserStateType(userStateType);
            return proposaluserstates;
        }

        #endregion

        #region MyRegion
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
