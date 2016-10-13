using Books.ViewModels;
using Books.Views;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace Books
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Icon = Imaging.CreateBitmapSourceFromHIcon(Properties.Resources.library.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            DataContext = new MainViewModel();
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
