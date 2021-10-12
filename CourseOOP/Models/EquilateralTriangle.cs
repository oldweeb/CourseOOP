using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourseOOP.Models
{
    public class EquilateralTriangle : Triangle
    {
        public EquilateralTriangle()
        {
            _a = new Point(0, 0);
            _b = new Point(3, 0);
            _c = new Point(1.5, 2.5);
        }

        public EquilateralTriangle(EquilateralTriangle triangle) : base(triangle) { }

        public EquilateralTriangle(Point a, Point b, Point c) : base(a, b, c)
        {
            if (!IsEquilateralTriangle(a, b, c))
            {
                throw new ArgumentException("This is not equilateral triangle.");
            }
        }
        public override double GetArea() => Math.Pow(AB, 2) * Math.Sqrt(3) / 4;

        public static bool IsEquilateralTriangle(Point a, Point b, Point c)
        {
            if (!IsTriangle(a, b, c))
            {
                return false;
            }

            Triangle triangle = new(a, b, c);
            return Math.Abs(triangle.AB - triangle.BC) <= 1e-8 && Math.Abs(triangle.BC - triangle.AC) <= 1e-8;
        }
    }
}
