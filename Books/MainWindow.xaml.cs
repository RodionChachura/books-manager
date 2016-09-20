using Books.Views;
using System.Windows;
using System.Windows.Controls;

namespace Books
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void housesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            infoControl.Content = housesGrid.SelectedValue;
        }
        private void authorsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            infoControl.Content = authorsGrid.SelectedValue;
        }
        private void booksGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            infoControl.Content = booksGrid.SelectedValue;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddHouse addHouse = new AddHouse();
            addHouse.Show();
            Hide();
        }
    }
}
