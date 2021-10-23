using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace CourseOOP.Models
{
    public class Triangle : IShape
    {
        protected Point _a;
        protected Point _b;
        protected Point _c;

        public Point A
        {
            get => _a;
            set => _a = value;
        }

        public Point B
        {
            get => _b;
            set => _b = value;
        }

        public Point C
        {
            get => _c;
            set => _c = value;
        }
        public double AB => Math.Sqrt(Math.Pow(_b.X - _a.X, 2) + Math.Pow(_b.Y - _a.Y, 2));
        public double AC => Math.Sqrt(Math.Pow(_c.X - _a.X, 2) + Math.Pow(_c.Y - _a.Y, 2));
        public double BC => Math.Sqrt(Math.Pow(_c.X - _b.X, 2) + Math.Pow(_c.Y - _b.Y, 2));
        public double AngleA
        {
            get
            {
                Vector ab = new(_b.X - _a.X, _b.Y - _a.Y);
                Vector ac = new(_c.X - _a.X, _c.Y - _a.Y);
                return Math.Abs(Vector.AngleBetween(ab, ac));
            }
        }

        public double AngleB
        {
            get
            {
                Vector ba = new(_a.X - _b.X, _a.Y - _b.Y);
                Vector bc = new(_c.X - _b.X, _c.Y - _b.Y);
                return Math.Abs(Vector.AngleBetween(ba, bc));
            }
        }
        public double AngleC
        {
            get
            {
                Vector cb = new(_b.X - _c.X, _b.Y - _c.Y);
                Vector ca = new(_a.X - _c.X, _a.Y - _c.Y);
                return Math.Abs(Vector.AngleBetween(cb, ca));
            }
        }

        [JsonProperty("Type")] public string ShapeType => this.GetType().Name;
        public Triangle()
        {
            _a = new Point(0, 0);
            _b = new Point(1, 1);
            _c = new Point(1, 0);
        }
        public Triangle(Point a, Point b, Point c)
        {
            if (!IsTriangle(a, b, c))
            {
                throw new ArgumentException("Such triangle does not exist.");
            }
            _a = new Point(a.X, a.Y);
            _b = new Point(b.X, b.Y);
            _c = new Point(c.X, c.Y);
        }

        public Triangle(Triangle triangle) : this(triangle.A, triangle.B, triangle.C) { }

        
        public virtual double GetArea()
        {
            double p = GetPerimeter() / 2;
            return Math.Sqrt(p * (p - AB) * (p - AC) * (p - BC));
        }

        public virtual double GetPerimeter() => AB + AC + BC;
        public override string ToString()
        {
            StringBuilder sb = new();
            _ = sb.Append($"A: ({A}); B: ({B}); C: ({C})|");
            _ = sb.Append($"∠A={AngleA:F2}°; ∠B={AngleB:F2}° ∠C={AngleC:F2}°|");
            _ = sb.Append($"AB={AB:F2}; BC={BC:F2}; AC={AC:F2}|");
            _ = sb.Append($"S={GetArea():F2}; P={GetPerimeter():F2}");
            return sb.ToString();
        }

        public static bool IsTriangle(Point a, Point b, Point c)
        {
            double ab = Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
            double ac = Math.Sqrt(Math.Pow(c.X - a.X, 2) + Math.Pow(c.Y - a.Y, 2));
            double bc = Math.Sqrt(Math.Pow(c.X - b.X, 2) + Math.Pow(c.Y - b.Y, 2));
            return ab + ac > bc && ab + bc > ac && ac + bc > ab;
        }

        public static Triangle Parse(string s)
        {
            if (!Regex.IsMatch(s, @"^\(\d+\.?\d*,\d+\.?\d*\) \(\d+\.?\d*,\d+\.?\d*\) \(\d+\.?\d*,\d+\.?\d*\)"))
            {
                throw new FormatException("String does not suit the format.");
            }

            MatchCollection mPoints = Regex.Matches(s, @"\d+\.?\d*,\d+\.?\d*");
            List<Point> points = new();
            foreach (Match point in mPoints)
            {
                points.Add(Point.Parse(point.Value));
            }
            return new Triangle(points[0], points[1], points[2]);
        }

        public static bool TryParse(string s, out Triangle triangle)
        {
            try
            {
                triangle = Triangle.Parse(s);
            }
            catch (FormatException)
            {
                triangle = new Triangle();
                return false;
            }

            return true;
        }
    }
}
