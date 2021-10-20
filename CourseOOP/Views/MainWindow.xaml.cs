using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CourseOOP.Models;
using Newtonsoft.Json;

namespace CourseOOP.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ShapeHandler handler = new(@"D:\Workspace\CourseOOP\CourseOOP\Data\shapes.json");
            ShapesGrid.ItemsSource = handler.Shapes;
            ShapesGrid.UpdateLayout();
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
        }

        private void TgBtn_Checked(object sender, RoutedEventArgs e)
        {
            GradientBG.Opacity = 0.3;
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TgBtn.IsChecked = false;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
