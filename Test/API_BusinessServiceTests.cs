using Api.Services.BusinessService;
using Api.Services.TutorService;
using BlazorEcommerceStaticWebApp.Api.Data;
using BlazorEcommerceStaticWebApp.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;


namespace Test
{
    public class API_BusinessServiceTests
    {

        #region usefulLinks
        // useful links for testing ef  async queries
        // https://learn.microsoft.com/en-gb/ef/ef6/fundamentals/testing/mocking?redirectedfrom=MSDN

        // https://www.andrewhoefling.com/Blog/Post/moq-entity-framework-dbset
        #endregion

        private static Mock<ApplicationDbContext> BuildMockContext()
        {
            var data = new List<Business>
            {
                new Business {  Name = "Test Business 1", BusinessId=1},
                new Business {  Name = "Test Business 2", BusinessId=2},
                new Business {  Name = "Test Business 3", BusinessId=3}
            };
            //.AsQueryable();

            var mockSet = new Mock<DbSet<Business>>();
            mockSet.As<IQueryable<Business>>().Setup(m => m.Provider).Returns(data.AsQueryable().Provider);
            mockSet.As<IQueryable<Business>>().Setup(m => m.Expression).Returns(data.AsQueryable().Expression);
            mockSet.As<IQueryable<Business>>().Setup(m => m.ElementType).Returns(data.AsQueryable().ElementType);
            mockSet.As<IQueryable<Business>>().Setup(m => m.GetEnumerator()).Returns(() => data.AsQueryable().GetEnumerator());

            mockSet.Setup(m => m.Remove(It.IsAny<Business>())).Callback<Business>(s =>
            {
                data.Remove(data.Find(t => t.BusinessId == s.BusinessId));
            });

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(t => t.Businesses).Returns(mockSet.Object);
            return mockContext;
        }

        [Fact]
        public void GetBusiness_Returns_ServiceResponse()
        {
            //arrange
            Mock<ApplicationDbContext> mockContext = BuildMockContext();
            var service = new BusinessService(mockContext.Object);
            
            //act
            var businesses = service.GetBusinesses();

            //assert
            Assert.Contains("ServiceResponse", businesses.GetType().Name);          
        }

     

        [Fact]
        public void GetBusiness_Returns_ServiceResponse_Containing_Expected_Businesses()
        {
            //arrange
            Mock<ApplicationDbContext> mockContext = BuildMockContext();
            var service = new BusinessService(mockContext.Object);
            
            //act
            var businesses = service.GetBusinesses();

            //assert
            Assert.Contains("ServiceResponse", businesses.GetType().Name);
            Assert.True(businesses.Data.Count == 3);
            Assert.True(businesses.Data.FirstOrDefault().Name == "Test Business 1");
        }

        [Fact]
        public void Delete_Returns_ServiceResponse()
        {
            //arrange
            Mock<ApplicationDbContext> mockContext = BuildMockContext();
            var service = new BusinessService(mockContext.Object);
            
            //act
            var res = service.DeleteBusiness(1);

            //assert
            Assert.Contains("ServiceResponse", res.GetType().Name);
        }

        [Fact]
        public void Delete_Returns_ServiceResponse_If_Tutor_Not_Found()
        {            
            //arrange
            Mock<ApplicationDbContext> mockContext = BuildMockContext();
            var service = new BusinessService(mockContext.Object);

            //act
            var res = service.DeleteBusiness(1009);

            //assert
            Assert.Contains("ServiceResponse", res.GetType().Name);
            Assert.True(res.Message == "Business not found.");

        }

        [Fact]
        public void Delete_Removes_Expected_Business()
        {
            //arrange
            Mock<ApplicationDbContext> mockContext = BuildMockContext();
            var service = new BusinessService(mockContext.Object);
            
            //act
            var res = service.DeleteBusiness(2);
            var deletedBusiness= mockContext.Object.Businesses.FirstOrDefault(x => x.BusinessId == 2);
            var remainingBusiness = mockContext.Object.Businesses.FirstOrDefault(x => x.BusinessId == 1);

            //assert
            Assert.Contains("ServiceResponse", res.GetType().Name);
            Assert.True(deletedBusiness == null);
            Assert.True(remainingBusiness != null);
            Assert.True(mockContext.Object.Businesses.Count() == 2);

            
        }
    }
}