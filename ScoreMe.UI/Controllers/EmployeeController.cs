using ScoreMe.DAL;
using ScoreMe.DAL.Model;
using ScoreMe.DAL.Objects;
using ScoreMe.DAL.Repositories;
using ScoreMe.UI.Attributes;
using ScoreMe.UI.Models;
using ScoreMe.UI.Services;
using ScoreMe.UTILITY.Custom;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using ScoreMe.DAL.DBModel;

namespace ScoreMe.UI.Controllers
{
    [LoginCheck]
    [AccessRightsCheck]
    public class EmployeeController : Controller
    {
        // GET: Employee
      
        [Description("Vəzifəli şəxs")]
        public ActionResult Index(int? page, string vl, string prm = null)
        {
            EmployeeRepository repository = new EmployeeRepository();
            try
            {
                Search search = new Search();

                search = SetValue(page, vl, prm);

                int pageSize = 15;
                int pageNumber = (page ?? 1);

                EmployeeVM viewModel = new EmployeeVM();
                viewModel.Search = search;

                viewModel.Search.pageSize = pageSize;
                viewModel.Search.pageNumber = pageNumber;

                viewModel.REmployeeList = repository.SW_GetEmployees(viewModel.Search);

                //viewModel.ListCount = repository.SW_OfficialPersonsDTOCount(viewModel.Search);
                //int[] pc = new int[viewModel.ListCount];

                //viewModel.Paging = pc.ToPagedList(pageNumber, pageSize);



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

        [Description("Yeni işçi əlavə etmək")]
        public ActionResult Add()
        {
            EmployeeVM viewModel = new EmployeeVM();
            viewModel = poulateDropDownList(viewModel);
            return View(viewModel);

        }
        [HttpPost]
        public ActionResult Add(EmployeeVM viewModel)
        {

            try
            {
                var UserProfile = (UserProfileSessionData)this.Session["UserProfile"];
                if (UserProfile != null)
                {

                    tbl_Employee item = new tbl_Employee()
                    {
                        FirstName = viewModel.FirstName,
                        LastName = viewModel.LastName,
                        FatherName = viewModel.FatherName,
                        UserId = viewModel.UserID,
                        GenderType = viewModel.GenderType,
                        InsertDate = DateTime.Now,
                        InsertUser = (int)UserProfile.UserId

                    };

                    CRUDOperation dataOperations = new CRUDOperation();
                    tbl_Employee dbItem = dataOperations.AddEmployee(item);
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

                viewModel = poulateDropDownList(viewModel);

                return View(viewModel);
            }
            throw new ApplicationException("Invalid model");

        }
        [Description("İşçi redaktə etmək")]
        public ActionResult Edit(int id)
        {
            EmployeeVM viewModel = new EmployeeVM();
            CRUDOperation dataOperations = new CRUDOperation();


            viewModel = poulateDropDownList(viewModel);

            tbl_Employee tblItem = dataOperations.GetEmployeeById(id);

            viewModel.ID = id;
            viewModel.FirstName = tblItem.FirstName;
            viewModel.LastName = tblItem.LastName;
            viewModel.FatherName = tblItem.FatherName;
            viewModel.UserID = tblItem.UserId;
            viewModel.GenderType = (int)tblItem.GenderType;

            return View(viewModel);

        }
        [HttpPost]
        public ActionResult Edit(EmployeeVM viewModel)
        {
            try
            {
                var UserProfile = (UserProfileSessionData)this.Session["UserProfile"];
                if (UserProfile != null)
                {
                    if (ModelState.IsValid)
                    {

                        tbl_Employee item = new tbl_Employee()
                        {
                            ID = viewModel.ID,
                            FirstName = viewModel.FirstName,
                            LastName = viewModel.LastName,
                            FatherName = viewModel.FatherName,
                            UserId = viewModel.UserID,
                            GenderType = viewModel.GenderType,
                            InsertDate = DateTime.Now,
                            InsertUser = (int)UserProfile.UserId

                        };


                        CRUDOperation dataOperations = new CRUDOperation();
                        tbl_Employee dbItem = dataOperations.UpdateEmployee(item);
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
        [Description("İşçi şəxs sil")]
        public ActionResult Delete(int id)
        {
            try
            {
                CRUDOperation dataOperations = new CRUDOperation();
                var UserProfile = (UserProfileSessionData)this.Session["UserProfile"];
                if (UserProfile != null)
                {
                    dataOperations.DeleteEmployee(id, (int)UserProfile.UserId);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Error", "Error"));
            }
        }
        private EmployeeVM poulateDropDownList(EmployeeVM viewModel)
        {
            viewModel.GenderTypeList = EnumService.GetGenderEnumTypes();
            viewModel.UserList = EnumService.GetUserListByEvID(32);
            return viewModel;
        }
    }
}