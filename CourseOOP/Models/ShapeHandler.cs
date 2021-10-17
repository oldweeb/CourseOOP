using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Windows;
using CourseOOP.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CourseOOP.Models
{
    public class ShapeHandler
    {
        private List<IShape> _shapes;

        public IShape this[int index]
        {
            get
            {
                if (index < 0 || index >= _shapes.Count)
                {
                    throw new IndexOutOfRangeException("Index is out of range.");
                }

                return _shapes[index];
            }
        }

        public ShapeHandler()
        {
            _shapes = new List<IShape>();
        }

        public ShapeHandler(string filePath)
        {
            _shapes = new List<IShape>();
            ReadFromFile(filePath);
        }

        public ShapeHandler(ShapeHandler handler)
        {
            _shapes = handler._shapes.ToList();
        }

        public void ReadFromFile(string filePath)
        {
            if (String.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("String is null or whitespace.", nameof(filePath));
            }
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found.", filePath);
            }

            FileInfo fileInfo = new(filePath);
            if (String.CompareOrdinal(fileInfo.Extension, ".txt") != 0 &&
                String.CompareOrdinal(fileInfo.Extension, ".json") != 0)
            {
                throw new NotSupportedException("Only *.txt and *.json files are supported.");
            }

            string file;
            using (StreamReader reader = new(filePath))
            {
                file = reader.ReadToEnd();
            }
            if (String.CompareOrdinal(fileInfo.Extension, ".json") == 0)
            {
                ReadJson(file);
                return;
            }
            using (StreamReader reader = new(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (String.IsNullOrWhiteSpace(line))
                    {

                    }
                }
            }

            throw new NotImplementedException();

        }

        public void ReadJson(string json)
        {
            JObject jObject = JObject.Parse(json);
            JToken shapesJToken = jObject["Shapes"] ?? throw new FormatException("Json file must contain attribute \"Shapes\"");
            foreach (JToken shapeJToken in shapesJToken)
            {
                JToken typeJT = shapeJToken["Type"] ?? throw new FormatException("Each token must contain attribute \"Type\".");
                string type = typeJT.Value<string>();
                IShape shape;
                switch (type)
                {
                    case "Triangle":
                        shape = shapeJToken.ToObject<Triangle>();
                        Triangle triangle = (Triangle)shape;
                        if (!Triangle.IsTriangle(triangle.A, triangle.B, triangle.C))
                        {
                            throw new InvalidShapeException();
                        }
                        break;
                    case "IsoscelesTriangle":
                        shape = shapeJToken.ToObject<IsoscelesTriangle>();
                        IsoscelesTriangle isosceles = (IsoscelesTriangle) shape;
                        if (!IsoscelesTriangle.IsIsoscelesTriangle(
                            isosceles.A,
                            isosceles.B,
                            isosceles.C
                            ))
                        {
                            throw new InvalidShapeException();
                        }
                        break;
                    case "EquilateralTriangle":
                        shape = shapeJToken.ToObject<EquilateralTriangle>();
                        EquilateralTriangle equilateral = (EquilateralTriangle) shape;
                        if (!EquilateralTriangle.IsEquilateralTriangle(
                            equilateral.A,
                            equilateral.B,
                            equilateral.C
                            ))
                        {
                            throw new InvalidShapeException();
                        }
                        break;
                    case "RightTriangle":
                        shape = shapeJToken.ToObject<RightTriangle>();
                        RightTriangle right = (RightTriangle) shape;
                        if (!RightTriangle.IsRightTriangle(right.A, right.B, right.C))
                        {
                            throw new InvalidShapeException();
                        }
                        break;
                    case "Quadrangle":
                        shape = shapeJToken.ToObject<Quadrangle>();
                        Quadrangle quadrangle = (Quadrangle) shape;
                        if (!Quadrangle.IsQuadrangle(
                            quadrangle.A,
                            quadrangle.B,
                            quadrangle.C,
                            quadrangle.D
                            ))
                        {
                            throw new InvalidShapeException();
                        }
                        break;
                    case "Rectangle":
                        shape = shapeJToken.ToObject<Rectangle>();
                        Rectangle rectangle = (Rectangle) shape;
                        if (!Rectangle.IsRectangle(
                            rectangle.A,
                            rectangle.B,
                            rectangle.C,
                            rectangle.D
                            ))
                        {
                            throw new InvalidShapeException();
                        }
                        break;
                    case "Trapezium":
                        shape = shapeJToken.ToObject<Trapezium>();
                        Trapezium trapezium = (Trapezium) shape;
                        if (!Trapezium.IsTrapezium(
                            trapezium.A,
                            trapezium.B,
                            trapezium.C,
                            trapezium.D
                            ))
                        {
                            throw new InvalidShapeException();
                        }
                        break;
                    case "Hexagon":
                        shape = shapeJToken.ToObject<Hexagon>();
                        break;
                    default:
                        throw new TypeNotSupportedException("This type of shapes is not supported.");
                }

                _shapes.Add(shape ?? throw new ShapeParseException("Failed to parse shape from JSON."));

            }
        }

        public void AddShape([DisallowNull] IShape shape)
        {
            if (shape == null)
            {
                throw new ArgumentNullException(nameof(shape), "Parameter is null.");
            }
            _shapes.Add(shape);
        }
    }
}
