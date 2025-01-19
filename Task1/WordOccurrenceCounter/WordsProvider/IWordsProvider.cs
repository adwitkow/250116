namespace WordOccurrenceCounter.WordsProvider;

public interface IWordsProvider
{
    IEnumerable<string> GetWords();
}
