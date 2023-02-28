using BlazorEcommerceStaticWebApp.Shared.Validations;


namespace Test
{
    public class ValidationTests
    {



        [Fact]
        public void Protopage_Validation_Returns_False_If_Protopage_Missing_From_Url()
        {

            // arrange
            var value = "https://microsoft.com";
            var attrib = new ProtopageUrlAttribute();

            // act
            var result = attrib.IsValid(value);

            // assert
            Assert.True(result == false);
        }

        [Theory]
        [InlineData("https://protopage.com", true)]
        [InlineData("http://protopage.com", true)]
        [InlineData("http://www.protopage.com", true)]
        [InlineData("https://www.protopage.com", true)]
        [InlineData("https://www.protopage1.com", false)]
        [InlineData("https://www.aprotopage.com", false)]
        public void Protopage_Validation_Returns_True_If_Url_Contains_Protopage(string urlToTest, bool expectedResult)
        {

            // arrange
            var value = urlToTest;
            var attrib = new ProtopageUrlAttribute();

            // act
            var result = attrib.IsValid(value);

            // assert
            Assert.True(result == expectedResult);
        }


    }
}