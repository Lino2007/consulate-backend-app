using Moq;
using NSI.BusinessLogic.Interfaces;
using NSI.Common.DataContracts.Enumerations;
using NSI.Common.Enumerations;
using NSI.DataContracts.Models;
using NSI.REST.Controllers;
using System;
using Xunit;

namespace NSI.Tests.ControllerTests
{
    public class AuthControllerTest
    {

        [Fact]
        public void GetUserInformation_CorrectEmail_ReturnsUser()
        {
            // Arrange
            var authMock = new Mock<IAuthManipulation>();
            authMock.Setup(MockItem => MockItem.GetByEmail("lbevanda1@etf.unsa.ba")).Returns(new User("Lino", "Bevanda", Gender.Male, "lbevanda1@etf.unsa.ba", "lbevanda1", "Sarajevo", DateTime.Now, "BiH"));
            var authController = new AuthController(authMock.Object);

            // Act
            var result = authController.GetUserInformation("lbevanda1@etf.unsa.ba");

            // Assert
            Assert.NotNull(result.Data);
            Assert.Null(result.Error.Errors);
            Assert.Equal(ResponseStatus.Succeeded, result.Success);
            Assert.Equal("Lino", result.Data.FirstName);
            Assert.Equal("lbevanda1@etf.unsa.ba", result.Data.Email);
        }

    }
}
