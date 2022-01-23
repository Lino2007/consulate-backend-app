using Moq;
using NSI.BusinessLogic.Implementations;
using NSI.DataContracts.Models;
using NSI.Repository.Interfaces;
using Xunit;

namespace NSI.Tests.ManipulationTests
{
    public class DocumentTypesManpulationTests
    {
        [Fact]
        public void GetDocumentTypeByName_ReturnsDocumentType()
        {
            // Arrange
            var docTMock = new Mock<IDocumentTypesRepository>();
            docTMock.Setup(MockItem => MockItem.GetByName("name")).Returns(new DocumentType("name"));
            var docTManipulation = new DocumentTypesManipulation(docTMock.Object);

            // Act
            var result = docTManipulation.GetByName("name");

            // Assert
            Assert.NotNull(result.Name);
            Assert.Equal("name", result.Name);

        }
    }
}
