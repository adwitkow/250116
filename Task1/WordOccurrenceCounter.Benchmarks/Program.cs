using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Running;
using Bogus;
using System.Text.RegularExpressions;
using WordOccurrenceCounter.IO;

BenchmarkRunner.Run<Benchmarks>();

[MemoryDiagnoser]
public class Benchmarks
{
    private const string FilePath = "testfile.txt";

    private Consumer _consumer = new Consumer();

    [Params("small", "large")]
    public string FileSize { get; set; }

    private int Size => FileSize == "small"
        ? 100
        : 50_000;

    [GlobalSetup]
    public void Setup()
    {
        var faker = new Faker();
        faker.Random = new Randomizer(42);

        if (File.Exists(FilePath))
        {
            File.Delete(FilePath);
        }

        using var writer = new StreamWriter(FilePath);
        for (int i = 0; i < Size; i++)
        {
            writer.WriteLine(faker.Lorem.Sentence(10, 10_000));
        }
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        if (File.Exists(FilePath))
        {
            File.Delete(FilePath);
        }
    }

    [Benchmark]
    public void ReadAllLines()
    {
        var lines = File.ReadAllLines(FilePath);
        foreach (var line in lines)
        {
            var words = Regex.Split(line, @"\W");
            foreach (var word in words)
            {
                _consumer.Consume(word);
            }
        }
    }

    [Benchmark]
    public void StreamReader()
    {
        using var reader = new StreamReader(FilePath);
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            var words = Regex.Split(line, @"\W");
            foreach (var word in words)
            {
                _consumer.Consume(word);
            }
        }
    }

    [Benchmark]
    public void BufferedStream()
    {
        using var stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read, 8192, FileOptions.SequentialScan);
        using var bufferedStream = new BufferedStream(stream);
        using var reader = new StreamReader(bufferedStream);
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            var words = Regex.Split(line, @"\W");
            foreach (var word in words)
            {
                _consumer.Consume(word);
            }
        }
    }

    [Benchmark]
    public void WordsReader()
    {
        using var reader = new WordsReader(FilePath);
        foreach (var word in reader.ReadWords())
        {
            _consumer.Consume(word);
        }
    }
}