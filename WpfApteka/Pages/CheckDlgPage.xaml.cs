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
    /// Interaction logic for CheckDlgPage.xaml
    /// </summary>
    public partial class CheckDlgPage : Page
    {
        public CheckDlgPage()
        {
            InitializeComponent();
        }

        private void CheckAddCommit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CheckAddRollback_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CheckPage());
        }
    }
}
