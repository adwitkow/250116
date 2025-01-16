using TriangleChecker.Model;

namespace TriangleChecker.Validation;

public interface ITriangleValidator
{
    public bool Validate(Triangle triangle);
}
