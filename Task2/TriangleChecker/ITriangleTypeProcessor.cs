using TriangleChecker.Model;

namespace TriangleChecker;

public interface ITriangleTypeProcessor
{
    public TriangleType Process(double sideA, double sideB, double sideC);

    public TriangleType Process(Triangle triangle);
}
