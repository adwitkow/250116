using TriangleChecker.Model;
using TriangleChecker.Validation;

namespace TriangleChecker;

public class TriangleTypeProcessor : ITriangleTypeProcessor
{
    private readonly ITriangleValidator _validator;

    public TriangleTypeProcessor(ITriangleValidator validator)
    {
        _validator = validator;
    }

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
        if (!_validator.Validate(triangle))
        {
            throw new ArgumentException("The provided lengths do not form a valid triangle.");
        }

        if (triangle.SideA == triangle.SideB
            && triangle.SideB == triangle.SideC)
        {
            return TriangleType.Equilateral;
        }
        
        if (triangle.SideA == triangle.SideB
            || triangle.SideB == triangle.SideC
            || triangle.SideA == triangle.SideC)
        {
            return TriangleType.Isosceles;
        }

        return TriangleType.Scalene;
    }
}
