using Moq;
using NSI.BusinessLogic.Implementations;
using NSI.BusinessLogic.Interfaces;
using NSI.Cache.Interfaces;
using NSI.Common.Enumerations;
using NSI.DataContracts.Models;
using NSI.DataContracts.Request;
using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NSI.Tests.ManipulationTests
{
    public class RequestsManipulationTests
    {
        [Fact]
        public void GetRequestsAsync_ReturnsRequests()
        {
            // Arrange
            var reqMock = new Mock<IRequestsRepository>();
            reqMock.Setup(MockItem => MockItem.GetRequestsAsync()).ReturnsAsync(() => {
                List<Request> requests = new List<Request>();
                requests.Add(new Request(new Guid(), "reason", RequestType.Passport));
                return requests;
            });
            var attchMock = new Mock<IAttachmentRepository>();
            var docMock = new Mock<IDocumentRepository>();
            var docTMock = new Mock<IDocumentTypesRepository>();
            var cacheProviderMock = new Mock<ICacheProvider>();
            var filesMock = new Mock<IFilesManipulation>();
            var reqManipulation = new RequestsManipulation(reqMock.Object, attchMock.Object, docMock.Object, docTMock.Object, cacheProviderMock.Object, filesMock.Object);

            // Act
            var result = reqManipulation.GetRequestsAsync();

            // Assert
            Assert.Equal(1, result.Result.Count);

        }

        [Fact]
        public void GetRequestsAsyncNull_ReturnsRequest()
        {
            // Arrange
            var reqMock = new Mock<IRequestsRepository>();
            reqMock.Setup(MockItem => MockItem.GetRequestsAsync()).ReturnsAsync(() => { return null; });
            var attchMock = new Mock<IAttachmentRepository>();
            var docMock = new Mock<IDocumentRepository>();
            var docTMock = new Mock<IDocumentTypesRepository>();
            var cacheProviderMock = new Mock<ICacheProvider>();
            var filesMock = new Mock<IFilesManipulation>();
            var reqManipulation = new RequestsManipulation(reqMock.Object, attchMock.Object, docMock.Object, docTMock.Object, cacheProviderMock.Object, filesMock.Object);

            // Act
            var result = reqManipulation.GetRequestsAsync();

            // Assert
            Assert.NotNull(result);

        }

        [Fact]
        public void GetEmployeeRequestsAsync_ReturnsRequestItemDto()
        {
            // Arrange
            var reqMock = new Mock<IRequestsRepository>();
            reqMock.Setup(MockItem => MockItem.GetEmployeeRequestsAsync("id")).Returns(() => { return null; });
            var attchMock = new Mock<IAttachmentRepository>();
            var docMock = new Mock<IDocumentRepository>();
            var docTMock = new Mock<IDocumentTypesRepository>();
            var cacheProviderMock = new Mock<ICacheProvider>();
            var filesMock = new Mock<IFilesManipulation>();
            var reqManipulation = new RequestsManipulation(reqMock.Object, attchMock.Object, docMock.Object, docTMock.Object, cacheProviderMock.Object, filesMock.Object);

            // Act
            var result = reqManipulation.GetEmployeeRequestsAsync("id", null);

            // Assert
            Assert.NotNull(result);

        }

        [Fact]
        public void SaveRequest_ReturnsRequest()
        {
            // Arrange
            var reqMock = new Mock<IRequestsRepository>();
            reqMock.Setup(MockItem => MockItem.SaveRequest(new Request(new Guid(), "reason", RequestType.Visa))).Returns(() => { return new Request(new Guid(), "reason", RequestType.Visa); });
            var attchMock = new Mock<IAttachmentRepository>();
            attchMock.Setup(MockItem => MockItem.SaveAttachment(new Attachment(new Guid(), new Guid()))).Returns(() => { return new Attachment(new Guid(), new Guid()); });
            attchMock.Setup(MockItem => MockItem.UpdateAttachment(new Attachment(new Guid(), new Guid()))).Returns(() => { return new Attachment(new Guid(), new Guid()); });
            var docMock = new Mock<IDocumentRepository>();
            var docTMock = new Mock<IDocumentTypesRepository>();
            docTMock.Setup(MockItem => MockItem.GetByName("name")).Returns(() => { return new DocumentType("name"); });
            var cacheProviderMock = new Mock<ICacheProvider>();
            var filesMock = new Mock<IFilesManipulation>();
            filesMock.Setup(MockItem => MockItem.UploadFile(null, "id")).Returns(() => { return null; });
            var reqManipulation = new RequestsManipulation(reqMock.Object, attchMock.Object, docMock.Object, docTMock.Object, cacheProviderMock.Object, filesMock.Object);

            // Act
            var result = reqManipulation.SaveRequest(new Guid(), "reason", RequestType.Visa, null, null);

            // Assert
            Assert.NotNull(result);

        }

        [Fact]
        public void UpdateRequestAsync_ReturnsRequest()
        {
            // Arrange
            var reqMock = new Mock<IRequestsRepository>();
            reqMock.Setup(MockItem => MockItem.GetRequestsAsync()).ReturnsAsync(() => {
                List<Request> requests = new List<Request>();
                requests.Add(new Request(new Guid(), "reason", RequestType.Passport));
                return requests;
            });
            reqMock.Setup(MockItem => MockItem.UpdateRequestAsync(new ReqItemRequest(), new User("Amila", "Lakovic", Gender.Female, "alakovic1@etf.unsa.ba", "alakovic1", "Sarajevo", DateTime.Now, "BiH"))).ReturnsAsync(() => { return new Request(new Guid(), "reason", RequestType.Visa); });
            var attchMock = new Mock<IAttachmentRepository>();
            var docMock = new Mock<IDocumentRepository>();
            var docTMock = new Mock<IDocumentTypesRepository>();
            var cacheProviderMock = new Mock<ICacheProvider>();
            var filesMock = new Mock<IFilesManipulation>();
            var reqManipulation = new RequestsManipulation(reqMock.Object, attchMock.Object, docMock.Object, docTMock.Object, cacheProviderMock.Object, filesMock.Object);

            // Act
            var result = reqManipulation.UpdateRequestAsync(new ReqItemRequest(), new User("Amila", "Lakovic", Gender.Female, "alakovic1@etf.unsa.ba", "alakovic1", "Sarajevo", DateTime.Now, "BiH"));

            // Assert
            Assert.NotNull(result);
        }


    }
}
