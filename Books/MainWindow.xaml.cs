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

        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            AddBook addBook = new AddBook();
            addBook.Show();
            Hide();
        }
        private void AddHouseButton_Click(object sender, RoutedEventArgs e)
        {
            AddHouse addHouse = new AddHouse();
            addHouse.Show();
            Hide();
        }
        private void AddAuthorButton_Click(object sender, RoutedEventArgs e)
        {
            AddAuthor addAuthor = new AddAuthor();
            addAuthor.Show();
            Hide();
        }
    }
}
