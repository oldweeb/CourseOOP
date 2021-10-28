using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;

namespace CourseOOP.Models
{
    public class RightTriangle : Triangle
    {
        public double Hypotenuse 
        {
            get
            {
                double ab = AB;
                double ac = AC;
                double bc = BC;
                return ab > ac && ab > bc ? ab : ac > ab && ac > bc ? ac : bc;
            }
        }
        public (double, double) Legs
        {
            get
            {
                List<double> legs = new() { AB, BC, AC };
                _ = legs.Remove(Hypotenuse);
                return (legs[0], legs[1]);
            }
        }
        /// <summary>
        /// Default RightTriangle constructor
        /// Initializes points with values: A = (0, 0); B = (0, 2); C = (4, 0)
        /// </summary>
        public RightTriangle()
        {
            _a = new Point(0, 0);
            _b = new Point(0, 2);
            _c = new Point(4, 0);
        }
        /// <summary>
        /// RightTriangle constructor with parameters
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public RightTriangle(Point a, Point b, Point c) : base(a, b, c)
        {
            if (!IsRightTriangle(a, b, c))
            {
                throw new ArgumentException("This is not a right triangle.");
            }
        }
        /// <summary>
        /// RightTriangle copy  constructor
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public RightTriangle(RightTriangle triangle) : base(triangle) { }

        
        public override double GetArea()
        {
            (double, double) legs = Legs;
            return legs.Item1 * legs.Item2 / 2;
        }
        public static bool IsRightTriangle(Point a, Point b, Point c)
        {
            if (!IsTriangle(a, b, c))
            {
                return false;
            }

            Triangle triangle = new(a, b, c);
            double ab = triangle.AB;
            double ac = triangle.AC;
            double bc = triangle.BC;
            double hypotenuse;
            (double, double) legs;
            if (ab > ac && ab > bc)
            {
                hypotenuse = ab;
                legs.Item1 = ac;
                legs.Item2 = bc;
            }
            else if (ac > ab && ac > bc)
            {
                hypotenuse = ac;
                legs.Item1 = ab;
                legs.Item2 = bc;
            }
            else
            {
                hypotenuse = bc;
                legs.Item1 = ab;
                legs.Item2 = ac;
            }
            
            return Math.Abs(Math.Pow(hypotenuse, 2) - Math.Pow(legs.Item1, 2) - Math.Pow(legs.Item2, 2)) <= 1e-8;
        }
        public new static RightTriangle Parse(string s)
        {
            if (!Regex.IsMatch(s, @"^\(-?\d+\.?\d*,\s*-?\d+\.?\d*\) \(-?\d+\.?\d*,\s*-?\d+\.?\d*\) \(-?\d+\.?\d*,\s*-?\d+\.?\d*\)"))
            {
                throw new FormatException("String does not suit the format.");
            }

            MatchCollection mPoints = Regex.Matches(s, @"\d+\.?\d*,\s*\d+\.?\d*");
            List<Point> points = new();
            foreach (Match point in mPoints)
            {
                points.Add(Point.Parse(point.Value));
            }
            return new RightTriangle(points[0], points[1], points[2]);
        }

        public static bool TryParse(string s, out RightTriangle triangle)
        {
            try
            {
                triangle = RightTriangle.Parse(s);
            }
            catch (FormatException)
            {
                triangle = new RightTriangle();
                return false;
            }
            catch (ArgumentException)
            {
                triangle = new RightTriangle();
                return false;
            }

            return true;
        }
    }
}
