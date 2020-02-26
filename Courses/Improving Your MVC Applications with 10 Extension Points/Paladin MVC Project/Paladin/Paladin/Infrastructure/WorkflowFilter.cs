using Paladin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Paladin.Infrastructure
{
    public class WorkflowFilter : FilterAttribute, IActionFilter
    {
        private int _highestCompletedStage;
        public int MinRequiredStage { get; set; }
        public int CurrentStage { get; set; }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var applicantId = filterContext.HttpContext.Session["Tracker"];
            if (applicantId != null)
            {
                Guid tracker;
                if (Guid.TryParse(applicantId.ToString(), out tracker))
                {
                    var _context = DependencyResolver.Current.GetService<PaladinDbContext>();
                    _highestCompletedStage = _context.Applicants.FirstOrDefault(x => x.ApplicantTracker == tracker).WorkFlowStage;
                    if (MinRequiredStage > _highestCompletedStage)
                    {

                        switch (_highestCompletedStage)
                        {
                            case (int)WorkflowValues.ApplicantInfo:
                                filterContext.Result = GenerateRedirectUrl("ApplicantInfo", "Applicant");
                                break;

                            case (int)WorkflowValues.AddressInfo:
                                filterContext.Result = GenerateRedirectUrl("AddressInfo", "Address");
                                break;

                            case (int)WorkflowValues.EmploymentInfo:
                                filterContext.Result = GenerateRedirectUrl("EmploymentInfo", "Employment");
                                break;

                            case (int)WorkflowValues.VehicleInfo:
                                filterContext.Result = GenerateRedirectUrl("VehicleInfo", "Vehicle");
                                break;

                            case (int)WorkflowValues.ProductInfo:
                                filterContext.Result = GenerateRedirectUrl("ProductInfo", "Products");
                                break;
                        }
                    }
                }
            }
            else
            {
                if (CurrentStage != (int)WorkflowValues.ApplicantInfo)
                {
                    filterContext.Result = GenerateRedirectUrl("ApplicantInfo", "Applicant");
                }
            }
        }

        private RedirectToRouteResult GenerateRedirectUrl(string action, string controller)
        {
            return new RedirectToRouteResult(new RouteValueDictionary(new { action = action, controller = controller }));
        }

        
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var _context = DependencyResolver.Current.GetService<PaladinDbContext>();
            var sessionId = HttpContext.Current.Session["Tracker"];
            if (sessionId != null)
            {
                Guid tracker;
                if (Guid.TryParse(sessionId.ToString(), out tracker))
                {
                    if (filterContext.HttpContext.Request.RequestType == "POST" && CurrentStage >= _highestCompletedStage)
                    {
                        var applicant = _context.Applicants.FirstOrDefault(x => x.ApplicantTracker == tracker);
                        applicant.WorkFlowStage = CurrentStage;
                        _context.SaveChanges();
                    }
                }
            }
        }
    }
}