using ScoreMe.DAL.DTO;
using ScoreMe.DAL.Repositories;
using ScoreMe.UI.Attributes;
using ScoreMe.UI.Models;
using ScoreMe.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScoreMe.UI.Controllers
{
    [LoginCheck]
    [AccessRightsCheck]
    public class TotatReporterController : Controller
    {
        // GET: TotatReporter
        [Description("Toplam bal və xərc Raporu")]
        public ActionResult ReportIndex()
        {
            TotalPointAndPriceReportVM viewModel = new TotalPointAndPriceReportVM();
            viewModel = populateDropDownList(viewModel);
            viewModel.ReportType = 1;
            return View(viewModel);
        }
        public ActionResult AjaxSearch(int reportType, int year)
        {
            List<TotalPointAndPriceDTO> data = new List<TotalPointAndPriceDTO>();
            try
            {

                if (reportType == 2)
                {
                    data = GetTotalPointReportDTOs( year);
                }
                else if (reportType == 3)
                {
                    data = GetTotalPriceReportDTOs( year);
                }
            }
            catch (Exception ex)
            {


            }
            //return PartialView("_ReportSearch", data);
            return PartialView("_PartialReport", data);

        }

        public List<TotalPointAndPriceDTO> GetTotalPointReportDTOs( int year)
        {
            TotalPointAndPriceRepository repository = new TotalPointAndPriceRepository();
            List<TotalPointAndPriceDTO> RTotalPointReports = repository.SW_GetTotalPointReports( year).ToList();
            return RTotalPointReports;
        }
        public List<TotalPointAndPriceDTO> GetTotalPriceReportDTOs( int year)
        {
            AppConsumeRepository repository = new AppConsumeRepository();
            //List<AppConsumeReportDTO> RAppCountReports = repository.SW_GetAppCountReports(userID, userName, year).OrderBy(x => x.AppType).ToList();
            return null;
        }
        private TotalPointAndPriceReportVM populateDropDownList(TotalPointAndPriceReportVM viewModel)
        {
         
            viewModel.ReportTypeList = EnumService.GetGroupReportTypes();
            return viewModel;
        }
    }
}