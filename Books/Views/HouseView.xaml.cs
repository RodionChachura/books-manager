using System.Windows;
using System.Windows.Controls;

namespace Books.Views
{
    /// <summary>
    /// Логика взаимодействия для HouseView.xaml
    /// </summary>
    public partial class HouseView : UserControl
    {
        public HouseView()
        {
            InitializeComponent();
        }


        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            AddHouse addHouse = new AddHouse();
            addHouse.DataContext = DataContext;
            addHouse.Show();
            App.Current.MainWindow.Hide();
        }
    }
}
