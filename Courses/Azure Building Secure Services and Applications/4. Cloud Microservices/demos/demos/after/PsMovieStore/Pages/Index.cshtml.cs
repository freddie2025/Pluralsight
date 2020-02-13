using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace PsMovieStore.Pages
{
    public class IndexModel : PageModel
    {
        HttpClient Client;
        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            Client = httpClientFactory.CreateClient("pricing");
        }

        public string[] Values { get; set; }

        public async Task OnGet()
        {
            var response = await Client.GetAsync("/api/values");
            var content = await response.Content.ReadAsStringAsync();
            Values = JsonConvert.DeserializeObject<string[]>(content);
        }
    }
}
