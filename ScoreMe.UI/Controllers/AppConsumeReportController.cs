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
    public class AppConsumeReportController : Controller
    {
        [Description("Tətbiqlərin Tüketimi Raporu")]
        public ActionResult ReportIndex()
        {
            AppConsumeReportVM viewModel = new AppConsumeReportVM();
            viewModel = populateDropDownList(viewModel);
            viewModel.ReportType = 1;
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

        public ActionResult AjaxSearch(string userIDName, int year, int reportType)
        {
            List<AppConsumeReportDTO> data = new List<AppConsumeReportDTO>();
            try
            {


                string[] list = userIDName.Split('~');
                if (reportType == 1)
                {
                    data = GetAppConsumeReportDTOs(int.Parse(list[0]), list[1], year);
                }
                else if (reportType == 2)
                {
                    data = GetAppCountReportDTOs(int.Parse(list[0]), list[1], year);
                }
            }
            catch (Exception ex)
            {


            }
            //return PartialView("_ReportSearch", data);
            return PartialView("_PartialReport", data);

        }

        public List<AppConsumeReportDTO> GetAppConsumeReportDTOs(int userID, string userName, int year)
        {
            AppConsumeRepository repository = new AppConsumeRepository();
            List<AppConsumeReportDTO> RAppConsumeReports = repository.SW_GetAppConsumeReports(userID, userName, year).OrderBy(x => x.AppType).ToList();
            return RAppConsumeReports;
        }
        public List<AppConsumeReportDTO> GetAppCountReportDTOs(int userID, string userName, int year)
        {
            AppConsumeRepository repository = new AppConsumeRepository();
            List<AppConsumeReportDTO> RAppCountReports = repository.SW_GetAppCountReports(userID, userName, year).OrderBy(x => x.AppType).ToList();
            return RAppCountReports;
        }

        [Description("Tətbiqlərin Birim Tüketimi Raporu")]
        public ActionResult ReportUnitIndex()
        {
            AppConsumeReportVM viewModel = new AppConsumeReportVM();
            viewModel = populateDropDownList(viewModel);
            viewModel.ReportType = 1;
            return View(viewModel);
        }

        public ActionResult UnitAjaxSearch(string userIDName, int year)
        {
            List<AppConsumeReportDTO> data = new List<AppConsumeReportDTO>();
            try
            {


                string[] list = userIDName.Split('~');

                data = GetUnitAppConsumeReportDTOs(int.Parse(list[0]), list[1], year);


            }
            catch (Exception ex)
            {


            }
            //return PartialView("_ReportSearch", data);
            return PartialView("_PartialReportUnit", data);

        }

        public List<AppConsumeReportDTO> GetUnitAppConsumeReportDTOs(int userID, string userName, int year)
        {
            AppConsumeRepository repository = new AppConsumeRepository();
            List<AppConsumeReportDTO> RAppConsumeReports = repository.SW_GetUnitAppCountReports(userID, userName, year).OrderBy(x => x.AppType).ToList();
            return RAppConsumeReports;
        }
        private AppConsumeReportVM populateDropDownList(AppConsumeReportVM viewModel)
        {
            viewModel.UserList = EnumService.GetAppConsumeUsersByTypeEVID(8);
            viewModel.ReportTypeList = EnumService.GetReportTypes();
            return viewModel;
        }
    }
}