using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using CourseOOP.Models;
using Rectangle = CourseOOP.Models.Rectangle;

namespace CourseOOP.Views
{
    /// <summary>
    /// Interaction logic for EditingPage.xaml
    /// </summary>
    public partial class EditingPage : Page
    {
        private MainWindow _parent;
        public EditingPage()
        {
            InitializeComponent();
        }

        public EditingPage(MainWindow parent)
        {
            InitializeComponent();
            _parent = parent;
            ShapeTypeBox.ItemsSource = parent.ShapeHandler.Shapes;
        }

        private void EditBtn_OnClick(object sender, RoutedEventArgs e)
        {
            string regexPatternPoint = @"^(\(\d+(,|;)\s*\d+\))|^(\d+(,|;)\s*\d+)";
            int index = ShapeTypeBox.SelectedIndex;
            if (index < 0)
            {
                _ = MessageBox.Show(_parent, "Select shape to edit first.", "Message.", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            IShape shapeToEdit = _parent.ShapeHandler.Shapes[index];
            if (shapeToEdit is Triangle)
            {
                if (!Regex.IsMatch(ATextBox.Text, regexPatternPoint))
                {
                    _ = MessageBox.Show(_parent, "Point A is invalid.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (!Regex.IsMatch(BTextBox.Text, regexPatternPoint))
                {
                    _ = MessageBox.Show(_parent, "Point B is invalid.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (!Regex.IsMatch(CTextBox.Text, regexPatternPoint))
                {
                    _ = MessageBox.Show(_parent, "Point C is invalid.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Point a = Point.Parse(Regex.Match(ATextBox.Text, @"(\d+(,|;)\s*\d+)").Value.Replace(";", ","));
                    Point b = Point.Parse(Regex.Match(BTextBox.Text, @"(\d+(,|;)\s*\d+)").Value.Replace(";", ","));
                    Point c = Point.Parse(Regex.Match(CTextBox.Text, @"(\d+(,|;)\s*\d+)").Value.Replace(";", ","));
                    if (!Triangle.IsTriangle(a, b, c))
                    {
                        _ = MessageBox.Show(_parent, "This is not a triangle.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        switch (shapeToEdit)
                        {
                            case RightTriangle when !RightTriangle.IsRightTriangle(a, b, c):
                                _ = MessageBox.Show(_parent, "This is not a right triangle.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            case IsoscelesTriangle when !IsoscelesTriangle.IsIsoscelesTriangle(a, b, c):
                                _ = MessageBox.Show(_parent, "This is not an isosceles triangle.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            case EquilateralTriangle when !EquilateralTriangle.IsEquilateralTriangle(a, b, c):
                                _ = MessageBox.Show(_parent, "This is not an equilateral triangle.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                        }
                        (shapeToEdit as Triangle).A = new(a.X, a.Y);
                        (shapeToEdit as Triangle).B = new(b.X, b.Y);
                        (shapeToEdit as Triangle).C = new(c.X, c.Y);
                    }
                }
            }
            else if (shapeToEdit is Quadrangle)
            {
                if (!Regex.IsMatch(ATextBox.Text, regexPatternPoint))
                {
                    _ = MessageBox.Show(_parent, "Point A is invalid.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (!Regex.IsMatch(BTextBox.Text, regexPatternPoint))
                {
                    _ = MessageBox.Show(_parent, "Point B is invalid.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (!Regex.IsMatch(CTextBox.Text, regexPatternPoint))
                {
                    _ = MessageBox.Show(_parent, "Point C is invalid.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (!Regex.IsMatch(DTextBox.Text, regexPatternPoint))
                {
                    _ = MessageBox.Show(_parent, "Point D is invalid.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Point a = Point.Parse(Regex.Match(ATextBox.Text, @"(\d+(,|;)\s*\d+)").Value.Replace(";", ","));
                    Point b = Point.Parse(Regex.Match(BTextBox.Text, @"(\d+(,|;)\s*\d+)").Value.Replace(";", ","));
                    Point c = Point.Parse(Regex.Match(CTextBox.Text, @"(\d+(,|;)\s*\d+)").Value.Replace(";", ","));
                    Point d = Point.Parse(Regex.Match(DTextBox.Text, @"(\d+(,|;)\s*\d+)").Value.Replace(";", ","));
                    if (!Quadrangle.IsQuadrangle(a, b, c, d))
                    {
                        _ = MessageBox.Show(_parent, "This is not a quadrangle.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        switch (shapeToEdit)
                        {
                            case Rectangle when !Rectangle.IsRectangle(a, b, c, d):
                                _ = MessageBox.Show(_parent, "This is not a rectangle.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            case Trapezium when !Trapezium.IsTrapezium(a, b, c, d):
                                _ = MessageBox.Show(_parent, "This is not a trapezium.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                        }
                    }
                    (shapeToEdit as Quadrangle).A = new(a.X, a.Y);
                    (shapeToEdit as Quadrangle).B = new(b.X, b.Y);
                    (shapeToEdit as Quadrangle).C = new(c.X, c.Y);
                    (shapeToEdit as Quadrangle).D = new(d.X, d.Y);
                }
            }
            else
            {
                if (!Double.TryParse(SideTextBox.Text, out double sideLength))
                {
                    _ = MessageBox.Show(_parent, "Failed to parse side length.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (sideLength <= 0.0)
                {
                    _ = MessageBox.Show(_parent, "Side length cannot be negative value.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                (shapeToEdit as Hexagon).SideLength = sideLength;
            }
            _parent.ShapeHandler.UpdateShapeAt(shapeToEdit, index);
            _parent.UpdateGrid();
            _parent.EditingPagesFrame.Content = null;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            _parent.EditingPagesFrame.Content = null;
        }

        private void ShapeTypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ShapeTypeBox.SelectedIndex;
            IShape shapeToEdit = _parent.ShapeHandler.Shapes[index];
            if (shapeToEdit is Triangle)
            {
                ATextBox.IsEnabled = true;
                BTextBox.IsEnabled = true;
                CTextBox.IsEnabled = true;
                DTextBox.IsEnabled = false;
                SideTextBox.IsEnabled = false;
                ATextBox.Text = $"({(shapeToEdit as Triangle).A})";
                BTextBox.Text = $"({(shapeToEdit as Triangle).B})";
                CTextBox.Text = $"({(shapeToEdit as Triangle).C})";
                DTextBox.Text = "";
                SideTextBox.Text = "";
            }
            else if (shapeToEdit is Quadrangle)
            {
                ATextBox.IsEnabled = true;
                BTextBox.IsEnabled = true;
                CTextBox.IsEnabled = true;
                DTextBox.IsEnabled = true;
                SideTextBox.IsEnabled = false;
                ATextBox.Text = $"({(shapeToEdit as Quadrangle).A})";
                BTextBox.Text = $"({(shapeToEdit as Quadrangle).B})";
                CTextBox.Text = $"({(shapeToEdit as Quadrangle).C})";
                DTextBox.Text = $"({(shapeToEdit as Quadrangle).D})";
                SideTextBox.Text = "";
            }
            else
            {
                ATextBox.IsEnabled = false;
                BTextBox.IsEnabled = false;
                CTextBox.IsEnabled = false;
                DTextBox.IsEnabled = false;
                SideTextBox.IsEnabled = true;
                ATextBox.Text = "";
                BTextBox.Text = "";
                CTextBox.Text = "";
                DTextBox.Text = "";
                SideTextBox.Text = $"{(shapeToEdit as Hexagon).SideLength}";
            }
        }
    }
}
