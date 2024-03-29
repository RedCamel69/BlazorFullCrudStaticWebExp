﻿using BlazorEcommerceStaticWebApp.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services.TutorService
{
    public interface ITutorService
    {
        ServiceResponse<List<Tutor>> GetTutors();

        Task<ServiceResponse<List<Tutor>>> GetTutorsAsync();

        ServiceResponse<Tutor> GetTutor(int Id);

        Task<ServiceResponse<Tutor>> GetTutorAsync(int Id);

        Task<ServiceResponse<Tutor>> UpdateTutor(Tutor tutor);

        Task<ServiceResponse<Tutor>> CreateTutor(Tutor tutor);


        Task<ServiceResponse<bool>> DeleteTutorAsync(int tutorId);

        ServiceResponse<bool> DeleteTutor(int tutorId);

    }
}
