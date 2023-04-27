using Api.Services.BusinessService;
using Api.Services.CourseService;
using Api.Services.HelperService;
using BlazorEcommerceStaticWebApp.Api.Data;
using BlazorEcommerceStaticWebApp.Shared;
using Microsoft.EntityFrameworkCore;
using Moq;


namespace Test
{
    public class API_CourseServiceTests
    {
        //todo tests for deleteasync

        #region usefulLinks
        // useful links for testing ef  async queries
        // https://learn.microsoft.com/en-gb/ef/ef6/fundamentals/testing/mocking?redirectedfrom=MSDN

        // https://www.andrewhoefling.com/Blog/Post/moq-entity-framework-dbset
        #endregion

        private static Mock<ApplicationDbContext> BuildMockContext()
        {
            var data = new List<Course>
            {
                new Course { 
                    Name="Course 1", 
                    CourseId = 1, 
                    StartDate=DateTime.Now, 
                    EndDate=DateTime.Now.AddDays(365), 
                    LanguageId=1, 
                    TutorId = 1, 
                    Tutor=new Tutor()
                    {
                         Id=1,
                         FirstName="Tutor 1 FName",
                         LastName = "Tutor 1 LName"
                    },
                    Language=new Language(){  
                        LanguageId=1, 
                        Name = "English", 
                        Code="en"} 
                },
                new Course {
                    Name="Course 2",
                    CourseId = 2,
                    StartDate=DateTime.Now,
                    EndDate=DateTime.Now.AddDays(265),
                    LanguageId=1,
                    TutorId = 2,
                    Language=new Language(){
                        LanguageId=1,
                        Name = "English",
                        Code="en"}
                },
                new Course {
                    Name="Course 3",
                    CourseId = 3,
                    StartDate=DateTime.Now,
                    EndDate=DateTime.Now.AddDays(365),
                    LanguageId=1,
                    TutorId = 1,
                    Language=new Language(){
                        LanguageId=1,
                        Name = "English",
                        Code="en"}
                }
            };
            //.AsQueryable();

            var mockSet = new Mock<DbSet<Course>>();
            mockSet.As<IQueryable<Course>>().Setup(m => m.Provider).Returns(data.AsQueryable().Provider);
            mockSet.As<IQueryable<Course>>().Setup(m => m.Expression).Returns(data.AsQueryable().Expression);
            mockSet.As<IQueryable<Course>>().Setup(m => m.ElementType).Returns(data.AsQueryable().ElementType);
            mockSet.As<IQueryable<Course>>().Setup(m => m.GetEnumerator()).Returns(() => data.AsQueryable().GetEnumerator());

            mockSet.Setup(m => m.Remove(It.IsAny<Course>())).Callback<Course>(s =>
            {
                data.Remove(data.Find(t => t.CourseId== s.CourseId));
            });

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(t => t.Courses).Returns(mockSet.Object);
            return mockContext;
        }

        [Fact]
        public void GetCourse_Returns_ServiceResponse()
        {
            //arrange
            Mock<ApplicationDbContext> mockContext = BuildMockContext();
            var mockHelperService = new Mock<IHelperService>();
            var service = new CourseService(mockContext.Object, mockHelperService.Object);

            //act
            var courses = service.GetCourse(1);

            //assert
            Assert.Contains("ServiceResponse", courses.GetType().Name);
        }

        [Fact]
        public void GetCourses_Returns_ServiceResponse()
        {
            //arrange
            Mock<ApplicationDbContext> mockContext = BuildMockContext();
            var mockHelperService = new Mock<IHelperService>();
            var service = new CourseService(mockContext.Object, mockHelperService.Object);

            //act
            var courses = service.GetCourses();

            //assert
            Assert.Contains("ServiceResponse", courses.GetType().Name);
        }



        [Fact]
        public void GetCourses_Returns_ServiceResponse_Containing_Expected_Course()
        {
            //arrange
            Mock<ApplicationDbContext> mockContext = BuildMockContext();
            var mockHelperService = new Mock<IHelperService>();
            var service = new CourseService(mockContext.Object, mockHelperService.Object);

            //act
            var courses = service.GetCourses();


            //assert
            Assert.Contains("ServiceResponse", courses.GetType().Name);
            Assert.True(courses.Data.Count == 3);
            Assert.True(courses.Data.FirstOrDefault().Name == "Course 1");
        }

        [Fact]
        public void GetCourses_Returns_ServiceResponse_Containing_Expected_Course_Containing_Tutor()
        {
            //arrange
            Mock<ApplicationDbContext> mockContext = BuildMockContext();
            var mockHelperService = new Mock<IHelperService>();
            var service = new CourseService(mockContext.Object, mockHelperService.Object);

            //act
            var courses = service.GetCourses();


            //assert
            Assert.Contains("ServiceResponse", courses.GetType().Name);
            Assert.True(courses.Data.Count == 3);
            Assert.True(courses.Data.FirstOrDefault().Name == "Course 1");
            Assert.True(courses.Data.FirstOrDefault().Tutor.FirstName == "Tutor 1 FName");
        }


        [Fact]
        public void Delete_Returns_ServiceResponse()
        {
            //arrange
            Mock<ApplicationDbContext> mockContext = BuildMockContext();
            var mockHelperService = new Mock<IHelperService>();
            var service = new CourseService(mockContext.Object, mockHelperService.Object);

            //act
            var res = service.DeleteCourse(1);

            //assert
            Assert.Contains("ServiceResponse", res.GetType().Name);
        }

        [Fact]
        public void Delete_Returns_ServiceResponse_If_Course_Not_Found()
        {
            //arrange
            Mock<ApplicationDbContext> mockContext = BuildMockContext();
            var mockHelperService = new Mock<IHelperService>();
            var service = new CourseService(mockContext.Object, mockHelperService.Object);

            //act
            var res = service.DeleteCourse(1009);

            //assert
            Assert.Contains("ServiceResponse", res.GetType().Name);
            Assert.True(res.Message == "Course not found.");

        }

        [Fact]
        public void Delete_Removes_Expected_Course()
        {
            //arrange
            Mock<ApplicationDbContext> mockContext = BuildMockContext();
            var mockHelperService = new Mock<IHelperService>();
            var service = new CourseService(mockContext.Object, mockHelperService.Object);

            //act
            var res = service.DeleteCourse(1);
            var deletedCourse = mockContext.Object.Courses.FirstOrDefault(x => x.CourseId == 1);
            var remainingCourses = mockContext.Object.Courses.FirstOrDefault(x => x.CourseId == 2);

            //assert
            Assert.Contains("ServiceResponse", res.GetType().Name);
            Assert.True(deletedCourse == null);
            Assert.True(remainingCourses != null);
            Assert.True(mockContext.Object.Courses.Count() == 2);


        }

        [Fact]
        public void Update_Returns_ServiceResponse()
        {
            //arrange
            Mock<ApplicationDbContext> mockContext = BuildMockContext();
            var mockHelperService = new Mock<IHelperService>();
            var service = new CourseService(mockContext.Object, mockHelperService.Object);

            //act
            var res = service.UpdateCourse(new Course()
            {
                CourseId = 1,
                Name = "Updated Course Name"
            });

            //assert
            Assert.Contains("ServiceResponse", res.GetType().Name);
        }

    }
}