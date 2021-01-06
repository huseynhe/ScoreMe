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
    public class EnumCategoryController : Controller
    {
        // GET: EnumCategory
        [LoginCheck]
        [AccessRightsCheck]
        [Description("Enum Kategori siyahısı")]
        public ActionResult Index(int? page, string vl, string prm = null)
        {
            EnumRepository repository = new EnumRepository();
            try
            {
                Search search = new Search();

                search = SetValue(page, vl, prm);

                int pageSize = 15;
                int pageNumber = (page ?? 1);

                EnumVM viewModel = new EnumVM();
                viewModel.Search = search;

                viewModel.Search.pageSize = pageSize;
                viewModel.Search.pageNumber = pageNumber;

                viewModel.REnumCategoryList = repository.SW_GetEnumCategories(viewModel.Search);

                viewModel.ListCount = repository.SW_GetEnumCategoriesCount(viewModel.Search);
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

        [Description("Yeni enum kategori əlavə etmək")]
        public ActionResult Create()
        {
            EnumVM viewModel = new EnumVM();
            return View(viewModel);

        }
        [HttpPost]
        public ActionResult Create(EnumVM viewModel)
        {

            try
            {
                var UserProfile = (UserProfileSessionData)this.Session["UserProfile"];
                if (UserProfile != null)
                {

                    tbl_EnumCategory item = new tbl_EnumCategory()
                    {
                        Code = viewModel.EnumCategoryCode,
                        Name = viewModel.EnumCategoryName,
                        Description = viewModel.EnumCategoryDesc,
                        InsertDate = DateTime.Now,
                        InsertUser = UserProfile.UserId

                    };

                    CRUDOperation dataOperations = new CRUDOperation();
                    tbl_EnumCategory dbItem = dataOperations.AddEnumCategory(item);
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
            catch (ApplicationException ex)
            {


                return View(viewModel);
            }
            throw new ApplicationException("Invalid model");

        }
        [Description("Enum karegoini redaktə etmək")]
        public ActionResult Edit(int id)
        {
            EnumVM viewModel = new EnumVM();
            CRUDOperation dataOperations = new CRUDOperation();

            tbl_EnumCategory tblItem = dataOperations.GetEnumCategoryById(id);

            viewModel.EnumCategoryID = id;
            viewModel.EnumCategoryCode = tblItem.Code;
            viewModel.EnumCategoryName = tblItem.Name;
            viewModel.EnumCategoryDesc = tblItem.Description;

            return View(viewModel);

        }
        [HttpPost]
        public ActionResult Edit(EnumVM viewModel)
        {
            try
            {
                var UserProfile = (UserProfileSessionData)this.Session["UserProfile"];
                if (UserProfile != null)
                {
                    if (ModelState.IsValid)
                    {

                        tbl_EnumCategory item = new tbl_EnumCategory()
                        {
                            ID = viewModel.EnumCategoryID,
                            Code = viewModel.EnumCategoryCode,
                            Name = viewModel.EnumCategoryName,
                            Description = viewModel.EnumCategoryDesc,
                            UpdateDate = DateTime.Now,
                            UpdateUser = UserProfile.UserId

                        };


                        CRUDOperation dataOperations = new CRUDOperation();
                        tbl_EnumCategory dbItem = dataOperations.UpdateEnumCategory(item);
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
        [Description("Enum kategorini sil")]
        public ActionResult Delete(int id)
        {
            try
            {
                CRUDOperation dataOperations = new CRUDOperation();
                var UserProfile = (UserProfileSessionData)this.Session["UserProfile"];
                if (UserProfile != null)
                {
                    dataOperations.DeleteEnumCategory(id, UserProfile.UserId);
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