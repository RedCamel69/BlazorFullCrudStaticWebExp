using Api.Services.LanguageService;
using BlazorEcommerceStaticWebApp.Api.Data;
using BlazorEcommerceStaticWebApp.Shared;
using Microsoft.EntityFrameworkCore;
using Moq;


namespace Test
{
    public class API_LanguageServiceTests
    {


        private static Mock<ApplicationDbContext> BuildMockContext()
        {
            var data = new List<Language>
            {
                new Language(){ LanguageId=1, Name="English", Code="en" },
                new Language(){ LanguageId=2, Name="French", Code="fr" }
            };

            var mockSet = new Mock<DbSet<Language>>();
            mockSet.As<IQueryable<Language>>().Setup(m => m.Provider).Returns(data.AsQueryable().Provider);
            mockSet.As<IQueryable<Language>>().Setup(m => m.Expression).Returns(data.AsQueryable().Expression);
            mockSet.As<IQueryable<Language>>().Setup(m => m.ElementType).Returns(data.AsQueryable().ElementType);
            mockSet.As<IQueryable<Language>>().Setup(m => m.GetEnumerator()).Returns(() => data.AsQueryable().GetEnumerator());

            mockSet.Setup(m => m.Remove(It.IsAny<Language>())).Callback<Language>(s =>
            {
                data.Remove(data.Find(t => t.LanguageId == s.LanguageId));
            });

            mockSet.Setup(m => m.Add(It.IsAny<Language>())).Callback<Language>(s =>
            {
                data.Add(s);
            });


            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(t => t.Languages).Returns(mockSet.Object);
            return mockContext;
        }

        [Fact]
        public void GetLanguages_Returns_ServiceResponse()
        {

            //arrange
            Mock<ApplicationDbContext> mockContext = BuildMockContext();
            var service = new LanguageService(mockContext.Object);

            //act
            var languages = service.GetLanguages();

            //assert
            Assert.Contains("ServiceResponse", languages.GetType().Name);

        }

        [Fact]
        public void GetStudents_Returns_ServiceResponse_Containing_Expected_Students()
        {

            //arrange
            Mock<ApplicationDbContext> mockContext = BuildMockContext();
            var service = new LanguageService(mockContext.Object);

            //act
            var languages = service.GetLanguages();

            //assert
            Assert.Contains("ServiceResponse", languages.GetType().Name);
            Assert.True(languages.Data.Count == 2);
            Assert.True(languages.Data.FirstOrDefault().Name == "English");
        }


    }
}