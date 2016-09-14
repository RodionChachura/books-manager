using Books.Abstraction;
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
            list.ItemsSource = Manager.books.Values.ToList();
        }

        private void ViewChoosed(object sender, SelectionChangedEventArgs e)
        {
            if (ViewChoose.SelectedIndex == 0)
            {
                if (list != null && list.DataContext != Manager.books.Values.ToList())
                {
                    list.ItemsSource = Manager.books.Values.ToList();
                }
            }
            else if (ViewChoose.SelectedIndex == 1)
            {
                if (list != null && list.DataContext != Manager.authors.Values.ToList())
                {
                    list.ItemsSource = Manager.authors.Values.ToList();
                }
            }
            else if (ViewChoose.SelectedIndex == 2)
            {
                if (list != null && list.DataContext != Manager.houses.Values.ToList())
                {
                    list.ItemsSource = Manager.houses.Values.ToList();
                }
            }

        }

    }
}
