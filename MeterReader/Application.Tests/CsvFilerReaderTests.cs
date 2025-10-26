using System.Text;
using Application.Parser;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Application.Tests;

[TestFixture]
public class CsvFilerReaderTests
{
    private Mock<IFormFile> _fileMock;
    private CsvFileReader _csvReader;

    [SetUp]
    public void SetUp()
    {
        _fileMock = new Mock<IFormFile>(MockBehavior.Strict);
        _csvReader = new CsvFileReader();
    }
    
    // Helper to make an IFormFile from string content
    private static IFormFile MakeFormFile(string content, string fileName = "meter-readings.csv")
    {
        var bytes = Encoding.UTF8.GetBytes(content);
        var stream = new MemoryStream(bytes);
        return new FormFile(stream, 0, bytes.Length, "file", fileName);
    }

    [Test]
    public void CanRead_ValidContentType_ReturnsTrue()
    {
        // Arrange
        _fileMock.Setup(x => x.ContentType).Returns("text/csv");
        var canRead = false;

        //Act
        canRead = _csvReader.CanRead(_fileMock.Object);

        //Assert
        Assert.That(canRead, Is.True);
    }

    [TestCase("text/plain")]
    [TestCase("1234")]
    [TestCase("csv")]
    public void CanRead_InValidContentType_ReturnsFalse(string contentType)
    {
        // Arrange
        _fileMock.Setup(x => x.ContentType).Returns(contentType);
        var canRead = false;

        //Act
        canRead = _csvReader.CanRead(_fileMock.Object);

        //Assert
        Assert.That(canRead, Is.False);
    }

    [Test]
    public async Task ReadAsync_ValidFile_ParsesRows_ReturnsValidResults()
    {
        //Arrange
        //Replace with test files.
        var csv =
        "AccountId,MeterReadingDateTime,MeterReadValue\r\n" +
        "1,2024-01-01 10:00:00,12345\r\n" +
        "2,2024-01-02 11:00:00,54321\r\n";
    
        var file = MakeFormFile(csv);
        
        //Act
        var (success, errors) = await _csvReader.ReadAsync(file);
 
        
        //Assert
        Assert.That(errors, Is.Empty);
        Assert.That(success, Has.Count.EqualTo(2)); 
    }
    
    [Test]
    public async Task ReadAsync_InValidFile_ParsesRows_ReturnsErrors()
    {
        //Arrange
        //Replace with test files.
        var csv =
            "AccountId,MeterReadingDateTime,MeterReadValue\r\n" +
            "1,2024-01-01 10:00:00,12345,gh\r\n" +
            "2,2024-01-02 11:00:00\r\n";
    
        var file = MakeFormFile(csv);
        
        //Act
        var (success, errors) = await _csvReader.ReadAsync(file);
 
        
        //Assert
        Assert.That(errors, Has.Count.EqualTo(2));
        Assert.That(success, Is.Empty); 
    }
}