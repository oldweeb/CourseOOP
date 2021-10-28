using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using CourseOOP.Models;
using Rectangle = CourseOOP.Models.Rectangle;

namespace CourseOOP.Views
{
    /// <summary>
    /// Interaction logic for AddingPage.xaml
    /// </summary>
    public partial class AddingPage : Page
    {
        private MainWindow _parent;
        public AddingPage()
        {
            InitializeComponent();
            _parent = null;
        }

        public AddingPage(MainWindow parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            string regexPatternPoint = @"^(\(\d+(,|;)\s*\d+\))|^(\d+(,|;)\s*\d+)";
            switch (ShapeTypeBox.SelectedIndex)
            {
                case <= 3:
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
                            int index = ShapeTypeBox.SelectedIndex;
                            Triangle triangle;
                            if (index == 0)
                            {
                                triangle = new(a, b, c);
                            }
                            else if (index == 1)
                            {
                                if (!RightTriangle.IsRightTriangle(a, b, c))
                                {
                                    _ = MessageBox.Show(_parent, "This is not a right triangle.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return;
                                }

                                triangle = new RightTriangle(a, b, c);
                            }
                            else if (index == 2)
                            {
                                if (!IsoscelesTriangle.IsIsoscelesTriangle(a, b, c))
                                {
                                    _ = MessageBox.Show(_parent, "This is not an isosceles triangle.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return;
                                }

                                triangle = new IsoscelesTriangle(a, b, c);
                            }
                            else
                            {
                                if (!EquilateralTriangle.IsEquilateralTriangle(a, b, c))
                                {
                                    _ = MessageBox.Show(_parent, "This is not an equilateral triangle.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return;
                                }

                                triangle = new EquilateralTriangle(a, b, c);
                            }
                            _parent.ShapeHandler.AddShape(triangle);
                            _parent.UpdateGrid();
                            _parent.EditingPagesFrame.Content = null;
                        }
                    }
                    break;
                case <= 6:
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
                        int index = ShapeTypeBox.SelectedIndex;
                        Quadrangle quadrangle;
                        if (index == 4)
                        {
                            if (!Quadrangle.IsQuadrangle(a, b, c, d))
                            {
                                _ = MessageBox.Show(_parent, "This is not a quadrangle.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }

                            quadrangle = new Quadrangle(a, b, c, d);
                        }
                        else if (index == 5)
                        {
                            if (!Rectangle.IsRectangle(a, b, c, d))
                            {
                                _ = MessageBox.Show(_parent, "This is not a rectangle.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }

                            quadrangle = new Rectangle(a, b, c, d);
                        }
                        else
                        {
                            if (!Trapezium.IsTrapezium(a, b, c, d))
                            {
                                _ = MessageBox.Show(_parent, "This is not a trapezium.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }

                            quadrangle = new Trapezium(a, b, c, d);
                        }
                        _parent.ShapeHandler.AddShape(quadrangle);
                        _parent.UpdateGrid();
                        _parent.EditingPagesFrame.Content = null;
                    }
                    break;
                case 7:
                    if (!Double.TryParse(SideTextBox.Text, out double sideLength))
                    {
                        _ = MessageBox.Show(_parent, "Failed to parse side length.", "Error.");
                        return;
                    }

                    if (sideLength <= 0.0)
                    {
                        _ = MessageBox.Show(_parent, "Side length cannot be negative value.", "Error.");
                        return;
                    }

                    Hexagon hexagon = new(sideLength);
                    _parent.ShapeHandler.AddShape(hexagon);
                    _parent.UpdateGrid();
                    _parent.EditingPagesFrame.Content = null;
                    break;
                default:
                    _ = MessageBox.Show(_parent, "Select shape type first.", "Message", MessageBoxButton.OK);
                    break;
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            _parent.EditingPagesFrame.Content = null;
        }

        private void ShapeTypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = ShapeTypeBox.SelectedIndex;
            switch (selectedIndex)
            {
                case <= 3:
                    ATextBox.IsEnabled = true;
                    BTextBox.IsEnabled = true;
                    CTextBox.IsEnabled = true;
                    DTextBox.IsEnabled = false;
                    SideTextBox.IsEnabled = false;
                    break;
                case <= 6:
                    ATextBox.IsEnabled = true;
                    BTextBox.IsEnabled = true;
                    CTextBox.IsEnabled = true;
                    DTextBox.IsEnabled = true;
                    SideTextBox.IsEnabled = false;
                    break;
                case 7:
                    ATextBox.IsEnabled = false;
                    BTextBox.IsEnabled = false;
                    CTextBox.IsEnabled = false;
                    DTextBox.IsEnabled = false;
                    SideTextBox.IsEnabled = true;
                    break;
            }
        }
    }
}
