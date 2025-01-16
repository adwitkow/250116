using TriangleChecker.Model;
using TriangleChecker.Validation;

namespace TriangleChecker.Tests.Validation;

public class TriangleValidatorTests
{
    private TriangleValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _validator = new TriangleValidator();
    }

    [TestCase(3, 3, 3)]
    [TestCase(3, 4, 4)]
    [TestCase(3, 4, 5)]
    [TestCase(1.5, 2.5, 3)]
    [TestCase(0.1, 0.1, 0.1)]
    [TestCase(1, 1, 1.999999999)]
    public void Validate_ValidTriangle_ReturnsTrue(double sideA, double sideB, double sideC)
    {
        var triangle = new Triangle
        {
            SideA = sideA,
            SideB = sideB,
            SideC = sideC
        };

        var result = _validator.Validate(triangle);

        Assert.IsTrue(result);
    }

    [TestCase(0, 3, 4)]
    [TestCase(-3, 4, 5)]
    [TestCase(1, 2, 3)]
    [TestCase(1, 10, 12)]
    public void Validate_ValidTriangle_ReturnsFalse(double sideA, double sideB, double sideC)
    {
        var triangle = new Triangle
        {
            SideA = sideA,
            SideB = sideB,
            SideC = sideC
        };

        var result = _validator.Validate(triangle);

        Assert.IsFalse(result);
    }
}
