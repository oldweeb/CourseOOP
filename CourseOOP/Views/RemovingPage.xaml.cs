using System.Windows;
using System.Windows.Controls;

namespace CourseOOP.Views
{
    /// <summary>
    /// Interaction logic for RemovingPage.xaml
    /// </summary>
    public partial class RemovingPage : Page
    {
        private MainWindow _parent;
        public RemovingPage()
        {
            InitializeComponent();
        }

        public RemovingPage(MainWindow parent)
        {
            InitializeComponent();
            _parent = parent;
            ShapeBox.ItemsSource = _parent.ShapeHandler.Shapes;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            _parent.EditingPagesFrame.Content = null;
        }

        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            int index = ShapeBox.SelectedIndex;
            if (index < 0)
            {
                _ = MessageBox.Show(_parent, "Select shape to remove first.", "Message.", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            _parent.ShapeHandler.RemoveAt(index);
            _parent.UpdateGrid();
            _parent.EditingPagesFrame.Content = null;
        }
    }
}
