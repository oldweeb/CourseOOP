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
    public class Quadrangle : IShape
    {
        protected Point _a = new(0, 0);
        protected Point _b = new(0, 1);
        protected Point _c = new(1, 1);
        protected Point _d = new(1, 0);

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

        public Point D
        {
            get => _d;
            set => _d = value;
        }
        [JsonIgnore]
        public double AB => Math.Sqrt(Math.Pow(_b.X - _a.X, 2) + Math.Pow(_b.Y - _a.Y, 2));
        [JsonIgnore]
        public double AD => Math.Sqrt(Math.Pow(_b.X - _a.X, 2) + Math.Pow(_b.Y - _a.Y, 2));
        [JsonIgnore]
        public double BC => Math.Sqrt(Math.Pow(_c.X - _b.X, 2) + Math.Pow(_c.Y - _b.Y, 2));
        [JsonIgnore]
        public double CD => Math.Sqrt(Math.Pow(_d.X - _c.X, 2) + Math.Pow(_d.Y - _c.Y, 2));
        [JsonIgnore]

        public double AngleA
        {
            get
            {
                Vector ab = new(_b.X - _a.X, _b.Y - _a.Y);
                Vector ad = new(_d.X - _a.X, _d.Y - _a.Y);
                return Math.Abs(Vector.AngleBetween(ab, ad));
            }
        }
        [JsonIgnore]
        public double AngleB
        {
            get
            {
                Vector ba = new(_a.X - _b.X, _a.Y - _b.Y);
                Vector bc = new(_c.X - _b.X, _c.Y - _b.Y);
                return Math.Abs(Vector.AngleBetween(ba, bc));
            }
        }
        [JsonIgnore]
        public double AngleC
        {
            get
            {
                Vector cb = new(_b.X - _c.X, _b.Y - _c.Y);
                Vector cd = new(_d.X - _c.X, _d.Y - _c.Y);
                return Math.Abs(Vector.AngleBetween(cb, cd));
            }
        }
        [JsonIgnore]
        public double AngleD
        {
            get
            {
                Vector da = new(_a.X - _d.X, _a.Y - _d.Y);
                Vector dc = new(_c.X - _d.X, _c.Y - _d.Y);
                return Math.Abs(Vector.AngleBetween(da, dc));
            }
        }
        [JsonIgnore]
        public (Point, Point) AC => (A, C);
        [JsonIgnore]
        public (Point, Point) BD => (B, D);
        [JsonProperty("Type")] public string ShapeType => this.GetType().Name;

        public Quadrangle()
        {
            _a = new Point(0, 0);
            _b = new Point(0, 1);
            _c = new Point(1, 1);
            _d = new Point(1, 0);
        }

        public Quadrangle(Point a, Point b, Point c, Point d)
        {
            if (!IsQuadrangle(a, b, c, d))
            {
                throw new ArgumentException("Such quadrangle does not exist.");
            }

            _a = new Point(a.X, a.Y);
            _b = new Point(b.X, b.Y);
            _c = new Point(c.X, c.Y);
            _d = new Point(d.X, d.Y);
        }

        public Quadrangle(Quadrangle quadrangle) : this(quadrangle.A, quadrangle.B, quadrangle.C, quadrangle.D) { }

        public virtual double GetArea()
        {
            double ac = Math.Sqrt(Math.Pow(AC.Item2.X - AC.Item1.X, 2) + Math.Pow(AC.Item2.Y - AC.Item1.Y, 2));
            double bd = Math.Sqrt(Math.Pow(BD.Item2.X - BD.Item1.X, 2) + Math.Pow(BD.Item2.Y - BD.Item1.Y, 2));
            Vector acVector = new(_c.X - _a.X, _c.Y - _a.Y);
            Vector bdVector = new(_d.X - _b.X, _d.Y - _b.Y);
            double alpha = Vector.AngleBetween(acVector, bdVector);
            alpha = alpha > 90.0 ? 180.0 - alpha : alpha;
            double area = ac * bd * Math.Sin(alpha) / 2;
            return area;
        }

        public virtual double GetPerimeter() => AB + AD + BC + CD;

        public static bool IsQuadrangle(Point a, Point b, Point c, Point d)
        {
            Vector ab = new(b.X - a.X, b.Y - a.Y);
            Vector ad = new(d.X - a.X, d.Y - a.Y);
            double angleA = Math.Abs(Vector.AngleBetween(ab, ad));
            ab.Negate(); // got ba vector
            Vector bc = new(c.X - b.X, c.Y - b.Y);
            double angleB = Math.Abs(Vector.AngleBetween(ab, bc));
            bc.Negate(); // got cb vector
            Vector cd = new(d.X - c.X, d.Y - c.Y);
            double angleC = Math.Abs(Vector.AngleBetween(bc, cd));
            cd.Negate(); // got dc vector
            ad.Negate(); // got da vector
            double angleD = Math.Abs(Vector.AngleBetween(cd, ad));
            double angleSum = angleA + angleB + angleC + angleD;
            if (Math.Abs(360.0 - angleSum) >= 1e-8)
            {
                return false;
            }
            return true;
        }

        public static Quadrangle Parse(string s)
        {
            if (!Regex.IsMatch(s, @"^\(\d+\.?\d*,\d+\.?\d*\) \(\d+\.?\d*,\d+\.?\d*\) \(\d+\.?\d*,\d+\.?\d*\) \(\d+\.?\d*,\d+\.?\d*\)"))
            {
                throw new FormatException("String does not suit the format.");
            }

            MatchCollection mPoints = Regex.Matches(s, @"\d+\.?\d*,\d+\.?\d*");
            List<Point> points = new();
            foreach (Match point in mPoints)
            {
                points.Add(Point.Parse(point.Value));
            }

            return new Quadrangle(points[0], points[1], points[2], points[3]);
        }

        public static bool TryParse(string s, out Quadrangle quadrangle)
        {
            try
            {
                quadrangle = Quadrangle.Parse(s);
            }
            catch (Exception)
            {
                quadrangle = new();
                return false;
            }
            return true;
        }
    }
}
