using BlazorEcommerceStaticWebApp.Api.Data;
using BlazorEcommerceStaticWebApp.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services.TutorService
{
    public class TutorService : ITutorService
    {
        private readonly ApplicationDbContext _context;

        public TutorService(
            ApplicationDbContext context
            )
        {
            _context = context;
        }

        public async Task<ServiceResponse<Tutor>> CreateTutor(Tutor tutor)
        {
            var response = new ServiceResponse<Tutor>();

            try
            {
                tutor.Business = null; //ef workaround to stop new business being created

                _context.Tutors.Add(tutor);

                await _context.SaveChangesAsync();

                response.Data = tutor;
                response.Message = "Successfully added new tutor";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = null;
                response.Message = ex.Message;
            }

            return response;

        }

        public async Task<ServiceResponse<bool>> DeleteTutorAsync(int tutorId)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var dbTutor = await _context.Tutors.FirstOrDefaultAsync(x => x.TutorId == tutorId);
                if (dbTutor == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Data = false,
                        Message = "Tutor not found."
                    };
                }
                //dbTutor.Deleted = true;
                _context.Tutors.Remove(dbTutor);

                await _context.SaveChangesAsync();
                response.Success = true;
                response.Data = true;
                response.Message = $"Tutor {dbTutor.TutorId} {dbTutor.FirstName + " " + dbTutor.LastName} deleted.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = false;
                response.Message = $"Error deleting tutor {tutorId} : {ex.Message} ";
            }
            return new ServiceResponse<bool> { Data = true };
        }

        public ServiceResponse<Tutor> GetTutor(int Id)
        {
            var response = new ServiceResponse<Tutor>
            {
                Data = _context.Tutors
                .Include(x => x.Business)
                                .FirstOrDefault(x => x.TutorId == Id)
            };

            return response;
        }

        public ServiceResponse<List<Tutor>> GetTutors()
        {
            var response = new ServiceResponse<List<Tutor>>();

            try
            {
                response.Data = _context.Tutors
                            .Include(x => x.Business)
                            .ToList();
                response.Message = "Successfully returned Tutors";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = null;
                response.Message = ex.Message;
            }

            return response;

        }

        public async Task<ServiceResponse<Tutor>> UpdateTutor(Tutor tutor)
        {
            tutor.Business = null; //ef workaround to stop new business being created
            _context.Tutors.Update(tutor);
            await _context.SaveChangesAsync();

            var updated = _context.Tutors.FirstOrDefault(x => x.TutorId == tutor.TutorId);
            var response = new ServiceResponse<Tutor>
            {
                Data = tutor,
                Message = "Successfully updated tutor"
            };

            return response;
        }

        public ServiceResponse<bool> DeleteTutor(int tutorId)
        {
            var dbTutor = _context.Tutors.FirstOrDefault(x => x.TutorId == tutorId);
            if (dbTutor == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Tutor not found."
                };
            }

            _context.Tutors.Attach(dbTutor);
            _context.Tutors.Remove(dbTutor);

            _context.SaveChanges();
            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<List<Tutor>>> GetTutorsAsync()
        {
            var response = new ServiceResponse<List<Tutor>>();

            try
            {
                response.Data = await _context.Tutors
                            .Include(x => x.Business)
                            .ToListAsync();
                response.Message = "Successfully returned Tutors";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = null;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<Tutor>> GetTutorAsync(int Id)
        {
            var response = new ServiceResponse<Tutor>
            {
                Data = await _context.Tutors
                            .Include(x => x.Business)
                            .FirstOrDefaultAsync(t => t.TutorId == Id)
            };

            return response;
        }
    }
}
