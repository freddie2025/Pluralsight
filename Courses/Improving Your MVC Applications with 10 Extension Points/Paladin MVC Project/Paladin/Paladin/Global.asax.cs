using AutoMapper;
using Paladin.Infrastructure;
using Paladin.Models;
using Paladin.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Paladin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["ActiveTheme"]))
            {
                var activeTheme = ConfigurationManager.AppSettings["ActiveTheme"];
                ViewEngines.Engines.Insert(0, new ThemeViewEngine(activeTheme));
            };
            ModelBinderProviders.BinderProviders.Add(new XmlModelBinderProvider());
            ValueProviderFactories.Factories.Insert(0, new HttpValueProviderFactory());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Mapper.CreateMap<ApplicantVM, Applicant>();
            Mapper.CreateMap<VehicleVM, Vehicle>();
            Mapper.CreateMap<AddressVM, Address>();
            Mapper.CreateMap<EmploymentVM, Employment>();
            Mapper.CreateMap<ProductsVM, Products>();

            Mapper.CreateMap<Applicant, ApplicantVM>();
            Mapper.CreateMap<Vehicle, VehicleVM>();
            Mapper.CreateMap<Address, AddressVM>();
            Mapper.CreateMap<Employment, EmploymentVM>();
            Mapper.CreateMap<Products, ProductsVM>();
        }
    }
}
