using BlazorEcommerceStaticWebApp.Shared;

namespace BlazorEcommerceStaticWebApp.Client.Services.BusinessService
{
    public interface IBusinessService
    {
        event Action BusinessesChanged;
        List<Business> Businesses { get; set; }

        Task GetBusinesses();

        Task CreateBusiness(Business business);
    }
}
