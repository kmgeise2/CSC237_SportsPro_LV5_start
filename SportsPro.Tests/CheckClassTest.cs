using CSC237_SportsPro_LV5_start.Models;
using Moq;
using Xunit;

namespace SportsPro.Tests
{
    public class CheckClassTest
    {
        [Fact]
        public void EmailExists_ReturnsAString()
        {
            // arrange
            var rep = new Mock<IRepository<Customer>>();
            rep.Setup(m => m.Get(It.IsAny<QueryOptions<Customer>>())).Returns(new Customer());

            // act
            var result = Check.EmailExists(rep.Object, "fake@email.com");

            // assert
            Assert.IsType<string>(result);
        }

        [Fact]
        public void EmailExists_ReturnsAnEmptyStringIfEmailIsNew()
        {
            // arrange
            var rep = new Mock<IRepository<Customer>>();
            rep.Setup(m => m.Get(It.IsAny<QueryOptions<Customer>>()));
            int expectedLength = 0;

            // act
            var result = Check.EmailExists(rep.Object, "fake@email.com");

            // assert
            Assert.Equal(expectedLength, result.Length);
        }

        [Fact]
        public void EmailExists_ReturnsAnEmptyStringIfEmailIsMissing()
        {
            // arrange
            var rep = new Mock<IRepository<Customer>>();
            rep.Setup(m => m.Get(It.IsAny<QueryOptions<Customer>>())).Returns(new Customer());
            int expectedLength = 0;

            // act
            var result = Check.EmailExists(rep.Object, null);

            // assert
            Assert.Equal(expectedLength, result.Length);
        }

        [Fact]
        public void EmailExists_ReturnsAnErrorMessageIfEmailExists()
        {
            // arrange
            var rep = new Mock<IRepository<Customer>>();
            rep.Setup(m => m.Get(It.IsAny<QueryOptions<Customer>>())).Returns(new Customer());
            bool isGreaterThanZero = true;

            // act
            var result = Check.EmailExists(rep.Object, "fake@email.com");

            // assert
            Assert.Equal(isGreaterThanZero, result.Length > 0);
        }
    }
}
