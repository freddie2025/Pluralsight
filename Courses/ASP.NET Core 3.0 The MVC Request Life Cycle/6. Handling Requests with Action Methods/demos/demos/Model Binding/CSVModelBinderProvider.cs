using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MvcSandbox.Controllers;

namespace MvcSandbox.Model_Binding
{
    public class CSVModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if(context.Metadata.ModelType == typeof(List<Order>))
            {
                return new CSVModelBinder();
            }

            return null;
        }
    }
}
