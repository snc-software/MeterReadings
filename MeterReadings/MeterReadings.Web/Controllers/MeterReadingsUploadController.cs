using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MeterReadings.ServiceModel.Requests.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MeterReadings.Web.Controllers
{
    [ApiController]
    public class MeterReadingsUploadController : ControllerBase
    {
        readonly IMediator _mediator;

        public MeterReadingsUploadController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost, DisableRequestSizeLimit]
        [Route("meter-reading-uploads")]
        public async Task<IActionResult> Upload()
        {
            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files[0];

            await _mediator.Send(new ProcessMeterReadingsCommand
            {
                UploadedMeterReadings = file
            });

            return Ok();
        }
    }
}