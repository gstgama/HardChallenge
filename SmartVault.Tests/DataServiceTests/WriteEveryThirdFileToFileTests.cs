using Moq;
using SmartVault.DataAccess.Interfaces;
using SmartVault.Domain;
using SmartVault.Domain.Interfaces;
using SmartVault.Domain.Services;
using System.Collections.Generic;
using Xunit;

namespace SmartVault.Tests.DataServiceTests
{
    public class WriteEveryThirdFileToFileTests
    {
        private readonly Mock<IDatabaseService> _databaseServiceMock;
        private readonly Mock<IFileService> _fileServiceMock;

        public WriteEveryThirdFileToFileTests()
        {
            _databaseServiceMock = new Mock<IDatabaseService>();
            _fileServiceMock = new Mock<IFileService>();
        }

        [Fact]
        public void WriteEveryThirdFileToFile_ShouldDoNothing_WhenFilePathsAreEmpty()
        {
            // Arrange
            _databaseServiceMock
                .Setup(m => m.GetFilePaths(It.IsAny<string>(), It.IsAny<Dictionary<string, object>>()))
                .Returns(new List<string>());

            IDataService dataService = new DataService(_databaseServiceMock.Object, _fileServiceMock.Object);

            // Act
            dataService.WriteEveryThirdFileToFile("accountId");

            // Assert
            _fileServiceMock.Verify(m => m.WriteToFile(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void WriteEveryThirdFileToFile_ShouldDoNothing_WhenFilePathsAreLessThanThree()
        {
            // Arrange
            _databaseServiceMock
                .Setup(m => m.GetFilePaths(It.IsAny<string>(), It.IsAny<Dictionary<string, object>>()))
                .Returns(new List<string> { "file1.txt", "file2.txt" });

            IDataService dataService = new DataService(_databaseServiceMock.Object, _fileServiceMock.Object);

            // Act
            dataService.WriteEveryThirdFileToFile("accountId");

            // Assert
            _fileServiceMock.Verify(m => m.WriteToFile(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void WriteEveryThirdFileToFile_ShouldWriteOnlyThirdFile()
        {
            // Arrange
            var filePaths = new List<string> { "file1.txt", "file2.txt", "file3.txt" };
            _databaseServiceMock
                .Setup(m => m.GetFilePaths(It.IsAny<string>(), It.IsAny<Dictionary<string, object>>()))
                .Returns(filePaths);

            _fileServiceMock.Setup(m => m.ReadFileContent("file3.txt")).Returns("Smith Property");

            IDataService dataService = new DataService(_databaseServiceMock.Object, _fileServiceMock.Object);

            // Act
            dataService.WriteEveryThirdFileToFile("accountId");

            // Assert
            _fileServiceMock.Verify(m => m.WriteToFile("Output.txt", "Smith Property"), Times.Once);
        }
    }
}