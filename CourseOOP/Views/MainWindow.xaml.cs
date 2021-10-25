using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CourseOOP.Exceptions;
using CourseOOP.Models;
using Microsoft.Win32;

namespace CourseOOP.Views
{
    public class DataGridRecord
    {
        public string ShapeType { get; set; }
        public string Information { get; set; }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ShapeHandler ShapeHandler { get; }
        public MainWindow()
        {
            InitializeComponent();
            ShapeHandler = new();
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            // Set tooltip visibility

            if (TgBtn.IsChecked == true)
            {
                ttRead.Visibility = Visibility.Collapsed;
                ttWrite.Visibility = Visibility.Collapsed;
                ttAdd.Visibility = Visibility.Collapsed;
                ttEdit.Visibility = Visibility.Collapsed;
                ttRemove.Visibility = Visibility.Collapsed;
                ttShow.Visibility = Visibility.Collapsed;
            }
            else
            {
                ttRead.Visibility = Visibility.Visible;
                ttWrite.Visibility = Visibility.Visible;
                ttAdd.Visibility = Visibility.Visible;
                ttEdit.Visibility = Visibility.Visible;
                ttRemove.Visibility = Visibility.Visible;
                ttShow.Visibility = Visibility.Visible;
            }
        }

        private void TgBtn_Unchecked(object sender, RoutedEventArgs e)
        {
            GradientBG.Opacity = 1;
            ShapesGrid.Opacity = 1;
            ShapesGrid.IsEnabled = true;
        }

        private void TgBtn_Checked(object sender, RoutedEventArgs e)
        {
            GradientBG.Opacity = 0.3;
            ShapesGrid.Opacity = 0.3;
            ShapesGrid.IsEnabled = false;
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TgBtn.IsChecked = false;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        /// <summary>
        /// Updating grid after changing shape list.
        /// </summary>
        public void UpdateGrid()
        {
            ShapesGrid.Items.Clear();
            foreach (IShape shape in ShapeHandler)
            {
                string shapeType = shape.ShapeType;
                string information = shape.ToString();
                DataGridRecord record = new()
                {
                    ShapeType = shapeType,
                    Information = information
                };

                ShapesGrid.Items.Add(record);
            }
        }
        /// <summary>
        /// Reading from file: either JSON or TXT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog dialog = new()
            {
                Filter = "Text files (*.txt)|*.txt|JSON files (*.json)|*.json"
            };
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    ShapeHandler.ReadFromFile(dialog.FileName);
                }
                catch (InvalidShapeException)
                {
                    _ = MessageBox.Show(
                        this,
                        "Failed to read some shape from the file you have chosen. Check your file and try again.",
                        "Error.",
                        MessageBoxButton.OK
                    );
                }
                catch (TypeNotSupportedException)
                {
                    _ = MessageBox.Show(
                        this,
                        "The file you have chosen contains shape of type that is not currently supported.",
                        "Error.",
                        MessageBoxButton.OK
                        );
                }
                catch (FormatException)
                {
                    _ = MessageBox.Show(
                        this,
                        "Failed to read some shape from the file you have chosen. Check your file and try again.",
                        "Error.",
                        MessageBoxButton.OK
                    );
                }
                catch (ArgumentException ex)
                {
                    _ = MessageBox.Show(this,
                        $"File contains invalid shape values. Message text: {ex.Message}",
                        "Error.",
                        MessageBoxButton.OK
                    );
                }
                finally
                {
                    UpdateGrid();
                }
            }
        }
        /// <summary>
        /// Saving shapes to file: either TXT or JSON.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StackPanel_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (!ShapeHandler.Shapes.Any())
            {
                _ = MessageBox.Show(this, "Nothing to save. Shapes list is empty.", "Message.", MessageBoxButton.OK);
                return;
            }
            SaveFileDialog dialog = new()
            {
                Filter = "Text Files (*.txt)|*.txt|JSON Files (*.json)|*.json"
            };
            if (dialog.ShowDialog() == true)
            {
                ShapeHandler.WriteToFile(dialog.FileName);
            }
        }
        /// <summary>
        /// Adding new shape to grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StackPanel_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            EditingPagesFrame.Content = new AddingPage(this);
        }
        /// <summary>
        /// Editing existing shape.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StackPanel_MouseDown_3(object sender, MouseButtonEventArgs e)
        {
            if (!ShapeHandler.Shapes.Any())
            {
                _ = MessageBox.Show(this, "Nothing to edit yet.", "Message.", MessageBoxButton.OK);
                return;
            }
            EditingPagesFrame.Content = new EditingPage(this);
        }
        /// <summary>
        /// Removing shape from grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StackPanel_MouseDown_4(object sender, MouseButtonEventArgs e)
        {
            if (!ShapeHandler.Shapes.Any())
            {
                _ = MessageBox.Show(this, "Nothing to remove yet.", "Message.", MessageBoxButton.OK);
                return;
            }

            EditingPagesFrame.Content = new RemovingPage(this);
        }
        /// <summary>
        /// Saving shapes' history.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StackPanel_MouseDown_5(object sender, MouseButtonEventArgs e)
        {
            if (!ShapeHandler.Shapes.Any())
            {
                _ = MessageBox.Show(this, "Nothing to save. Shapes list is empty.", "Message.", MessageBoxButton.OK);
                return;
            }
            SaveFileDialog dialog = new()
            {
                Filter = "Text Files (*.txt)|*.txt"
            };
            if (dialog.ShowDialog() == true)
            {
                ShapeHandler.WriteShapesHistoryToFile(dialog.FileName);
            }
        }
    }
}
