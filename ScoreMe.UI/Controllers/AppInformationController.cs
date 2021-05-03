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
                viewModel.RAppInformationList = cRUDOperation.GetAppGroupInformations();

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
                        tbl_AppGroupInformation item = new tbl_AppGroupInformation()
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

                        tbl_AppGroupInformation operatorInformationControl = dataOperations.ControlAppGroupInformation(item);
                        if (operatorInformationControl != null)
                        {
                            TempData["success"] = "notOk";
                            TempData["message"] = "Eyni parametrelerə sahib məlumat sistemdə mövcudur";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            tbl_AppGroupInformation dbItem = dataOperations.UpdateAppGroupInformation(item);
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
        [Description("App məlumatını redaktə etmək")]
        public ActionResult Edit(int id)
        {
            AppInformationVM viewModel = new AppInformationVM();


            CRUDOperation dataOperations = new CRUDOperation();

            tbl_AppGroupInformation tblItem = dataOperations.GetAppGroupInformationById(id);


            viewModel.ID = id;
            viewModel.CategoryType = tblItem.CategoryType;
            viewModel.CategoryName = tblItem.CategoryName;
            viewModel.PriceUsage = tblItem.PriceUsage == null ? 0 : (decimal)tblItem.PriceUsage;
            viewModel.PointUsage = tblItem.PointUsage == null ? 0 : (decimal)tblItem.PointUsage;
            viewModel.PriceCount = tblItem.PriceCount == null ? 0 : (decimal)tblItem.PriceCount;
            viewModel.PointCount = tblItem.PointCount == null ? 0 : (decimal)tblItem.PointCount;

            return View(viewModel);

        }
        [HttpPost]
        public ActionResult Edit(AppInformationVM viewModel)
        {
            try
            {

                var UserProfile = (UserProfileSessionData)this.Session["UserProfile"];
                if (UserProfile != null)
                {
                    if (ModelState.IsValid)
                    {
                        tbl_AppGroupInformation item = new tbl_AppGroupInformation()
                        {
                            ID = viewModel.ID,
                            CategoryType = viewModel.CategoryType,
                            CategoryName = viewModel.CategoryName,
                            PriceUsage = viewModel.PriceUsage,
                            PointUsage = viewModel.PointUsage,
                            PriceCount = viewModel.PriceCount,
                            PointCount = viewModel.PointCount,
                            UpdateDate = DateTime.Now,
                            UpdateUser = UserProfile.UserId
                        };

                        CRUDOperation dataOperations = new CRUDOperation();
                        tbl_AppGroupInformation dbItem = dataOperations.UpdateAppGroupInformation(item);
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
                return View(viewModel);
            }

        }
        [Description("App məlumatını sil")]
        public ActionResult Delete(int id)
        {
            try
            {
                CRUDOperation dataOperations = new CRUDOperation();
                var UserProfile = (UserProfileSessionData)this.Session["UserProfile"];
                if (UserProfile != null)
                {
                    dataOperations.DeleteAppGroupInformation(id, UserProfile.UserId);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Error", "Error"));
            }
        }
 
    }
}