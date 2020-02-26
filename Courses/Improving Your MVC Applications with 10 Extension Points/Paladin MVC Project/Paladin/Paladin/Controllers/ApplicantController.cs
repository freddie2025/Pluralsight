using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Paladin.Models;
using System.Data.Entity;
using AutoMapper;
using Paladin.ViewModels;
using Paladin.Infrastructure;

namespace Paladin.Controllers
{
    [WorkflowFilter(
        MinRequiredStage = (int)WorkflowValues.Begin,
        CurrentStage = (int)WorkflowValues.ApplicantInfo)]
    public class ApplicantController : Controller
    {
        private PaladinDbContext _context;

        public ApplicantController(PaladinDbContext context) 
        {
            _context = context;
        }

        // GET: Applicant
        [HttpGet]
        public ActionResult ApplicantInfo()
        {
            var applicant = new ApplicantVM();
            if (Session["Tracker"] != null)
            {
                var tracker = (Guid)Session["Tracker"];
                applicant = Mapper.Map<ApplicantVM>(_context.Applicants.FirstOrDefault(x => x.ApplicantTracker == tracker));
            }

            return View(applicant);
        }

        [HttpPost]
        public ActionResult ApplicantInfo(ApplicantVM vm)
        {
            
            if (ModelState.IsValid)
            {
                if (Session["Tracker"] == null)
                {
                    Session["Tracker"] = Guid.NewGuid();
                }
                var tracker = (Guid)Session["Tracker"];

                var existingApplicant = _context.Applicants.FirstOrDefault(x => x.ApplicantTracker == tracker);
                if (existingApplicant != null)
                {
                    Mapper.Map(vm, existingApplicant);
                    _context.Entry(existingApplicant).State = EntityState.Modified;
                }
                else
                {
                    var newApplicant = Mapper.Map<Applicant>(vm);
                    newApplicant.ApplicantTracker = tracker;
                    _context.Applicants.Add(newApplicant);
                }
                _context.SaveChanges();
                
                return RedirectToAction("AddressInfo", "Address");
            }

            return View(vm);
        }
    }
}