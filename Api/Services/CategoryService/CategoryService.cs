using BlazorEcommerceStaticWebApp.Api.Data;
using BlazorEcommerceStaticWebApp.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public CategoryService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContext
            )
        {
            _context = context;
            _httpContext = httpContext;
        }
        public ServiceResponse<List<Category>> GetCategories()
        {
            var categories =  _context.Categories
            .Where(c => c.Visible && !c.Deleted)
            .ToList();
            return new ServiceResponse<List<Category>>()
            {
                Data = categories
            };
        }
    }
}
