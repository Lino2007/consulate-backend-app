using Moq;
using NSI.BusinessLogic.Interfaces;
using NSI.DataContracts.Response;
using System;
using System.Threading.Tasks;
using Xunit;
using NSI.DataContracts.Models;
using NSI.Common.Enumerations;
using NSI.REST.Controllers;
using System.Net;

namespace NSI.Tests.ControllerTests
{
    public class DocControllerTest
    {

        [Fact]
        public async Task GetDocumentIfNotExpired_ValidId_ReturnsValidDocument()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            var docMock = new Mock<IDocumentsManipulation>();
            docMock.Setup(MockItem => MockItem.GetDocumentWithStatus(id)).ReturnsAsync(new DocumentStatusResponse(new Document(id, new Guid(), DateTime.UtcNow, "url", "Valid document"), DocumentStatus.Valid));
            var authController = new DocumentController(docMock.Object);

            // Act
            var result = await authController.GetDocumentIfNotExpired(id);

            // Assert
            Assert.Equal((int) HttpStatusCode.OK, result.StatusCode);
            Assert.Contains("Document is VALID.", result.Content);
        }

        [Fact]
        public async Task GetDocumentIfNotExpired_InvalidId_ReturnsInvalid()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            var docMock = new Mock<IDocumentsManipulation>();
            docMock.Setup(MockItem => MockItem.GetDocumentWithStatus(id)).ReturnsAsync(new DocumentStatusResponse(null, DocumentStatus.Invalid));
            var authController = new DocumentController(docMock.Object);

            // Act
            var result = await authController.GetDocumentIfNotExpired(id);

            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Contains("Document is INVALID.", result.Content);
            Assert.Contains("Document does not exist.", result.Content);
        }


         [Fact]
         public async Task GetDocumentIfNotExpired_ValidId_ReturnsExpired()
         {
             // Arrange
             Guid id = Guid.NewGuid();
             var docMock = new Mock<IDocumentsManipulation>();
             docMock.Setup(MockItem => MockItem.GetDocumentWithStatus(id)).ReturnsAsync(new DocumentStatusResponse(new Document(id, new Guid(), DateTime.UtcNow, "url", "Valid document"), DocumentStatus.Expired));
             var authController = new DocumentController(docMock.Object);

             // Act
             var result = await authController.GetDocumentIfNotExpired(id);

             // Assert
             Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
             Assert.Contains("Document has EXPIRED.", result.Content);
             Assert.Contains("Document expired at ", result.Content);
         }

        [Fact]
        public async Task GetDocumentIfNotExpired_ValidId_ReturnsInvalidHashErr()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            var docMock = new Mock<IDocumentsManipulation>();
            docMock.Setup(MockItem => MockItem.GetDocumentWithStatus(id)).ReturnsAsync(new DocumentStatusResponse(new Document(id, new Guid(), DateTime.UtcNow, "url", "Valid document"), DocumentStatus.Invalid));
            var authController = new DocumentController(docMock.Object);

            // Act
            var result = await authController.GetDocumentIfNotExpired(id);

            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Contains("Document is INVALID.", result.Content);
            Assert.Contains("Document hash does not match.", result.Content);
        }
    }
}
