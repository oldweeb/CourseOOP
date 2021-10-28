using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;

namespace CourseOOP.Models
{
    public class EquilateralTriangle : Triangle
    {
        /// <summary>
        /// EquilateralTriangle default constructor.
        /// Initializes points with values: A = (0, 0); B = (3, 0); C = (1.5, 2.5)
        /// </summary>
        public EquilateralTriangle()
        {
            _a = new Point(0, 0);
            _b = new Point(3, 0);
            _c = new Point(1.5, 2.5);
        }
        /// <summary>
        /// EquilateralTriangle copy constructor
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public EquilateralTriangle(EquilateralTriangle triangle) : base(triangle) { }

        /// <summary>
        /// EquilateralTriangle constructor with parameters
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
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
        public new static EquilateralTriangle Parse(string s)
        {
            if (!Regex.IsMatch(s, @"^\(-?\d+\.?\d*,\s*-?\d+\.?\d*\) \(-?\d+\.?\d*,\s*-?\d+\.?\d*\) \(-?\d+\.?\d*,\s*-?\d+\.?\d*\)"))
            {
                throw new FormatException("String does not suit the format.");
            }

            MatchCollection mPoints = Regex.Matches(s, @"-?\d+\.?\d*,\s*-?\d+\.?\d*");
            List<Point> points = new();
            foreach (Match point in mPoints)
            {
                points.Add(Point.Parse(point.Value));
            }
            return new EquilateralTriangle(points[0], points[1], points[2]);
        }

        public static bool TryParse(string s, out EquilateralTriangle triangle)
        {
            try
            {
                triangle = EquilateralTriangle.Parse(s);
            }
            catch (FormatException)
            {
                triangle = new EquilateralTriangle();
                return false;
            }
            catch (ArgumentException)
            {
                triangle = new EquilateralTriangle();
                return false;
            }

            return true;
        }
    }
}
