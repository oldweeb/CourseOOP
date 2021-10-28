using System;
using System.Text;
using System.Text.RegularExpressions;
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

        /// <summary>
        /// Default Hexagon constructor.
        /// Initializes SideLength with 1.0
        /// </summary>
        public Hexagon()
        {
            _sideLength = 1.0;
        }

        /// <summary>
        /// Hexagon constructor with parameter.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public Hexagon(double sideLength)
        {
            SideLength = sideLength;
        }

        /// <summary>
        /// Hexagon copy constructor.
        /// </summary>
        public Hexagon(Hexagon hexagon)
        {
            this.SideLength = hexagon.SideLength;
        }
        public double GetArea() => 3 * Math.Sqrt(3) * _sideLength * _sideLength / 2;

        public double GetPerimeter() => _sideLength * 6;

        public override string ToString()
        {
            StringBuilder sb = new();
            _ = sb.Append($"Side length: {SideLength:F2}| S={GetArea():F2}; P={GetPerimeter():F2}");
            return sb.ToString();
        }

        public static Hexagon Parse(string s)
        {
            if (!Regex.IsMatch(s, @"^\d+\.?\d*"))
            {
                throw new FormatException("String does not suit the format.");
            }

            return new Hexagon(Double.Parse(s));
        }

        public static bool TryParse(string s, out Hexagon hexagon)
        {
            try
            {
                hexagon = Hexagon.Parse(s);
            }
            catch (FormatException)
            {
                hexagon = new Hexagon();
                return false;
            }

            return true;
        }
    }
}
