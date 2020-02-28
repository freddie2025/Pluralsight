// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc;

namespace MvcSandbox.Controllers
{
    [TypeFilter(typeof(KillSwitchAuthorizationFilter))]
    public class HomeController : Controller
    {
        [ModelBinder]
        public string Id { get; set; }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/contact-us", Name = "Contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [Route("/contact-us", Name = "Contact")]
        public IActionResult Contact([FromForm]Contact info)
        {
            // Contact logic

            return View();
        }
    }

    public class Contact
    {
        public string Name { get; set; }
        public string Message { get; set; }

    }
}
