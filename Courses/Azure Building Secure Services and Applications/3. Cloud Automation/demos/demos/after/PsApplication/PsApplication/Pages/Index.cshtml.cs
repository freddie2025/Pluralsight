using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace PsApplication.Pages
{
    public class IndexModel : PageModel
    {
        public string Secret { get; set; }

        public void OnGet([FromServices] IConfiguration config)
        {
            Secret = config["Secret"] ?? "No secret found";
        }
    }
}
