using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourseOOP.Models
{
    public class Rectangle : Quadrangle
    {
        public override double GetArea() => AB * BC;
        public override double GetPerimeter() => (AB + BC) * 2;

        public Rectangle() : base() { }

        public Rectangle(Point a, Point b, Point c, Point d) : base(a, b, c, d)
        {
            if (!IsRectangle(a, b, c, d))
            {
                throw new ArgumentException("This is not a rectangle.");
            }
        }
        public Rectangle(Rectangle rectangle) : base(rectangle) { }

        public static bool IsRectangle(Point a, Point b, Point c, Point d)
        {
            if (!IsQuadrangle(a, b, c, d))
            {
                return false;
            }

            Quadrangle quadrangle = new(a, b, c, d);
            List<double> angles = new()
            {
                quadrangle.AngleA,
                quadrangle.AngleB,
                quadrangle.AngleC,
                quadrangle.AngleD
            };
            if (angles.Count(angle => Math.Abs(90.0 - angle) <= 1e-8) != angles.Count)
            {
                return false;
            }

            return true;
        }
    }
}
