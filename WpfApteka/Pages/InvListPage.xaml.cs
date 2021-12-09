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
    /// Interaction logic for InvListPage.xaml
    /// </summary>
    public partial class InvListPage : Page
    {
        public InvListPage()
        {
            InitializeComponent();
            DataContext = this;
            InvListGrid.ItemsSource = SourceCore.DB.INVOICE_LIST.ToList();
        }

        private void AddInvListButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CopyInvListButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditInvListButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteInvListButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
