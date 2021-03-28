using ScoreMe.DAL.DTO;
using ScoreMe.DAL.Repositories;
using ScoreMe.UI.Attributes;
using ScoreMe.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScoreMe.UI.Controllers
{
    [LoginCheck]
    [AccessRightsCheck]
    public class ExpenseController : Controller
    {
        // GET: Expense
        [Description("Məxaric və Mədaxil siyahisi")]
        public ActionResult Index()
        {
            SMSReportVM viewModel = new SMSReportVM();
            return View(viewModel);
        }
        public ActionResult AjaxSearch(string userName, int year)
        {
            List<SMSReportShortDTO> data = new List<SMSReportShortDTO>();
            if (userName != "994559387890")
            {

                data = GetShortParseSMSReportDTOs(userName, year);
            }

            //return PartialView("_ReportSearch", data);
            return PartialView("_PartialReport", data);

        }

        public List<SMSReportShortDTO> GetShortParseSMSReportDTOs(string userName, int year)
        {
            SMSRepository repository = new SMSRepository();
            List<SMSReportShortDTO> RSMSReports = repository.SW_GetSMSReportShorParseList(userName, year);
            return RSMSReports;
        }
    }
}