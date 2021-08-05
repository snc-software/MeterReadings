using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using MeterReadings.ServiceModel.Requests.Commands;
using MeterReadings.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MeterReadings.Web.Tests
{
    [TestClass]
    public class MeterReadingsUploadControllerTests
    {
        Mock<IMediator> _mockMediator;
        Mock<IFormFile> _mockFormFile;
        Mock<IFormCollection> _mockFormCollection;
        MeterReadingsUploadController _controller;

        [TestMethod]
        public async Task UploadingAMeterReadingFileProcessesTheMeterReadings()
        {
            await _controller.Upload();
            
            _mockMediator.Verify(m => m.Send(
                It.Is<ProcessMeterReadingsCommand>(x => x.UploadedMeterReadings == _mockFormFile.Object)
                , It.IsAny<CancellationToken>()), Times.Once());
        }

        [TestMethod]
        public async Task UploadingAMeterReadingFileReturnsOk()
        {
            var response = await _controller.Upload() as OkResult;

            response.Should().NotBeNull();
        }
        
        
        [TestInitialize]
        public void Setup()
        {
            _mockFormFile = new Mock<IFormFile>();
            _mockMediator = new Mock<IMediator>();
            _mockFormCollection = new Mock<IFormCollection>();
            _mockFormCollection
                .Setup(m => m.Files)
                .Returns(new FormFileCollection
                {
                    _mockFormFile.Object
                });

            _controller = new MeterReadingsUploadController(_mockMediator.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() 
                }
            };
            _controller.Request.Form = _mockFormCollection.Object;
        }
    }
}