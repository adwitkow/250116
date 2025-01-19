namespace WordOccurrenceCounter.WordsProvider;

public class WordsProviderFactory : IWordsProviderFactory
{
    public IWordsProvider CreateFileProvider(string fileName)
    {
        return new FileWordsProvider(fileName);
    }
}
