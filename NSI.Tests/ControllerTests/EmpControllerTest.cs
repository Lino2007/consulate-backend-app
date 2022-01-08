using System;
using System.Collections.Generic;
using NSI.BusinessLogic.Interfaces;
using NSI.DataContracts.Models;
using NSI.Common.Enumerations;
using NSI.REST.Controllers;
using NSI.DataContracts.Request;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Configuration;
using NSI.Common.DataContracts.Enumerations;
using NSI.Common.Collation;
using NSI.DataContracts.Dto;

namespace NSI.Tests.ControllerTests
{
    public class EmpControllerTest
    {

        [Fact]
        public void EmpControllerTest1()
        {
            var empMock = new Mock<IEmployeeManipulation>();


            empMock.Setup(EmpMockItem => EmpMockItem.GetAllEmployees()).Returns(() => null);
            var empController = new EmployeeController(empMock.Object);

            var result =  empController.GetAllEmployees();

            Assert.Null(result.Data);
            Assert.Null(result.Error.Errors);
            Assert.Equal(ResponseStatus.Succeeded, result.Success);
        }

    }
}
