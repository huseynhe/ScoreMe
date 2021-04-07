using ScoreMe.DAL;
using ScoreMe.DAL.DBModel;
using ScoreMe.DAL.Objects;
using ScoreMe.UI.Attributes;
using ScoreMe.UI.Enums;
using ScoreMe.UI.Models;
using ScoreMe.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScoreMe.UI.Controllers
{
    [LoginCheck]
    [AccessRightsCheck]
    public class AppInformationController : Controller
    {
        [Description("App məlumatlarının siyahısı")]
        public ActionResult Index()
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            try
            {
               


                AppInformationVM viewModel = new AppInformationVM();
                viewModel.RAppInformationList = cRUDOperation.GetAppInformations();

               return View(viewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Error", "Error"));
            }
        }
 
        [Description("Yeni App məlumatını əlavə etmək")]
        public ActionResult Create()
        {
            AppInformationVM viewModel = new AppInformationVM();
           // viewModel = poulateDropDownList(viewModel);
            return View(viewModel);

        }
        [HttpPost]
        public ActionResult Create(AppInformationVM viewModel)
        {

            try
            {
                var UserProfile = (UserProfileSessionData)this.Session["UserProfile"];
                if (UserProfile != null)
                {
                    if (ModelState.IsValid)
                    {
                        tbl_AppInformation item = new tbl_AppInformation()
                        {
                            CategoryType = viewModel.CategoryType,
                            CategoryName = viewModel.CategoryName,
                            PriceUsage = viewModel.PriceUsage,
                            PointUsage = viewModel.PointUsage,
                            PriceCount = viewModel.PriceCount,
                            PointCount = viewModel.PointCount,
                            InsertDate = DateTime.Now,
                            InsertUser = UserProfile.UserId
                        };
                        CRUDOperation dataOperations = new CRUDOperation();

                        tbl_AppInformation operatorInformationControl = dataOperations.ControlAppInformation(item);
                        if (operatorInformationControl != null)
                        {
                            TempData["success"] = "notOk";
                            TempData["message"] = "Eyni parametrelerə sahib məlumat sistemdə mövcudur";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            tbl_AppInformation dbItem = dataOperations.AddAppInformation(item);
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

                return View(viewModel);
            }


        }
        [Description("Operator məlumatını redaktə etmək")]
        public ActionResult Edit(int id)
        {
            OperatorInformationVM viewModel = new OperatorInformationVM();


            CRUDOperation dataOperations = new CRUDOperation();

            tbl_OperatorInformation tblItem = dataOperations.GetOperatorInformationById(id);
            tbl_EnumValue enumValue = dataOperations.GetEnumValueById((int)tblItem.OperatorChanelType_EVID);
            viewModel.EnumCategoryID = enumValue.EnumCategoryID;
            viewModel = poulateDropDownList(viewModel);

            viewModel.ID = id;
            viewModel.OperatorTypeEVID = tblItem.OperatorType_EVID == null ? 0 : (int)tblItem.OperatorType_EVID;
            viewModel.Name = tblItem.Name;
            viewModel.OperatorChanelTypeEVID = tblItem.OperatorChanelType_EVID == null ? 0 : (int)tblItem.OperatorChanelType_EVID;
            viewModel.InOutTypeEVID = tblItem.InOutType_EVID == null ? 0 : (int)tblItem.InOutType_EVID;
            viewModel.Price = tblItem.Price == null ? 0 : (decimal)tblItem.Price;
            viewModel.Point = tblItem.Point == null ? 0 : (decimal)tblItem.Point;

            return View(viewModel);

        }
        [HttpPost]
        public ActionResult Edit(OperatorInformationVM viewModel)
        {
            try
            {

                var UserProfile = (UserProfileSessionData)this.Session["UserProfile"];
                if (UserProfile != null)
                {
                    if (ModelState.IsValid)
                    {
                        tbl_OperatorInformation item = new tbl_OperatorInformation()
                        {
                            ID = viewModel.ID,
                            OperatorType_EVID = viewModel.OperatorTypeEVID,
                            Name = viewModel.Name,
                            OperatorChanelType_EVID = viewModel.OperatorChanelTypeEVID,
                            InOutType_EVID = viewModel.InOutTypeEVID,
                            Price = viewModel.Price,
                            Point = viewModel.Point,
                            UpdateDate = DateTime.Now,
                            UpdateUser = UserProfile.UserId
                        };

                        CRUDOperation dataOperations = new CRUDOperation();
                        tbl_OperatorInformation dbItem = dataOperations.UpdateOperatorInformation(item);
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
        [Description("Operator məlumatını sil")]
        public ActionResult Delete(int id)
        {
            try
            {
                CRUDOperation dataOperations = new CRUDOperation();
                var UserProfile = (UserProfileSessionData)this.Session["UserProfile"];
                if (UserProfile != null)
                {
                    dataOperations.DeleteOperatorInformation(id, UserProfile.UserId);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Error", "Error"));
            }
        }
        private OperatorInformationVM poulateDropDownList(OperatorInformationVM viewModel)
        {
            viewModel.OperatorTypeList = EnumService.GetEnumValueListByEcID((int)CategoryEnum.OperatorType);
            viewModel.OperatorChanelTypeList = EnumService.GetEnumValueListByEcID((int)CategoryEnum.OperatorChanelType);
            if (viewModel.EnumCategoryID == 17)
            {
                viewModel.InOutTypeList = EnumService.GetEnumValueListByEcIDForINOUT((int)CategoryEnum.InOutTypeMessage);
            }
            else
            {
                viewModel.InOutTypeList = EnumService.GetEnumValueListByEcIDForINOUT((int)CategoryEnum.InOutTypeCall);
            }

            viewModel.OperatorPrefixList = EnumService.GetEnumValueListByEcIDForPrefix((int)CategoryEnum.OperatorPrefixType);
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