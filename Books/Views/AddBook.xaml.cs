using Books.ViewModels;
using System.Windows;

namespace Books.Views
{
    /// <summary>
    /// Логика взаимодействия для AddBook.xaml
    /// </summary>
    public partial class AddBook : Window
    {
        public AddBook()
        {
            InitializeComponent();
            DataContext = new BookViewModel();
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            App.Current.MainWindow.Show();
            base.OnClosing(e);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
