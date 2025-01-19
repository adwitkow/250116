namespace WordOccurrenceCounter;

public interface IWordsProvider
{
    IEnumerable<string> GetWords();
}
