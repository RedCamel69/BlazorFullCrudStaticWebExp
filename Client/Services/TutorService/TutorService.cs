using BlazorEcommerceStaticWebApp.Shared;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Json;

namespace BlazorEcommerceStaticWebApp.Client.Services.TutorService
{
    public class TutorService : ITutorService
    {

        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManger;
        private readonly IConfiguration _config;

        private bool _showServiceRequestResponses;
        
        public event Action TutorsChanged;

        public List<Tutor> Tutors { get; set; } = new List<Tutor>();
        public string Response { get;set;}        
        public bool RequestSuccessful { get; set ; }

        public TutorService(HttpClient http, NavigationManager navigationManger, IConfiguration configuration)
        {
            _http = http;
            _navigationManger = navigationManger;
            _config = configuration;

            _showServiceRequestResponses = Convert.ToBoolean(_config["Show_Service_Request_Responses"]);
        }

        public async Task GetTutors()
        {
            var res =
                await _http.GetFromJsonAsync<ServiceResponse<List<Tutor>>>("api/Tutors");



            if (res != null && res.Data != null)
            {
                Tutors = res.Data;
            }

            if (res != null)
            {
                RequestSuccessful = res.Success;
                if (!res.Success)
                {
                    Response = "Error retrieving Tutors " + (_showServiceRequestResponses ? res.Message : String.Empty);
                }
                else
                {
                    Response = "Tutors successfully retrieved " + (_showServiceRequestResponses ? res.Message : String.Empty);
                }
                
            }


            //CurrentPage = 1;
            //PageCount = 0;

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
            await _http.DeleteAsync($"api/tutor/{tutor.TutorId}");
            _navigationManger.NavigateTo("tutors");
        }
    }
}
