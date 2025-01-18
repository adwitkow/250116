using System.Collections.Concurrent;

namespace WordOccurrenceCounter;

public class WordCounter : IWordCounter
{
    private const int InitialValue = 1;

    private readonly ConcurrentDictionary<string, int> _wordCounts;

    public WordCounter()
    {
        _wordCounts = new ConcurrentDictionary<string, int>();
    }

    public void AddToCount(string word)
    {
        _wordCounts.AddOrUpdate(word, _ => InitialValue, (_, count) => count + 1);
    }

    public int GetCount(string word)
    {
        return _wordCounts.TryGetValue(word, out var count) ? count : 0;
    }

    public IReadOnlyDictionary<string, int> GetCounts()
    {
        return _wordCounts;
    }
}
