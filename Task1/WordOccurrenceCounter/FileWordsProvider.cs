using WordOccurrenceCounter.IO;

namespace WordOccurrenceCounter;

public class FileWordsProvider : IWordsProvider
{
    private readonly string _fileName;

    public FileWordsProvider(string fileName)
    {
        _fileName = fileName;
    }

    public string FileName => _fileName;

    public IEnumerable<string> GetWords()
    {
        using var reader = new WordsReader(_fileName);

        // One more layer of yields is needed to make sure
        // that WordsReader won't be disposed before
        // this object's consumer finishes enumeration
        foreach (var word in reader.ReadWords())
        {
            yield return word;
        }
    }
}
