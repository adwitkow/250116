namespace WordOccurrenceCounter;

public class WordsCollector
{
    private readonly IWordCounter _wordCounter;
    private readonly IWordsProviderFactory _wordsProviderFactory;

    public WordsCollector(IWordCounter wordCounter, IWordsProviderFactory wordsProviderFactory)
    {
        _wordCounter = wordCounter;
        _wordsProviderFactory = wordsProviderFactory;
    }

    public IReadOnlyDictionary<string, int> CollectWordsFromFiles(IEnumerable<string> fileNames)
    {
        Parallel.ForEach(fileNames, fileName =>
        {
            var provider = _wordsProviderFactory.CreateFileProvider(fileName);

            foreach (var word in provider.GetWords())
            {
                _wordCounter.AddToCount(word);
            }
        });

        return _wordCounter.GetCounts();
    }
}
