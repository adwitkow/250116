using TriangleChecker.Model;

namespace TriangleChecker;

public interface ITriangleTypeProcessor
{
    public TriangleType DetermineTriangleType(double sideA, double sideB, double sideC);

    public TriangleType DetermineTriangleType(Triangle triangle);
}
