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

namespace WpfApteka.Pages
{
    /// <summary>
    /// Interaction logic for ShopListPage.xaml
    /// </summary>
    public partial class ShopListPage : Page
    {
        public ShopListPage()
        {
            InitializeComponent();
            DataContext = this;
            ShopListGrid.ItemsSource = SourceCore.DB.SHOPPING_LIST.ToList();
        }

        private void AddShopListButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CopyShopListButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditShopListButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteShopListButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
