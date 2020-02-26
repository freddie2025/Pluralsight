using Paladin.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paladin.Controllers
{
    public abstract class PaladinController : Controller
    {
        private Guid _tracker;

        protected Guid Tracker
        {
            get { return _tracker; }
            private set { _tracker = value; }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["Tracker"] != null)
            {
                Guid.TryParse(Session["Tracker"].ToString(), out _tracker);
            }
        }

        public ActionResult XML(object model)
        {
            return new XMLResult(model);
        }

        public ActionResult CSV(IEnumerable model)
        {
            return new CSVResult(model, "TestCSV");
        }
    }
}