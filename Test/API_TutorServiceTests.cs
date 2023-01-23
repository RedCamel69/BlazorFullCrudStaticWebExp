using Api.Services.TutorService;
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
    public class API_TutorServiceTests
    {

        #region usefulLinks
        // useful links for testing ef  async queries
        // https://learn.microsoft.com/en-gb/ef/ef6/fundamentals/testing/mocking?redirectedfrom=MSDN

        // https://www.andrewhoefling.com/Blog/Post/moq-entity-framework-dbset
        #endregion

        [Fact]
        public void GetTutors_Returns_ServiceResponse()
        {

            var data = new List<Tutor>
            {
                new Tutor {  FirstName="Tutor 1 FName", LastName="Tutor 1 LName", Phone = "11111111"},
                new Tutor {  FirstName="Tutor 1 FName", LastName="Tutor 1 LName", Phone = "2222222"},
                new Tutor {  FirstName="Tutor 1 FName", LastName="Tutor 1 LName", Phone = "3333333"}
            }.AsQueryable();

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            var mockSet = new Mock<DbSet<Tutor>>();
            mockSet.As<IQueryable<Tutor>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Tutor>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Tutor>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Tutor>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(t => t.Tutors).Returns(mockSet.Object);

            var service = new TutorService(mockContext.Object, mockHttpContextAccessor.Object);
            var tutors = service.GetTutors();

            Assert.Contains("ServiceResponse", tutors.GetType().Name);
        }

        [Fact]
        public void GetTutors_Returns_ServiceResponse_Containing_Expected_Categories()
        {

            var data = new List<Tutor>
            {
                new Tutor {  FirstName="Tutor 1 FName", LastName="Tutor 1 LName", Phone = "11111111"},
                new Tutor {  FirstName="Tutor 1 FName", LastName="Tutor 1 LName", Phone = "2222222"},
                new Tutor {  FirstName="Tutor 1 FName", LastName="Tutor 1 LName", Phone = "3333333"}
            }.AsQueryable();

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            var mockSet = new Mock<DbSet<Tutor>>();
            mockSet.As<IQueryable<Tutor>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Tutor>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Tutor>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Tutor>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(t => t.Tutors).Returns(mockSet.Object);

            var service = new TutorService(mockContext.Object, mockHttpContextAccessor.Object);
            var tutors = service.GetTutors();

            Assert.Contains("ServiceResponse", tutors.GetType().Name);
            Assert.True(tutors.Data.Count==3);
            Assert.True(tutors.Data.FirstOrDefault().FirstName == "Tutor 1 FName");

        }


        [Fact]
        public void Delete_Returns_ServiceResponse()
        {

            var data = new List<Tutor>
            {
                new Tutor {   Id=1,FirstName="Tutor 1 FName", LastName="Tutor 1 LName", Phone = "11111111"},
                new Tutor {   Id=2,FirstName="Tutor 1 FName", LastName="Tutor 1 LName", Phone = "2222222"},
                new Tutor {   Id=3,FirstName="Tutor 1 FName", LastName="Tutor 1 LName", Phone = "3333333"}
            }.AsQueryable();

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            var mockSet = new Mock<DbSet<Tutor>>();
            mockSet.As<IQueryable<Tutor>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Tutor>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Tutor>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Tutor>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(t => t.Tutors).Returns(mockSet.Object);

            var service = new TutorService(mockContext.Object, mockHttpContextAccessor.Object);
            var res = service.DeleteTutor(1);

            Assert.Contains("ServiceResponse", res.GetType().Name);
        }

        [Fact]
        public void Delete_Returns_ServiceResponse_If_Tutor_Not_Found()
        {

            var data = new List<Tutor>
            {
                new Tutor {   Id=1,FirstName="Tutor 1 FName", LastName="Tutor 1 LName", Phone = "11111111"},
                new Tutor {   Id=2,FirstName="Tutor 1 FName", LastName="Tutor 1 LName", Phone = "2222222"},
                new Tutor {   Id=3,FirstName="Tutor 1 FName", LastName="Tutor 1 LName", Phone = "3333333"}
            }.AsQueryable();

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            var mockSet = new Mock<DbSet<Tutor>>();
            mockSet.As<IQueryable<Tutor>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Tutor>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Tutor>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Tutor>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(t => t.Tutors).Returns(mockSet.Object);

            var service = new TutorService(mockContext.Object, mockHttpContextAccessor.Object);
            var res = service.DeleteTutor(1001);

            Assert.Contains("ServiceResponse", res.GetType().Name);
            Assert.True(res.Message == "Tutor not found.");
        }

    }
} 