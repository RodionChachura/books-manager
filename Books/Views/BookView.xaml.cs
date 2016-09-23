using System.Windows;
using System.Windows.Controls;

namespace Books.Views
{
    /// <summary>
    /// Логика взаимодействия для BookView.xaml
    /// </summary>
    public partial class BookView : UserControl
    {
        public BookView()
        {
            InitializeComponent();
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            AddBook addBook = new AddBook();
            addBook.DataContext = DataContext;
            addBook.Show();
            App.Current.MainWindow.Hide();
        }
    }
}
