using Api.Services.CategoryService;
using Api.Services.ProductService;
using BlazorEcommerceStaticWebApp.Api.Data;
using BlazorEcommerceStaticWebApp.Shared;
using BlazorEcommerceStaticWebApp.Test;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net.Http;
using System.Reflection.Metadata;

namespace Test
{
    public class API_CategoryServiceTests
    {
    
        [Fact]
        public void GetCategories_Returns_ServiceResponse()
        {

            var data = new List<Category>
            {
                new Category { Name="Cat 1", Visible = true, Deleted=false},
                new Category { Name="Cat 2", Visible = true, Deleted=false},
                new Category { Name="Cat 3", Visible = true, Deleted=false}
            }.AsQueryable();

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            var mockSet = new Mock<DbSet<Category>>();
            mockSet.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Categories).Returns(mockSet.Object);

            var service = new CategoryService(mockContext.Object, mockHttpContextAccessor.Object);
            var categories = service.GetCategories();

            Assert.Contains("ServiceResponse", categories.GetType().Name);
        }

        [Fact]
        public void GetCategories_Returns_ServiceResponse_Containing_Expected_Categories()
        {

            var data = new List<Category>
            {
                new Category { Name="Cat 1", Visible = true, Deleted=false},
                new Category { Name="Cat 2", Visible = true, Deleted=false},
                new Category { Name="Cat 3", Visible = true, Deleted=false}
            }.AsQueryable();

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            var mockSet = new Mock<DbSet<Category>>();
            mockSet.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Categories).Returns(mockSet.Object);

            var service = new CategoryService(mockContext.Object, mockHttpContextAccessor.Object);
            var categories = service.GetCategories();

            Assert.Contains("ServiceResponse", categories.GetType().Name);
            Assert.True(categories.Data.Count==3);
            Assert.True(categories.Data.FirstOrDefault().Name == "Cat 1");

        }

    }
} 