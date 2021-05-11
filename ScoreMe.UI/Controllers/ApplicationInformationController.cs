using PagedList;
using ScoreMe.DAL;
using ScoreMe.DAL.DBModel;
using ScoreMe.DAL.Objects;
using ScoreMe.DAL.Repositories;
using ScoreMe.UI.Attributes;
using ScoreMe.UI.Models;
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
    public class ApplicationInformationController : Controller
    {
        [Description("Tətbiqlərin siyahısı")]
        public ActionResult Index(int? page, string vl, string prm = null)
        {
            ApplicationInformationRepository repository = new ApplicationInformationRepository();
            try
            {
                Search search = new Search();

                search = SetValue(page, vl, prm);

                int pageSize = 15;
                int pageNumber = (page ?? 1);

                ApplicationInformationVM viewModel = new ApplicationInformationVM();
                viewModel.Search = search;

                viewModel.Search.pageSize = pageSize;
                viewModel.Search.pageNumber = pageNumber;

                viewModel.RApplicationInformationList = repository.SW_GetApplicationInformations(viewModel.Search);

                viewModel.ListCount = repository.SW_GetApplicationInformationsCount(viewModel.Search);
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

        [Description("Yeni tətbiq əlavə etmək")]
        public ActionResult Create()
        {
            ApplicationInformationVM viewModel = new ApplicationInformationVM();
            viewModel = poulateDropDownList(viewModel);
            return View(viewModel);

        }
        [HttpPost]
        public ActionResult Create(ApplicationInformationVM viewModel)
        {

            try
            {
                var UserProfile = (UserProfileSessionData)this.Session["UserProfile"];
                if (UserProfile != null)
                {
                    if (ModelState.IsValid)
                    {
                        tbl_ApplicationInformation item = new tbl_ApplicationInformation()
                        {
                            Platform = viewModel.Platform.Trim(),
                            GroupName = viewModel.GroupName.Trim(),
                            AppName = viewModel.AppName.Trim(),
                            ShortName = viewModel.ShortName.Trim(),
                            Author = viewModel.Author,
                            Price = viewModel.Price,
                            Point = viewModel.Price,
                            NetUsage = viewModel.NetUsage,
                            InsertDate = DateTime.Now,
                            InsertUser = UserProfile.UserId

                        };

                        CRUDOperation dataOperations = new CRUDOperation();
                        tbl_ApplicationInformation dbItem = dataOperations.AddApplicationInformation(item);
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
                throw new ApplicationException("Invalid model");
            }
            catch (ApplicationException ex)
            {
                viewModel = poulateDropDownList(viewModel);

                return View(viewModel);
            }
   

        }
        [Description("Tətbiqi redaktə etmək")]
        public ActionResult Edit(int id)
        {
            ApplicationInformationVM viewModel = new ApplicationInformationVM();
            viewModel = poulateDropDownList(viewModel);
            CRUDOperation dataOperations = new CRUDOperation();

            tbl_ApplicationInformation tblItem = dataOperations.GetApplicationInformationById(id);

            viewModel.ID = id;
            viewModel.Platform = tblItem.Platform;
            viewModel.GroupName = tblItem.GroupName;
            viewModel.AppName = tblItem.AppName;
            viewModel.ShortName = tblItem.ShortName;
            viewModel.Author = tblItem.Author;
            viewModel.Price = tblItem.Price;
            viewModel.Point = tblItem.Point;
            viewModel.NetUsage = tblItem.NetUsage;

            return View(viewModel);

        }
        [HttpPost]
        public ActionResult Edit(ApplicationInformationVM viewModel)
        {
            try
            {
                var UserProfile = (UserProfileSessionData)this.Session["UserProfile"];
                if (UserProfile != null)
                {
                    if (ModelState.IsValid)
                    {
                        tbl_ApplicationInformation item = new tbl_ApplicationInformation()
                        {
                            ID = viewModel.ID,
                            Platform = viewModel.Platform.Trim(),
                            GroupName = viewModel.GroupName.Trim(),
                            AppName = viewModel.AppName.Trim(),
                            ShortName = viewModel.ShortName.Trim(),
                            Author = viewModel.Author,
                            Price = viewModel.Price,
                            Point = viewModel.Point,
                            NetUsage = viewModel.NetUsage,
                            UpdateDate = DateTime.Now,
                            UpdateUser = UserProfile.UserId

                        };

                        CRUDOperation dataOperations = new CRUDOperation();
                        tbl_ApplicationInformation dbItem = dataOperations.UpdateApplicationInformation(item);
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
        [Description("Tətbiqi sil")]
        public ActionResult Delete(int id)
        {
            try
            {
                CRUDOperation dataOperations = new CRUDOperation();
                var UserProfile = (UserProfileSessionData)this.Session["UserProfile"];
                if (UserProfile != null)
                {
                    dataOperations.DeleteApplicationInformation(id, UserProfile.UserId);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Error", "Error"));
            }
        }
        private ApplicationInformationVM poulateDropDownList(ApplicationInformationVM viewModel)
        {
            //viewModel.e = EnumService.GetEnumCategoryList();
            return viewModel;
        }

        [Description("Məlumat bazasındakı tətbiqlərin siyahısı")]
        public ActionResult AppNameIndex(int? page, string vl, string prm = null)
        {
            ApplicationInformationRepository repository = new ApplicationInformationRepository();
            try
            {
                Search search = new Search();

                search = SetValue(page, vl, prm);

                int pageSize = 15;
                int pageNumber = (page ?? 1);

                ApplicationInformationVM viewModel = new ApplicationInformationVM();
                viewModel.Search = search;

                viewModel.Search.pageSize = pageSize;
                viewModel.Search.pageNumber = pageNumber;

                viewModel.RApplicationInformationList = repository.SW_GetApplicationNames(viewModel.Search);

                viewModel.ListCount = repository.SW_GetApplicationNamesCount(viewModel.Search);
                int[] pc = new int[viewModel.ListCount];

                viewModel.Paging = pc.ToPagedList(pageNumber, pageSize);



                return Request.IsAjaxRequest()
              ? (ActionResult)PartialView("AppNamePartialIndex", viewModel)
              : View(viewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Error", "Error"));
            }
        }
    }
}