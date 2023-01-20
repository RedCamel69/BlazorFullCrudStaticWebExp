using BlazorEcommerceStaticWebApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.ProductService
{
    public interface IProductService
    {
        ServiceResponse<List<Product>> GetProducts();

        Task<ServiceResponse<List<Product>>> GetProductsAsync();

        ServiceResponse<List<Product>> GetProductsByTitle();

        Task<ServiceResponse<List<Product>>> GetProductsByTitleASync();

        Task<ServiceResponse<Product>> GetProductAsync(int productId);

        ServiceResponse<Product> GetProduct(int productId);

        Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryUrl);

        ServiceResponse<List<Product>> GetProductsByCategory(string categoryUrl);

        ServiceResponse<ProductSearchResult> SearchProducts(string searchText, int page);

        Task<ServiceResponse<ProductSearchResult>> SearchProductsAsync(string searchText, int page);

        Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText);

        ServiceResponse<List<Product>> GetFeaturedProducts();

        Task<ServiceResponse<List<Product>>> GetFeaturedProductsAsync();

        Task<ServiceResponse<List<Product>>> GetAdminProducts();

        Task<ServiceResponse<List<Product>>> GetAdminProductsAsync();

        Task<ServiceResponse<Product>> CreateProduct(Product product);
        Task<ServiceResponse<Product>> UpdateProduct(Product product);
        ServiceResponse<bool> DeleteProduct(int productId);
        Task<ServiceResponse<bool>> DeleteProductAsync(int productId);
    }
}
