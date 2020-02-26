using AutoMapper;
using Paladin.Infrastructure;
using Paladin.Models;
using Paladin.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paladin.Controllers
{
    [WorkflowFilter(
        MinRequiredStage = (int)WorkflowValues.EmploymentInfo,
        CurrentStage = (int)WorkflowValues.VehicleInfo)]
    public class VehicleController : Controller
    {
        private PaladinDbContext _context;

        public VehicleController(PaladinDbContext context)
        {
            _context = context;
        }
        // GET: Vehicle
        public ActionResult VehicleInfo()
        {
            if (Session["Tracker"] == null)
            {
                return RedirectToAction("ApplicantInfo", "Applicant");
            }
            var tracker = (Guid)Session["Tracker"];

            var vehicle = new VehicleVM();
            var existingVehicle = _context.Applicants.FirstOrDefault(x => x.ApplicantTracker == tracker).Vehicle.FirstOrDefault();
            if (existingVehicle != null)
            {
                vehicle = Mapper.Map<VehicleVM>(existingVehicle);
            }
            
            return View(vehicle);
        }

        [HttpPost]
        public ActionResult VehicleInfo(VehicleVM vm)
        {
            if (Session["Tracker"] == null)
            {
                return RedirectToAction("ApplicantInfo", "Applicant");
            }
            var tracker = (Guid)Session["Tracker"];

            if (ModelState.IsValid)
            {
                var applicant = _context.Applicants.FirstOrDefault(x => x.ApplicantTracker == tracker);

                var existingVehicle = applicant.Vehicle.FirstOrDefault();
                if (existingVehicle != null)
                {
                    Mapper.Map(vm, existingVehicle);
                    _context.Entry(existingVehicle).State = EntityState.Modified;
                }
                else
                {
                    var vehicle = Mapper.Map<Vehicle>(vm);
                    applicant.Vehicle.Add(vehicle);
                }

                _context.SaveChanges();
                return RedirectToAction("ProductInfo", "Products");
            }

            return View();
        }
    }
}