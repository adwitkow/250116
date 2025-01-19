using WordOccurrenceCounter.WordsProvider;

namespace WordOccurrenceCounter.Tests;

public class WordsProviderFactoryTests
{
    [Test]
    public void CreateFileProvider_ReturnsFileWordsProvider()
    {
        const string fileName = "Test";
        var factory = new WordsProviderFactory();

        var provider = factory.CreateFileProvider(fileName);
        var wordsProvider = provider as FileWordsProvider;

        Assert.That(wordsProvider, Is.Not.Null);
        Assert.That(wordsProvider.FileName, Is.EqualTo(fileName));
    }
}
