using PagedList;
using ScoreMe.DAL;
using ScoreMe.DAL.DBModel;
using ScoreMe.DAL.DTO;
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
    public class PackageController : Controller
    {
        [Description("Paket siyahısı")]
        public ActionResult Index(int? page, string vl, string prm = null)
        {
            PackageRepository repository = new PackageRepository();
            try
            {
                Search search = new Search();

                search = SetValue(page, vl, prm);

                int pageSize = 15;
                int pageNumber = (page ?? 1);

                PackageVM viewModel = new PackageVM();
                viewModel.Search = search;

                viewModel.Search.pageSize = pageSize;
                viewModel.Search.pageNumber = pageNumber;

                viewModel.RPackageList = repository.SW_GetPackages(viewModel.Search);

                viewModel.ListCount = repository.SW_GetPackagesCount(viewModel.Search);
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

        [Description("Yeni paket əlavə etmək")]
        public ActionResult Create()
        {
            PackageVM viewModel = new PackageVM();
            viewModel = poulateDropDownList(viewModel);
            return View(viewModel);

        }
        [HttpPost]
        public ActionResult Create(PackageVM viewModel)
        {

            try
            {
                var UserProfile = (UserProfileSessionData)this.Session["UserProfile"];
                if (UserProfile != null)
                {
                    if (ModelState.IsValid)
                    {
                        tbl_Package item = new tbl_Package()
                        {
                            Mobile_EVID = viewModel.Mobile_EVID,
                            PackageName = viewModel.PackageName,
                            PackageSize = viewModel.PackageSize,
                            Validity = viewModel.Validity,
                            ValidityDesc = viewModel.ValidityDesc,
                            InsertDate = DateTime.Now,
                            InsertUser = UserProfile.UserId
                        };
                        CRUDOperation dataOperations = new CRUDOperation();
                        tbl_Package dbItem = dataOperations.AddPackage(item);
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
                throw new ApplicationException("Invalid model");
            }
            catch (ApplicationException ex)
            {
                viewModel = poulateDropDownList(viewModel);

                return View(viewModel);
            }
           

        }
        [Description("Paket redaktə etmək")]
        public ActionResult Edit(int id)
        {
            PackageVM viewModel = new PackageVM();
            viewModel = poulateDropDownList(viewModel);
            CRUDOperation dataOperations = new CRUDOperation();

            tbl_Package tblItem = dataOperations.GetPackageByID(id);

            viewModel.PackageID = id;
            viewModel.Mobile_EVID = tblItem.Mobile_EVID;
            viewModel.PackageName = tblItem.PackageName;
            viewModel.PackageSize = tblItem.PackageSize;
            viewModel.Validity = tblItem.Validity;
            viewModel.ValidityDesc = tblItem.ValidityDesc;
            return View(viewModel);

        }
        [HttpPost]
        public ActionResult Edit(PackageVM viewModel)
        {
            try
            {
            
                var UserProfile = (UserProfileSessionData)this.Session["UserProfile"];
                if (UserProfile != null)
                {
                    if (ModelState.IsValid)
                    {
                        tbl_Package item = new tbl_Package()
                        {
                            ID = viewModel.PackageID,
                            Mobile_EVID = viewModel.Mobile_EVID,
                            PackageName = viewModel.PackageName,
                            PackageSize = viewModel.PackageSize,
                            Validity = viewModel.Validity,
                            ValidityDesc=viewModel.ValidityDesc,
                            UpdateDate = DateTime.Now,
                            UpdateUser = UserProfile.UserId

                        };

                        CRUDOperation dataOperations = new CRUDOperation();
                        tbl_Package dbItem = dataOperations.UpdatePackage(item);
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
        [Description("Paket sil")]
        public ActionResult Delete(int id)
        {
            try
            {
                CRUDOperation dataOperations = new CRUDOperation();
                var UserProfile = (UserProfileSessionData)this.Session["UserProfile"];
                if (UserProfile != null)
                {
                    dataOperations.DeletePackage(id, UserProfile.UserId);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Error", "Error"));
            }
        }
        private PackageVM poulateDropDownList(PackageVM viewModel)
        {
            viewModel.MobileEVList = EnumService.GetEnumValueListByEcID((int)CategoryEnum.OperatorType);
            return viewModel;
        }


        #region PackagePrice
        public ActionResult PriceIndex(int packageID)
        {
            PackageRepository repository = new PackageRepository();
            List<PackagePriceDTO> packagePrices = repository.GetPackagePricesByPackageID(packageID);
            PackageVM packageVM = new PackageVM();
            packageVM.PackageID = packageID;
            packageVM.RPackagePriceList = packagePrices;
            return View(packageVM);
        }
        public ActionResult CreatePrice(int packageID)
        {
            PackageVM viewModel = new PackageVM();
            viewModel.PackageID = packageID;
            viewModel = populatePriceDropDownList(viewModel);
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult CreatePrice(PackageVM viewModel)
        {
            try
            {
                var UserProfile = (UserProfileSessionData)this.Session["UserProfile"];
                if (UserProfile != null)
                {

                    tbl_PackagePrice price = new tbl_PackagePrice()
                    {
                        PackageID = viewModel.PackageID,
                        Source_EVID = viewModel.Source_EVID,
                        BeginDate = viewModel.BeginDate,
                        EndDate = viewModel.EndDate,
                        Price = viewModel.Price,
                        Point = viewModel.Point,
                        InsertDate = DateTime.Now,
                        InsertUser = UserProfile.UserId

                    };

                    CRUDOperation operations = new CRUDOperation();
                    tbl_PackagePrice itemDB = operations.AddPackagePrice(price);
                    if (itemDB != null)
                    {
                        TempData["success"] = "Ok";
                        TempData["message"] = "Məlumatlar uğurla əlavə olundu";
                        return RedirectToAction("PriceIndex", new { packageID = itemDB.PackageID });

                    }
                    else
                    {
                        TempData["success"] = "notOk";
                        TempData["message"] = "Məlumatlar əlavə olunarkən xəta baş verdi";
                        return RedirectToAction("PriceIndex", new { packageID = itemDB.PackageID });

                    }


                }
                throw new ApplicationException("Invalid model");
            }
            catch (ApplicationException ex)
            {

                viewModel = populatePriceDropDownList(viewModel);
                return View(viewModel);
            }
        }

        public ActionResult EditPrice(int priceID)
        {
            PackageVM viewModel = new PackageVM();
            CRUDOperation operations = new CRUDOperation();
            tbl_PackagePrice price = operations.GetPackagePriceByID(priceID);
            viewModel.PackageID = price.PackageID;
            viewModel.PackagePriceID = price.ID;
            viewModel.Source_EVID = price.Source_EVID==null?0:(Int64)price.Source_EVID;
            viewModel.BeginDate = price.BeginDate;
            viewModel.EndDate = price.EndDate;
            viewModel.Price = price.Price==null?0:(decimal)price.Price;
            viewModel.Point = price.Point == null ? 0 : (decimal)price.Point; 
            viewModel = populatePriceDropDownList(viewModel);
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult EditPrice(PackageVM viewModel)
        {
            try
            {
                var UserProfile = (UserProfileSessionData)this.Session["UserProfile"];
                if (UserProfile != null)
                {

                    tbl_PackagePrice price = new tbl_PackagePrice()
                    {

                        PackageID = viewModel.PackageID,
                        ID=viewModel.PackagePriceID,
                        Source_EVID = viewModel.Source_EVID,
                        BeginDate = viewModel.BeginDate,
                        EndDate = viewModel.EndDate,
                        Price = viewModel.Price,
                        Point = viewModel.Point,
                        UpdateDate = DateTime.Now,
                        UpdateUser = UserProfile.UserId

                    

                    };

                    CRUDOperation operations = new CRUDOperation();
                    tbl_PackagePrice itemDB = operations.UpdatePackagePrice(price);
                    if (itemDB != null)
                    {
                        TempData["success"] = "Ok";
                        TempData["message"] = "Məlumatlar uğurla redakte olundu";
                        return RedirectToAction("PriceIndex", new { packageID = itemDB.PackageID });

                    }
                    else
                    {
                        TempData["success"] = "notOk";
                        TempData["message"] = "Məlumatlar redakte olunarkən xəta baş verdi";
                        return RedirectToAction("PriceIndex", new { packageID = itemDB.PackageID });
                    }


                }
                throw new ApplicationException("Invalid model");
            }
            catch (ApplicationException ex)
            {

                viewModel = populatePriceDropDownList(viewModel);
                return View(viewModel);
            }

        }
        public ActionResult DeletePrice(int priceID, int packageID)
        {
            try
            {
                CRUDOperation dataOperations = new CRUDOperation();
                var UserProfile = (UserProfileSessionData)this.Session["UserProfile"];
                if (UserProfile != null)
                {
                    dataOperations.DeletePackagePrice(priceID, UserProfile.UserId);
                }
                return RedirectToAction("PriceIndex", new { packageID = packageID });
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Error", "Error"));
            }
        }
        private PackageVM populatePriceDropDownList(PackageVM viewModel)
        {
            viewModel.SourceEVList = EnumService.GetEnumValueListByEcID((int)CategoryEnum.SourceType);
            return viewModel;
        }
        #endregion

    }
}