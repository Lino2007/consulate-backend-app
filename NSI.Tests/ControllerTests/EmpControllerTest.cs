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

        [Fact]
        public void EmpControllerTest2()
        {
            var empMock = new Mock<IEmployeeManipulation>();
            var user = new User("Name", "Surname", Gender.Male, "email@etf.unsa.ba", "username1", "Cityy", DateTime.Now, "BiH");
            empMock.Setup(EmpMockItem => EmpMockItem.SaveEmployee(null)).Returns(() => user);
            var empController = new EmployeeController(empMock.Object);

            var ner = new NewEmployeeRequest();
            ner.FirstName = ner.LastName = ner.Country = ner.PlaceOfBirth = ner.Username = "..";
            ner.Email = "email@email.com";

            var result = empController.SaveEmployee(ner);

            Assert.Null(result.Error.Errors);
            Assert.Equal(ResponseStatus.Succeeded, result.Success);
        }


        [Fact]
        public void EmpControllerTest3()
        {
            var empMock = new Mock<IEmployeeManipulation>();
            var user = new User("Name", "Surname", Gender.Male, "email@etf.unsa.ba", "username1", "Cityy", DateTime.Now, "BiH");
            empMock.Setup(EmpMockItem => EmpMockItem.SaveEmployee(null)).Returns(() => user);
            var empController = new EmployeeController(empMock.Object);

            var ner = new NewEmployeeRequest();
            var result = empController.SaveEmployee(ner);

            Assert.Null(result.Data);
            Assert.Equal(ResponseStatus.Failed, result.Success);
        }

    }
}
