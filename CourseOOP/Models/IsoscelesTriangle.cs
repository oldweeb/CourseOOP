using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;

namespace CourseOOP.Models
{
    public class IsoscelesTriangle : Triangle
    {
        /// <summary>
        /// Default IsoscelesTriangle.
        /// Initializes points with values: A = (0, 0); B = (1.5, 1); C = (3, 0)
        /// </summary>
        public IsoscelesTriangle()
        {
            _a = new Point(0, 0);
            _b = new Point(1.5, 1);
            _c = new Point(3, 0);
        }

        /// <summary>
        /// IsoscelesTriangle constructor with parameters.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public IsoscelesTriangle(Point a, Point b, Point c) : base(a, b, c)
        {
            if (!IsIsoscelesTriangle(a, b, c))
            {
                throw new ArgumentException("This is not isosceles triangle.");
            }
        }

        /// <summary>
        /// IsoscelesTriangle copy constructor.
        /// </summary>
        /// <param name="triangle"></param>
        /// <exception cref="ArgumentException"></exception>
        public IsoscelesTriangle(IsoscelesTriangle triangle) : base(triangle) { }
        public override double GetArea()
        {
            double ab = AB, ac = AC, bc = BC;
            (Point, Point) basis;
            Tuple<(Point, Point), (Point, Point)> sides;
            if (Math.Abs(ab - ac) >= 1e-8)
            {
                basis.Item1 = new Point(B.X, B.Y);
                basis.Item2 = new Point(C.X, C.Y);
                sides = new((new Point(A.X, A.Y), new Point(B.X, B.Y)), (new Point(A.X, A.Y), new Point(C.X, C.Y)));
            }
            else if (Math.Abs(ab - bc) >= 1e-8)
            {
                basis.Item1 = new Point(A.X, A.Y);
                basis.Item2 = new Point(C.X, C.Y);
                sides = new((new Point(A.X, A.Y), new Point(B.X, B.Y)), (new Point(B.X, B.Y), new Point(C.X, C.Y)));
            }
            else
            {
                basis.Item1 = new Point(A.X, A.Y);
                basis.Item2 = new Point(B.X, B.Y);
                sides = new((new Point(B.X, B.Y), new Point(C.X, C.Y)), (new Point(C.X, C.Y), new Point(A.X, A.Y)));
            }

            Point basisMiddle = new((basis.Item1.X + basis.Item2.X) / 2, (basis.Item1.Y + basis.Item2.Y) / 2);
            Point top = sides.Item2.Item1;

            double h = Math.Sqrt(Math.Pow(basisMiddle.X - top.X, 2) + Math.Pow(basisMiddle.Y - top.Y, 2));
            double basisLength = Math.Sqrt(Math.Pow(basis.Item1.X - basis.Item2.X, 2) +
                                           Math.Pow(basis.Item2.X - basis.Item2.Y, 2));
            return h * basisLength / 2;
        }

        public static bool IsIsoscelesTriangle(Point a, Point b, Point c)
        {
            if (!IsTriangle(a, b, c))
            {
                return false;
            }

            Triangle triangle = new(a, b, c);
            double ab = triangle.AB;
            double ac = triangle.AC;
            double bc = triangle.BC;
            return Math.Abs(ab - bc) >= 1e-8 ||
                   Math.Abs(ab - ac) >= 1e-8 ||
                   Math.Abs(ac - bc) >= 1e-8;
        }
        public new static IsoscelesTriangle Parse(string s)
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
            return new IsoscelesTriangle(points[0], points[1], points[2]);
        }

        public static bool TryParse(string s, out IsoscelesTriangle triangle)
        {
            try
            {
                triangle = IsoscelesTriangle.Parse(s);
            }
            catch (FormatException)
            {
                triangle = new IsoscelesTriangle();
                return false;
            }
            catch (ArgumentException)
            {
                triangle = new IsoscelesTriangle();
                return false;
            }

            return true;
        }
    }
}
