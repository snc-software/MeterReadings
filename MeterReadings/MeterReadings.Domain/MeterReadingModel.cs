using System;
using CsvHelper.Configuration.Attributes;

namespace MeterReadings.Domain
{
    public class MeterReadingModel
    {
        [Ignore]
        public Guid Id { get; set; }
        public int AccountId { get; set; }
        public DateTime MeterReadingDateTime { get; set; }
        public string MeterReadValue { get; set; }
    }
}