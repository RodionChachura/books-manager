using Books.Models;
using Books.Views;
using System;
using System.Collections.Generic;
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

namespace Books
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            FilesManager.Initialization();
            InitializeComponent();
            CurrentModelsView = new BooksView();

        }

        private void ViewChoosed(object sender, SelectionChangedEventArgs e)
        {
            if (ViewChoose.SelectedItem.ToString() == "Books")
                CurrentModelsView = new BooksView();
            else if (ViewChoose.SelectedItem.ToString() == "Authors")
                CurrentModelsView = new AuthorsView();
            else if (ViewChoose.SelectedItem.ToString() == "Publication Houses")
                CurrentModelsView = new PublicationHousesView();
        }

    }
}
