using WordOccurrenceCounter.WordsProvider;

namespace WordOccurrenceCounter.IntegrationTests;

[TestFixture]
[Category("Integration")]
public class FileWordsProviderTests
{
    [Test]
    public void GetWords_ShouldUseReaderCorrectly()
    {
        var provider = new FileWordsProvider(Constants.Oneliner1Path);

        var words = provider.GetWords().ToArray();

        var expected = new[] { "Go", "do", "that", "thing", "that", "you", "do", "so", "well" };
        Assert.That(words, Is.EquivalentTo(expected));
    }
}
