using DateTimeTests.Common;
using Xunit;
using Microsoft.EntityFrameworkCore;
using DTWebAPI.Services;
using DTWebAPI.Services.AddClass;
using DTWebAPI.Models;
using Shouldly;

namespace DateTimeTests.Values.Commands
{
    public class WebAPIRepTests : TestCommandBase
    {
        [Fact]
        public async Task GetValues_Success()
        {
            // Arrange
            var WebAPIRep = new SQLDTWebAPIRepository(Context);

            // Act
            var listValues = WebAPIRep.GetValues(8);

            // Assert
            Assert.NotNull(listValues);
        }

        [Fact]
        public async Task GetResult_Success()
        {
            // Arrange
            var WebAPIRep = new SQLDTWebAPIRepository(Context);

            // Act
            var listResults = WebAPIRep.GetResults(new Filters());

            // Assert
            Assert.NotNull(listResults);
        }

        [Fact]
        public async Task GetResultFromFile_Success()
        {
            // Arrange
            var WebAPIRep = new SQLDTWebAPIRepository(Context);

            // Act
            var listResults = WebAPIRep.GetResults(new Filters() { FileName = "Rom.csv" });

            // Assert
            Assert.Equal("1", listResults.Value.Count().ToString());
        }

        [Fact]
        public async Task GetResultForDate_Success()
        {
            // Arrange
            var WebAPIRep = new SQLDTWebAPIRepository(Context);

            // Act
            var listResults = WebAPIRep.GetResults(new Filters() { FromMinDateTime = new DateTime(2008, 05, 05, 8, 0, 0), 
                                                                  ToMinDateTime = new DateTime(2008, 07, 13, 14, 0, 0) });

            // Assert
            Assert.Equal("1", listResults.Value.Count().ToString());
        }

        [Fact]
        public async Task GetResultForAverIndex_Success()
        {
            // Arrange
            var WebAPIRep = new SQLDTWebAPIRepository(Context);

            // Act
            var listResults = WebAPIRep.GetResults(new Filters() { MinAverIndex = 40.869, MaxAverIndex = 78.6584 });

            // Assert
            Assert.Equal("1", listResults.Value.Count().ToString());
        }

        [Fact]
        public async Task GetResultForAverTime_Success()
        {
            // Arrange
            var WebAPIRep = new SQLDTWebAPIRepository(Context);

            // Act
            var listResults = WebAPIRep.GetResults(new Filters() { MinAverTime = 2, MaxAverTime = 10 });

            // Assert
            Assert.Equal("0", listResults.Value.Count().ToString());
        }

        [Fact]
        public async Task GetFiles_Success()
        {
            // Arrange
            var WebAPIRep = new SQLDTWebAPIRepository(Context);

            // Act
            var fileInf = WebAPIRep.GetFiles("Rom");

            // Assert
            Assert.Contains(".csv", fileInf.Name);
            fileInf.ShouldBeOfType<FileInf>();
        }

        [Fact]
        public async Task GetFiles_Unsuccess()
        {
            // Arrange
            var WebAPIRep = new SQLDTWebAPIRepository(Context);

            // Act
            var fileInf = WebAPIRep.GetFiles("IncorrectName");

            // Assert
            Assert.Null(fileInf);
        }

        [Fact]
        public async Task GetOrAddFile_Success()
        {
            // Arrange
            var WebAPIRep = new SQLDTWebAPIRepository(Context);

            // Act
            var fileInf = WebAPIRep.GetOrAddFile("NewFile");

            // Assert
            fileInf.ShouldBeOfType<FileInf>();
        }
    }
}
