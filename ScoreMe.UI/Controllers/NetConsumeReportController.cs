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
    public class NetConsumeReportController : Controller
    {
        [Description("Internet Tüketimi Raporu")]
        public ActionResult ReportIndex()
        {
            NetConsumeReportVM viewModel = new NetConsumeReportVM();
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
            List<NetConsumeReportDTO> data = new List<NetConsumeReportDTO>();
            string[] list = userIDName.Split('~');
            if (true)
            {
                data = GetNetConsumeReportDTOs(int.Parse(list[0]), list[1], year);
            }

            //return PartialView("_ReportSearch", data);
            return PartialView("_PartialReport", data);

        }

        public List<NetConsumeReportDTO> GetNetConsumeReportDTOs(int userID, string userName, int year)
        {
            NetConsumeRepository repository = new NetConsumeRepository();
            List<NetConsumeReportDTO> RNetConsumeReports = repository.SW_GetNetConsumeReports(userID, userName, year).OrderBy(x => x.SourceDesc).ToList();
            return RNetConsumeReports;
        }

        private NetConsumeReportVM populateDropDownList(NetConsumeReportVM viewModel)
        {
            viewModel.UserList = EnumService.GetNetConsumeUsersByTypeEVID(8);
            return viewModel;
        }
    }
}