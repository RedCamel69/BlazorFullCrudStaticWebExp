using Api.Migrations;
using BlazorEcommerceStaticWebApp.Api.Data;
using BlazorEcommerceStaticWebApp.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
            var dbTutor = await _context.Tutors.FirstOrDefaultAsync(x=>x.Id == tutorId);           
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
            return new ServiceResponse<bool> { Data = true };
        }
          
        public ServiceResponse<Tutor> GetTutor(int Id)
        {
            var response = new ServiceResponse<Tutor>
            {
                Data = _context.Tutors
                .Include(x => x.Business)
                                .FirstOrDefault(x => x.Id == Id)
            };

            return response;
        }

        public ServiceResponse<List<Tutor>> GetTutors()
        {
            var response = new ServiceResponse<List<Tutor>>
            {
                Data = _context.Tutors
                            .Include(x=>x.Business)
                            .ToList()
            };

            return response;
        

        }

        public async Task<ServiceResponse<Tutor>> UpdateTutor(Tutor tutor)
        {
            tutor.Business = null; //ef workaround to stop new business being created
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

        public ServiceResponse<bool> DeleteTutor(int tutorId)
        {
            var dbTutor =  _context.Tutors.FirstOrDefault(x => x.Id == tutorId);
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
    }
} 
