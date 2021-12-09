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
    /// Interaction logic for SupPage.xaml
    /// </summary>
    public partial class SupPage : Page
    {
        string NAME_SUP;
        string ADDRESS;
        string CITY;
        string COUNTRY;
        string TELEPHONE;

        public SupPage()
        {
            InitializeComponent();
            DataContext = this;
            SupDataGrid.ItemsSource = SourceCore.DB.SUPPLIER.ToList();
        }

        private void AddSupButton_Click(object sender, RoutedEventArgs e)
        {
            if (SupChangeColumn.Width.Value == 0)
            {
                SupChangeColumn.Width = new GridLength(250);
                SupSplitterColumn.Width = GridLength.Auto;
                if ((sender as Button).Content.ToString() == "Добавить")
                {
                    SupDataGrid.SelectedItem = null;
                }
            }
            else
            {
                SupChangeColumn.Width = new GridLength(0);
                SupSplitterColumn.Width = new GridLength(0);
            }
        }

        private void CopySupButton_Click(object sender, RoutedEventArgs e)
        {
            if (SupChangeColumn.Width.Value == 0)
            {
                SupChangeColumn.Width = new GridLength(250);
                SupSplitterColumn.Width = GridLength.Auto;
                if (SupDataGrid.SelectedItem != null)
                {
                    SupLabel.Content = "Выбран поставщик";
                    NAME_SUP = NAME_SUPTextBox.Text;
                    ADDRESS = ADDRESSTextBox.Text;
                    CITY = CITYTextBox.Text;
                    COUNTRY = COUNTRYTextBox.Text;
                    TELEPHONE = TELEPHONETextBox.Text;
                    SupDataGrid.IsHitTestVisible = false;
                    SupDataGrid.SelectedItem = null;
                    NAME_SUPTextBox.Text = NAME_SUP;
                    ADDRESSTextBox.Text = ADDRESS;
                    CITYTextBox.Text = CITY;
                    COUNTRYTextBox.Text = COUNTRY;
                    TELEPHONETextBox.Text = TELEPHONE;
                }
            }
            else
            {
                SupChangeColumn.Width = new GridLength(0);
                SupSplitterColumn.Width = new GridLength(0);
                SupDataGrid.IsHitTestVisible = true;
            }
        }

        private void EditSupButton_Click(object sender, RoutedEventArgs e)
        {
            if (SupChangeColumn.Width.Value == 0)
            {
                SupChangeColumn.Width = new GridLength(250);
                SupSplitterColumn.Width = GridLength.Auto;
            }
            else
            {
                SupChangeColumn.Width = new GridLength(0);
                SupSplitterColumn.Width = new GridLength(0);
            }
        }

        private void DeleteSupButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранную запись о поставщике?", "Внимание!", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                try
                {
                    Data.SUPPLIER DeletingSup = (Data.SUPPLIER)SupDataGrid.SelectedItem;
                    if (SupDataGrid.SelectedIndex < SupDataGrid.Items.Count - 1)
                    {
                        SupDataGrid.SelectedIndex++;
                    }
                    else
                    {
                        if (SupDataGrid.SelectedIndex > 0)
                        {
                            SupDataGrid.SelectedIndex--;
                        }
                    }
                    Data.SUPPLIER SelectingSup = (Data.SUPPLIER)SupDataGrid.SelectedItem;
                    SupDataGrid.SelectedItem = DeletingSup;
                    SourceCore.DB.SUPPLIER.Remove(DeletingSup);
                    SourceCore.DB.SaveChanges();
                    UpdateGrid(SelectingSup);
                }
                catch
                {
                    MessageBox.Show("Невозможно удалить запись, так как она используется в других справочниках базы данных.",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                }
                SupDataGrid.Focus();
            }
        }

        private void CloseEdChangeClick(object sender, RoutedEventArgs e)
        {
            SupChangeColumn.Width = new GridLength(0);
            SupSplitterColumn.Width = new GridLength(0);
            SupDataGrid.IsHitTestVisible = true;
        }

        private void CommitButtonSup(object sender, RoutedEventArgs e)
        {
            var S = new Data.SUPPLIER();
            S.NAME_SUP = NAME_SUPTextBox.Text;
            S.ADDRESS = ADDRESSTextBox.Text;
            S.CITY = CITYTextBox.Text;
            S.COUNTRY = CITYTextBox.Text;
            S.TELEPHONE = TELEPHONETextBox.Text;
            if (SupDataGrid.SelectedItem == null)
            {
                SourceCore.DB.SUPPLIER.Add(S);
            }
            SourceCore.DB.SaveChanges();
            UpdateGrid(S);
            SupDataGrid.Focus();
            CloseEdChangeClick(sender, e);
        }

        public void UpdateGrid(Data.SUPPLIER Sup)
        {
            if ((Sup == null) && (SupDataGrid.ItemsSource != null))
            {
                Sup = (Data.SUPPLIER)SupDataGrid.SelectedItem;
            }
            SupDataGrid.ItemsSource = SourceCore.DB.SUPPLIER.ToList();
            SupDataGrid.SelectedItem = Sup;
        }
    }
}
