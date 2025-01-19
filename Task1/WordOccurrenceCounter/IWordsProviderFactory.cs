namespace WordOccurrenceCounter;

public interface IWordsProviderFactory
{
    IWordsProvider CreateFileProvider(string fileName);
}