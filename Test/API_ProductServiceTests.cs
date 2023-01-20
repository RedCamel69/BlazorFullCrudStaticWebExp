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
    public class API_ProductServiceTests
    {

        // after https://learn.microsoft.com/en-gb/ef/ef6/fundamentals/testing/mocking?redirectedfrom=MSDN

        // after https://www.andrewhoefling.com/Blog/Post/moq-entity-framework-dbset

        [Fact]
        public void GetProducts_Returns_ServiceResponse()
        {
            var data = new List<Product>
            {
                new Product {   Id = 1, Title = "BBB" },
                new Product {   Id = 2, Title = "ZZZ" },
                new Product {   Id = 2, Title = "AAA" }
            }.AsQueryable();

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);

            var service = new ProductService(mockContext.Object, mockHttpContextAccessor.Object);
            var products = service.GetProducts();

            Assert.Contains("ServiceResponse", products.GetType().Name);
        }

        [Fact]
        public void GetProducts_returns_list_of_products()
        {
            var data = new List<Product>
            {
                new Product {   Id = 1, Title = "BBB" },
                new Product {   Id = 2, Title = "ZZZ" },
                new Product {   Id = 2, Title = "AAA" }
            }.AsQueryable();

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);

            var service = new ProductService(mockContext.Object, mockHttpContextAccessor.Object);
            var products = service.GetProducts();

            Assert.Equal(3, products.Data.Count);
            //Assert.AreEqual("AAA", blogs[0].Name); 
            //Assert.AreEqual("BBB", blogs[1].Name);
            //Assert.AreEqual("ZZZ", blogs[2].Name);
        }


        [Fact]
        public async void GetProductsByTitle_returns_list_of_products_ordered_by_title()
        {
            var data = new List<Product>
            {
                new Product {   Id = 1, Title = "BBB" },
                new Product {   Id = 2, Title = "ZZZ" },
                new Product {   Id = 2, Title = "AAA" }
            }.AsQueryable();

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);

            var service = new ProductService(mockContext.Object, mockHttpContextAccessor.Object);
            var products = service.GetProductsByTitle();

            Assert.Equal(3, products.Data.Count);
            Assert.Equal("AAA", products.Data[0].Title);
            Assert.Equal("BBB", products.Data[1].Title);
            Assert.Equal("ZZZ", products.Data[2].Title);
        }

        [Fact]
        public async void GetProductsByCategory_returns_list_of_products_for_supplied_category_url()
        {
            var data = new List<Product>
                {
                    new Product {   Id = 1, Title = "BBB", Category= new Category(){  Id=1, Url = "TestCat1"}, Variants= new List<ProductVariant>() },
                    new Product {   Id = 2, Title = "ZZZ", Category= new Category(){  Id=1, Url = "TestCat1"}, Variants= new List<ProductVariant>() },
                    new Product {Id = 2, Title = "AAA", Category = new Category() { Id = 1, Url = "TestCat1" }, Variants = new List < ProductVariant >()}
                }.AsQueryable();

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);

            var service = new ProductService(mockContext.Object, mockHttpContextAccessor.Object);
            var products = service.GetProductsByCategory("TestCat1");

            Assert.True(products.Data.Count == 3);
        }

        [Fact]
        public async void GetProductsByCategory_returns_filtered_list_of_products_for_supplied_category_url()
        {
            var data = new List<Product>
                {
                    new Product {   Id = 1, Title = "BBB", Category= new Category(){  Id=1, Url = "TestCat1"}, Variants= new List<ProductVariant>() },
                    new Product {   Id = 2, Title = "ZZZ", Category= new Category(){  Id=1, Url = "TestCat2"}, Variants= new List<ProductVariant>() },
                    new Product {Id = 2, Title = "AAA", Category = new Category() { Id = 1, Url = "TestCat3" }, Variants = new List < ProductVariant >()}
                }.AsQueryable();

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);

            var service = new ProductService(mockContext.Object, mockHttpContextAccessor.Object);
            var products = service.GetProductsByCategory("TestCat2");

            Assert.True(products.Data.Count == 1);
        }

        [Fact]
        public async void GetProductsByCategory_returns_filtered_list_of_visible_products_for_supplied_category_url()
        {
            var data = new List<Product>
                {
                    new Product {   Id = 1, Title = "BBB", Category= new Category(){  Id=1, Url = "TestCat1"}, Variants= new List<ProductVariant>() , Visible=true},
                    new Product {   Id = 2, Title = "ZZZ", Category= new Category(){  Id=1, Url = "TestCat3"}, Variants= new List<ProductVariant>() , Visible = false},
                    new Product {Id = 2, Title = "AAA", Category = new Category() { Id = 1, Url = "TestCat3" }, Variants = new List < ProductVariant >(), Visible = true}
                }.AsQueryable();

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);

            var service = new ProductService(mockContext.Object, mockHttpContextAccessor.Object);
            var products = service.GetProductsByCategory("TestCat3");

            Assert.True(products.Data.Count == 1);
            Assert.True(products.Data[0].Title=="AAA");

        }

        [Fact]
        public async void GetProduct_returns_expected_product()
        {
            var data = new List<Product>
                {
                    new Product {   Id = 1, Title = "BBB", Category= new Category(){  Id=1, Url = "TestCat1"}, Variants= new List<ProductVariant>() , Visible=true, Images=new List<Image>()},
                    new Product {   Id = 2, Title = "ZZZ", Category= new Category(){  Id=1, Url = "TestCat3"}, Variants= new List<ProductVariant>() , Visible = false, Images=new List<Image>()},
                    new Product {Id = 2, Title = "AAA", Category = new Category() { Id = 1, Url = "TestCat3" }, Variants = new List < ProductVariant >(), Visible = true, Images=new List<Image>()}
                }.AsQueryable();

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            mockHttpContextAccessor.Setup(h=>h.HttpContext.User.IsInRole("Admin")).Returns(true);

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);

            var service = new ProductService(mockContext.Object, mockHttpContextAccessor.Object);
            var product = service.GetProduct(2);

            Assert.True(product.Data.Title == "ZZZ");

        }

        //  todo async ef core unit testing
        //[Fact]
        //public async void GetProductsAsync_returns_list_of_products()
        //{
        //    var data = new List<Product>
        //    {
        //        new Product {   Id = 1, Title = "BBB" },
        //        new Product {   Id = 2, Title = "ZZZ" },
        //        new Product {   Id = 2, Title = "AAA" }
        //    }.AsQueryable();

        //    var mockSet = new Mock<DbSet<Product>>();
        //    mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(data.Provider);
        //    mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(data.Expression);
        //    mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(data.ElementType);
        //    mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

        //    var mockContext = new Mock<ApplicationDbContext>();
        //    mockContext.Setup(c => c.Products).Returns(mockSet.Object);

        //    var service = new ProductService(mockContext.Object);
        //    var products = await service.GetProductsAsync();

        //    Assert.Equal(3, products.Data.Count);
        //    Assert.Equal("AAA", products.Data[0].Title);
        //    Assert.Equal("BBB", products.Data[1].Title);
        //    Assert.Equal("ZZZ", products.Data[2].Title);
        //}

        [Fact]
        public async void CreateProduct_returns_service_response_containing_new_product()
        {

            var data = new List<Product>
                {
                    new Product {   Id = 1, Title = "BBB", Category= new Category(){  Id=1, Url = "TestCat1"}, Variants= new List<ProductVariant>() , Visible=true, Images=new List<Image>()},
                    new Product {   Id = 2, Title = "ZZZ", Category= new Category(){  Id=1, Url = "TestCat3"}, Variants= new List<ProductVariant>() , Visible = false, Images=new List<Image>()},
                    new Product {Id = 2, Title = "AAA", Category = new Category() { Id = 1, Url = "TestCat3" }, Variants = new List < ProductVariant >(), Visible = true, Images=new List<Image>()}
                }.AsQueryable();

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            mockHttpContextAccessor.Setup(h => h.HttpContext.User.IsInRole("Admin")).Returns(true);

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);
            var service = new ProductService(mockContext.Object, mockHttpContextAccessor.Object);

            var res = service.CreateProduct(new Product() { 
                Title = "Test Title"  ,
                Category = new Category() { Name="Test Category"}
            });

            Assert.True(res.Result.Data.Title == "Test Title");



        }

        [Fact]
        public async void DeleteProduct_returns_service_response_containing_boolean_flag()
        {

            var data = new List<Product>
                {
                    new Product {   Id = 1, Title = "BBB", Category= new Category(){  Id=1, Url = "TestCat1"}, Variants= new List<ProductVariant>() , Visible=true, Images=new List<Image>()},
                    new Product {   Id = 2, Title = "ZZZ", Category= new Category(){  Id=3, Url = "TestCat3"}, Variants= new List<ProductVariant>() , Visible = false, Images=new List<Image>()},
                    new Product {Id = 3, Title = "AAA", Category = new Category() { Id = 3, Url = "TestCat3" }, Variants = new List < ProductVariant >(), Visible = true, Images=new List<Image>()}
                }.AsQueryable();

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            mockHttpContextAccessor.Setup(h => h.HttpContext.User.IsInRole("Admin")).Returns(true);

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);
            var service = new ProductService(mockContext.Object, mockHttpContextAccessor.Object);

            var res = service.DeleteProduct(1);

            Assert.True(res.Data == true);

        }

        [Fact]
        public async void DeleteProduct_sets_Deleted_Value_On_Product()
        {

            var data = new List<Product>
                {
                    new Product {   Id = 1, Title = "BBB", Category= new Category(){  Id=1, Url = "TestCat1"}, Variants= new List<ProductVariant>() , Visible=true, Images=new List<Image>()},
                    new Product {   Id = 2, Title = "ZZZ", Category= new Category(){  Id=3, Url = "TestCat3"}, Variants= new List<ProductVariant>() , Visible = false, Images=new List<Image>()},
                    new Product {Id = 3, Title = "AAA", Category = new Category() { Id = 3, Url = "TestCat3" }, Variants = new List < ProductVariant >(), Visible = true, Images=new List<Image>()}
                }.AsQueryable();

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            mockHttpContextAccessor.Setup(h => h.HttpContext.User.IsInRole("Admin")).Returns(true);

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);
            var service = new ProductService(mockContext.Object, mockHttpContextAccessor.Object);

            var res = service.DeleteProduct(1);
            var expectedDeletedProduct = mockContext.Object.Products.FirstOrDefault(p => p.Id == 1);

            Assert.True(expectedDeletedProduct !=null);
            Assert.True(expectedDeletedProduct.Deleted == true);
        }

        [Fact]
        public async void GetAdminProducts_returns_expected_products()
        {
            //expected to return all products not deleted irrespective of visible flag
            var data = new List<Product>
                {
                    new Product {   Id = 1, Title = "BBB", Category= new Category(){  Id=1, Url = "TestCat1"}, Variants= new List<ProductVariant>() , Visible=true, Images=new List<Image>(), Deleted=false},
                    new Product {   Id = 2, Title = "ZZZ", Category= new Category(){  Id=1, Url = "TestCat3"}, Variants= new List<ProductVariant>() , Visible = false, Images=new List<Image>(), Deleted = true},
                    new Product {Id = 2, Title = "AAA", Category = new Category() { Id = 1, Url = "TestCat3" }, Variants = new List < ProductVariant >(), Visible = false, Images=new List<Image>(), Deleted = false}
                }.AsQueryable();

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            mockHttpContextAccessor.Setup(h => h.HttpContext.User.IsInRole("Admin")).Returns(true);

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);

            var service = new ProductService(mockContext.Object, mockHttpContextAccessor.Object);
            var product = await service.GetAdminProducts();

            Assert.True(product.Data.Count == 2);
        }



        [Fact]
        public async void GetFeaturedProducts_returns_expected_products()
        {
            //expected to return all products with featured flag not not deleted and visible 
            var data = new List<Product>
                {
                    new Product {   Id = 1, Title = "BBB", Category= new Category(){  Id=1, Url = "TestCat1"}, Variants= new List<ProductVariant>() , Visible=true, Images=new List<Image>(), Deleted=false, Featured=true},
                    new Product {   Id = 2, Title = "ZZZ", Category= new Category(){  Id=1, Url = "TestCat3"}, Variants= new List<ProductVariant>() , Visible = false, Images=new List<Image>(), Deleted = true, Featured=true},
                    new Product {Id = 2, Title = "AAA", Category = new Category() { Id = 1, Url = "TestCat3" }, Variants = new List < ProductVariant >(), Visible = false, Images=new List<Image>(), Deleted = false,Featured=true}
                }.AsQueryable();

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            mockHttpContextAccessor.Setup(h => h.HttpContext.User.IsInRole("Admin")).Returns(true);

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);

            var service = new ProductService(mockContext.Object, mockHttpContextAccessor.Object);
            var product = service.GetFeaturedProducts();

            Assert.True(product.Data.Count == 1);
        }


        [Fact]
        public void SearchProducts_returns_list_of_products()
        {
            var data = new List<Product>
            {
                new Product {   Id = 1, Title = "BBB", Description="Product Description 1" },
                new Product {   Id = 2, Title = "ZZZ", Description="Product Description 2" },
                new Product {   Id = 2, Title = "AAA" , Description="Product Description 3"}
            }.AsQueryable();

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);

            var service = new ProductService(mockContext.Object, mockHttpContextAccessor.Object);
            var products = service.SearchProducts("AAA", 1);

            Assert.Equal(1, products.Data.Products.Count);
            //Assert.AreEqual("AAA", blogs[0].Name); 
            //Assert.AreEqual("BBB", blogs[1].Name);
            //Assert.AreEqual("ZZZ", blogs[2].Name);
        }
    }
} 