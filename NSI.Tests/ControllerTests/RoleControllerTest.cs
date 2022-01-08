using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using NSI.BusinessLogic.Interfaces;
using NSI.Cache.Interfaces;
using NSI.REST.Controllers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NSI.DataContracts.Request;
using NSI.Common.DataContracts.Enumerations;

namespace NSI.Tests.ControllerTests
{
    public class RoleControllerTest
    {
        [Fact]
        public async Task RoleControllerTest1()
        {
            var roleMock = new Mock<IRolesManipulation>();
            var usrPerMock = new Mock<IUserPermissionManipulation>();
            var cacheMock = new Mock<ICacheProvider>();

            roleMock.Setup(roleMock => roleMock.GetRolesAsync(new Guid(), null, null, null)).ReturnsAsync(() => { return null; });
            var roleController = new RoleController(roleMock.Object, usrPerMock.Object, cacheMock.Object);

            var result = await roleController.GetRoles(new DataContracts.Request.BasicRequest(), new Guid());

            Assert.Null(result.Error.Errors);
        }

        [Fact]
        public void RoleControllerTest2()
        {
            var roleMock = new Mock<IRolesManipulation>();
            var usrPerMock = new Mock<IUserPermissionManipulation>();
            var cacheMock = new Mock<ICacheProvider>();

            roleMock.Setup(roleMock => roleMock.SaveRole(null)).Returns(() => { return null; });
            var roleController = new RoleController(roleMock.Object, usrPerMock.Object, cacheMock.Object);

            var result =  roleController.SaveRole(new NameRequest());

            Assert.Null(result.Error.Errors);
            Assert.Equal(ResponseStatus.Succeeded, result.Success);
        }

        [Fact]
        public void RoleControllerTest3()
        {
            var roleMock = new Mock<IRolesManipulation>();
            var usrPerMock = new Mock<IUserPermissionManipulation>();
            var cacheMock = new Mock<ICacheProvider>();

            roleMock.Setup(roleMock => roleMock.DeleteRole(null)).Returns(() => { return ResponseStatus.Succeeded; });
            var roleController = new RoleController(roleMock.Object, usrPerMock.Object, cacheMock.Object);

            var result = roleController.SaveRole(new NameRequest());

            Assert.Null(result.Error.Errors);
            Assert.Equal(ResponseStatus.Succeeded, result.Success);
        }
    }
}
