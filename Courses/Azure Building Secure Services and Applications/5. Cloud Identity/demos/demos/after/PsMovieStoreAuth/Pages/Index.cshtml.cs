using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PsMovieStoreAuth.Services;

namespace PsMovieStoreAuth.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApiClient apiClient;

        public string[] Values { get; set; }

        public IndexModel(ApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task OnGet()
        {
            Values = await apiClient.GetValues();
        }
    }
}
