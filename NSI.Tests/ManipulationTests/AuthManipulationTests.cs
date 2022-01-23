using Moq;
using NSI.BusinessLogic.Implementations;
using NSI.Common.Enumerations;
using NSI.DataContracts.Models;
using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NSI.Tests.ManipulationTests
{
    public class AuthManipulationTests
    {
        [Fact]
        public void GetUserInformation_CorrectEmail_ReturnsUser()
        {
            // Arrange
            var authMock = new Mock<IAuthRepository>();
            authMock.Setup(MockItem => MockItem.GetByEmail("alakovic1@etf.unsa.ba")).Returns(new User("Amila",
                "Lakovic", Gender.Female, "alakovic1@etf.unsa.ba", "alakovic1", "Sarajevo", DateTime.Now, "BiH"));
            var rolesMock = new Mock<IRolesRepository>();
            rolesMock.Setup(MockItem => MockItem.GetRolesAsync(new Guid())).ReturnsAsync(() => { return null; });
            var authManipulation = new AuthManipulation(authMock.Object, rolesMock.Object);

            // Act
            var result = authManipulation.GetByEmail("alakovic1@etf.unsa.ba");

            // Assert
            Assert.NotNull(result.Email);
            Assert.NotNull(result.Username);
            Assert.Equal("Amila", result.FirstName);
            Assert.Equal("alakovic1@etf.unsa.ba", result.Email);
        }

        [Fact]
        public void GetRoleFromEmail_ReturnsRole()
        {
            // Arrange
            var authMock = new Mock<IAuthRepository>();
            authMock.Setup(MockItem => MockItem.GetByEmail("alakovic1@etf.unsa.ba")).Returns(new User("Amila",
                "Lakovic", Gender.Female, "alakovic1@etf.unsa.ba", "alakovic1", "Sarajevo", DateTime.Now, "BiH"));
            var rolesMock = new Mock<IRolesRepository>();
            rolesMock.Setup(MockItem => MockItem.GetRoleByUserId(new Guid())).Returns(() => new Role("newRole"));
            var authManipulation = new AuthManipulation(authMock.Object, rolesMock.Object);

            // Act
            var result = authManipulation.GetRoleFromEmail("alakovic1@etf.unsa.ba");

            // Assert
            Assert.NotNull(result.Name);
            Assert.Equal("newRole", result.Name);
        }

        [Fact]
        public void GetRoleFromEmail_DoesNotReturnRole()
        {
            // Arrange
            var authMock = new Mock<IAuthRepository>();
            authMock.Setup(MockItem => MockItem.GetByEmail("alakovic1@etf.unsa.ba")).Returns(new User("Amila",
                "Lakovic", Gender.Female, "alakovic1@etf.unsa.ba", "alakovic1", "Sarajevo", DateTime.Now, "BiH"));
            var rolesMock = new Mock<IRolesRepository>();
            rolesMock.Setup(MockItem => MockItem.GetRoleByUserId(new Guid())).Returns(() => new Role("newRole"));
            var authManipulation = new AuthManipulation(authMock.Object, rolesMock.Object);

            // Act
            var result = authManipulation.GetRoleFromEmail(null);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetRolesFromEmail_ReturnsRoles()
        {
            // Arrange
            var authMock = new Mock<IAuthRepository>();
            authMock.Setup(MockItem => MockItem.GetByEmail("alakovic1@etf.unsa.ba")).Returns(new User("Amila",
                "Lakovic", Gender.Female, "alakovic1@etf.unsa.ba", "alakovic1", "Sarajevo", DateTime.Now, "BiH"));
            var rolesMock = new Mock<IRolesRepository>();
            rolesMock.Setup(MockItem => MockItem.GetRolesByUserId(new Guid())).ReturnsAsync(() =>
            {
                List<Role> roles = new List<Role>();
                roles.Add(new Role("newRole"));
                return roles;
            });
            var authManipulation = new AuthManipulation(authMock.Object, rolesMock.Object);

            // Act
            var result = authManipulation.GetRoles("alakovic1@etf.unsa.ba");

            // Assert
            Assert.Equal(1, result.Result.Count);
        }
    }
}
