namespace WordOccurrenceCounter.WordsProvider;

public interface IWordsProviderFactory
{
    IWordsProvider CreateFileProvider(string fileName);
}