using System.Collections.Generic;
using MeterReadings.Domain;

namespace MeterReadings.ApplicationLogic.Interfaces
{
    public interface IMeterReadingCreator
    {
        void Create(IList<MeterReadingModel> meterReadings);
    }
}