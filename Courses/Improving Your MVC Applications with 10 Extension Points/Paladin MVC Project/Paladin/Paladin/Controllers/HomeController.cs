using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Paladin.Models;

namespace Paladin.Controllers
{
    [OverrideAuthorization]
    public class HomeController : Controller
    {
        private PaladinDbContext _context;

        public HomeController(PaladinDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            ViewBag.Home = "true";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Final()
        {
            Session.Clear();
            return View();
        }

        public ActionResult Clear()
        {
            Session.Clear();
            return View();
        }

        public ActionResult ProgressBar(int currentStage)
        {
            if (Session["Tracker"] != null) //Have they started the workflow?
            {
                Guid tracker;
                if(Guid.TryParse(Session["Tracker"].ToString(), out tracker))
                {
                    var highestStage = _context.Applicants.FirstOrDefault(x => x.ApplicantTracker == tracker).WorkFlowStage;
                    return PartialView(new Progress { CurrentStage = currentStage, HighestStage = highestStage });
                }
                
            }
            //If not, show the first page
            return PartialView(new Progress { CurrentStage = 10, HighestStage = 10 });
        }
    }
}