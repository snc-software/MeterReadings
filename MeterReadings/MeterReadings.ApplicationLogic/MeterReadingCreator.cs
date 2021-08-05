using System.Collections.Generic;
using FluentValidation;
using MeterReadings.ApplicationLogic.Interfaces;
using MeterReadings.Domain;
using MeterReadings.Infrastructure;

namespace MeterReadings.ApplicationLogic
{
    public class MeterReadingCreator : IMeterReadingCreator
    {
        readonly MeterReadingDbContext _context;
        readonly IValidator<MeterReadingModel> _validator;

        public MeterReadingCreator(MeterReadingDbContext context, IValidator<MeterReadingModel> validator)
        {
            _context = context;
            _validator = validator;
        }
        
        public void Create(IList<MeterReadingModel> meterReadings)
        {
            foreach (var meterReading in meterReadings)
            {
                CreateMeterReadingIfValid(meterReading);
            }
            _context.SaveChangesAsync();
        }

        void CreateMeterReadingIfValid(MeterReadingModel meterReading)
        {
            if (_validator.Validate(meterReading).IsValid)
            {
                _context.Add(meterReading);
            }
        }
    }
}