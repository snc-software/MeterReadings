using System;
using System.Linq;
using FluentAssertions;
using FluentValidation;
using MeterReadings.ApplicationLogic.Validators;
using MeterReadings.Domain;
using MeterReadings.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MeterReadings.ApplicationLogic.Tests.ValidatorTests
{
    [TestClass]
    public class MeterReadingModelValidatorTests
    {
        MeterReadingDbContext _dbContext;
        IValidator<MeterReadingModel> _validator;
        

        [TestMethod]
        public void AValidMeterReadingModelReturnsAValidResult()
        {
            var validModel = new MeterReadingModel
            {
                AccountId = 1234,
                MeterReadingDateTime = DateTime.Today.AddDays(-2),
                MeterReadValue = "00012"
            };

            var result = _validator.Validate(validModel);

            result.IsValid.Should().BeTrue();
        }

        [TestMethod]
        public void AMeterReadingModelWithAnInvalidAccountIdReturnsAValidationError()
        {
            var invalidModel = new MeterReadingModel
            {
                AccountId = 0,
                MeterReadingDateTime = DateTime.Today.AddDays(-2),
                MeterReadValue = "00012"
            };

            var result = _validator.Validate(invalidModel);

            result.IsValid.Should().BeFalse();
            result.Errors.First().ToString().Should().Be("A meter reading must be attached to a valid account");
        }
        
        [TestMethod]
        public void AMeterReadingModelWithANullMeterReadValueReturnsAValidationError()
        {
            var invalidModel = new MeterReadingModel
            {
                AccountId = 1234,
                MeterReadingDateTime = DateTime.Today.AddDays(-2),
                MeterReadValue = null
            };

            var result = _validator.Validate(invalidModel);

            result.IsValid.Should().BeFalse();
            result.Errors.First().ToString().Should().Be("'Meter Read Value' must not be empty.");
        }
        
        [TestMethod]
        public void AMeterReadingModelWithAnInvalidMeterReadValueReturnsAValidationError()
        {
            var invalidModel = new MeterReadingModel
            {
                AccountId = 1234,
                MeterReadingDateTime = DateTime.Today.AddDays(-2),
                MeterReadValue = "1234"
            };

            var result = _validator.Validate(invalidModel);

            result.IsValid.Should().BeFalse();
            result.Errors.First().ToString().Should().Be("'Meter Read Value' must be 5 digits.");
        }

        [TestInitialize]
        public void Setup()
        {
            var dbOptions = new DbContextOptionsBuilder<MeterReadingDbContext>()
                .UseInMemoryDatabase("in-memory")
                .Options;
            _dbContext = new MeterReadingDbContext(dbOptions);

            if (!_dbContext.Accounts.Any())
            {
                _dbContext.Accounts.Add(new AccountModel
                {
                    AccountId = 1234,
                    FirstName = "Test",
                    LastName = "Person"
                });
                _dbContext.SaveChanges();
            }
            

            _validator = new MeterReadingModelValidator(_dbContext);
        }
    }
}