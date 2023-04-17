using Api.Services.StudentService;
using BlazorEcommerceStaticWebApp.Api.Data;
using BlazorEcommerceStaticWebApp.Shared;
using Microsoft.EntityFrameworkCore;
using Moq;


namespace Test
{
    public class API_StudentServiceTests
    {

        #region usefulLinks
        // useful links for testing ef  async queries
        // https://learn.microsoft.com/en-gb/ef/ef6/fundamentals/testing/mocking?redirectedfrom=MSDN

        // https://www.andrewhoefling.com/Blog/Post/moq-entity-framework-dbset
        #endregion

        private static Mock<ApplicationDbContext> BuildMockContext()
        {
            var data = new List<Student>
            {
                new Student { StudentId=1, FirstName="John", LastName="Lennon", School="School 1", Language = new BlazorEcommerceStaticWebApp.Shared.Language(){ LanguageId=1, Name="English", Code="en" }, LanguageId= 1, NickName="Test Nick Name" },
                new Student { StudentId=2, FirstName="Paul", LastName="McCartney", School="School 2"},
                new Student { StudentId=3, FirstName="Ringo", LastName="Starr", School="School 1"},
                new Student { StudentId=4, FirstName="George", LastName="Harrison", School="School 1"}
            };

            var mockSet = new Mock<DbSet<Student>>();
            mockSet.As<IQueryable<Student>>().Setup(m => m.Provider).Returns(data.AsQueryable().Provider);
            mockSet.As<IQueryable<Student>>().Setup(m => m.Expression).Returns(data.AsQueryable().Expression);
            mockSet.As<IQueryable<Student>>().Setup(m => m.ElementType).Returns(data.AsQueryable().ElementType);
            mockSet.As<IQueryable<Student>>().Setup(m => m.GetEnumerator()).Returns(() => data.AsQueryable().GetEnumerator());

            mockSet.Setup(m => m.Remove(It.IsAny<Student>())).Callback<Student>(s =>
            {
                data.Remove(data.Find(t => t.StudentId == s.StudentId));
            });

            mockSet.Setup(m => m.Add(It.IsAny<Student>())).Callback<Student>(s =>
            {
                data.Add(s);
            });


            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(t => t.Students).Returns(mockSet.Object);
            return mockContext;
        }

        [Fact]
        public void GetStudents_Returns_ServiceResponse()
        {

            //arrange
            Mock<ApplicationDbContext> mockContext = BuildMockContext();
            var service = new StudentService(mockContext.Object);

            //act
            var students = service.GetStudents();

            //assert
            Assert.Contains("ServiceResponse", students.GetType().Name);

        }

        [Fact]
        public void GetStudents_Returns_ServiceResponse_Containing_Expected_Students()
        {

            //arrange
            Mock<ApplicationDbContext> mockContext = BuildMockContext();
            var service = new StudentService(mockContext.Object);

            //act
            var students = service.GetStudents();

            //assert
            Assert.Contains("ServiceResponse", students.GetType().Name);
            Assert.True(students.Data.Count == 4);
            Assert.True(students.Data.FirstOrDefault().FirstName == "John");
        }

        [Fact]
        public void GetStudents_Returns_Students_With_Language()
        {

            //arrange
            Mock<ApplicationDbContext> mockContext = BuildMockContext();
            var service = new StudentService(mockContext.Object);

            //act
            var students = service.GetStudents();

            //assert
            Assert.True(students.Data.Count == 4);
            Assert.True(students.Data.FirstOrDefault().LanguageId == 1);
            Assert.True(students.Data.FirstOrDefault().Language != null);
        }

