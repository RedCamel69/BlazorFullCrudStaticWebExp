using BlazorEcommerceStaticWebApp.Api.Data;
using BlazorEcommerceStaticWebApp.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public ProductService(
            ApplicationDbContext context , 
            IHttpContextAccessor httpContext
            )
        {
            _context = context;
            _httpContext = httpContext;
        }
        public async Task<ServiceResponse<Product>> CreateProduct(Product product)
        {
            try
            {
                foreach (var variant in product.Variants)
                {
                    variant.ProductType = null; //ef workaround to stop new product type being created
                }
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return new ServiceResponse<Product> { Data = product };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Product>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public ServiceResponse<bool> DeleteProduct(int productId)
        {
            //var dbProduct = _context.Products.Find(productId);
            // Find not working with tests as we merely have a list of objects but no primary key
            var dbProduct = _context.Products.FirstOrDefault(x=>x.Id==productId);
            if (dbProduct == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Product not found."
                };
            }

            dbProduct.Deleted = true;

            _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<bool>> DeleteProductAsync(int productId)
        {
            var dbProduct = await _context.Products.FindAsync(productId);
            if (dbProduct == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Product not found."
                };
            }

            dbProduct.Deleted = true;

            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<List<Product>>> GetAdminProducts()
        {
            var response = new ServiceResponse<List<Product>>();

            try
            {
                response = new ServiceResponse<List<Product>>
                {
                    Data = _context.Products
              .Where(p => !p.Deleted)
              .Include(p => p.Variants.Where(v => !v.Deleted))
              .ThenInclude(v => v.ProductType)
              .Include(p => p.Images)
              .ToList()
                };

                return response;
            }

            catch(Exception ex)
            {
                return new ServiceResponse<List<Product>>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<ServiceResponse<List<Product>>> GetAdminProductsAsync()
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _context.Products
          .Where(p => !p.Deleted)
          .Include(p => p.Variants.Where(v => !v.Deleted))
          .ThenInclude(v => v.ProductType)
          .Include(p => p.Images)
          .ToListAsync()
            };

            return response;
        }
        public ServiceResponse<List<Product>> GetFeaturedProducts()
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data =   _context.Products
               .Where(x => x.Featured == true && !x.Deleted && x.Visible)
               .Include(p => p.Variants.Where(v => !v.Deleted && v.Visible))
               .Include(p => p.Images)
               .ToList()
            };

            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetFeaturedProductsAsync()
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _context.Products
              .Where(x => x.Featured == true && !x.Deleted && x.Visible)
              .Include(p => p.Variants.Where(v => !v.Deleted && v.Visible))
              .Include(p => p.Images)
              .ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<Product>> GetProductAsync(int productId)
        {
            var response = new ServiceResponse<Product>();
            Product product = null;

            if (_httpContext.HttpContext.User.IsInRole("Admin"))
            {
                product = await _context.Products
               .Include(p => p.Variants.Where(v => !v.Deleted))
               .ThenInclude(v => v.ProductType)
               .Include(p => p.Images)
               .FirstOrDefaultAsync(p => p.Id == productId && !p.Deleted);
            }
            else
            {
                product = await _context.Products
                .Include(p => p.Variants.Where(v => v.Visible && !v.Deleted))
                .ThenInclude(v => v.ProductType)
                 .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == productId && !p.Deleted && p.Visible);
            }

            if (product == null)
            {
                response.Success = false;
                response.Message = "Sorry this product does not exist";
            }
            else
            {
                response.Success = true;
                response.Data = product;
            }

            return response;
        }

        public ServiceResponse<Product> GetProduct(int productId)
        {
            var response = new ServiceResponse<Product>();
            Product product = null;

            if (_httpContext.HttpContext.User.IsInRole("Admin"))
            {
                product =  _context.Products
               .Include(p => p.Variants.Where(v => !v.Deleted))
               .ThenInclude(v => v.ProductType)
               .Include(p => p.Images)
               .FirstOrDefault(p => p.Id == productId && !p.Deleted);
            }
            else
            {
                product =  _context.Products
                .Include(p => p.Variants.Where(v => v.Visible && !v.Deleted))
                .ThenInclude(v => v.ProductType)
                 .Include(p => p.Images)
                .FirstOrDefault(p => p.Id == productId && !p.Deleted && p.Visible);
            }

            if (product == null)
            {
                response.Success = false;
                response.Message = "Sorry this product does not exist";
            }
            else
            {
                response.Success = true;
                response.Data = product;
            }

            return response;
        }

        public ServiceResponse<List<Product>> GetProducts()
        {

            var response = new ServiceResponse<List<Product>>
            {
                Data = _context.Products.ToList<Product>()
            };

            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _context.Products
                   .Where(p => !p.Deleted && p.Visible)
                   .Include(_ => _.Variants.Where(v => !v.Deleted && v.Visible))
                    .ThenInclude(v => v.ProductType)
                    .Include(p => p.Images)
                   .ToListAsync()

            };

            return response;
        }

     

        public async Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryUrl)
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _context.Products
                .Where(p => p.Category.Url.ToLower() == categoryUrl.ToLower() && !p.Deleted && p.Visible)
                 .Include(_ => _.Variants.Where(v => !v.Deleted && v.Visible))
                  .Include(p => p.Images)
                .ToListAsync()
            };

            return response;
        }

        public ServiceResponse<List<Product>> GetProductsByCategory(string categoryUrl)
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = _context.Products
                .Where(p => p.Category.Url.ToLower() == categoryUrl.ToLower() && !p.Deleted && p.Visible)
                 .Include(_ => _.Variants.Where(v => !v.Deleted && v.Visible))
                  .Include(p => p.Images)
                .ToList()
            };

            return response;
        }

        public ServiceResponse<List<Product>> GetProductsByTitle()
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = _context.Products
                            .OrderBy(p=>p.Title)
                            .ToList<Product>()
            };

            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsByTitleASync()
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _context.Products
                           .OrderBy(p => p.Title)
                           .ToListAsync<Product>()
            };

            return response;
        }

        public Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText)
        {
            throw new NotImplementedException();
        }

    
        public Task<ServiceResponse<Product>> UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        ServiceResponse<List<Product>> IProductService.GetProductsByTitle()
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<ProductSearchResult> SearchProducts(string searchText, int page)
        {
            var pageResults = 2f;
            //var pageCount = Math.Ceiling((FindProductsBySearchText(searchText)).Count / pageResults);
            var pageCount = Math.Ceiling(
                            _context.Products
                            .Where(p => p.Title.ToLower().Contains(searchText.ToLower())
                                ||
                                p.Description.ToLower().Contains(searchText.ToLower())
                                && !p.Deleted && p.Visible
                                )
                            .Include(p => p.Variants).ToList().Count / pageResults
                            );


            var products = _context.Products
                            .Where(p => p.Title.ToLower().Contains(searchText.ToLower())
                            ||
                            p.Description.ToLower().Contains(searchText.ToLower())
                            && !p.Deleted && p.Visible)
                            .Include(p => p.Variants)
                             .Include(p => p.Images)
                            .Skip((page - 1) * (int)pageResults)
                            .Take((int)pageResults)
                            .ToList();

            var response = new ServiceResponse<ProductSearchResult>
            {
                Data = new ProductSearchResult
                {
                    Products = products,
                    CurrentPage = page,
                    Pages = (int)pageCount
                }
            };

            return response;
        }

        public async Task<ServiceResponse<ProductSearchResult>> SearchProductsAsync(string searchText, int page)
        {
            var pageResults = 2f;
            //var pageCount = Math.Ceiling((FindProductsBySearchText(searchText)).Count / pageResults);
            var pageCount = Math.Ceiling(
                            _context.Products
                            .Where(p => p.Title.ToLower().Contains(searchText.ToLower())
                                ||
                                p.Description.ToLower().Contains(searchText.ToLower())
                                && !p.Deleted && p.Visible
                                )
                            .Include(p => p.Variants).ToList().Count / pageResults
                            );


            var products = await _context.Products
                            .Where(p => p.Title.ToLower().Contains(searchText.ToLower())
                            ||
                            p.Description.ToLower().Contains(searchText.ToLower())
                            && !p.Deleted && p.Visible)
                            .Include(p => p.Variants)
                             .Include(p => p.Images)
                            .Skip((page - 1) * (int)pageResults)
                            .Take((int)pageResults)
                            .ToListAsync();

            var response = new ServiceResponse<ProductSearchResult>
            {
                Data = new ProductSearchResult
                {
                    Products = products,
                    CurrentPage = page,
                    Pages = (int)pageCount
                }
            };

            return response;
        }
    }
    
}
