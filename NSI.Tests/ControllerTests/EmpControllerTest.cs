using System;
using System.Collections.Generic;
using NSI.BusinessLogic.Interfaces;
using NSI.DataContracts.Models;
using NSI.Common.Enumerations;
using NSI.REST.Controllers;
using NSI.DataContracts.Request;
using Xunit;
using Moq;
using NSI.Common.DataContracts.Enumerations;

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

        [Fact]
        public void EmpControllerTest4()
        {
            var empMock = new Mock<IEmployeeManipulation>();
            empMock.Setup(EmpMockItem => EmpMockItem.GetAllEmployeesAndUsers()).Returns(() => null);
            var empController = new EmployeeController(empMock.Object);

            var result = empController.GetAllEmployeesAndUsers();

            Assert.Null(result.Data);
            Assert.Null(result.Error.Errors);
            Assert.Equal(ResponseStatus.Succeeded, result.Success);
        }

        [Fact]
        public void EmpControllerTest5()
        {
            var empMock = new Mock<IEmployeeManipulation>();
            empMock.Setup(MockItem => MockItem.GetAllEmployeesAndUsers()).Returns(() => {
                List<User> users = new List<User>();
                return users;
            });
            var empController = new EmployeeController(empMock.Object);

            var result = empController.GetAllEmployeesAndUsers();

            Assert.NotNull(result.Data);
            Assert.Null(result.Error.Errors);
            Assert.Equal(ResponseStatus.Succeeded, result.Success);
        }
        
        [Fact]
        public void UpdateEmployeeFailedTest()
        {
            var empMock = new Mock<IEmployeeManipulation>();
            var user = new User("Name", "Surname", Gender.Male, "email@etf.unsa.ba", "username1", "Cityy", DateTime.Now, "BiH");
            empMock.Setup(EmpMockItem => EmpMockItem.SaveEmployee(null)).Returns(() => user);
            var empController = new EmployeeController(empMock.Object);

            var updateEmployeeRequest = new UpdateEmployeeRequest();
            var result = empController.UpdateEmployee(updateEmployeeRequest, "lbevanda1@etf.unsa.ba");

            Assert.Null(result.Data);
            Assert.Equal(ResponseStatus.Failed, result.Success);
        }
        
        [Fact]
        public void UpdateEmployeeSucceededTest()
        {
            var empMock = new Mock<IEmployeeManipulation>();
            var user = new User("Name", "Surname", Gender.Male, "email@etf.unsa.ba", "username1", "Cityy", DateTime.Now, "BiH");
            empMock.Setup(EmpMockItem => EmpMockItem.SaveEmployee(null)).Returns(() => user);
            var empController = new EmployeeController(empMock.Object);

            var updateEmployeeRequest = new UpdateEmployeeRequest();
            updateEmployeeRequest.FirstName = "Lino";
            updateEmployeeRequest.LastName = "Bevanda";
            updateEmployeeRequest.Gender = "Male";
            updateEmployeeRequest.Username = "Lino";
            updateEmployeeRequest.PlaceOfBirth = "Sarajevo";
            updateEmployeeRequest.DateOfBirth = DateTime.Now;
            updateEmployeeRequest.Country = "BiH";
            var result = empController.UpdateEmployee(updateEmployeeRequest, "lbevanda1@etf.unsa.ba");

            Assert.NotNull(result);
            Assert.Equal(ResponseStatus.Succeeded, result.Success);
        }
        
        [Fact]
        public void DeleteEmployeeTest()
        {
            var empMock = new Mock<IEmployeeManipulation>();
            var user = new User("Name", "Surname", Gender.Male, "email@etf.unsa.ba", "username1", "Cityy", DateTime.Now, "BiH");
            empMock.Setup(EmpMockItem => EmpMockItem.SaveEmployee(null)).Returns(() => user);
            var empController = new EmployeeController(empMock.Object);
            
            var result = empController.DeleteEmployee("lbevanda1@etf.unsa.ba");
            
            Assert.Equal(ResponseStatus.Failed, result.Success);
        }
        
        [Fact]
        public void DeleteEmployeeEmailNullTest()
        {
            var empMock = new Mock<IEmployeeManipulation>();
            var user = new User("Name", "Surname", Gender.Male, "email@etf.unsa.ba", "username1", "Cityy", DateTime.Now, "BiH");
            empMock.Setup(EmpMockItem => EmpMockItem.SaveEmployee(null)).Returns(() => user);
            var empController = new EmployeeController(empMock.Object);
            
            var result = empController.DeleteEmployee(null);
            
            Assert.Equal(ResponseStatus.Failed, result.Success);
        }
    }
}
