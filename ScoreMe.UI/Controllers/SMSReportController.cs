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
    public class SMSReportController : Controller
    {
        // GET: SMSReport
        [Description("SMS Report siyahisi")]
        public ActionResult Index()
        {
            SMSReportVM viewModel = new SMSReportVM();
            viewModel = populateDropDownList(viewModel);
            return View(viewModel);
        }

        [Description("SMS Report siyahisi")]
        public ActionResult ReportIndex()
        {
            SMSReportVM viewModel = new SMSReportVM();
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
            List<SMSReportDTO> data = new List<SMSReportDTO>();
           string[] list= userIDName.Split('~');
            if (true)
            {
                 data = GetSMSReportDTOs(int.Parse(list[0]), list[1], year);
            }
          
            //return PartialView("_ReportSearch", data);
            return PartialView("_PartialReport", data);
            
        }

        public List<SMSReportDTO> GetSMSReportDTOs(int userID,string userName, int year)
        {
            SMSRepository repository = new SMSRepository();
            List<SMSReportDTO> RSMSReports = repository.SW_GetSMSReports(userID, userName, year);
            List<SMSReportDTO> RSMSReportShortMsjs = repository.SW_GetSMSReportShortMsjs(userID, userName, year);
            return RSMSReports.Concat(RSMSReportShortMsjs).OrderBy(x=>x.INOUT_EVType).ToList();
        }


        private SMSReportVM populateDropDownList(SMSReportVM viewModel)
        {
            viewModel.UserList = EnumService.GetMessajeUsersByTypeEVID(8);
            return viewModel;
        }
    }
}