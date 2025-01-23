using Moq;
using SmartVault.DataAccess.Interfaces;
using SmartVault.Domain;
using SmartVault.Domain.Interfaces;
using SmartVault.Domain.Services;
using System.Collections.Generic;
using Xunit;

namespace SmartVault.Tests.DataServiceTests
{
    public class GetAllFileSizesTests
    {
        private readonly Mock<IDatabaseService> _databaseServiceMock;
        private readonly Mock<IFileService> _fileServiceMock;

        public GetAllFileSizesTests()
        {
            _databaseServiceMock = new Mock<IDatabaseService>();
            _fileServiceMock = new Mock<IFileService>();
        }

        [Fact]
        public void GetAllFileSizes_ShouldReturnZero_WhenNoFilePathsAreReturned()
        {
            // Arrange
            _databaseServiceMock
                .Setup(m => m.GetFilePaths(It.IsAny<string>(), null))
                .Returns(new List<string>());

            IDataService dataService = new DataService(_databaseServiceMock.Object, _fileServiceMock.Object);

            // Act
            var result = dataService.GetAllFileSizes();

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void GetAllFileSizes_ShouldReturnCorrectTotalSize()
        {
            // Arrange
            _databaseServiceMock
                .Setup(db => db.GetFilePaths(It.IsAny<string>(), null))
                .Returns(new List<string> { "file1.txt", "file2.txt" });

            _fileServiceMock
                .Setup(fs => fs.GetFileSize("file1.txt"))
                .Returns(500);
            _fileServiceMock
                .Setup(fs => fs.GetFileSize("file2.txt"))
                .Returns(300);

            IDataService dataService = new DataService(_databaseServiceMock.Object, _fileServiceMock.Object);

            // Act
            var totalSize = dataService.GetAllFileSizes();

            // Assert
            Assert.Equal(800, totalSize);
        }
    }
}