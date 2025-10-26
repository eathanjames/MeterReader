using Application.DTO;
using Application.Interfaces;
using Application.Validation;
using Moq;

namespace Application.Tests;

[TestFixture]
public class MeterReadingValidatorTests
{
    private Mock<IAccountRepository> _accountRepositoryMock;
    private Mock<IMeterReadingRepository> _meterReadingRepositoryMock;
    private MeterReadingValidator _meterReadingValidator;    
    
    [SetUp]
    public void Setup()
    {
        _accountRepositoryMock = new Mock<IAccountRepository>();
        _meterReadingRepositoryMock = new Mock<IMeterReadingRepository>();
        _meterReadingValidator = new MeterReadingValidator(_accountRepositoryMock.Object, _meterReadingRepositoryMock.Object);
    }

    [Test]
    public async Task ValidateAsync_AllValid_ReturnsSuccess()
    {
        // Arrange
        var row = new MeterReadRow
        {
            AccountId = 111,
            MeterReadValue = "11111",
            MeterReadingDateTime = DateTime.Now
        };
        
        _accountRepositoryMock.Setup(x => x.ExistsAsync(row.AccountId)).ReturnsAsync(true);
        _meterReadingRepositoryMock.Setup(x => x.ExistsAsync(row.AccountId, row.MeterReadingDateTime, row.MeterReadValue))
            .ReturnsAsync(false);
        _meterReadingRepositoryMock.Setup(x => x.NewerExistsAsync(row.AccountId, row.MeterReadingDateTime, row.MeterReadValue))
            .ReturnsAsync(false);

        // Act
        var result = await _meterReadingValidator.ValidateAsync(row);

        // Assert
        Assert.That(result.success, Is.True);
        Assert.That(result.Errors, Is.Empty);
    }
    
    [Test]
    public async Task ValidateAsync_InvalidAccount_ReturnsError()
    {
        // Arrange
        var row = new MeterReadRow
        {
            AccountId = 111,
            MeterReadValue = "11111",
            MeterReadingDateTime = DateTime.Now
        };
        
        _accountRepositoryMock.Setup(x => x.ExistsAsync(row.AccountId)).ReturnsAsync(false);

        // Act
        var result = await _meterReadingValidator.ValidateAsync(row);

        // Assert
        Assert.That(result.success, Is.False);
        Assert.That(result.Errors, Is.Not.Empty);
    }
    
    [TestCase("00")]
    [TestCase("aa")]
    [TestCase("VOID")]
    [TestCase("0a0a0")]
    [TestCase("000000")]
    [TestCase("aaaaa")]
    public async Task ValidateAsync_MeterReadingValueChecker_ReturnsError(string meterReadingValue)
    {
        // Arrange
        var row = new MeterReadRow
        {
            AccountId = 111,
            MeterReadValue = meterReadingValue,
            MeterReadingDateTime = DateTime.Now
        };
        
        _accountRepositoryMock.Setup(x => x.ExistsAsync(row.AccountId)).ReturnsAsync(true);

        // Act
        var result = await _meterReadingValidator.ValidateAsync(row);

        // Assert
        Assert.That(result.success, Is.False);
        Assert.That(result.Errors, Is.Not.Empty);
    }
    
    [Test]
    public async Task ValidateAsync_IsDuplicate_ReturnsError()
    {
        // Arrange
        var row = new MeterReadRow
        {
            AccountId = 111,
            MeterReadValue = "11111",
            MeterReadingDateTime = DateTime.Now
        };
        
        _accountRepositoryMock.Setup(x => x.ExistsAsync(row.AccountId)).ReturnsAsync(true);
        _meterReadingRepositoryMock.Setup(x => x.ExistsAsync(row.AccountId, row.MeterReadingDateTime, row.MeterReadValue))
            .ReturnsAsync(true);
        _meterReadingRepositoryMock.Setup(x => x.NewerExistsAsync(row.AccountId, row.MeterReadingDateTime, row.MeterReadValue))
            .ReturnsAsync(false);
        // Act
        var result = await _meterReadingValidator.ValidateAsync(row);

        // Assert
        Assert.That(result.success, Is.False);
        Assert.That(result.Errors, Is.Not.Empty);
    }
    
    [Test]
    public async Task ValidateAsync_IsOlderEntryAsync_ReturnsError()
    {
        // Arrange
        var row = new MeterReadRow
        {
            AccountId = 111,
            MeterReadValue = "11111",
            MeterReadingDateTime = DateTime.Now
        };
        
        _accountRepositoryMock.Setup(x => x.ExistsAsync(row.AccountId)).ReturnsAsync(true);
        _meterReadingRepositoryMock.Setup(x => x.ExistsAsync(row.AccountId, row.MeterReadingDateTime, row.MeterReadValue))
            .ReturnsAsync(false);
        _meterReadingRepositoryMock.Setup(x => x.NewerExistsAsync(row.AccountId, row.MeterReadingDateTime, row.MeterReadValue))
            .ReturnsAsync(true);
        // Act
        var result = await _meterReadingValidator.ValidateAsync(row);
        
        

        // Assert
        Assert.That(result.success, Is.False);
        Assert.That(result.Errors, Is.Not.Empty);
    }
}