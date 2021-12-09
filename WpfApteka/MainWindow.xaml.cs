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

namespace WpfApteka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MedButton_Click(object sender, RoutedEventArgs e)
        {
            RealFrame.Navigate(new Pages.MedPage());
        }

        private void CatButton_Click(object sender, RoutedEventArgs e)
        {
            RealFrame.Navigate(new Pages.CatPage());
        }

        private void RealButton_Click(object sender, RoutedEventArgs e)
        {
            RealFrame.Navigate(new Pages.RealPage());
        }

        private void WorkButton_Click(object sender, RoutedEventArgs e)
        {
            RealFrame.Navigate(new Pages.WorkPage());
        }

        private void FabButton_Click(object sender, RoutedEventArgs e)
        {
            RealFrame.Navigate(new Pages.FabPage());
        }

        private void SupButton_Click(object sender, RoutedEventArgs e)
        {
            RealFrame.Navigate(new Pages.SupPage());
        }

        private void Invoice_ListButton_Click(object sender, RoutedEventArgs e)
        {
            RealFrame.Navigate(new Pages.InvListPage());
        }

        private void InvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            RealFrame.Navigate(new Pages.InvPage());
        }

        private void Shop_ListButton_Click(object sender, RoutedEventArgs e)
        {
            RealFrame.Navigate(new Pages.ShopListPage());
        }

        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            RealFrame.Navigate(new Pages.CheckPage());
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PreviosPageButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClosePageButton_Click(object sender, RoutedEventArgs e)
        {
            RealFrame.Content = null;
        }
    }
}
