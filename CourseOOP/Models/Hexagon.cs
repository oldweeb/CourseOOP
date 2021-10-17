using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace CourseOOP.Models
{
    public class Hexagon : IShape
    {
        private double _sideLength;

        public double SideLength
        {
            get => _sideLength;
            set
            {
                if (value <= 0.0)
                {
                    throw new ArgumentException("Side length must be positive value.", nameof(value));
                }

                _sideLength = value;
            }
        }
        [JsonProperty("Type")] public string ShapeType => this.GetType().Name;

        public Hexagon()
        {
            _sideLength = 1.0;
        }

        public Hexagon(double sideLength)
        {
            SideLength = sideLength;
        }

        public Hexagon(Hexagon hexagon)
        {
            this.SideLength = hexagon.SideLength;
        }
        public double GetArea() => 3 * Math.Sqrt(3) * _sideLength * _sideLength / 2;

        public double GetPerimeter() => _sideLength * 6;
    }
}
