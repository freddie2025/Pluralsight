using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paladin.Infrastructure
{
    public enum WorkflowValues
    {
        Begin = 0,
        ApplicantInfo = 10,
        AddressInfo = 20,
        EmploymentInfo = 30,
        VehicleInfo = 40,
        ProductInfo = 50,
        Final = 60
    }
}