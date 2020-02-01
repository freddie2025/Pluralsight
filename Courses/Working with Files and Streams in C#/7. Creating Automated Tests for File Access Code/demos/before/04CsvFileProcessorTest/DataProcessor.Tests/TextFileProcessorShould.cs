using System;
using Xunit;
using System.IO.Abstractions.TestingHelpers;

namespace DataProcessor.Tests
{
    public class TextFileProcessorShould
    {
        [Fact]
        public void MakeSecondLineUpperCase()
        {
            // Create a mock input file
            var mockInputFile = new MockFileData("Line 1\nLine 2\nLine 3");

            // Setup mock file system starting state
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(@"c:\root\in\myfile.txt", mockInputFile);
            mockFileSystem.AddDirectory(@"c:\root\out");

            // Create TextFileProcessor with mock file system
            var sut = new TextFileProcessor(@"c:\root\in\myfile.txt",
                                            @"c:\root\out\myfile.txt",
                                            mockFileSystem);

            // Process test file
            sut.Process();

            // Check mock file system for output file
            Assert.True(mockFileSystem.FileExists(@"c:\root\out\myfile.txt"));

            // Check content of output file in mock file system
            MockFileData processedFile = mockFileSystem.GetFile(@"c:\root\out\myfile.txt");

            string[] lines = processedFile.TextContents.SplitLines();

            Assert.Equal("Line 1", lines[0]);
            Assert.Equal("LINE 2", lines[1]);
            Assert.Equal("Line 3", lines[2]);
        }
    }
}
