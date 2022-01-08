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

namespace NSI.Tests.ControllerTests
{
    public class RoleControllerTest
    {
        [Fact]
        public async Task ControllerTest1()
        {
            var roleMock = new Mock<IRolesManipulation>();
            var usrPerMock = new Mock<IUserPermissionManipulation>();
            var cacheMock = new Mock<ICacheProvider>();

            roleMock.Setup(roleMock => roleMock.GetRolesAsync(new Guid(), null, null, null)).ReturnsAsync(() => { return null; });
            var roleController = new RoleController(roleMock.Object, usrPerMock.Object, cacheMock.Object);

            var result = await roleController.GetRoles(new DataContracts.Request.BasicRequest(), new Guid());

            Assert.Null(result.Data);
        }
    }
}
