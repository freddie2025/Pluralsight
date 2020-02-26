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
        MinRequiredStage = (int)WorkflowValues.ApplicantInfo,
        CurrentStage = (int)WorkflowValues.AddressInfo)]
    public class AddressController : PaladinController
    {
        private PaladinDbContext _context;

        public AddressController(PaladinDbContext context)
        {
            _context = context;
        }
        // GET: Address
        public ActionResult AddressInfo()
        {
            var addresses = new Addresses();
            var applicant = _context.Applicants.FirstOrDefault(x => x.ApplicantTracker == Tracker);
            var existingMain = applicant.Addresses.FirstOrDefault(x => x.IsMailing == false);
            if (existingMain != null)
            {
                addresses.MainAddress = Mapper.Map<AddressVM>(existingMain);
            }

            var existingMailing = applicant.Addresses.FirstOrDefault(x => x.IsMailing == true);
            if (existingMailing != null)
            {
                addresses.MailingAddress = Mapper.Map<AddressVM>(existingMailing);
            }
            
            return View(addresses);
        }

        [HttpPost]
        public ActionResult AddressInfo(Addresses vm)
        {
            if (Session["Tracker"] == null)
            {
                return RedirectToAction("ApplicantInfo", "Applicant");
            }
            var tracker = (Guid)Session["Tracker"];

            if (ModelState.IsValid)
            {
                var applicant = _context.Applicants.FirstOrDefault(x => x.ApplicantTracker == tracker);

                //Check if main address already exists, if so update it
                var existingMain = _context.Addresses.FirstOrDefault(x => x.ApplicantId == applicant.ApplicantId && x.IsMailing == false);
                if (existingMain != null)
                {
                    Mapper.Map(vm.MainAddress, existingMain);
                    _context.Entry(existingMain).State = EntityState.Modified;
                }
                else
                {
                    vm.MainAddress.IsMailing = false;
                    var newMainAddress = Mapper.Map<Address>(vm.MainAddress);
                    applicant.Addresses.Add(newMainAddress);
                }

                //Check if mailing address already exists, if so update it
                var existingMailing = _context.Addresses.FirstOrDefault(x => x.ApplicantId == applicant.ApplicantId && x.IsMailing == true);
                if (existingMailing != null)
                {
                    Mapper.Map(vm.MailingAddress, existingMailing);
                    _context.Entry(existingMailing).State = EntityState.Modified;
                }
                else
                {
                    vm.MailingAddress.IsMailing = true;
                    var newMailingAddress = Mapper.Map<Address>(vm.MailingAddress);
                    applicant.Addresses.Add(newMailingAddress);
                }
                _context.SaveChanges();
                return RedirectToAction("EmploymentInfo", "Employment");
            }
            return View();
        }
    }
}