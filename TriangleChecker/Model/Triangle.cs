using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriangleChecker.Model;

public struct Triangle
{
    public required double SideA { get; init; }

    public required double SideB { get; init; }

    public required double SideC { get; init; }
}
