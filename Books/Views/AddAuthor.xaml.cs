using Books.ViewModels;
using System.Windows;

namespace Books.Views
{
    /// <summary>
    /// Логика взаимодействия для AddAuthor.xaml
    /// </summary>
    public partial class AddAuthor : Window
    {
        public AddAuthor()
        {
            InitializeComponent();
            DataContext = new AuthorViewModel();
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
