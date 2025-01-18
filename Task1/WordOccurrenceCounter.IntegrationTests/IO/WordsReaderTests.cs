using Bogus;
using WordOccurrenceCounter.IO;

namespace WordOccurrenceCounter.IntegrationTests.IO;

[TestFixture]
[Category("Integration")]
public class WordsReaderTests
{
    private const int Seed = 42;
    private const string TestDataDirectory = "TestData";

    private static readonly string Oneliner1Path = Path.Combine(TestDataDirectory, "Oneliner1.txt");
    private static readonly string Oneliner2Path = Path.Combine(TestDataDirectory, "Oneliner2.txt");
    private static readonly string OnelinerSpecialPath = Path.Combine(TestDataDirectory, "OnelinerSpecial.txt");
    private static readonly string LongOnelinerPath = Path.Combine(TestDataDirectory, "LongOneliner.txt");
    private static readonly string MultipleLinesPath = Path.Combine(TestDataDirectory, "MultipleLines.txt");

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var faker = new Faker();
        faker.Random = new Randomizer(Seed);

        // 10 million words of lorem ipsum,
        // beginning with a capital letter and ending with a dot.
        var sentence = faker.Lorem.Sentence(10_000_000);
        File.WriteAllText(LongOnelinerPath, sentence);

        // 1k short sentences, each in its own line,
        // formatted in the same manner as above
        var sentences = faker.Lorem.Sentences(1_000);
        File.WriteAllText(MultipleLinesPath, sentences);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        if (File.Exists(LongOnelinerPath))
        {
            File.Delete(LongOnelinerPath);
        }

        if (File.Exists(MultipleLinesPath))
        {
            File.Delete(MultipleLinesPath);
        }
    }

    [Test]
    public void ReadWords_OneLiner1()
    {
        using var reader = new WordsReader(Oneliner1Path);
        
        var words = reader.ReadWords().ToArray();

        var expected = new[] { "Go", "do", "that", "thing", "that", "you", "do", "so", "well" };
        Assert.That(words, Is.EquivalentTo(expected));
    }

    [Test]
    public void ReadWords_OneLiner2()
    {
        using var reader = new WordsReader(Oneliner2Path);

        var words = reader.ReadWords().ToArray();

        var expected = new[] { "I", "play", "football", "well" };
        Assert.That(words, Is.EquivalentTo(expected));
    }

    [Test]
    public void ReadWords_OneLinerWithSpecialCharacters()
    {
        using var reader = new WordsReader(OnelinerSpecialPath);

        var words = reader.ReadWords().ToArray();

        var expected = new[] { "Lorem", "ipsum", "dolor", "sit", "amet" };
        Assert.That(words, Is.EquivalentTo(expected));
    }

    [Test]
    public void ReadWords_LongOneliner()
    {
        using var reader = new WordsReader(LongOnelinerPath);

        var words = reader.ReadWords().ToArray();

        int expectedCount = 10_000_000;
        Assert.Multiple(() =>
        {
            Assert.That(words[0], Is.EqualTo("Libero"));
            Assert.That(words[^1], Is.EqualTo("voluptatem"));
            Assert.That(words, Has.Length.EqualTo(expectedCount));
        });
    }

    [Test]
    public void ReadWords_MultipleLines()
    {
        using var reader = new WordsReader(MultipleLinesPath);

        var words = reader.ReadWords().ToArray();

        Assert.Multiple(() =>
        {
            Assert.That(words[0], Is.EquivalentTo("Soluta"));
            Assert.That(words[1000], Is.EquivalentTo("Animi"));
            Assert.That(words[3000], Is.EquivalentTo("ut"));
            Assert.That(words[6000], Is.EquivalentTo("vel"));
            Assert.That(words[^1], Is.EquivalentTo("porro"));
            Assert.That(words, Has.Length.EqualTo(6402));
        });
    }
}
