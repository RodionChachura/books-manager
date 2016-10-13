using Books.ViewModels;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace Books.Views
{
    /// <summary>
    /// Логика взаимодействия для AddBook.xaml
    /// </summary>
    public partial class AddBook : Window
    {
        public AddBook()
        {
            Icon = Imaging.CreateBitmapSourceFromHIcon(Properties.Resources.book.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
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
