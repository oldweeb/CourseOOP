using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourseOOP.Models
{
    public class IsoscelesTriangle : Triangle
    {
        public IsoscelesTriangle()
        {
            _a = new Point(0, 0);
            _b = new Point(1.5, 1);
            _c = new Point(3, 0);
        }

        public IsoscelesTriangle(Point a, Point b, Point c) : base(a, b, c)
        {
            if (!IsIsoscelesTriangle(a, b, c))
            {
                throw new ArgumentException("This is not isosceles triangle.");
            }
        }

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
            if (IsTriangle(a, b, c))
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
    }
}
