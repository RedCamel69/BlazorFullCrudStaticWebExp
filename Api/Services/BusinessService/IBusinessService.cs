using BlazorEcommerceStaticWebApp.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services.BusinessService
{
    public interface IBusinessService
    {
        ServiceResponse<List<Business>> GetBusinesses();

        Task<ServiceResponse<Business>> CreateBusiness(Business business);
    }
}
