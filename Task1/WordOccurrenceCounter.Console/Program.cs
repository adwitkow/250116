using Cocona;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WordOccurrenceCounter;
using WordOccurrenceCounter.WordsProvider;

var builder = CoconaApp.CreateBuilder();

builder.Services.AddTransient<IWordCounter, WordCounter>();
builder.Services.AddTransient<WordsCollector>();
builder.Services.AddScoped<IWordsProviderFactory, WordsProviderFactory>();

var app = builder.Build();
app.Run((WordsCollector collector,
    ILogger<Program> logger, [Option('f')]string[] files) =>
{
    // TODO: Could use some validation, at least check the file extensions?
    logger.LogInformation("Counting word occurrences within files:\n{Files}", string.Join('\n', files));

    try
    {
        var counts = collector.CollectWordsFromFiles(files);

        var ordered = counts.OrderByDescending(pair => pair.Value)
            .Select(pair => $"{pair.Value}: {pair.Key}");
        var formatted = string.Join('\n', ordered);

        logger.LogInformation("Word counts found in provided files:\n{Counts}", formatted);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An exception has occurred when trying to count the word occurrences within files: {Files}", string.Join(", ", files));
    }
});