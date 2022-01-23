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
    public class PermissionManipulationTests
    {
        [Fact]
        public void GetPermissionsOfUser_ReturnsPermissions()
        {
            // Arrange
            var permMock = new Mock<IPermissionsRepository>();
            permMock.Setup(MockItem => MockItem.GetPermissionsAsync("User")).ReturnsAsync(() =>
            {
                List<Permission> permissions = new List<Permission>();
                permissions.Add(new Permission("newPermission"));
                return permissions;
            });
            var permissionsManipulation = new PermissionsManipulation(permMock.Object);

            // Act
            var result = permissionsManipulation.GetPermissionsAsync("User", null, null, null);

            // Assert
            Assert.Equal(1, result.Result.Count);
        }

        [Fact]
        public void GetPermissionsOfUser_ReturnsPermissionsWithPaging()
        {
            // Arrange
            var permMock = new Mock<IPermissionsRepository>();
            permMock.Setup(MockItem => MockItem.GetPermissionsAsync("User")).ReturnsAsync(() =>
            {
                List<Permission> permissions = new List<Permission>();
                permissions.Add(new Permission("newPermission"));
                return permissions;
            });
            var permissionsManipulation = new PermissionsManipulation(permMock.Object);

            // Act
            var result = permissionsManipulation.GetPermissionsAsync("User", new Paging(), null, null);

            // Assert
            Assert.Equal(1, result.Result.Count);
        }

        [Fact]
        public void SavePermissions_ReturnsPermissionNull()
        {
            // Arrange
            var permMock = new Mock<IPermissionsRepository>();
            permMock.Setup(MockItem => MockItem.SavePermission(new Permission("User"))).Returns(new Permission("User"));
            var permissionsManipulation = new PermissionsManipulation(permMock.Object);

            // Act
            var result = permissionsManipulation.SavePermission("User");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void SavePermissionToRole_ReturnsPermissionNull()
        {
            // Arrange
            var permMock = new Mock<IPermissionsRepository>();
            permMock.Setup(MockItem => MockItem.SavePermissionToRole(new RolePermission(new Guid(), new Guid())))
                .Returns(new RolePermission(new Guid(), new Guid()));
            var permissionsManipulation = new PermissionsManipulation(permMock.Object);

            // Act
            var result = permissionsManipulation.SavePermissionToRole(new Guid(), new Guid());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void RemovePermissionFromRole_ReturnsResponseStatus()
        {
            // Arrange
            var permMock = new Mock<IPermissionsRepository>();
            permMock.Setup(MockItem => MockItem.RemovePermissionFromRole(new RolePermission(new Guid(), new Guid())))
                .Returns(1);
            var permissionsManipulation = new PermissionsManipulation(permMock.Object);

            // Act
            var result = permissionsManipulation.RemovePermissionFromRole(new Guid(), new Guid());

            // Assert
            Assert.Equal(ResponseStatus.Failed, result);
        }

        [Fact]
        public void GetPermissionsByUserId_ReturnsPermissions()
        {
            // Arrange
            var permMock = new Mock<IPermissionsRepository>();
            permMock.Setup(MockItem => MockItem.GetPermissionsByUserId(new Guid())).ReturnsAsync(() =>
            {
                List<Permission> permissions = new List<Permission>();
                permissions.Add(new Permission("newPermission"));
                return permissions;
            });
            var permissionsManipulation = new PermissionsManipulation(permMock.Object);

            // Act
            var result = permissionsManipulation.GetPermissionsByUserId(new Guid());

            // Assert
            Assert.Equal(1, result.Result.Count);
        }
    }
}
