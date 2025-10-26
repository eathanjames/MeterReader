using Application.DTO;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Application.Tests;

[TestFixture]
public class MeterReadingServiceTests
{
        private Mock<IFileProcessor> _processorMock;
        private Mock<IMeterReadingValidator> _validatorMock;
        private Mock<IMeterReadingRepository> _repoMock;
        private MeterReadingService _service;
        private Mock<IFormFile> _fileMock;

        [SetUp]
        public void SetUp()
        {
            _processorMock = new Mock<IFileProcessor>();
            _validatorMock = new Mock<IMeterReadingValidator>();
            _repoMock = new Mock<IMeterReadingRepository>();
            _service = new MeterReadingService(_processorMock.Object, _validatorMock.Object, _repoMock.Object);
            _fileMock = new Mock<IFormFile>(MockBehavior.Strict);
        }
        
        [Test]
        public async Task ProcessMeterReadingFileAsync_AllValid_CreatesEntries_AndReturnsSuccesses()
        {
            // Arrange
            var row1 = new MeterReadRow { RowNumber = 1, AccountId = 1, MeterReadValue = "12345", MeterReadingDateTime = DateTime.UtcNow };
            var row2 = new MeterReadRow { RowNumber = 2, AccountId = 2, MeterReadValue = "54321", MeterReadingDateTime = DateTime.UtcNow.AddMinutes(-1) };
            var successes = new List<MeterReadRow> { row1, row2 };
            var initialErrors = new List<FileReaderRowError>();

            _processorMock
                .Setup(p => p.ProcessFileAsync(_fileMock.Object))
                .ReturnsAsync((success: successes, Errors: initialErrors));

            _validatorMock.Setup(v => v.ValidateAsync(row1))
                .ReturnsAsync((true, new List<string>()));
            _validatorMock.Setup(v => v.ValidateAsync(row2))
                .ReturnsAsync((true, new List<string>()));

            _repoMock.Setup(r => r.CreateAsync(It.IsAny<MeterReading>())).Returns(Task.CompletedTask);

            // Act
            var result = await _service.ProcessMeterReadingFileAsync(_fileMock.Object);

            // Assert
            _repoMock.Verify(r => r.CreateAsync(It.IsAny<MeterReading>()), Times.Exactly(2));
            Assert.That(result.Successful, Is.EquivalentTo(successes));
            Assert.That(result.Failures, Is.Empty);
        }
}