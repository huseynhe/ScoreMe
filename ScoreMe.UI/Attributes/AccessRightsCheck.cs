using ScoreMe.DAL.Objects;
using ScoreMe.DAL.Repositories;
using ScoreMe.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ScoreMe.UI.Attributes

{
    public class AccessRightsCheck : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            UserProfileSessionData UserProfile = (UserProfileSessionData)HttpContext.Current.Session["UserProfile"];
            string UserName = UserProfile.UserName;
            Int64 UserId = UserProfile.UserId;
            UrlSessionData CurrentUrl = (UrlSessionData)HttpContext.Current.Session["CurrentUrl"];

            string ActionName = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
            string ControllerName = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString() + "Controller";

            AccessRightsRepository uar = new AccessRightsRepository();
            string ControllerDescription = EnumService.GetControllerDescription(ControllerName);
            string ActionDescription = EnumService.GetActionDescription(ControllerName, ActionName);
            bool uacresult = uar.UserAccessCheck(UserId, ControllerName, ActionName);
            if (UserName.ToLower() != "adm" && !uacresult && ControllerName != ControllerDescription && ActionDescription != null)
            {
                string RedirectUrl = "/Home/AccessRightsError?CName=" + CurrentUrl.Controller + "&AName=" + CurrentUrl.Action;
                //filterContext.HttpContext.Response.Redirect(RedirectUrl,false);
 
                filterContext.Result = new RedirectToRouteResult("Error_Deafult", new RouteValueDictionary(new { controller = "Home", action = "AccessRightsError", CName = CurrentUrl.Controller, AName = CurrentUrl.Action }));
            }
            else
            {
                CurrentUrl.Controller = ControllerName.Substring(0, ControllerName.IndexOf("Controller"));
                CurrentUrl.Action = ActionName;
                HttpContext.Current.Session["CurrentUrl"] = CurrentUrl;
                base.OnActionExecuting(filterContext);
            }
        }
    }
}