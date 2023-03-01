using BlazorEcommerceStaticWebApp.Api.Data;
using BlazorEcommerceStaticWebApp.Shared;
using Microsoft.EntityFrameworkCore;
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

        public ServiceResponse<bool> DeleteBusiness(int businessId)
        {
            var dbBusiness =  _context.Businesses.FirstOrDefault(x => x.BusinessId == businessId);
            if (dbBusiness == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Business not found."
                };
            }

            _context.Businesses.Attach(dbBusiness);
            _context.Businesses.Remove(dbBusiness);
             _context.SaveChanges();
            
            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<bool>> DeleteBusinessAsync(int businessId)
        {
            var dbBusiness = await _context.Businesses.FirstOrDefaultAsync(x => x.BusinessId == businessId);
            if (dbBusiness == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Business not found."
                };
            }
         
            _context.Businesses.Remove(dbBusiness);

            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
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
