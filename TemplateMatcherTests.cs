using System.Text.RegularExpressions;
using Xunit;

public class TemplateMatcherTests
{
    [Fact]
    public void MatchTemplate_ReturnsCorrectTemplate()
    {
        // Arrange
        var templateMatcher = new TemplateMatcher();
        var template1 = new Template1();
        var template2 = new Template2();

        templateMatcher.AddTemplate(new Regex(@"HSBC.*Credit Card Transaction.*Notification", RegexOptions.IgnoreCase), template1);
        templateMatcher.AddTemplate(new Regex(@"HSBC.*Personal Credit Card.*eAlert", RegexOptions.IgnoreCase), template2);

        // Act
        var result1 = templateMatcher.MatchTemplate("HSBC Credit Card Transaction Notification");
        var result2 = templateMatcher.MatchTemplate("HSBC Personal Credit Card eAlert");

        // Assert
        Assert.Equal(template1, result1);
        Assert.Equal(template2, result2);
    }

    [Fact]
    public void MatchTemplate_ReturnsNullIfNoMatch()
    {
        // Arrange
        var templateMatcher = new TemplateMatcher();
        var template1 = new Template1();

        templateMatcher.AddTemplate(new Regex(@"HSBC.*Credit Card Transaction.*Notification", RegexOptions.IgnoreCase), template1);

        // Act
        var result = templateMatcher.MatchTemplate("Non-Matching Subject");

        // Assert
        Assert.Null(result);
    }
}