using PagedList;
using ScoreMe.DAL.Objects;
using ScoreMe.DAL.Repositories;
using ScoreMe.UI.Attributes;
using ScoreMe.UI.Models;
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
    public class ProviderController : Controller
    {
        // GET: Provider
        [Description("Provderlərin siyahısı")]
        public ActionResult Index(int? page, string vl, string prm = null)
        {
            ProviderRepository repository = new ProviderRepository();
            try
            {
                Search search = new Search();

                search = SetValue(page, vl, prm);

                int pageSize = 15;
                int pageNumber = (page ?? 1);

                ProviderVM viewModel = new ProviderVM();
                viewModel.Search = search;

                viewModel.Search.pageSize = pageSize;
                viewModel.Search.pageNumber = pageNumber;

                viewModel.RProviderList = repository.SW_GetProviders(viewModel.Search);

                viewModel.ListCount = repository.SW_GetProvidersCount(viewModel.Search);
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

        public ActionResult ProposalIndex(int? page, Int64? providerId, string vl, string prm = null)
        {
            TempData["providerId"] = providerId;
            ProposalVM proposalVM = new ProposalVM();
            ProposalRepository proposalRepository = new ProposalRepository();
            try
            {
                Search search = new Search();
                search = SetValue(page, vl, prm);
                search.ProviderID = (Int64)providerId;


                int pageSize = 15;
                int pageNumber = (page ?? 1);

                proposalVM.Search = search;
                proposalVM.Search.pageSize = pageSize;
                proposalVM.Search.pageNumber = pageNumber;
                proposalVM.ProviderID = search.ProviderID;
                proposalVM.RProposalList = proposalRepository.SW_GePropsals(proposalVM.Search);

                proposalVM.ListCount = proposalRepository.SW_GePropsalssCount(proposalVM.Search);
                int[] pc = new int[proposalVM.ListCount];

                proposalVM.Paging = pc.ToPagedList(pageNumber, pageSize);

                //  viewModel.vehicleId = vehicleId;

                return Request.IsAjaxRequest()
              ? (ActionResult)PartialView("ProposalPartialIndex", proposalVM)
              : View(proposalVM);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Error", "Error"));
            }

        }
    }
}