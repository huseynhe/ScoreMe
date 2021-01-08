using ScoreMe.DAL.DTO;
using ScoreMe.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScoreMe.UI.Controllers
{
    public class AmReportController : Controller
    {
        ReportRepository repository = new ReportRepository();
        [HttpGet]
        public JsonResult PieChart3D()
        {
            long sum = 0;
            List<ReportDTO> modelList = repository.GetUserReports();
            foreach (var item in modelList)
            {
                sum = sum + (int)item.count;
            }
            return Json(new { data = modelList, sum = sum }, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public JsonResult Variableheight3DPieChart()
        {
            long sum = 0;
            List<ReportDTO> modelList = repository.GetProviderReports();
            foreach (var item in modelList)
            {
                sum = sum + (int)item.count;
            }
            return Json(new { data = modelList, sum = sum }, JsonRequestBehavior.AllowGet);

        }
        
    }
}