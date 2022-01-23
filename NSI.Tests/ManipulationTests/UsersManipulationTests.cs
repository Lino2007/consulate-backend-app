using Moq;
using NSI.BusinessLogic.Implementations;
using NSI.Common.DataContracts.Enumerations;
using NSI.Common.Enumerations;
using NSI.DataContracts.Models;
using NSI.DataContracts.Request;
using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using NSI.Common.Collation;
using Xunit;

namespace NSI.Tests.ManipulationTests
{
    public class UsersManipulationTests
    {
        [Fact]
        public void GetUserInformationById_CorrectEmail_ReturnsUser()
        {
            // Arrange
            var usersMock = new Mock<IUsersRepository>();
            usersMock.Setup(MockItem => MockItem.GetById(new Guid())).Returns(new User("Amila", "Lakovic",
                Gender.Female, "alakovic1@etf.unsa.ba", "alakovic1", "Sarajevo", DateTime.Now, "BiH"));
            var usersManipulation = new UsersManipulation(usersMock.Object);

            // Act
            var result = usersManipulation.GetById(new Guid());

            // Assert
            Assert.NotNull(result.Email);
            Assert.NotNull(result.Username);
            Assert.Equal("Amila", result.FirstName);
            Assert.Equal("alakovic1@etf.unsa.ba", result.Email);
        }

        [Fact]
        public void GetUserInformationByEmail_CorrectEmail_ReturnsUser()
        {
            // Arrange
            var usersMock = new Mock<IUsersRepository>();
            usersMock.Setup(MockItem => MockItem.GetByEmail("alakovic1@etf.unsa.ba")).Returns(new User("Amila",
                "Lakovic", Gender.Female, "alakovic1@etf.unsa.ba", "alakovic1", "Sarajevo", DateTime.Now, "BiH"));
            var usersManipulation = new UsersManipulation(usersMock.Object);

            // Act
            var result = usersManipulation.GetByEmail("alakovic1@etf.unsa.ba");

            // Assert
            Assert.NotNull(result.Email);
            Assert.NotNull(result.Username);
            Assert.Equal("Amila", result.FirstName);
            Assert.Equal("alakovic1@etf.unsa.ba", result.Email);
        }

        [Fact]
        public void GetUserInformationByEmail_InCorrectEmail_ReturnsNull()
        {
            // Arrange
            var usersMock = new Mock<IUsersRepository>();
            usersMock.Setup(MockItem => MockItem.GetByEmail("alakovic1@etf.unsa.ba")).Returns(new User("Amila",
                "Lakovic", Gender.Female, "alakovic1@etf.unsa.ba", "alakovic1", "Sarajevo", DateTime.Now, "BiH"));
            var usersManipulation = new UsersManipulation(usersMock.Object);

            // Act
            var result = usersManipulation.GetByEmail("alakovic@etf.unsa.ba");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetNewUser_UserRequestNull_ReturnsNull()
        {
            // Arrange
            var usersMock = new Mock<IUsersRepository>();
            usersMock.Setup(MockItem => MockItem.GetByEmail("alakovic1@etf.unsa.ba")).Returns(new User("Amila",
                "Lakovic", Gender.Female, "alakovic1@etf.unsa.ba", "alakovic1", "Sarajevo", DateTime.Now, "BiH"));
            var usersManipulation = new UsersManipulation(usersMock.Object);

            // Act
            NewUserRequest newUserRequest = new NewUserRequest();

            var result = usersManipulation.SaveUser(newUserRequest);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetNewUser_UserRequestGenderIsNumberNull_ReturnsNull()
        {
            // Arrange
            var usersMock = new Mock<IUsersRepository>();
            usersMock.Setup(MockItem => MockItem.GetByEmail("alakovic1@etf.unsa.ba")).Returns(new User("Lino",
                "Bevanda", Gender.Male, "lbevanda1@etf.unsa.ba", "lbevanda1", "Sarajevo", DateTime.Now, "BiH"));
            var usersManipulation = new UsersManipulation(usersMock.Object);

            // Act
            NewUserRequest newUserRequest = new NewUserRequest();
            newUserRequest.Gender = 2;

            var result = usersManipulation.SaveUser(newUserRequest);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void RemoveUserByEmail_CorrectEmail_ReturnsResponseStatusSuccess()
        {
            // Arrange
            var usersMock = new Mock<IUsersRepository>();
            usersMock.Setup(MockItem => MockItem.RemoveUser("alakovic1@etf.unsa.ba")).Returns(ResponseStatus.Succeeded);
            var usersManipulation = new UsersManipulation(usersMock.Object);

            // Act
            var result = usersManipulation.RemoveUser("alakovic1@etf.unsa.ba");

            // Assert
            Assert.Equal(ResponseStatus.Succeeded, result);
        }

        [Fact]
        public void GetUsers_ReturnsUsers()
        {
            // Arrange
            var usersMock = new Mock<IUsersRepository>();
            usersMock.Setup(MockItem => MockItem.GetUsersAsync()).ReturnsAsync(() =>
            {
                List<User> users = new List<User>();
                users.Add(new User("Amila", "Lakovic", Gender.Female, "alakovic1@etf.unsa.ba", "alakovic1", "Sarajevo",
                    DateTime.Now, "BiH"));
                return users;
            });
            var usersManipulation = new UsersManipulation(usersMock.Object);

            // Act
            var result = usersManipulation.GetUsers(null, null, null);

            // Assert
            Assert.Equal(1, result.Result.Count);
        }

        [Fact]
        public void GetUsers_GetsNull()
        {
            // Arrange
            var usersMock = new Mock<IUsersRepository>();
            usersMock.Setup(MockItem => MockItem.GetUsersAsync()).ReturnsAsync(() => { return null; });
            var usersManipulation = new UsersManipulation(usersMock.Object);

            // Act
            var result = usersManipulation.GetUsers(new Paging(), null, null);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetAllPerson_ReturnsPeople()
        {
            // Arrange
            var usersMock = new Mock<IUsersRepository>();
            usersMock.Setup(MockItem => MockItem.GetAllPersonAsync()).ReturnsAsync(() =>
            {
                List<User> users = new List<User>();
                users.Add(new User("Amila", "Lakovic", Gender.Female, "alakovic1@etf.unsa.ba", "alakovic1", "Sarajevo",
                    DateTime.Now, "BiH"));
                return users;
            });
            var usersManipulation = new UsersManipulation(usersMock.Object);

            // Act
            var result = usersManipulation.GetAllPerson();

            // Assert
            Assert.Equal(1, result.Result.Count);
        }

        [Fact]
        public void GetAllPerson_GetsNull()
        {
            // Arrange
            var usersMock = new Mock<IUsersRepository>();
            usersMock.Setup(MockItem => MockItem.GetAllPersonAsync()).ReturnsAsync(() => { return null; });
            var usersManipulation = new UsersManipulation(usersMock.Object);

            // Act
            var result = usersManipulation.GetAllPerson();

            // Assert
            Assert.NotNull(result);
        }
    }
}
