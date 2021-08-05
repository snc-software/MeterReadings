using MediatR;
using Microsoft.AspNetCore.Http;

namespace MeterReadings.ServiceModel.Requests.Commands
{
    public class ProcessMeterReadingsCommand : IRequest
    {
        public IFormFile UploadedMeterReadings { get; set; }
    }
}