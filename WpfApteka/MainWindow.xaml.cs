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
        private List<Page> ActivePages;
        private int CurrentPageIndex;

        public MainWindow()
        {
            InitializeComponent();
            ActivePages = new List<Page>();
            CurrentPageIndex = -1;
        }

        private void MedButton_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(typeof(Pages.MedPage));
        }

        private void CatButton_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(typeof(Pages.CatPage));
        }

        private void RealButton_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(typeof(Pages.RealPage));
        }

        private void WorkButton_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(typeof(Pages.WorkPage));
        }

        private void FabButton_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(typeof(Pages.FabPage));
        }

        private void SupButton_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(typeof(Pages.SupPage));
        }

        private void Invoice_ListButton_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(typeof(Pages.InvListPage));
        }

        private void InvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(typeof(Pages.InvPage));
        }

        private void Shop_ListButton_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(typeof(Pages.ShopListPage));
        }

        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(typeof(Pages.CheckPage));
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            //CurrentPageIndex++;
            //PharmacyKioskFrame.Navigate(ActivePages[CurrentPageIndex]);
        }

        private void PreviosPageButton_Click(object sender, RoutedEventArgs e)
        {
            //CurrentPageIndex--;
            //PharmacyKioskFrame.Navigate(ActivePages[CurrentPageIndex]);
        }

        private void ClosePageButton_Click(object sender, RoutedEventArgs e)
        {
            Page Page;
            ActivePages.RemoveAt(CurrentPageIndex);
            if (CurrentPageIndex > 0)
            {
                CurrentPageIndex--;
                Page = ActivePages[CurrentPageIndex];
            }
            else
            {
                if (CurrentPageIndex < ActivePages.Count)
                {
                    Page = ActivePages[CurrentPageIndex];
                }
                else
                {
                    Page = null;
                }
            }
            PharmacyKioskFrame.Navigate(Page);

        }

        private void ShowPage(Type PageType)
        {
            Page Page;
            if (PageType != null)
            {
                int Index = GetActivePageIndexByType(PageType);
                if (Index < 0)
                {
                    Page = (Page)Activator.CreateInstance(PageType);
                    ActivePages.Add(Page);
                    CurrentPageIndex = ActivePages.Count - 1;
                }
                else
                {
                    Page = ActivePages[Index];
                    CurrentPageIndex = Index;
                }
            }
            else
            {
                Page = null;
            }
            PharmacyKioskFrame.Navigate(Page);
        }

        private int GetActivePageIndexByType(Type PageType)
        {
            int Index = ActivePages.Count - 1;
            while ((Index >= 0) && (ActivePages[Index].GetType() != PageType))
            {
                Index--;
            }
            return Index;
        }

    }
}