        [Fact]
        public void GetStudent_Returns_ServiceResponse()
        {
            //arrange
            Mock<ApplicationDbContext> mockContext = BuildMockContext();
            var service = new StudentService(mockContext.Object);

            //act
            var students = service.GetStudent(1);

            //assert
            Assert.Contains("ServiceResponse", students.GetType().Name);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetStudent_Existing_Student_Returns_ServiceResponse(int studentId)
        {
            //arrange
            Mock<ApplicationDbContext> mockContext = BuildMockContext();
            var service = new StudentService(mockContext.Object);

            //act
            var student = service.GetStudent(studentId);

            //assert
            Assert.Contains("Students successfully retrieved", student.Message);
            Assert.True(student.Success == true);
            Assert.True(student.Data.StudentId == studentId);
        }

        [Theory]
        [InlineData(1001)]
        [InlineData(2001)]
        public void GetStudent_Non_Existing_Student_Returns_ServiceResponse(int studentId)
        {
            //arrange
            Mock<ApplicationDbContext> mockContext = BuildMockContext();
            var service = new StudentService(mockContext.Object);

            //act
            var student = service.GetStudent(studentId);

            //assert
            Assert.Contains($"Student with id of {studentId} could not be found", student.Message);
            Assert.True(student.Success == false);
            Assert.True(student.Data == null);
        }


        [Fact]
        public void Delete_Returns_ServiceResponse()
        {
            //arrange
            Mock<ApplicationDbContext> mockContext = BuildMockContext();
            var service = new StudentService(mockContext.Object);

            //act
            var res = service.DeleteStudent(2);

            //assert
            Assert.Contains("ServiceResponse", res.GetType().Name);
            Assert.True(mockContext.Object.Students.Count() == 3);
            Assert.True(mockContext.Object.Students.FirstOrDefault().FirstName == "John");

        }

        [Fact]
        public void Delete_Returns_ServiceResponse_If_Student_Not_Found()
        {

            //arrange
            Mock<ApplicationDbContext> mockContext = BuildMockContext();
            var service = new StudentService(mockContext.Object);

            //act
            var res = service.DeleteStudent(2001);

            //assert
            Assert.Contains("ServiceResponse", res.GetType().Name);
            Assert.True(mockContext.Object.Students.Count() == 4);
            Assert.True(mockContext.Object.Students.FirstOrDefault().FirstName == "John");
            Assert.Contains("Student not found", res.Message);
        }

        [Fact]
        public void Delete_Removes_Expected_Student()
        {
            //arrange
            Mock<ApplicationDbContext> mockContext = BuildMockContext();
            var service = new StudentService(mockContext.Object);

            //act
            var res = service.DeleteStudent(1);

            //assert
            Assert.Contains("ServiceResponse", res.GetType().Name);
            Assert.True(mockContext.Object.Students.Count() == 3);
            Assert.True(mockContext.Object.Students.FirstOrDefault().FirstName == "Paul");
        }

        [Fact]
        public void Create_Returns_ServiceResponse()
        {
            //arrange
            Mock<ApplicationDbContext> mockContext = BuildMockContext();
            var service = new StudentService(mockContext.Object);

            var student = new Student()
            {
                FirstName = "Test",
                LastName = "Student 1",
                School = "Test School",
                NickName = "Test",
                LanguageId = 1
            };

            //act
            var res = service.CreateStudent(student).Result;

            //assert
            Assert.Contains("ServiceResponse", res.GetType().Name);

        }

        [Fact]
        public void Create_Successfully_Creates_Expected_Entity()
        {
            //arrange
            Mock<ApplicationDbContext> mockContext = BuildMockContext();
            var service = new StudentService(mockContext.Object);

            var student = new Student()
            {
                FirstName = "Test",
                LastName = "Student 1",
                School = "Test School"
            };

            //act
            var res = service.CreateStudent(student).Result;

            //assert
            Assert.Contains("ServiceResponse", res.GetType().Name);

        }

        [Fact]
        public void Update_Returns_ServiceResponse()
        {
            //arrange
            Mock<ApplicationDbContext> mockContext = BuildMockContext();
            var service = new StudentService(mockContext.Object);

            var student = new Student()
            {
                FirstName = "Updated First",
                LastName = "Updated Last",
                School = "Updated School",
                StudentId = 1
            };

            //act
            var res = service.UpdateStudent(student).Result;

            //assert
            Assert.Contains("ServiceResponse", res.GetType().Name);

        }

        [Fact]
        public void Update_Incomplete_Details_Returns_ServiceResponse()
        {
            //arrange
            Mock<ApplicationDbContext> mockContext = BuildMockContext();
            var service = new StudentService(mockContext.Object);

            var student = new Student()
            {
                FirstName = "Updated First",
                LastName = "Updated Last",
                School = "Updated School"
                //StudentId = 1
            };

            //act
            var res = service.UpdateStudent(student).Result;

            //assert
            Assert.Contains("ServiceResponse", res.GetType().Name);

        }
    }
}