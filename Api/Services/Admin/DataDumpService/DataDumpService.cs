using BlazorEcommerceStaticWebApp.Api.Data;
using BlazorEcommerceStaticWebApp.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
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


        public ServiceResponse<string> Diagnostics2()
        {
            var response = new ServiceResponse<string>();

            _context.Database.CloseConnection();

            //2 lines azure only!
            File.Copy("D:\\home\\turin2.db", "D:\\home\\turin3.db", true);
            File.Copy("D:\\home\\turin3.db", "D:\\home\\turin2.db",true);
            File.SetAttributes("D:\\home\\turin2.db", FileAttributes.Normal);

            response.Data = _context.Database.GetConnectionString() + " " + Convert.ToString(_context.Database.CanConnect());

            return response;

        }

        public ServiceResponse<Everything> GetEverything()
        {
            var response = new ServiceResponse<Everything>();


           

            response.Data = new Everything()
            {
                Languages = _context.Languages ?? null
                //,
                //Tutors = _context.Tutors ?? null,
                //Courses=_context.Courses ?? null,
                //Businesses = _context.Businesses ?? null,
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
