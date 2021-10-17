using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourseOOP.Models
{
    public class Trapezium : Quadrangle
    {
        public (double, double) Basis
        {
            get
            {
                Vector ab = new(B.X - A.X, B.Y - A.Y);
                Vector cd = new(D.X - C.X, D.Y - C.Y);
                if (Vector.AngleBetween(ab, cd) <= 1e-8)
                {
                    return (AB, CD);
                }

                return (BC, CD);
            }
        }

        public Trapezium() : base() { }

        public Trapezium(Point a, Point b, Point c, Point d) : base(a, b, c, d)
        {
            if (!IsTrapezium(a, b, c, d))
            {
                throw new ArgumentException("This is not a trapezium.");
            }
        }

        public Trapezium(Trapezium trapezium) : base (trapezium) { } 
        public override double GetArea()
        {
            return base.GetArea();
        }

        public static bool IsTrapezium(Point a, Point b, Point c, Point d)
        {
            if (!IsQuadrangle(a, b, c, d))
            {
                return false;
            }

            Quadrangle quadrangle = new(a, b, c, d);
            Vector ab = new(quadrangle.B.X - quadrangle.A.X, quadrangle.B.Y - quadrangle.A.Y);
            Vector bc = new(quadrangle.C.X - quadrangle.B.X, quadrangle.C.Y - quadrangle.B.Y);
            Vector cd = new(quadrangle.D.X - quadrangle.C.X, quadrangle.D.Y - quadrangle.C.Y);
            Vector ad = new(quadrangle.D.X - quadrangle.A.X, quadrangle.D.Y - quadrangle.A.Y);
            return (Vector.AngleBetween(ab, cd) <= 1e-8 &&
                   !(Vector.AngleBetween(bc, ad) <= 1e-8)) ||
                   (!(Vector.AngleBetween(ab, cd) <= 1e-8) &&
                    Vector.AngleBetween(bc, ad) <= 1e-8);
        }
    }
}
