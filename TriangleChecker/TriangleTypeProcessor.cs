using TriangleChecker.Model;

namespace TriangleChecker;

internal class TriangleTypeProcessor : ITriangleTypeProcessor
{
    public TriangleType Process(double sideA, double sideB, double sideC)
    {
        var triangle = new Triangle
        {
            SideA = sideA,
            SideB = sideB,
            SideC = sideC
        };

        return Process(triangle);
    }

    public TriangleType Process(Triangle triangle)
    {
        throw new NotImplementedException();
    }
}
