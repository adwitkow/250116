namespace WordOccurrenceCounter;

public class WordsProviderFactory : IWordsProviderFactory
{
    public IWordsProvider CreateFileProvider(string fileName)
    {
        return new FileWordsProvider(fileName);
    }
}
