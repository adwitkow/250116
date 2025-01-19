namespace WordOccurrenceCounter.IO;

public class WordsReader : IDisposable
{
    private const int DefaultBufferSize = 8192; // 8 KB feels pretty good

    private readonly StreamReader _reader;
    private readonly char[] _buffer;
    private string? _leftover;

    private bool disposedValue;

    public WordsReader(string path) : this(path, DefaultBufferSize)
    {
    }

    public WordsReader(string path, int bufferSize)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("File path cannot be null or empty.", nameof(path));
        }

        if (bufferSize <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(bufferSize), "Buffer size must be greater than zero.");
        }

        _reader = new StreamReader(path);
        _buffer = new char[bufferSize];
        _leftover = null;
    }

    public IEnumerable<string> ReadWords()
    {
        int charsRead;

        while ((charsRead = _reader.Read(_buffer, 0, _buffer.Length)) > 0)
        {
            int wordStartIndex = 0;

            for (int i = 0; i < charsRead; i++)
            {
                if (char.IsLetter(_buffer[i]))
                {
                    continue;
                }

                if (i > wordStartIndex)
                {
                    string word = ExtractWord(wordStartIndex, i);
                    yield return word;
                }
                else if (_leftover is not null)
                {
                    yield return _leftover;
                    _leftover = null;
                }

                wordStartIndex = i + 1;
            }

            if (wordStartIndex < charsRead)
            {
                _leftover += new string(_buffer, wordStartIndex, charsRead - wordStartIndex);
            }
        }

        if (!string.IsNullOrEmpty(_leftover))
        {
            yield return _leftover;
            _leftover = null;
        }
    }

    private string ExtractWord(int startIndex, int endIndex)
    {
        string word = _leftover + new string(_buffer, startIndex, endIndex - startIndex);
        _leftover = null;

        return word;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _reader?.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~WordReader()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}