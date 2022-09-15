using FluentAssertions;
using Alaveri.Globalization.Development;

namespace Alaveri.Globalization.Test;

[TestClass]
public class Tests
{
    [TestMethod]
    public void Setup()
    {
    }

    [TestMethod]
    public void TranslateTest()
    {
        var translation = DevTranslator.Current.Translate("TestTranslationPhrase");
        translation.Should().Be("Translated Phrase");
    }
}