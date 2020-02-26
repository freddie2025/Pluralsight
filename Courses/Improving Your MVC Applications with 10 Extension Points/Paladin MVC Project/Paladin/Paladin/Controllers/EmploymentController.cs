using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Paladin.Models;
using System.Data.Entity;
using Paladin.ViewModels;
using AutoMapper;
using Paladin.Infrastructure;

namespace Paladin.Controllers
{
    [WorkflowFilter(
        MinRequiredStage = (int)WorkflowValues.AddressInfo,
        CurrentStage = (int)WorkflowValues.EmploymentInfo)]
    public class EmploymentController : Controller
    {
        private PaladinDbContext _context;

        public EmploymentController(PaladinDbContext context)
        {
            _context = context;
        }
        // GET: Employment
        public ActionResult EmploymentInfo()
        {
            if (Session["Tracker"] == null)
            {
                return RedirectToAction("ApplicantInfo", "Applicant");
            }
            var tracker = (Guid)Session["Tracker"];

            var employment = new Employments();
            var existingPrimary = _context.Applicants.FirstOrDefault(x => x.ApplicantTracker == tracker).Employment.FirstOrDefault(x => x.IsPrimary);
            if (existingPrimary != null)
            {
                employment.PrimaryEmployer = Mapper.Map<EmploymentVM>(existingPrimary);
            }

            var existingPrevious = _context.Applicants.FirstOrDefault(x => x.ApplicantTracker == tracker).Employment.FirstOrDefault(x => x.IsPrimary == false);
            if (existingPrevious != null)
            {
                employment.PreviousEmployer = Mapper.Map<EmploymentVM>(existingPrevious);
            }
            
            return View(employment);
        }

        [HttpPost]
        public ActionResult EmploymentInfo(Employments vm)
        {
            if (Session["Tracker"] == null)
            {
                return RedirectToAction("ApplicantInfo", "Applicant");
            }
            var tracker = (Guid)Session["Tracker"];

            if (ModelState.IsValid)
            {
                var applicant = _context.Applicants.FirstOrDefault(x => x.ApplicantTracker == tracker);

                var existingEmployment = applicant.Employment.FirstOrDefault(x => x.IsPrimary);
                if (existingEmployment != null)
                {
                    Mapper.Map(vm.PrimaryEmployer, existingEmployment);
                    _context.Entry(existingEmployment).State = EntityState.Modified;
                }
                else
                {
                    var newEmployment = Mapper.Map<Employment>(vm.PrimaryEmployer);
                    newEmployment.IsPrimary = true;
                    applicant.Employment.Add(newEmployment);
                }


                var existingPrevious = applicant.Employment.FirstOrDefault(x => x.IsPrimary == false);
                if (existingPrevious != null)
                {
                    Mapper.Map(vm.PreviousEmployer, existingPrevious);
                    _context.Entry(existingPrevious).State = EntityState.Modified;
                }
                else
                {
                    var newEmployment = Mapper.Map<Employment>(vm.PreviousEmployer);
                    newEmployment.IsPrimary = false;
                    applicant.Employment.Add(newEmployment);
                }

                _context.SaveChanges();
                return RedirectToAction("VehicleInfo", "Vehicle");
            }
            return View();
        }
    }
}