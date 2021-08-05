using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MeterReadings.ApplicationLogic.Interfaces;
using MeterReadings.Domain;
using MeterReadings.ServiceModel.Requests.Commands;

namespace MeterReadings.ServiceModel.Handlers.Commands
{
    public class ProcessMeterReadingsCommandHandler : AsyncRequestHandler<ProcessMeterReadingsCommand>
    {
        readonly ICsvFileReader _csvFileReader;
        readonly IMeterReadingCreator _creator;

        public ProcessMeterReadingsCommandHandler(ICsvFileReader csvFileReader,
            IMeterReadingCreator creator)
        {
            _csvFileReader = csvFileReader;
            _creator = creator;
        }

        protected override async Task Handle(ProcessMeterReadingsCommand request, CancellationToken cancellationToken)
        {
            var meterReadings = _csvFileReader
                .ReadAndConvertFromCsv<MeterReadingModel>(request.UploadedMeterReadings)
                .ToList();
            
            if (meterReadings.Any())
            {
                _creator.Create(meterReadings);
            }
        }
    }
}