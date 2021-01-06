using ScoreMe.DAL;
using ScoreMe.DAL.Model;
using ScoreMe.DAL.Objects;
using ScoreMe.DAL.Repositories;
using ScoreMe.UI.Attributes;
using ScoreMe.UI.Models;
using ScoreMe.UI.Services;
using ScoreMe.UTILITY;
using ScoreMe.UTILITY.Custom;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using ScoreMe.DAL.DBModel;
using ScoreMe.UI.Enums;

namespace ScoreMe.UI.Controllers
{

    [LoginCheck]
    [AccessRightsCheck]
    [Description("İstifadəçilər")]
    public class UserController : Controller
    {
        // GET: Users

        public ActionResult Index(int? page, int? userId, string vl, string prm = null)
        {
            UserVM userViewModel = null;
            try
            {
                UsersRepository repository = new UsersRepository();
                CRUDOperation CRUDOperation = new CRUDOperation();
                tbl_User userObj = new tbl_User();

                Search search = new Search();

                if (userId != null)
                {
                    Search ss = new Search();
                    //  ss.VehicleId = (int)vehicleId;
                    Session["SearchInfo"] = ss;
                    search = ss;
                }
                else
                {
                    search = SetValue(page, vl, prm);
                }


                int pageSize = 15;
                int pageNumber = (page ?? 1);

                userViewModel = new UserVM();
                userViewModel.Search = search;
                userViewModel.Search.pageSize = pageSize;
                userViewModel.Search.pageNumber = pageNumber;

                userViewModel.RUserList = repository.SW_GetUserDetails(userViewModel.Search);
                userViewModel.listCount = repository.SW_GetUserDetailsCount(userViewModel.Search);

                int[] pc = new int[userViewModel.listCount];

                userViewModel.Paging = pc.ToPagedList(pageNumber, pageSize);

                return Request.IsAjaxRequest()
         ? (ActionResult)PartialView("PartialIndex", userViewModel)
         : View(userViewModel);

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

        [Description("Yeni istifadəçi əlavə etmək")]
        public ActionResult Create()
        {
            UserVM viewModel = new UserVM();
            viewModel = poulateDropDownList(viewModel);
            return View(viewModel);

        }
        [HttpPost]
        public ActionResult Create(UserVM viewModel)
        {
            try
            {
                var UserProfile = (UserProfileSessionData)this.Session["UserProfile"];
                if (UserProfile != null)
                {
                    if (ModelState.IsValid)
                    {

                        tbl_User userItem = new tbl_User()
                        {
                            UserName = viewModel.UserName,
                            //FirstName = viewModel.FirstName,
                            //LastName = viewModel.LastName,
                            UserType_EVID = viewModel.UserTypeEvID,
                            AccountLocked = viewModel.LockType,
                            Password = EncodeAndDecode.Base64Encode(viewModel.Password),
                            InsertDate = DateTime.Now,
                            InsertUser = UserProfile.UserId

                        };

                        CRUDOperation CRUDOperation = new CRUDOperation();
                        string responseMsj = string.Empty;
                        tbl_User userDB = CRUDOperation.AddUser(userItem);
                        if (userDB!=null)
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
            catch (ApplicationException ex)
            {

                viewModel = poulateDropDownList(viewModel);

                return View(viewModel);
            }
            viewModel = poulateDropDownList(viewModel);

            return View(viewModel);


        }

        [Description("İstifadəçini redaktə etmək")]
        public ActionResult Edit(int id)
        {

            CRUDOperation CRUDOperation = new CRUDOperation();
            tbl_User userobj = CRUDOperation.GetUserById(id);
            UserVM viewModel = new UserVM()
            {
                UserName = userobj.UserName,
                UserTypeEvID=userobj.UserType_EVID,
                LockType = userobj.AccountLocked,
                Password = userobj.Password,
                Id = userobj.ID,

            };
            viewModel = poulateDropDownList(viewModel);
            return View(viewModel);

        }
        [HttpPost]
        public ActionResult Edit(UserVM viewModel)
        {

            var UserProfile = (UserProfileSessionData)this.Session["UserProfile"];
            if (UserProfile != null)
            {
                tbl_User userItem = new tbl_User()
                {
                    ID = viewModel.Id,
                    UserName = viewModel.UserName,
                    UserType_EVID=viewModel.UserTypeEvID,
                    AccountLocked = viewModel.LockType,
                    Password = EncodeAndDecode.Base64Encode(viewModel.Password),
                    UpdateUser = UserProfile.UserId,

                };

                CRUDOperation CRUDOperation = new CRUDOperation();
                string responseMsj = string.Empty;
                tbl_User userDB = CRUDOperation.UpdateUser(userItem);
                if (userDB!=null)
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
            return RedirectToAction("Index");
        }
        private UserVM poulateDropDownList(UserVM viewModel)
        {
            viewModel.LockTypes = EnumService.GetLockEnumTypes();
            viewModel.UserTypeList = EnumService.GetEnumValueListByEcID((int)CategoryEnum.UserType);

            return viewModel;
        }

        public ActionResult Delete(int id)
        {


            try
            {
                var UserProfile = (UserProfileSessionData)this.Session["UserProfile"];
                if (UserProfile != null)
                {
                    CRUDOperation CRUDOperation = new CRUDOperation();

                    string responseMsj = string.Empty;
                    tbl_User userDB = CRUDOperation.DeleteUser(id, UserProfile.UserId);
                    if (userDB != null)
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
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Error", "Error"));
            }



        }

    }
}
