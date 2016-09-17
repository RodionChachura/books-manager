using Books.Abstraction;
using Books.Converters;
using Books.Models;
using Books.ViewModels;
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
            Manager.Initialization();
            InitializeComponent();
            booksGrid.ItemsSource = new BooksViewModel(Manager.books.Values.ToList().Cast<Book>().ToList());
            authorsGrid.ItemsSource = new AuthorsViewModel(Manager.authors.Values.ToList().Cast<Author>().ToList());
            housesGrid.ItemsSource = new HousesViewModel(Manager.houses.Values.ToList().Cast<House>().ToList());
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
    }
}
