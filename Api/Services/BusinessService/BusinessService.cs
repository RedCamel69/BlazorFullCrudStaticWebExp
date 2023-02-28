using BlazorEcommerceStaticWebApp.Api.Data;
using BlazorEcommerceStaticWebApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services.BusinessService
{
    public class BusinessService : IBusinessService
    {
        private readonly ApplicationDbContext _context;

        public BusinessService(
            ApplicationDbContext context
            )
        {
            _context = context;
        }

        public async Task<ServiceResponse<Business>> CreateBusiness(Business business)
        {
            var response = new ServiceResponse<Business>();

            try
            {
                _context.Businesses.Add(business);
                await _context.SaveChangesAsync();

                response.Data = business;
                response.Message = "Successfully added new business";
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

        public ServiceResponse<List<Business>> GetBusinesses()
        {
            var response = new ServiceResponse<List<Business>>
            {
                Data = _context.Businesses
                           .ToList()
            };

            return response;
        }
    }
}
