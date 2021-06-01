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
    public class GroupManagerController : Controller
    {
        // GET: GroupManager
        [Description("Grouplarin siyahısı")]
        public ActionResult Index(int? page, string vl, string prm = null)
        {
            GroupRepository repository = new GroupRepository();
            try
            {
                Search search = new Search();

                search = SetValue(page, vl, prm);

                int pageSize = 15;
                int pageNumber = (page ?? 1);

                GroupVM viewModel = new GroupVM();
                viewModel.Search = search;

                viewModel.Search.pageSize = pageSize;
                viewModel.Search.pageNumber = pageNumber;

                viewModel.RGroupList = repository.SW_GetGroups(viewModel.Search);

                viewModel.ListCount = repository.SW_GetGroupsCount(viewModel.Search);
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
        [Description("Yeni qrup əlavə etmək")]
        public ActionResult Create()
        {
            GroupVM viewModel = new GroupVM();
            viewModel = poulateDropDownList(viewModel);
            return View(viewModel);

        }
        [HttpPost]
        public ActionResult Create(GroupVM viewModel)
        {

            try
            {
                var UserProfile = (UserProfileSessionData)this.Session["UserProfile"];
                if (UserProfile != null)
                {
                    if (ModelState.IsValid)
                    {
                        tbl_Group item = new tbl_Group()
                        {
                             GroupType= viewModel.GroupType,
                            Name = viewModel.GroupName,
                            Description = viewModel.Description,
                            StartLimit = viewModel.StartLimit,
                            EndLimit = viewModel.EndLimit,
                            InsertDate = DateTime.Now,
                            InsertUser = UserProfile.UserId
                        };
                        CRUDOperation dataOperations = new CRUDOperation();
                        tbl_Group dbItem = dataOperations.AddGroup(item);
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
        [Description("Qrup redaktə etmək")]
        public ActionResult Edit(int id)
        {
            GroupVM viewModel = new GroupVM();
            viewModel = poulateDropDownList(viewModel);
            CRUDOperation dataOperations = new CRUDOperation();

            tbl_Group tblItem = dataOperations.GetGroupByID(id);

            viewModel.GroupID = id;
            viewModel.GroupType = tblItem.GroupType==null?0:(int)tblItem.GroupType;
            viewModel.GroupName = tblItem.Name;
            viewModel.Description = tblItem.Description;
            viewModel.StartLimit = tblItem.StartLimit==null?0:(decimal)tblItem.StartLimit;
            viewModel.EndLimit = tblItem.EndLimit==null?0:(decimal)tblItem.EndLimit;
            return View(viewModel);

        }
        [HttpPost]
        public ActionResult Edit(GroupVM viewModel)
        {
            try
            {

                var UserProfile = (UserProfileSessionData)this.Session["UserProfile"];
                if (UserProfile != null)
                {
                    if (ModelState.IsValid)
                    {
                        tbl_Group item = new tbl_Group()
                        {
                            ID = viewModel.GroupID,
                            GroupType = viewModel.GroupType,
                            Name = viewModel.GroupName,
                            Description = viewModel.Description,
                            StartLimit = viewModel.StartLimit,
                            EndLimit = viewModel.EndLimit,
                            UpdateDate = DateTime.Now,
                            UpdateUser = UserProfile.UserId

                        };

                        CRUDOperation dataOperations = new CRUDOperation();
                        tbl_Group dbItem = dataOperations.UpdateGroup(item);
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

        [Description("Qrup sil")]
        public ActionResult Delete(int id)
        {
            try
            {
                CRUDOperation dataOperations = new CRUDOperation();
                var UserProfile = (UserProfileSessionData)this.Session["UserProfile"];
                if (UserProfile != null)
                {
                    dataOperations.DeleteGroup(id, UserProfile.UserId);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Error", "Error"));
            }
        }
        private GroupVM poulateDropDownList(GroupVM viewModel)
        {
            viewModel.GroupTypeList = EnumService.GetEnumValueCodeListByEcID((int)CategoryEnum.GroupType);
            return viewModel;
        }
    }
}