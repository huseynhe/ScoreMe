using ScoreMe.DAL;
using ScoreMe.DAL.DBModel;
using ScoreMe.DAL.Model;
using ScoreMe.DAL.Objects;
using ScoreMe.DAL.Repositories;
using ScoreMe.UI.Attributes;
using ScoreMe.UI.Models;
using ScoreMe.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScoreMe.UI.Controllers
{
    public class LoginController : Controller
    {
        //UserViewModel usersViewModel = new UserViewModel();
        UserProfileSessionData UserProfile;

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserVM uvm)
        {
            if (ModelState.IsValid)
            {
                string IPAddress = GetIPAddress();
                LoginRepository repository = new LoginRepository();
                CRUDOperation dataOperation = new CRUDOperation();
                string result = repository.DoLogin(uvm.UserName, EncodeAndDecode.Base64Encode(uvm.Password), IPAddress);

                if (result == "Uğurlu")
                {
                    tbl_User userObj = dataOperation.GetUserByUserName(uvm.UserName);
                    tbl_Employee employeeObj = dataOperation.GetEmployeeByUserId(userObj.ID);
                    UserProfile = new UserProfileSessionData()
                    {
                        UserId = userObj.ID,
                        EmployeeID = employeeObj.ID,
                        UserName = userObj.UserName,
                        FirstName = employeeObj.FirstName,
                        LastName = employeeObj.LastName,

                    };

                    this.Session["UserProfile"] = UserProfile;
                    UrlSessionData CurrentUrl = new UrlSessionData
                    {
                        Controller = "Home",
                        Action = "Index"
                    };
                    this.Session["CurrentUrl"] = CurrentUrl;
                    return RedirectToAction("Index", "Home");
                }
                else if (result == "İstifadəçi adı tapılmadı")

                {
                    ViewBag.NotValidUser = result;

                }
                else
                {
                    ViewBag.Failedcount = "Şifrənin səf cəhd sayısı: "+result;
                }
                return View("Login");
            }
            else
            {
                return View("Login", uvm);
            }
        }

        public ActionResult LoginOut()
        {
            this.Session["UserProfile"] = null;
            return RedirectToAction("Login");
        }

        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }
            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
    }
}