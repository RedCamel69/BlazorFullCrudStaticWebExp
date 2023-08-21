using BlazorEcommerceStaticWebApp.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Net;

namespace BlazorEcommerceStaticWebApp.Client.Services.Admin
{
    public class SyncService : ISyncService
    {
        private readonly HttpClient _http;

        public SyncService(HttpClient http)
        {
            _http = http;           
        }

        public async Task Sync()
        {
            try
            {
                var result = await _http.GetAsync($"https://nice-ocean-07e29d003.3.azurestaticapps.net/api/dump");
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var data = await result.Content.ReadFromJsonAsync<Everything>();

                    //todo: Inspect response for errors
                    if (data != null)
                    {
                        var tutors = data.Tutors.ToList();
                        var nusinesses = data.Businesses.ToList();
                    }
                }
            }catch (Exception ex)
            {
                var message = ex.Message;
            }

            return;
        }
    }
}
