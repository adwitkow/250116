using Cocona;
using Microsoft.Extensions.DependencyInjection;
using TriangleChecker;
using TriangleChecker.Validation;

var builder = CoconaApp.CreateBuilder();

builder.Services.AddTransient<ITriangleValidator, TriangleValidator>();
builder.Services.AddTransient<ITriangleTypeProcessor, TriangleTypeProcessor>();

var app = builder.Build();
app.Run((ITriangleTypeProcessor typeProcessor,
    [Option("Side A", ['a', 'A', '1'])] double sideA,
    [Option("Side B", ['b', 'B', '2'])] double sideB,
    [Option("Side C", ['c', 'C', '3'])] double sideC) =>
{
    var result = typeProcessor.Process(sideA, sideB, sideC);
    
    Console.WriteLine(result);
});