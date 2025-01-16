using Cocona;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TriangleChecker;
using TriangleChecker.Model;
using TriangleChecker.Validation;

var builder = CoconaApp.CreateBuilder();

builder.Services.AddTransient<ITriangleValidator, TriangleValidator>();
builder.Services.AddTransient<ITriangleTypeProcessor, TriangleTypeProcessor>();

var app = builder.Build();
app.Run((ITriangleTypeProcessor typeProcessor,
    ILogger<Program> logger,
    [Option("Side A", ['a', 'A', '1'])] double sideA,
    [Option("Side B", ['b', 'B', '2'])] double sideB,
    [Option("Side C", ['c', 'C', '3'])] double sideC) =>
{
    logger.LogInformation("Determining the triangle type from provided sides: [{SideA}, {SideB}, {SideC}]", sideA, sideB, sideC);

    TriangleType result;
    try
    {
        result = typeProcessor.DetermineTriangleType(sideA, sideB, sideC);
        logger.LogInformation("The provided triangle is '{Type}'!", result);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An exception has occurred when trying to determine the triangle type of [{SideA}, {SideB}, {SideC}]", sideA, sideB, sideC);
    }
});