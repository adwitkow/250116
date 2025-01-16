using Moq;
using TriangleChecker.Model;
using TriangleChecker.Validation;

namespace TriangleChecker.Tests;

public class TriangleProcessorTests
{
    private TriangleTypeProcessor _processor;

    private Mock<ITriangleValidator> _validatorMock;

    [SetUp]
    public void SetUp()
    {
        _validatorMock = new Mock<ITriangleValidator>();
        _processor = new TriangleTypeProcessor(_validatorMock.Object);
    }

    [Test]
    public void Process_InvalidInput_ThrowsException()
    {
        _validatorMock.Setup(validator => validator.Validate(It.IsAny<Triangle>()))
            .Returns(false);

        Assert.Throws<ArgumentException>(() => _processor.Process(It.IsAny<Triangle>()));
    }

    [TestCase(3, 3, 3, ExpectedResult = TriangleType.Equilateral)]
    [TestCase(3, 4, 4, ExpectedResult = TriangleType.Isosceles)]
    [TestCase(3, 4, 5, ExpectedResult = TriangleType.Scalene)]
    public TriangleType Process_ValidInput_ReturnsCorrectType(double sideA, double sideB, double sideC)
    {
        _validatorMock.Setup(validator => validator.Validate(It.IsAny<Triangle>()))
            .Returns(true);

        var triangle = new Triangle
        {
            SideA = sideA,
            SideB = sideB,
            SideC = sideC
        };

        var result = _processor.Process(triangle);

        return result;
    }
}
