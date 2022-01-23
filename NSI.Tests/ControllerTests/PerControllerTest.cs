using System.Collections.Generic;
using NSI.BusinessLogic.Interfaces;
using NSI.DataContracts.Models;
using NSI.Common.Enumerations;
using NSI.REST.Controllers;
using NSI.DataContracts.Request;
using System.Threading.Tasks;
using Xunit;
using Moq;
using NSI.Common.DataContracts.Enumerations;
using NSI.Cache.Interfaces;
using NSI.Common.DataContracts.Base;

namespace NSI.Tests.ControllerTests
{
    public class PerControllerTest
    {
        [Fact]
        public async Task PermissionControllerTest1()
        {

            var permMock = new Mock<IPermissionsManipulation>();
            var cacheMock = new Mock<ICacheProvider>();
            var permissionList = new List<Permission>() { null };
            permMock.Setup(PermMockItem => PermMockItem.GetPermissionsAsync(null, null, null, null)).ReturnsAsync(permissionList);
            var permissionController = new PermissionController(permMock.Object, cacheMock.Object);

            var result = await permissionController.GetPermissions(new BasicRequest(), null);

            Assert.NotNull(result.Data);
            Assert.Null(result.Error.Errors);
            Assert.Equal(ResponseStatus.Succeeded, result.Success);
        }

        [Fact]
        public void PermissionControllerTest2()
        {

            var permMock = new Mock<IPermissionsManipulation>();
            var cacheMock = new Mock<ICacheProvider>();

            cacheMock.Setup(CacheMockItem => CacheMockItem.Get<Dictionary<string, List<PermissionEnum>>>("..")).Returns(() => null);
            permMock.Setup(PermMockItem => PermMockItem.SavePermission(null)).Returns(() => null);
            var permissionController = new PermissionController(permMock.Object, cacheMock.Object);
            BaseResponse<Permission> result = null;
            try
            {
                result = permissionController.SavePermission(null);
            }
            catch 
            {
             
            }
            Assert.Null(result);
        }
        
        [Fact]
        public void SavePermissionToRole()
        {

            var permMock = new Mock<IPermissionsManipulation>();
            var cacheMock = new Mock<ICacheProvider>();
            var permissionList = new List<Permission>() { null };
            permMock.Setup(PermMockItem => PermMockItem.GetPermissionsAsync(null, null, null, null)).ReturnsAsync(permissionList);
            var permissionController = new PermissionController(permMock.Object, cacheMock.Object);

            var result = permissionController.SavePermissionToRole(new RolePermissionRequest());
            
            Assert.Null(result.Error.Errors);
            Assert.Equal(ResponseStatus.Succeeded, result.Success);
        }
        
        [Fact]
        public void RemovePermissionFromRole()
        {

            var permMock = new Mock<IPermissionsManipulation>();
            var cacheMock = new Mock<ICacheProvider>();
            var permissionList = new List<Permission>() { null };
            permMock.Setup(PermMockItem => PermMockItem.GetPermissionsAsync(null, null, null, null)).ReturnsAsync(permissionList);
            var permissionController = new PermissionController(permMock.Object, cacheMock.Object);

            var result = permissionController.RemovePermissionFromRole(new RolePermissionRequest());
            
            Assert.NotNull(result);
        }
    }
}
