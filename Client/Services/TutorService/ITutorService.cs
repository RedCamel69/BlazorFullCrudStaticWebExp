using BlazorEcommerceStaticWebApp.Shared;

namespace BlazorEcommerceStaticWebApp.Client.Services.TutorService
{
    //
    public interface ITutorService
    {
        event Action TutorsChanged;

        List<Tutor> Tutors { get; set; }

        String Response { get; set; }
        bool RequestSuccessful { get; set; }  

        Task GetTutors();

        Task<Tutor?> GetTutorById(int id);

        Task CreateTutor(Tutor tutor);

        Task UpdateTutor(Tutor tutor);

        Task DeleteTutor(Tutor product);
    }
}
