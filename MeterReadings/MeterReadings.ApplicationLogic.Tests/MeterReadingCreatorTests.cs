using System;
using System.Collections.Generic;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using MeterReadings.ApplicationLogic.Interfaces;
using MeterReadings.Domain;
using MeterReadings.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MeterReadings.ApplicationLogic.Tests
{
    [TestClass]
    public class MeterReadingCreatorTests
    {
        Mock<IValidator<MeterReadingModel>> _mockValidator;
        MeterReadingDbContext _dbContext;
        IMeterReadingCreator _creator;

        [TestMethod]
        public void AValidModelIsAddedToTheDatabase()
        {
            var validModel = new MeterReadingModel
            {
                AccountId = 1234,
                MeterReadingDateTime = DateTime.Today.AddDays(-1),
                MeterReadValue = "00012"
            };
            SetupForValidationToPass(validModel);
            
            _creator.Create(new List<MeterReadingModel>{validModel});

            _dbContext.MeterReadings.Should().Contain(validModel);
        }
        
        [TestMethod]
        public void AnInvalidModelIsNotAddedToTheDatabase()
        {
            var invalidModel = new MeterReadingModel
            {
                AccountId = 1234,
                MeterReadingDateTime = DateTime.Today.AddDays(-1),
                MeterReadValue = "00012"
            };
            SetupForValidationToFail(invalidModel);
            
            _creator.Create(new List<MeterReadingModel>{invalidModel});

            _dbContext.MeterReadings.Should().NotContain(invalidModel);
        }

        [TestInitialize]
        public void Setup()
        {
            var dbOptions = new DbContextOptionsBuilder<MeterReadingDbContext>()
                .UseInMemoryDatabase("in-memory")
                .Options;
            _dbContext = new MeterReadingDbContext(dbOptions);

            _mockValidator = new Mock<IValidator<MeterReadingModel>>();

            _mockValidator
                .Setup(m => m.Validate(It.IsAny<MeterReadingModel>()))
                .Returns(new ValidationResult(new List<ValidationFailure>()));

            _creator = new MeterReadingCreator(_dbContext, _mockValidator.Object);
        }

        void SetupForValidationToPass(MeterReadingModel model)
        {
            _mockValidator
                .Setup(m => m.Validate(model))
                .Returns(new ValidationResult(new List<ValidationFailure>()));
        }

        void SetupForValidationToFail(MeterReadingModel model)
        {
            _mockValidator
                .Setup(m => m.Validate(model))
                .Returns(new ValidationResult(new List<ValidationFailure> {new("AccountId", "failed")}));
        }
    }
}