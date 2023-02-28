using BlazorEcommerceStaticWebApp.Shared;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Json;

namespace BlazorEcommerceStaticWebApp.Client.Services.TutorService
{
    public class TutorService : ITutorService
    {
        //
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManger;

        public event Action TutorsChanged;

        public List<Tutor> Tutors { get; set; } = new List<Tutor>();

        public TutorService(HttpClient http, NavigationManager navigationManger)
        {
            _http = http;
            _navigationManger = navigationManger;
        }

        public async Task GetTutors()
        {
            var res =
                await _http.GetFromJsonAsync<ServiceResponse<List<Tutor>>>("api/Tutors");



            if (res != null && res.Data != null)
                Tutors = res.Data;

            //CurrentPage = 1;
            //PageCount = 0;

            //if (Tutors.Count == 0)
            //{
            //    Message = "No Products Found";
            //}

            //if (Products.Count > 0)
            //{
            //    Message = "Products Found";
            //}

            TutorsChanged.Invoke();
        }

        public async Task<Tutor?> GetTutorById(int id)
        {
            var result = await _http.GetAsync($"api/tutor/{id}");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var result2 = await result.Content.ReadFromJsonAsync<ServiceResponse<Tutor>>();
                return result2.Data;
            }
            return null;
        }

        public async Task CreateTutor(Tutor tutor)
        {
            await _http.PostAsJsonAsync("api/tutor", tutor);
            _navigationManger.NavigateTo("tutors");
        }

        public async Task UpdateTutor(Tutor tutor)
        {
            await _http.PutAsJsonAsync("api/tutor", tutor);
            _navigationManger.NavigateTo("tutors");
        }

        public async Task DeleteTutor(Tutor tutor)
        {
            await _http.DeleteAsync($"api/tutor/{tutor.Id}");
            _navigationManger.NavigateTo("tutors");
        }
    }
}
