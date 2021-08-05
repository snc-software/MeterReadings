using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace MeterReadings.ApplicationLogic.Interfaces
{
    public interface ICsvFileReader
    {
        IEnumerable<T> ReadAndConvertFromCsv<T>(IFormFile uploadedFile);
    }
}