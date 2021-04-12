using ScoreMe.DAL.DTO;
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

namespace ScoreMe.UI.Controllers
{
    [LoginCheck]
    [AccessRightsCheck]
    public class CallReportController : Controller
    {
        

        [Description("CALL Report siyahisi")]
        public ActionResult ReportIndex()
        {
            CALLReportVM viewModel = new CALLReportVM();
            viewModel = populateDropDownList(viewModel);
            return View(viewModel);
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

        public ActionResult AjaxSearch(string userIDName, int year)
        {
            List<CALLReportDTO> data = new List<CALLReportDTO>();
            string[] list = userIDName.Split('~');
            if (true)
            {
                data = GetCALLReportDTOs(int.Parse(list[0]), list[1], year);
            }

            //return PartialView("_ReportSearch", data);
            return PartialView("_PartialReport", data);

        }

        public List<CALLReportDTO> GetCALLReportDTOs(int userID, string userName, int year)
        {
            CALLRepository repository = new CALLRepository();
            List<CALLReportDTO> RCALLReports = repository.SW_GetCALLReports(userID, userName, year).OrderBy(x => x.Name).ToList();
            return RCALLReports;
        }


        private CALLReportVM populateDropDownList(CALLReportVM viewModel)
        {
            viewModel.UserList = EnumService.GetCALLUsersByTypeEVID(8);
            return viewModel;
        }
    }
}