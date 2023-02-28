using BlazorEcommerceStaticWebApp.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorEcommerceStaticWebApp.Client.Services.BusinessService
{
    public class BusinessService : IBusinessService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManger;

        public event Action BusinessesChanged;

        public BusinessService(HttpClient http, NavigationManager navigationManger)
        {
            _http = http;
            _navigationManger = navigationManger;
        }

        public List<Business> Businesses { get; set; } = new List<Business>();

        public async Task CreateBusiness(Business business)
        {
            await _http.PostAsJsonAsync("api/business", business);
            //  _navigationManger.NavigateTo("tutors");
        }

        public async Task GetBusinesses()
        {

            //todo: erorr handling strategy
            var res =
                await _http.GetFromJsonAsync<ServiceResponse<List<Business>>>("api/Businesses");

            if (res != null && res.Data != null)
                Businesses = res.Data;

            BusinessesChanged.Invoke();
        }
    }
}
