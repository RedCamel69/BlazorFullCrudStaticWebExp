using BlazorEcommerceStaticWebApp.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorEcommerceStaticWebApp.Client.Services.LanguageService
{
    public class LanguageService : ILanguageService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManger;

        public List<Language> Languages { get; set; } = new List<Language>();

        public event Action LanguagesChanged;

        public LanguageService(HttpClient http, NavigationManager navigationManger)
        {
            _http = http;
            _navigationManger = navigationManger;
        }

        public async Task GetLanguages()
        {
            var res =
               await _http.GetFromJsonAsync<ServiceResponse<List<Language>>>("api/Languages");



            if (res != null && res.Data != null)
                Languages = res.Data;


            LanguagesChanged.Invoke();
        }
    }
}
