using BlazorEcommerceStaticWebApp.Api.Data;
using BlazorEcommerceStaticWebApp.Shared;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.TutorService
{
    public class TutorService : ITutorService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public TutorService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContext
            )
        {
            _context = context;
            _httpContext = httpContext;
        }

        public async Task<ServiceResponse<Tutor>> CreateTutor(Tutor tutor)
        {

            _context.Tutors.Add(tutor);
            await _context.SaveChangesAsync();
           
            var response = new ServiceResponse<Tutor>
            {
                Data = tutor,
                Message = "Successfully added new tutor"
            };

            return response;
        }

        public ServiceResponse<bool> DeleteTutor(int tutorId)
        {
            //var dbTutor = _context.Tutors.Find(tutorId);
            // Find not working with tests as we merely have a list of objects but no primary key
            var dbTutor  = _context.Tutors.FirstOrDefault(x => x.Id == tutorId);
            if (dbTutor == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Tutor not found."
                };
            }
        
            _context.Tutors.Remove(dbTutor);

            _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<bool>> DeleteTutorAsync(int tutorId)
        {
            try
            {
                var dbTutor = await _context.Tutors.FindAsync(tutorId);
                if (dbTutor == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Data = false,
                        Message = "Tutor not found."
                    };
                }

                _context.Tutors.Remove(dbTutor);

                await _context.SaveChangesAsync();

                return new ServiceResponse<bool> { Data = true, Success = true, Message = String.Format("Tutor {0} sucessfully deleted.", tutorId) };
            }
            catch(Exception ex)
            {
                var e = ex;
                var p = ex;
                return new ServiceResponse<bool> { Data = false, Success = false, Message = ex.Message };
            }
        }

        public ServiceResponse<Tutor> GetTutor(int Id)
        {
            var response = new ServiceResponse<Tutor>
            {
                Data = _context.Tutors
                                .FirstOrDefault(x => x.Id == Id)
            };

            return response;
        }

        public ServiceResponse<List<Tutor>> GetTutors()
        {
            var response = new ServiceResponse<List<Tutor>>
            {
                Data = _context.Tutors.ToList()
            };

            return response;
        

        }

        public async Task<ServiceResponse<Tutor>> UpdateTutor(Tutor tutor)
        {
            _context.Tutors.Update(tutor);
            await _context.SaveChangesAsync();

            var updated = _context.Tutors.FirstOrDefault(x=>x.Id== tutor.Id);
            var response = new ServiceResponse<Tutor>
            {
                Data = tutor,
                Message = "Successfully updated tutor"
            };

            return response;
        }
    }
}
