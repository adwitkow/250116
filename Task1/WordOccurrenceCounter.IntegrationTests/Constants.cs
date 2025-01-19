namespace WordOccurrenceCounter.IntegrationTests;

public static class Constants
{
    public const int Seed = 42;
    private const string TestDataDirectory = "TestData";

    public static readonly string Oneliner1Path = Path.Combine(TestDataDirectory, "Oneliner1.txt");
    public static readonly string Oneliner2Path = Path.Combine(TestDataDirectory, "Oneliner2.txt");
    public static readonly string OnelinerSpecialPath = Path.Combine(TestDataDirectory, "OnelinerSpecial.txt");
    public static readonly string LongOnelinerPath = Path.Combine(TestDataDirectory, "LongOneliner.txt");
    public static readonly string MultipleLinesPath = Path.Combine(TestDataDirectory, "MultipleLines.txt");
}
