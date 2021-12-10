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
        int ID_M;
        int COUNTS;
        public ShopListPage()
        {
            InitializeComponent();
            DataContext = this;
            ShopListDataGrid.ItemsSource = SourceCore.DB.SHOPPING_LIST.ToList();
            MedComboBox.ItemsSource = SourceCore.DB.MEDICINE.ToList();
        }

        private void AddShopListButton_Click(object sender, RoutedEventArgs e)
        {
            if (ShopListChangeColumn.Width.Value == 0)
            {
                ShopListChangeColumn.Width = new GridLength(250);
                ShopListSplitterColumn.Width = GridLength.Auto;
                if ((sender as Button).Content.ToString() == "Добавить")
                {
                    ShopListDataGrid.SelectedItem = null;
                }
            }
            else
            {
                ShopListChangeColumn.Width = new GridLength(0);
                ShopListSplitterColumn.Width = new GridLength(0);
            }
        }

        private void CopyShopListButton_Click(object sender, RoutedEventArgs e)
        {
            if (ShopListChangeColumn.Width.Value == 0)
            {
                ShopListChangeColumn.Width = new GridLength(250);
                ShopListSplitterColumn.Width = GridLength.Auto;
                if (ShopListDataGrid.SelectedItem != null)
                {
                    ShopListLabel.Content = "Выбранный список покупок";
                    ID_M = MedComboBox.SelectedIndex;
                    COUNTS = Convert.ToInt32(COUNTSTextBox.Text);
                    
                    ShopListDataGrid.IsHitTestVisible = false;
                    ShopListDataGrid.SelectedItem = null;
                    MedComboBox.SelectedIndex = ID_M;
                    COUNTSTextBox.Text = COUNTS.ToString();
                }
            }
            else
            {
                ShopListChangeColumn.Width = new GridLength(0);
                ShopListSplitterColumn.Width = new GridLength(0);
                ShopListDataGrid.IsHitTestVisible = true;
            }
        }

        private void EditShopListButton_Click(object sender, RoutedEventArgs e)
        {
            if (ShopListChangeColumn.Width.Value == 0)
            {
                ShopListChangeColumn.Width = new GridLength(250);
                ShopListSplitterColumn.Width = GridLength.Auto;
            }
            else
            {
                ShopListChangeColumn.Width = new GridLength(0);
                ShopListSplitterColumn.Width = new GridLength(0);
            }
        }

        private void DeleteShopListButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранную запись списка покупок?", "Внимание!", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                try
                {
                    Data.SHOPPING_LIST DeletingShopList = (Data.SHOPPING_LIST)ShopListDataGrid.SelectedItem;
                    if (ShopListDataGrid.SelectedIndex < ShopListDataGrid.Items.Count - 1)
                    {
                        ShopListDataGrid.SelectedIndex++;
                    }
                    else
                    {
                        if (ShopListDataGrid.SelectedIndex > 0)
                        {
                            ShopListDataGrid.SelectedIndex--;
                        }
                    }
                    Data.SHOPPING_LIST SelectingShopList = (Data.SHOPPING_LIST)ShopListDataGrid.SelectedItem;
                    ShopListDataGrid.SelectedItem = DeletingShopList;
                    SourceCore.DB.SHOPPING_LIST.Remove(DeletingShopList);
                    SourceCore.DB.SaveChanges();
                    UpdateGrid(SelectingShopList);
                }
                catch
                {
                    MessageBox.Show("Невозможно удалить запись, так как она используется в других справочниках базы данных.",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                }
                ShopListDataGrid.Focus();
            }
        }

        private void CloseEdChangeClick(object sender, RoutedEventArgs e)
        {
            ShopListChangeColumn.Width = new GridLength(0);
            ShopListSplitterColumn.Width = new GridLength(0);
            ShopListDataGrid.IsHitTestVisible = true;
        }

        private void CommitButtonShopList(object sender, RoutedEventArgs e)
        {
            var S = new Data.SHOPPING_LIST();
            S.MEDICINE = (Data.MEDICINE)MedComboBox.SelectedItem;
            S.COUNTS = Convert.ToInt32(COUNTSTextBox.Text);
            if (ShopListDataGrid.SelectedItem == null)
            {
                SourceCore.DB.SHOPPING_LIST.Add(S);
            }
            SourceCore.DB.SaveChanges();
            UpdateGrid(S);
            ShopListDataGrid.Focus();
            CloseEdChangeClick(sender, e);
        }

        public void UpdateGrid(Data.SHOPPING_LIST ShopList)
        {
            if ((ShopList == null) && (ShopListDataGrid.ItemsSource != null))
            {
                ShopList = (Data.SHOPPING_LIST)ShopListDataGrid.SelectedItem;
            }
            ShopListDataGrid.ItemsSource = SourceCore.DB.SHOPPING_LIST.ToList();
            ShopListDataGrid.SelectedItem = ShopList;
        }

        private void FilterShopListComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FilterShopListBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
