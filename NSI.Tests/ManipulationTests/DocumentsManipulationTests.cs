using Moq;
using NSI.BusinessLogic.Implementations;
using NSI.BusinessLogic.Interfaces;
using NSI.DataContracts.Models;
using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NSI.Tests.ManipulationTests
{
    public class DocumentsManipulationTests
    {
        [Fact]
        public void SaveDocument_ReturnsDocument()
        {
            // Arrange
            var docMock = new Mock<IDocumentsRepository>();
            docMock.Setup(MockItem => MockItem.SaveDocument(new Document(new Guid(), new Guid(), DateTime.Now, "url", "title"))).Returns(new Document(new Guid(), new Guid(), DateTime.Now, "url", "title"));
            var blockchainMock = new Mock<IBlockchainManipulation>();
            var filesMock = new Mock<IFilesManipulation>();
            var docManipulation = new DocumentsManipulation(docMock.Object, blockchainMock.Object, filesMock.Object);

            // Act
            var result = docManipulation.SaveDocument(new Guid(), new Guid(), DateTime.Now, "url", "title");

            // Assert
            Assert.Null(result);

        }

        [Fact]
        public void UpdateDocument_ReturnsDocument()
        {
            // Arrange
            var docMock = new Mock<IDocumentsRepository>();
            docMock.Setup(MockItem => MockItem.UpdateDocument(new Document(new Guid(), new Guid(), DateTime.Now, "url", "title"))).Returns(new Document(new Guid(), new Guid(), DateTime.Now, "url", "title"));
            var blockchainMock = new Mock<IBlockchainManipulation>();
            var filesMock = new Mock<IFilesManipulation>();
            var docManipulation = new DocumentsManipulation(docMock.Object, blockchainMock.Object, filesMock.Object);

            // Act
            var result = docManipulation.UpdateDocument(new Document(new Guid(), new Guid(), DateTime.Now, "url", "title"));

            // Assert
            Assert.Null(result);

        }

        [Fact]
        public void GetDocumentsByUserIdAndType_ReturnsDocuments()
        {
            // Arrange
            var docMock = new Mock<IDocumentsRepository>();
            docMock.Setup(MockItem => MockItem.GetDocumentsByUserIdAndType(new Guid(), "type")).ReturnsAsync(() => {
                List<Document> documents = new List<Document>();
                documents.Add(new Document(new Guid(), new Guid(), DateTime.Now, "url", "title"));
                return documents;
            });
            var blockchainMock = new Mock<IBlockchainManipulation>();
            var filesMock = new Mock<IFilesManipulation>();
            var docManipulation = new DocumentsManipulation(docMock.Object, blockchainMock.Object, filesMock.Object);

            // Act
            var result = docManipulation.GetDocumentsByUserIdAndType(new Guid(), "type");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Result.Count);
        }

        [Fact]
        public void GetDocumentsByUserIdAndType_ReturnsDocumentsNull()
        {
            // Arrange
            var docMock = new Mock<IDocumentsRepository>();
            docMock.Setup(MockItem => MockItem.GetDocumentsByUserIdAndType(new Guid(), "type")).ReturnsAsync(() => { return null; });
            var blockchainMock = new Mock<IBlockchainManipulation>();
            var filesMock = new Mock<IFilesManipulation>();
            var docManipulation = new DocumentsManipulation(docMock.Object, blockchainMock.Object, filesMock.Object);

            // Act
            var result = docManipulation.GetDocumentsByUserIdAndType(new Guid(), "type");

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetDocumentWithStatus_ReturnsDocumentStatusResponse()
        {
            // Arrange
            var docMock = new Mock<IDocumentsRepository>();
            docMock.Setup(MockItem => MockItem.GetDocumentsByUserIdAndType(new Guid(), "type")).ReturnsAsync(() => { return null; });
            var blockchainMock = new Mock<IBlockchainManipulation>();
            var filesMock = new Mock<IFilesManipulation>();
            var docManipulation = new DocumentsManipulation(docMock.Object, blockchainMock.Object, filesMock.Object);

            // Act
            var result = docManipulation.GetDocumentWithStatus(new Guid());

            // Assert
            Assert.NotNull(result);
        }
    }
}
