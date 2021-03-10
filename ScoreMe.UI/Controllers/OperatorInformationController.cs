using PagedList;
using ScoreMe.DAL;
using ScoreMe.DAL.DBModel;
using ScoreMe.DAL.Objects;
using ScoreMe.DAL.Repositories;
using ScoreMe.UI.Attributes;
using ScoreMe.UI.Enums;
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
    public class OperatorInformationController : Controller
    {
        [Description("Operator məlumatlarının siyahısı")]
        public ActionResult Index(int? page, string vl, string prm = null)
        {
            OperatorInformationRepository repository = new OperatorInformationRepository();
            try
            {
                Search search = new Search();

                search = SetValue(page, vl, prm);

                int pageSize = 15;
                int pageNumber = (page ?? 1);

                OperatorInformationVM viewModel = new OperatorInformationVM();
                viewModel.Search = search;

                viewModel.Search.pageSize = pageSize;
                viewModel.Search.pageNumber = pageNumber;

                viewModel.ROperatorInformationList = repository.SW_GetOperatorInformations(viewModel.Search);

                viewModel.ListCount = repository.SW_GetOperatorInformationsCount(viewModel.Search);
                int[] pc = new int[viewModel.ListCount];

                viewModel.Paging = pc.ToPagedList(pageNumber, pageSize);



                return Request.IsAjaxRequest()
              ? (ActionResult)PartialView("PartialIndex", viewModel)
              : View(viewModel);
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

        [Description("Yeni Operator məlumatını əlavə etmək")]
        public ActionResult Create()
        {
            OperatorInformationVM viewModel = new OperatorInformationVM();
            viewModel = poulateDropDownList(viewModel);
            return View(viewModel);

        }
        [HttpPost]
        public ActionResult Create(OperatorInformationVM viewModel)
        {

            try
            {
                var UserProfile = (UserProfileSessionData)this.Session["UserProfile"];
                if (UserProfile != null)
                {

                    tbl_OperatorInformation item = new tbl_OperatorInformation()
                    {
                        OperatorType_EVID = viewModel.OperatorTypeEVID,
                        Name = viewModel.Name,
                        OperatorChanelType_EVID = viewModel.OperatorChanelTypeEVID,
                        InOutType_EVID = viewModel.InOutTypeEVID,
                        Price = viewModel.Price,
                        Point=viewModel.Point,
                        InsertDate = DateTime.Now,
                        InsertUser = UserProfile.UserId
                    };
                    CRUDOperation dataOperations = new CRUDOperation();
                    tbl_OperatorInformation dbItem = dataOperations.AddOperatorInformation(item);
                    if (dbItem != null)
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
            catch (ApplicationException ex)
            {
                viewModel = poulateDropDownList(viewModel);

                return View(viewModel);
            }
            throw new ApplicationException("Invalid model");

        }
        [Description("Operator məlumatını redaktə etmək")]
        public ActionResult Edit(int id)
        {
            OperatorInformationVM viewModel = new OperatorInformationVM();
            viewModel = poulateDropDownList(viewModel);
            CRUDOperation dataOperations = new CRUDOperation();

            tbl_OperatorInformation tblItem = dataOperations.GetOperatorInformationById(id);

            viewModel.ID = id;
            viewModel.OperatorTypeEVID = tblItem.OperatorType_EVID==null?0:(int)tblItem.OperatorType_EVID;
            viewModel.Name = tblItem.Name;
            viewModel.OperatorChanelTypeEVID = tblItem.OperatorChanelType_EVID == null ? 0 : (int)tblItem.OperatorChanelType_EVID;
            viewModel.InOutTypeEVID = tblItem.InOutType_EVID == null ? 0 : (int)tblItem.InOutType_EVID;
            viewModel.Price = tblItem.Price==null?0:(decimal)tblItem.Price;
            viewModel.Point = tblItem.Point == null ? 0 : (decimal)tblItem.Point;

            return View(viewModel);

        }
        [HttpPost]
        public ActionResult Edit(OperatorInformationVM viewModel)
        {
            try
            {

                var UserProfile = (UserProfileSessionData)this.Session["UserProfile"];
                if (UserProfile != null)
                {
                    if (ModelState.IsValid)
                    {
                        tbl_OperatorInformation item = new tbl_OperatorInformation()
                        {
                            ID=viewModel.ID,
                            OperatorType_EVID = viewModel.OperatorTypeEVID,
                            Name = viewModel.Name,
                            OperatorChanelType_EVID = viewModel.OperatorChanelTypeEVID,
                            InOutType_EVID = viewModel.InOutTypeEVID,
                            Price = viewModel.Price,
                            Point = viewModel.Point,
                            UpdateDate = DateTime.Now,
                            UpdateUser = UserProfile.UserId
                        };

                        CRUDOperation dataOperations = new CRUDOperation();
                        tbl_OperatorInformation dbItem = dataOperations.UpdateOperatorInformation(item);
                        if (dbItem != null)
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
                }

                throw new ApplicationException("Invalid model");
            }
            catch (ApplicationException ex)
            {
                viewModel = poulateDropDownList(viewModel);
                return View(viewModel);
            }

        }
        [Description("Operator məlumatını sil")]
        public ActionResult Delete(int id)
        {
            try
            {
                CRUDOperation dataOperations = new CRUDOperation();
                var UserProfile = (UserProfileSessionData)this.Session["UserProfile"];
                if (UserProfile != null)
                {
                    dataOperations.DeleteOperatorInformation(id, UserProfile.UserId);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Error", "Error"));
            }
        }
        private OperatorInformationVM poulateDropDownList(OperatorInformationVM viewModel)
        {
            viewModel.OperatorTypeList = EnumService.GetEnumValueListByEcID((int)CategoryEnum.OperatorType);
            viewModel.OperatorChanelTypeList = EnumService.GetEnumValueListByEcID((int)CategoryEnum.OperatorChanelType);
            viewModel.InOutTypeList = EnumService.GetEnumValueListByEcIDForINOUT((int)CategoryEnum.InOutType);
            viewModel.OperatorPrefixList = EnumService.GetEnumValueListByEcIDForPrefix((int)CategoryEnum.OperatorPrefixType);
            return viewModel;
        }
    }
}