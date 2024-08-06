using System.Net;
using Moq;
using Moq.Protected;
using WebApp.Models;
using WebApp.Services;
using Microsoft.Extensions.Configuration;

namespace NUnitTests;

[TestFixture]
public class StringProcessServiceTests
{
    private HttpClient _httpClient;
    private StringProcessService _service;
    private Mock<IConfiguration> _configurationMock;

    [SetUp]
    public void SetUp()
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("[0]")
            })
            .Verifiable();

        _httpClient = new HttpClient(handlerMock.Object);

        _configurationMock = new Mock<IConfiguration>();
        var sectionMock = new Mock<IConfigurationSection>();
        sectionMock.Setup(a => a.Value).Returns("3");
        _configurationMock.Setup(c => c.GetSection("Settings:ParallelLimit")).Returns(sectionMock.Object);

        _service = new StringProcessService(_httpClient, _configurationMock.Object);
    }

    [Test]
    public void ProcessString_ShouldProcessCorrectly_ForEvenLengthString()
    {
        var result = _service.ProcessString("abcdef");
        Assert.That(result, Is.EqualTo("cbafed"));
    }

    [Test]
    [TestCase("a", "aa")]
    [TestCase("abcde", "edcbaabcde")]
    public void ProcessString_ShouldProcessCorrectly_ForOddLengthString(string input, string expected)
    {
        var result = _service.ProcessString(input);
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ValidateString_ShouldReturnError_WhenStringContainsInvalidChars()
    {
        var result = _service.ValidateString("abc1");
        Assert.That(result, Is.EqualTo("Ошибка: найдены неподходящие символы - 1."));
    }

    [Test]
    public void GetCharCounts_ShouldReturnCorrectCounts()
    {
        var result = _service.GetCharCounts("aabbcc");
        Assert.That(result['a'], Is.EqualTo(2));
        Assert.That(result['b'], Is.EqualTo(2));
        Assert.That(result['c'], Is.EqualTo(2));
    }

    [Test]
    public void GetLongestVowelSubstring_ShouldReturnLongestSubstring()
    {
        var result = _service.GetLongestVowelSubstring("cbayuoiedcbbcdeiouyabc");
        Assert.That(result, Is.EqualTo("ayuoiedcbbcdeiouya"));
    }

    [Test]
    public void SortString_ShouldSortCorrectly_WithQuickSort()
    {
        var result = _service.SortString("dbca", SortAlgorithmNames.QuickSort);
        Assert.That(result, Is.EqualTo("abcd"));
    }

    [Test]
    public void SortString_ShouldSortCorrectly_WithTreeSort()
    {
        var result = _service.SortString("dbca", SortAlgorithmNames.TreeSort);
        Assert.That(result, Is.EqualTo("abcd"));
    }
}
