using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PsMovieStoreAuth.Services
{
    public class ApiClient
    {
        private readonly string resourceId;
        private readonly string authority;
        private readonly string appId;
        private readonly string appSecret;
        private readonly HttpClient client;
        private bool tokenSet = false;

        public ApiClient(HttpClient client, 
                         IConfiguration configuration)
        {
            resourceId = configuration["Api:ResourceId"];
            authority = 
                $"{configuration["AzureAd:Instance"]}{configuration["AzureAd:TenantId"]}";

            appId = configuration["AzureAd:ClientId"];
            appSecret = configuration["AzureAd:ClientSecret"];

            this.client = client;
            this.client.BaseAddress = new Uri(configuration["Api:BaseUrl"]);
        }

        public async Task SetToken()
        {
            if (!tokenSet)
            {
                var authContext = new AuthenticationContext(authority);
                var credential = new ClientCredential(appId, appSecret);
                var authResult = await authContext.AcquireTokenAsync(resourceId, credential);
                var token = authResult.AccessToken;

                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                tokenSet = true;
            }
        }

        public async Task<string[]> GetValues()
        {
            await SetToken();

            var response = await client.GetAsync("/api/values");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"{response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<string[]>(content);
        }
    }
}
