namespace WordOccurrenceCounter.Tests;

public class WordCounterTests
{
    [Test]
    public void AddToCount_Parallel_ShouldCountCorrectly()
    {
        var wordCounter = new WordCounter();
        var words = new[] { "test", "example", "parallel", "test", "example", "test" };
        int iterations = 1000;

        Parallel.For(0, iterations, i =>
        {
            foreach (var word in words)
            {
                wordCounter.AddToCount(word);
            }
        });

        Assert.Multiple(() =>
        {
            Assert.That(wordCounter.GetCount("test"), Is.EqualTo(iterations * 3));
            Assert.That(wordCounter.GetCount("example"), Is.EqualTo(iterations * 2));
            Assert.That(wordCounter.GetCount("parallel"), Is.EqualTo(iterations * 1));
        });
    }
}
