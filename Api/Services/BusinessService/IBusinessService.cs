using BlazorEcommerceStaticWebApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.BusinessService
{
    public interface IBusinessService
    {
        ServiceResponse<List<Business>> GetBusinesses();

        Task<ServiceResponse<Business>> CreateBusiness(Business business);
    }
}
