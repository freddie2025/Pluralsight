using AutoMapper;
using Paladin.Controllers;
using Paladin.Infrastructure;
using Paladin.Models;
using Paladin.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Paladin.Areas.Admin.Controllers
{
    public class DataExportController : PaladinController
    {
        private PaladinDbContext _context;

        public DataExportController(PaladinDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetQuotesCSV()
        {
            var applicants = _context.Applicants.ToList();
            var mappedApplicants = new List<ApplicantVM>();
            foreach (var app in applicants)
            {
                mappedApplicants.Add(Mapper.Map<ApplicantVM>(app));
            }

            return CSV(mappedApplicants);
        }

        
    }
}