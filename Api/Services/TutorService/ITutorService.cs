using BlazorEcommerceStaticWebApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.TutorService
{
    public interface ITutorService
    {
        ServiceResponse<List<Tutor>> GetTutors();

        ServiceResponse<Tutor> GetTutor(int Id);

        Task<ServiceResponse<Tutor>> UpdateTutor(Tutor tutor);

        Task<ServiceResponse<Tutor>> CreateTutor(Tutor tutor);


        Task<ServiceResponse<bool>> DeleteTutorAsync(int tutorId);

    }
}
