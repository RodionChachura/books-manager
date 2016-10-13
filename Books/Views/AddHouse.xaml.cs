using Books.ViewModels;
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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Books.Views
{
    /// <summary>
    /// Логика взаимодействия для AddHouse.xaml
    /// </summary>
    public partial class AddHouse : Window
    {
        public AddHouse()
        {
            Icon = Imaging.CreateBitmapSourceFromHIcon(Properties.Resources.house.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            InitializeComponent();
            DataContext = new HouseViewModel();
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
