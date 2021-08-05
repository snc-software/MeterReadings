using System.Collections.Generic;
using System.IO;
using System.Text;
using FluentAssertions;
using MeterReadings.ApplicationLogic.Interfaces;
using MeterReadings.ApplicationLogic.Tests.TestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MeterReadings.ApplicationLogic.Tests
{
    [TestClass]
    public class CsvFileReaderTests
    {
        readonly ICsvFileReader _csvFileReader = new CsvFileReader();
        IFormFile _formFile;
        IList<TestModel> _testModels;

        [TestMethod]
        public void ConvertingACsvFileReturnsTheConvertedModelList()
        {
            var result = _csvFileReader
                .ReadAndConvertFromCsv<TestModel>(_formFile);

            result.Should().BeEquivalentTo(_testModels);
        }
        
        [TestInitialize]
        public void Setup()
        {
            _formFile = BuildTestFile();
            _testModels = new List<TestModel>
            {
                new()
                {
                    Id = 1,
                    Content = "test1"
                },
                new()
                {
                    Id = 2,
                    Content = "test2"
                }
            };
        }   
        
        
        IFormFile BuildTestFile()
        {
            byte[] bytes = Encoding.UTF8.GetBytes(
                "Id,Content\n1,test1\n2,test2");

            var file = new FormFile(
                baseStream: new MemoryStream(bytes),
                baseStreamOffset: 0,
                length: bytes.Length,
                name: "Data",
                fileName: "dummy.csv"
            )
            {
                Headers = new HeaderDictionary(),
                ContentType = "text/csv"
            };

            return file;
        }
    }
}