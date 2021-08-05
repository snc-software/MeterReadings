using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MeterReadings.ApplicationLogic.Interfaces;
using MeterReadings.Domain;
using MeterReadings.ServiceModel.Handlers.Commands;
using MeterReadings.ServiceModel.Requests.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MeterReadings.ServiceModel.Tests
{
    [TestClass]
    public class ProcessMeterReadingsCommandHandlerTests
    {
        Mock<ICsvFileReader> _mockCsvFileReader;
        Mock<IMeterReadingCreator> _mockMeterReadingCreator;
        IFormFile _formFile;
        IRequestHandler<ProcessMeterReadingsCommand> _handler;
        ProcessMeterReadingsCommand _command;

        [TestMethod]
        public async Task ProcessingMeterReadingsConvertsTheFormFileToObjects()
        {
            await _handler.Handle(_command, default);
            
            _mockCsvFileReader.Verify(m => 
                m.ReadAndConvertFromCsv<MeterReadingModel>(_formFile), Times.Once);
        }
        
        [TestMethod]
        public async Task IfThereAreMeterReadingsInTheCsvTheyAreCreated()
        {
            var records = new List<MeterReadingModel>
            {
                new()
                {
                    AccountId = 1234,
                    MeterReadingDateTime = DateTime.Today.AddDays(-1),
                    MeterReadValue = "00123"
                }
            };
            
            SetupCsvReaderToReturn(records);
            
            await _handler.Handle(_command, default);
            
            _mockMeterReadingCreator.Verify(m => 
                m.Create(records), Times.Once);
        }
        
        [TestMethod]
        public async Task IfThereAreNoMeterReadingsInTheCsvTheyAreNoneCreated()
        {
            var records = new List<MeterReadingModel>();
            
            SetupCsvReaderToReturn(records);
            
            await _handler.Handle(_command, default);
            
            _mockMeterReadingCreator.Verify(m => 
                m.Create(It.IsAny<IList<MeterReadingModel>>()), Times.Never);
        }

        void SetupCsvReaderToReturn(List<MeterReadingModel> records)
        {
            _mockCsvFileReader
                .Setup(m =>
                    m.ReadAndConvertFromCsv<MeterReadingModel>(_formFile))
                .Returns(records);
        }

        [TestInitialize]
        public void Setup()
        {
            _formFile = BuildTestFile();
            SetupMocks();
            _command = new ProcessMeterReadingsCommand
            {
                UploadedMeterReadings = _formFile
            };
            _handler = new ProcessMeterReadingsCommandHandler(_mockCsvFileReader.Object,
                _mockMeterReadingCreator.Object);
        }

        void SetupMocks()
        {
            _mockCsvFileReader = new Mock<ICsvFileReader>();
            _mockMeterReadingCreator = new Mock<IMeterReadingCreator>();

            _mockCsvFileReader
                .Setup(m => m.ReadAndConvertFromCsv<MeterReadingModel>(_formFile))
                .Returns(new List<MeterReadingModel>());

            _mockMeterReadingCreator
                .Setup(m => m.Create(It.IsAny<IList<MeterReadingModel>>()));
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