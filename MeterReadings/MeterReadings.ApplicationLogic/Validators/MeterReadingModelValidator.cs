using FluentValidation;
using MeterReadings.Domain;

namespace MeterReadings.ApplicationLogic.Validators
{
    public class MeterReadingModelValidator : AbstractValidator<MeterReadingModel>
    {
        public MeterReadingModelValidator()
        {
            RuleFor(x => x.AccountId)
                .GreaterThan(0);
            RuleFor(x => x.MeterReadValue)
                .NotNull()
                .Matches("^\\d{5}$")
                .WithMessage("'Meter Read Value' must be 5 digits.");
        }
    }
}