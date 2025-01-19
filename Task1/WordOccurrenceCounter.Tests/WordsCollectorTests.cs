using Moq;
using WordOccurrenceCounter.WordsProvider;

namespace WordOccurrenceCounter.Tests;

[TestFixture]
public class WordsCollectorTests
{
    private const string File1 = "file1.txt";
    private const string File2 = "file2.txt";

    private Mock<IWordCounter> _wordCounterMock;
    private Mock<IWordsProviderFactory> _factoryMock;
    private Mock<IWordsProvider> _providerMock1;
    private Mock<IWordsProvider> _providerMock2;

    private WordsCollector _wordsCollector;

    [SetUp]
    public void SetUp()
    {
        _wordCounterMock = new Mock<IWordCounter>();
        _factoryMock = new Mock<IWordsProviderFactory>();
        _providerMock1 = new Mock<IWordsProvider>();
        _providerMock2 = new Mock<IWordsProvider>();

        _wordsCollector = new WordsCollector(_wordCounterMock.Object, _factoryMock.Object);
    }

    [Test]
    public void CollectWordsFromFiles_CallsFactoryAndWordCounterForEachFile()
    {
        string[] fileNames = [File1, File2];
        _factoryMock.Setup(f => f.CreateFileProvider(File1))
            .Returns(_providerMock1.Object);
        _factoryMock.Setup(f => f.CreateFileProvider(File2))
            .Returns(_providerMock2.Object);
        _providerMock1.Setup(p => p.GetWords())
            .Returns(["word1", "word2"]);
        _providerMock2.Setup(p => p.GetWords())
            .Returns(["word3", "word1"]);

        var result = _wordsCollector.CollectWordsFromFiles(fileNames);

        _providerMock1.Verify(p => p.GetWords(), Times.Once);
        _providerMock2.Verify(p => p.GetWords(), Times.Once);
        _factoryMock.Verify(f => f.CreateFileProvider(It.IsAny<string>()), Times.Exactly(fileNames.Length));
        _wordCounterMock.Verify(c => c.AddToCount(It.IsAny<string>()), Times.Exactly(4));
    }

    [Test]
    public void CollectWordsFromFiles_EmptyInput_DoesNotRunParallelProcess()
    {
        var fileNames = Enumerable.Empty<string>();

        var result = _wordsCollector.CollectWordsFromFiles(fileNames);

        _wordCounterMock.Verify(c => c.AddToCount(It.IsAny<string>()), Times.Never);
        _factoryMock.Verify(f => f.CreateFileProvider(It.IsAny<string>()), Times.Never);
    }

    [Test]
    public void CollectWordsFromFiles_ReturnsCountsFromWordCounter()
    {
        var fileNames = new[] { File1 };
        _factoryMock.Setup(f => f.CreateFileProvider(File1))
            .Returns(_providerMock1.Object);
        _providerMock1.Setup(p => p.GetWords())
            .Returns(["word1", "word2", "word1"]);
        var expectedCounts = new Dictionary<string, int>
        {
            { "word1", 2 },
            { "word2", 1 }
        };

        _wordCounterMock.Setup(c => c.GetCounts()).Returns(expectedCounts);

        var result = _wordsCollector.CollectWordsFromFiles(fileNames);

        Assert.That(result, Is.EqualTo(expectedCounts));
    }
}