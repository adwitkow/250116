# 1. WordOccurrenceCounter
```
Usage: WordOccurrenceCounter.Console [--files <String>...] [--help] [--version]

E.g: WordOccurrenceCounter.Console -f ..\..\..\..\WordOccurrenceCounter.IntegrationTests\TestData\Oneliner1.txt -f ..\..\..\..\WordOccurrenceCounter.IntegrationTests\TestData\Oneliner2.txt

WordOccurrenceCounter.Console

Options:
  -f, --files <String>...     (Required)
  -h, --help                 Show help message
  --version                  Show version
```

## File reading benchmarks

In most cases: reading files line by line, then splitting on non-word regex to gather all the words.

In the case of WordsReader reading is done word by word, so no additional splits are necessary.

Test files are generated using a constant seed, each line consists of 10-10k words of lorem ipsum (ish)
* Small: 100 lines (3.24 MB)
* Large: 50k lines (1.57 GB)

```
BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5131/22H2/2022Update)
12th Gen Intel Core i5-12600K, 1 CPU, 16 logical and 10 physical cores
.NET SDK 9.0.101
  [Host]     : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2
```

| Method         | FileSize | Mean         | Error      | StdDev     | Gen0         | Gen1         | Gen2         | Allocated   |
|--------------- |--------- |-------------:|-----------:|-----------:|-------------:|-------------:|-------------:|------------:|
| ReadAllLines   | large    | 19,307.65 ms | 145.925 ms | 136.499 ms | 1657000.0000 |  623000.0000 |   11000.0000 | 19540.61 MB |
| StreamReader   | large    | 17,263.46 ms | 146.790 ms | 137.308 ms | 2105000.0000 | 1428000.0000 | 1005000.0000 | 19539.92 MB |
| BufferedStream | large    | 17,048.05 ms | 137.324 ms | 128.453 ms | 2097000.0000 | 1422000.0000 | 1005000.0000 | 19539.92 MB |
| WordsReader    | large    |  7,145.76 ms |  67.731 ms |  63.356 ms |  876000.0000 |            - |            - |  8739.05 MB |
| ReadAllLines   | small    |     41.50 ms |   0.803 ms |   1.073 ms |    4166.6667 |    2833.3333 |    1000.0000 |    39.17 MB |
| StreamReader   | small    |     37.17 ms |   0.698 ms |   0.653 ms |    4428.5714 |    2500.0000 |    2214.2857 |    39.16 MB |
| BufferedStream | small    |     36.16 ms |   0.722 ms |   0.859 ms |    4428.5714 |    2500.0000 |    2214.2857 |    39.17 MB |
| WordsReader    | small    |     14.55 ms |   0.247 ms |   0.231 ms |    1765.6250 |      78.1250 |            - |    17.61 MB |

# 2. TriangleChecker
```
Usage: TriangleChecker.Console [--side a <Double>] [--side b <Double>] [--side c <Double>] [--help] [--version]

TriangleChecker.Console

Options:
  -a, -A, -1, --side a <Double>     (Required)
  -b, -B, -2, --side b <Double>     (Required)
  -c, -C, -3, --side c <Double>     (Required)
  -h, --help                       Show help message
  --version                        Show version
```