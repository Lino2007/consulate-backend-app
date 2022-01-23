using Moq;
using NSI.BusinessLogic.Implementations;
using NSI.Common.DataContracts.Enumerations;
using NSI.Common.Enumerations;
using NSI.DataContracts.Models;
using NSI.DataContracts.Request;
using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using Xunit;

namespace NSI.Tests.ManipulationTests
{
    public class EmployeeManipulationTests
    {
        [Fact]
        public void GetAllEmployees_ReturnsAllEmployees()
        {
            // Arrange
            var employeeMock = new Mock<IEmployeeRepository>();
            employeeMock.Setup(MockItem => MockItem.GetAllEmployees()).Returns(() =>
            {
                List<User> users = new List<User>();
                users.Add(new User("Amila", "Lakovic", Gender.Female, "alakovic1@etf.unsa.ba", "alakovic1", "Sarajevo",
                    DateTime.Now, "BiH"));
                return users;
            });
            var employeeManipulation = new EmployeeManipulation(employeeMock.Object);

            // Act
            var result = employeeManipulation.GetAllEmployees();

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void GetAllEmployees_ReturnsNoEmployees()
        {
            // Arrange
            var employeeMock = new Mock<IEmployeeRepository>();
            employeeMock.Setup(MockItem => MockItem.GetAllEmployees()).Returns(() =>
            {
                List<User> users = new List<User>();
                return users;
            });
            var employeeManipulation = new EmployeeManipulation(employeeMock.Object);

            // Act
            var result = employeeManipulation.GetAllEmployees();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetAllEmployees_ReturnsAllEmployeesNull()
        {
            // Arrange
            var employeeMock = new Mock<IEmployeeRepository>();
            employeeMock.Setup(MockItem => MockItem.GetAllEmployees()).Returns(() => { return null; });
            var employeeManipulation = new EmployeeManipulation(employeeMock.Object);

            // Act
            var result = employeeManipulation.GetAllEmployees();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void SaveEmployee_ReturnsEmployeeNull()
        {
            // Arrange
            var employeeMock = new Mock<IEmployeeRepository>();
            employeeMock
                .Setup(MockItem => MockItem.SaveEmployee(new User("Amila", "Lakovic", Gender.Female,
                    "alakovic1@etf.unsa.ba", "alakovic1", "Sarajevo", DateTime.Now, "BiH"))).Returns(new User("Amila",
                    "Lakovic", Gender.Female, "alakovic1@etf.unsa.ba", "alakovic1", "Sarajevo", DateTime.Now, "BiH"));
            var employeeManipulation = new EmployeeManipulation(employeeMock.Object);

            // Act
            var result = employeeManipulation.SaveEmployee(new NewEmployeeRequest());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void DeleteEmployee_ReturnsResponseStatus()
        {
            // Arrange
            var employeeMock = new Mock<IEmployeeRepository>();
            employeeMock.Setup(MockItem => MockItem.DeleteEmployee("alakovic1@etf.unsa.ba"))
                .Returns(ResponseStatus.Succeeded);
            var employeeManipulation = new EmployeeManipulation(employeeMock.Object);

            // Act
            var result = employeeManipulation.DeleteEmployee("alakovic1@etf.unsa.ba");

            // Assert
            Assert.Equal(ResponseStatus.Succeeded, result);
        }

        [Fact]
        public void UpdateEmployee_ReturnsUserNull()
        {
            // Arrange
            var employeeMock = new Mock<IEmployeeRepository>();
            employeeMock
                .Setup(MockItem => MockItem.UpdateEmployee("alakovic1@etf.unsa.ba", new UpdateEmployeeRequest()))
                .Returns(new User("Amila", "Lakovic", Gender.Female, "alakovic1@etf.unsa.ba", "alakovic1", "Sarajevo",
                    DateTime.Now, "BiH"));
            var employeeManipulation = new EmployeeManipulation(employeeMock.Object);

            // Act
            var result = employeeManipulation.UpdateEmployee("alakovic1@etf.unsa.ba", new UpdateEmployeeRequest());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetAllEmployeesAndUsers_ReturnsAllEmployeesAndUsers()
        {
            // Arrange
            var employeeMock = new Mock<IEmployeeRepository>();
            employeeMock.Setup(MockItem => MockItem.GetEmployeesAndUsers()).Returns(() =>
            {
                List<User> users = new List<User>();
                users.Add(new User("Amila", "Lakovic", Gender.Female, "alakovic1@etf.unsa.ba", "alakovic1", "Sarajevo",
                    DateTime.Now, "BiH"));
                return users;
            });
            var employeeManipulation = new EmployeeManipulation(employeeMock.Object);

            // Act
            var result = employeeManipulation.GetAllEmployeesAndUsers();

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void GetAllEmployeesAndUsers_ReturnsNoEmployeesAndUsers()
        {
            // Arrange
            var employeeMock = new Mock<IEmployeeRepository>();
            employeeMock.Setup(MockItem => MockItem.GetEmployeesAndUsers()).Returns(() =>
            {
                List<User> users = new List<User>();
                return users;
            });
            var employeeManipulation = new EmployeeManipulation(employeeMock.Object);

            // Act
            var result = employeeManipulation.GetAllEmployeesAndUsers();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetAllEmployeesAndUsers_ReturnsAllEmployeesAndUsersNull()
        {
            // Arrange
            var employeeMock = new Mock<IEmployeeRepository>();
            employeeMock.Setup(MockItem => MockItem.GetEmployeesAndUsers()).Returns(() => { return null; });
            var employeeManipulation = new EmployeeManipulation(employeeMock.Object);

            // Act
            var result = employeeManipulation.GetAllEmployeesAndUsers();

            // Assert
            Assert.Null(result);
        }
    }
}
