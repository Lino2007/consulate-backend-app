using System;
using System.Collections.Generic;
using NSI.BusinessLogic.Interfaces;
using NSI.DataContracts.Models;
using NSI.Common.Enumerations;
using NSI.REST.Controllers;
using NSI.DataContracts.Request;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Xunit;
using Moq;
using Microsoft.Extensions.Configuration;
using NSI.Common.DataContracts.Enumerations;
using NSI.Common.Collation;
using NSI.DataContracts.Dto;
using Xunit.Abstractions;

namespace NSI.Tests.ControllerTests
{
    public class ReqControllerTest
    {
        [Fact]
        public async Task ReqControllerTest1()
        {
            var userMock = new Mock<IUsersManipulation>();
            var reqMock = new Mock<IRequestsManipulation>();
            var user = new User("Name", "Surname", Gender.Male, "email@etf.unsa.ba", "username1", "Cityy", DateTime.Now, "BiH");
            var configMock = new Mock<IConfiguration>();
            IList<Request> reqList = new List<Request>();
            configMock.Setup(sb => sb[It.IsAny<string>()]).Returns("...");
            userMock.Setup(UserMockItem => UserMockItem.GetByEmail(null)).Returns(user);
            reqMock.Setup(ReqMockItem => ReqMockItem.GetRequestsAsync()).ReturnsAsync(reqList);
            var requestController = new RequestController(reqMock.Object, userMock.Object, null, null, null, null, configMock.Object, null);

            var result = await requestController.GetRequests(null);

            Assert.NotNull(result.Data);
            Assert.Null(result.Error.Errors);
            Assert.Equal(ResponseStatus.Succeeded, result.Success);
        }

        [Fact]
        public async Task ReqControllerTest2()
        {
            var userMock = new Mock<IUsersManipulation>();
            var reqMock = new Mock<IRequestsManipulation>();
            var configMock = new Mock<IConfiguration>();

            var reqItem = new RequestItemDto(null, null, null);
            IList<RequestItemDto> reqList = new List<RequestItemDto>() { reqItem};
            configMock.Setup(sb => sb[It.IsAny<string>()]).Returns("...");
            reqMock.Setup(ReqMockItem => ReqMockItem.GetRequestPage(new Paging())).ReturnsAsync(reqList);
            var requestController = new RequestController(reqMock.Object, null, null, null, null, null, configMock.Object, null);

            var br = new BasicRequest();
            br.Paging = new Paging();
            br.Paging.TotalRecords = br.Paging.RecordsPerPage = br.Paging.Page = 10;
            var result = await requestController.GetRequestsWithPaging(br);

 
            Assert.Null(result.Error.Errors);
            Assert.Equal(ResponseStatus.Succeeded, result.Success);
        }


        [Fact]
        public async Task ReqControllerTest3()
        {
            var configMock = new Mock<IConfiguration>();
            configMock.Setup(sb => sb[It.IsAny<string>()]).Returns("...");
            var requestController = new RequestController(null, null, null, null, null, null, configMock.Object, null);

            var result = await requestController.GetRequestsWithPaging(new BasicRequest());

            Assert.Equal(ResponseStatus.Failed, result.Success);
        }


        [Fact]
        public async Task ReqControllerTest4()
        {
            var userMock = new Mock<IUsersManipulation>();
            var reqMock = new Mock<IRequestsManipulation>();
            var configMock = new Mock<IConfiguration>();

            var reqItem = new RequestItemDto(null, null, null);
            IList<RequestItemDto> reqList = new List<RequestItemDto>() { reqItem };
            configMock.Setup(sb => sb[It.IsAny<string>()]).Returns("...");
            reqMock.Setup(ReqMockItem => ReqMockItem.GetEmployeeRequestsAsync("..", new Paging())).ReturnsAsync(reqList);
            var requestController = new RequestController(reqMock.Object, null, null, null, null, null, configMock.Object, null);

            var br = new BasicRequest();
            br.Paging = new Paging();
            br.Paging.TotalRecords = br.Paging.RecordsPerPage = br.Paging.Page = 10;
            var result = await requestController.GetRequestsByEmployeeId("id", br);


            Assert.Null(result.Error.Errors);
            Assert.Equal(ResponseStatus.Succeeded, result.Success);
        }


        [Fact]
        public async Task ReqControllerTest5()
        {
            var configMock = new Mock<IConfiguration>();
            configMock.Setup(sb => sb[It.IsAny<string>()]).Returns("...");
            var requestController = new RequestController(null, null, null, null, null, null, configMock.Object, null);

            var result = await requestController.GetRequestsByEmployeeId(null ,new BasicRequest());

            Assert.Equal(ResponseStatus.Failed, result.Success);
        }
    }
}
