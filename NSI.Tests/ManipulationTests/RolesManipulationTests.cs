using Moq;
using NSI.BusinessLogic.Implementations;
using NSI.Common.DataContracts.Enumerations;
using NSI.DataContracts.Models;
using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using NSI.Common.Collation;
using Xunit;

namespace NSI.Tests.ManipulationTests
{
    public class RolesManipulationTests
    {
        [Fact]
        public void GetRoles_ReturnsRoles()
        {
            // Arrange
            var rolesMock = new Mock<IRolesRepository>();
            rolesMock.Setup(MockItem => MockItem.GetRolesAsync(new Guid())).ReturnsAsync(() =>
            {
                List<Role> roles = new List<Role>();
                roles.Add(new Role("newRole"));
                return roles;
            });
            var rolesManipulation = new RolesManipulation(rolesMock.Object);

            // Act
            var result = rolesManipulation.GetRolesAsync(new Guid(), null, null, null);

            // Assert
            Assert.Equal(1, result.Result.Count);
        }

        [Fact]
        public void GetRoles_MocksNull_ReturnsRoles()
        {
            // Arrange
            var rolesMock = new Mock<IRolesRepository>();
            rolesMock.Setup(MockItem => MockItem.GetRolesAsync(new Guid())).ReturnsAsync(() => { return null; });
            var rolesManipulation = new RolesManipulation(rolesMock.Object);

            // Act
            var result = rolesManipulation.GetRolesAsync(new Guid(), new Paging(), null, null);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void SavesRole_ReturnsRoleNull()
        {
            // Arrange
            var rolesMock = new Mock<IRolesRepository>();
            rolesMock.Setup(MockItem => MockItem.SaveRole(null)).Returns(() => { return null; });
            var rolesManipulation = new RolesManipulation(rolesMock.Object);

            // Act
            var result = rolesManipulation.SaveRole("newRole");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void DeleteRole_ReturnsFailed()
        {
            // Arrange
            var rolesMock = new Mock<IRolesRepository>();
            rolesMock.Setup(MockItem => MockItem.DeleteRole(new Guid())).Returns(1);
            var rolesManipulation = new RolesManipulation(rolesMock.Object);

            // Act
            var result = rolesManipulation.DeleteRole("12345678-1234-1234-1234-123456789999");

            // Assert
            Assert.Equal(ResponseStatus.Failed, result);
        }

        [Fact]
        public void SaveRoleToUser_ReturnsUserRole()
        {
            // Arrange
            var rolesMock = new Mock<IRolesRepository>();
            rolesMock.Setup(MockItem => MockItem.SaveRoleToUser(new UserRole(new Guid(), new Guid())))
                .Returns(new UserRole(new Guid(), new Guid()));
            var rolesManipulation = new RolesManipulation(rolesMock.Object);

            // Act
            var result = rolesManipulation.SaveRoleToUser(new Guid(), new Guid());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void RemoveRoleFromUser_ReturnsesponseStatus()
        {
            // Arrange
            var rolesMock = new Mock<IRolesRepository>();
            rolesMock.Setup(MockItem => MockItem.RemoveRoleFromUser(new UserRole(new Guid(), new Guid()))).Returns(0);
            var rolesManipulation = new RolesManipulation(rolesMock.Object);

            // Act
            var result = rolesManipulation.RemoveRoleFromUser(new Guid(), new Guid());

            // Assert
            Assert.Equal(ResponseStatus.Failed, result);
        }
    }
}
