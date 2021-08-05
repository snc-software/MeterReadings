using System.Linq;
using FluentValidation;
using MeterReadings.Domain;
using MeterReadings.Infrastructure;

namespace MeterReadings.ApplicationLogic.Validators
{
    public class MeterReadingModelValidator : AbstractValidator<MeterReadingModel>
    {
        readonly MeterReadingDbContext _dbContext;

        public MeterReadingModelValidator(MeterReadingDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.AccountId)
                .Must(BeValidAccount)
                .WithMessage("A meter reading must be attached to a valid account");
            
            RuleFor(x => x.MeterReadValue)
                .NotNull()
                .Matches("^\\d{5}$")
                .WithMessage("'Meter Read Value' must be 5 digits.");
        }

        bool BeValidAccount(int accountId)
        {
            return _dbContext.Accounts.Any(x => x.AccountId == accountId);
        }
        
    }
}