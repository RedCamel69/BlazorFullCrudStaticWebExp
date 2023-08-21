using BlazorEcommerceStaticWebApp.Api.Data;
using BlazorEcommerceStaticWebApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.Admin.DataDumpService
{
    public class DataDumpService : IDataDumpService
    {
        private readonly ApplicationDbContext _context;

        public DataDumpService(
            ApplicationDbContext context
            )
        {
            _context = context;
        }

        public ServiceResponse<List<Business>> GetBusinesses()
        {
            var response = new ServiceResponse<List<Business>>
            {
                Data = _context.Businesses
                           .ToList()
            };

            return response;
        }

        public ServiceResponse<Everything> GetEverything()
        {
            var response = new ServiceResponse<Everything>();

            response.Data = new Everything()
            {
                Languages = _context.Languages ?? null,
                Tutors = _context.Tutors ?? null,
                Courses=_context.Courses ?? null,
                Businesses = _context.Businesses ?? null,
            };

            //response.Data.Tutors = _context.Tutors;
            //response.Data.Businesses = _context.Businesses;

            //{

            //     Data.Tutors = _context.Tutors,
            //    Data.Businesses = _context.Businesses
            //};

            return response;
        }

        public ServiceResponse<List<Tutor>> GetTutors()
        {
            var response = new ServiceResponse<List<Tutor>>
            {
                Data = _context.Tutors
                           .ToList()
            };

            return response;
        }
    }
}
