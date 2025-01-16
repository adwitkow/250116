using TriangleChecker.Model;

namespace TriangleChecker.Validation;

public class TriangleValidator : ITriangleValidator
{
    public bool Validate(Triangle triangle)
    {
        return triangle.SideA > 0 && triangle.SideB > 0 && triangle.SideC > 0
            && triangle.SideA + triangle.SideB > triangle.SideC
            && triangle.SideA + triangle.SideC > triangle.SideB
            && triangle.SideB + triangle.SideC > triangle.SideA;
    }
}
