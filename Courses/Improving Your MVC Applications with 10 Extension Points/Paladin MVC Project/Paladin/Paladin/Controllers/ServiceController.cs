using AutoMapper;
using Paladin.Infrastructure;
using Paladin.Models;
using Paladin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace Paladin.Controllers
{
    public class ServiceController : Controller
    {
        private PaladinDbContext _context;

        public ServiceController()
        {
            _context = new PaladinDbContext();
        }

        public ActionResult GetApplicantsForReminders()
        {
            var applicants = _context.Applicants.ToList();
            var vmApplicants = new List<ApplicantVM>();
            foreach(var app in applicants)
            {
                vmApplicants.Add(Mapper.Map<ApplicantVM>(app));
            }

            return new XMLResult(vmApplicants);
        }

    }
}