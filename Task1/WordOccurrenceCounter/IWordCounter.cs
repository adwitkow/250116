namespace WordOccurrenceCounter;

public interface IWordCounter
{
    void AddToCount(string word);

    int GetCount(string word);

    IReadOnlyDictionary<string, int> GetCounts();
}