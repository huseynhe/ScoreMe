using PagedList;
using ScoreMe.DAL;
using ScoreMe.DAL.DBModel;
using ScoreMe.DAL.Objects;
using ScoreMe.DAL.Repositories;
using ScoreMe.UI.Attributes;
using ScoreMe.UI.Enums;
using ScoreMe.UI.Models;
using ScoreMe.UI.Services;
using ScoreMe.UTILITY.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace ScoreMe.UI.Controllers
{
    [LoginCheck]
    [AccessRightsCheck]
    [Description("SMS gonderici")]
    public class SMSSenderInfoController : Controller
    {
        [Description("SMS gondericilerin siyahısı")]
        public ActionResult Index(int? page, string vl, string prm = null)
        {
            SMSSenderInfoRepository repository = new SMSSenderInfoRepository();
            try
            {
                Search search = new Search();

                search = SetValue(page, vl, prm);

                int pageSize = 15;
                int pageNumber = (page ?? 1);

                SMSSenderInfoVM viewModel = new SMSSenderInfoVM();
                viewModel.Search = search;

                viewModel.Search.pageSize = pageSize;
                viewModel.Search.pageNumber = pageNumber;

                viewModel.RSMSSenderInfoList = repository.SW_GetGetSMSSenderInfos(viewModel.Search);

                viewModel.ListCount = repository.SW_GetSMSSenderInfosCount(viewModel.Search);
                int[] pc = new int[viewModel.ListCount];

                viewModel.Paging = pc.ToPagedList(pageNumber, pageSize);



                return Request.IsAjaxRequest()
              ? (ActionResult)PartialView("PartialIndex", viewModel)
              : View(viewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Error", "Error"));
            }
        }
        public Search SetValue(int? page, string vl, string prm = null)
        {
            if (prm == null && page == null)
            {
                Search ss = new Search();
                Session["SearchInfo"] = ss;
            }

            if (!string.IsNullOrEmpty(vl))
            {
                vl = StripTag.strSqlBlocker(vl);
            }

            Search search = new Search();

            search = (Search)Session["SearchInfo"];

            if (prm != null)
            {
                PropertyInfo propertyInfos = search.GetType().GetProperty(prm);
                propertyInfos.SetValue(search, Convert.ChangeType(vl, propertyInfos.PropertyType), null);
            }

            Session["SearchInfo"] = search;

            return search;

        }

        [Description("Yeni SMS gonderici əlavə etmək")]
        public ActionResult Create()
        {
            SMSSenderInfoVM viewModel = new SMSSenderInfoVM();
            viewModel = poulateDropDownList(viewModel);
            return View(viewModel);

        }
        [HttpPost]
        public ActionResult Create(SMSSenderInfoVM viewModel)
        {

            try
            {
                var UserProfile = (UserProfileSessionData)this.Session["UserProfile"];
                if (UserProfile != null)
                {
                    if (ModelState.IsValid)
                    {
                        tbl_SMSSenderInfo item = new tbl_SMSSenderInfo()
                        {
                            ActivityType = viewModel.ActivityTypeEVID,
                            SenderName = viewModel.SenderName,
                            Description=viewModel.Description,
                            Number = viewModel.Number,
                            Price=viewModel.Price,
                            Point=viewModel.Point,
                            Cheque=viewModel.Cheque,
                            InsertDate = DateTime.Now,
                            InsertUser = UserProfile.UserId
                        };
                        CRUDOperation dataOperations = new CRUDOperation();

                        tbl_SMSSenderInfo sMSSenderInfoControl = dataOperations.GetSMSSenderInfoByName(item.SenderName);
                        if (sMSSenderInfoControl != null)
                        {
                            TempData["success"] = "notOk";
                            TempData["message"] = "Eyni parametrelerə sahib məlumat sistemdə mövcudur";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            tbl_SMSSenderInfo dbItem = dataOperations.AddSMSSenderInfo(item);
                            if (dbItem != null)
                            {
                                TempData["success"] = "Ok";
                                TempData["message"] = "Məlumatlar uğurla əlavə olundu";
                                return RedirectToAction("Index");

                            }
                            else
                            {
                                TempData["success"] = "notOk";
                                TempData["message"] = "Məlumatlar əlavə olunarkən xəta baş verdi";
                                return RedirectToAction("Index");

                            }

                        }


                    }

                }
                throw new ApplicationException("Invalid model");
            }

            catch (ApplicationException ex)
            {
                viewModel = poulateDropDownList(viewModel);

                return View(viewModel);
            }


        }
        [Description("SMS gonderici məlumatını redaktə etmək")]
        public ActionResult Edit(int id)
        {
            SMSSenderInfoVM viewModel = new SMSSenderInfoVM();
            CRUDOperation dataOperations = new CRUDOperation();
            tbl_SMSSenderInfo tblItem = dataOperations.GetSMSSenderInfoByID(id);
            viewModel = poulateDropDownList(viewModel);
            viewModel.ID = id;
            viewModel.ActivityTypeEVID = tblItem.ActivityType;
            viewModel.SenderName = tblItem.SenderName;
            viewModel.Description = tblItem.Description;
            viewModel.Number = tblItem.Number;
            viewModel.Price = tblItem.Price;
            viewModel.Point = tblItem.Point;
            viewModel.Cheque = tblItem.Cheque;
            return View(viewModel);

        }
        [HttpPost]
        public ActionResult Edit(SMSSenderInfoVM viewModel)
        {
            try
            {

                var UserProfile = (UserProfileSessionData)this.Session["UserProfile"];
                if (UserProfile != null)
                {
                    if (ModelState.IsValid)
                    {
                        tbl_SMSSenderInfo item = new tbl_SMSSenderInfo()
                        {
                            ID = viewModel.ID,
                            ActivityType = viewModel.ActivityTypeEVID,
                            SenderName = viewModel.SenderName,
                            Description = viewModel.Description,
                            Number = viewModel.Number,
                            Price = viewModel.Price,
                            Point = viewModel.Point,
                            Cheque = viewModel.Cheque,
                            IsParse=viewModel.IsParse,
                            UpdateDate = DateTime.Now,
                            UpdateUser = UserProfile.UserId
                        };

                        CRUDOperation dataOperations = new CRUDOperation();
                        tbl_SMSSenderInfo dbItem = dataOperations.UpdateSMSSenderInfo(item);
                        if (dbItem != null)
                        {
                            TempData["success"] = "Ok";
                            TempData["message"] = "Məlumatlar uğurla dəyişdirildi";
                            return RedirectToAction("Index");

                        }
                        else
                        {
                            TempData["success"] = "notOk";
                            TempData["message"] = "Məlumatlar dəyişdirilərkən xəta baş verdi";
                            return RedirectToAction("Index");

                        }
                    }
                }

                throw new ApplicationException("Invalid model");
            }
            catch (ApplicationException ex)
            {
                viewModel = poulateDropDownList(viewModel);
                return View(viewModel);
            }

        }
        [Description("SMS gonderici məlumatını sil")]
        public ActionResult Delete(int id)
        {
            try
            {
                CRUDOperation dataOperations = new CRUDOperation();
                var UserProfile = (UserProfileSessionData)this.Session["UserProfile"];
                if (UserProfile != null)
                {
                    dataOperations.DeleteSMSSenderInfo(id, UserProfile.UserId);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Error", "Error"));
            }
        }
        private SMSSenderInfoVM poulateDropDownList(SMSSenderInfoVM viewModel)
        {
            viewModel.ActivityTypeList = EnumService.GetEnumValueListByEcID((int)CategoryEnum.ActivityType);
            viewModel.IsParseList = EnumService.GetBoleanEnumTypes();
            return viewModel;
        }

        public ActionResult FillInOutType(int chanelTypeEVID)
        {
            int categoryID = 0;
            if (chanelTypeEVID == 48)
            {
                categoryID = (int)CategoryEnum.InOutTypeMessage;
            }
            else if (chanelTypeEVID == 49)
            {
                categoryID = (int)CategoryEnum.InOutTypeCall;
            }
            IEnumerable<SelectListItem> InOutTypeList = EnumService.GetEnumValueListByEcIDForINOUT(categoryID);
            return Json(InOutTypeList, JsonRequestBehavior.AllowGet);
        }
    }
}