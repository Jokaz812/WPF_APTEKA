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
        int ID_I;
        int ID_M;
        int COUNTS;
        public InvListPage()
        {
            InitializeComponent();
            DataContext = this;
            InvListDataGrid.ItemsSource = SourceCore.DB.INVOICE_LIST.ToList();
            InvComboBox.ItemsSource = SourceCore.DB.INVOICE.ToList();
            MedComboBox.ItemsSource = SourceCore.DB.MEDICINE.ToList();
        }

        private void AddInvListButton_Click(object sender, RoutedEventArgs e)
        {
            if (InvListChangeColumn.Width.Value == 0)
            {
                InvListChangeColumn.Width = new GridLength(250);
                InvListSplitterColumn.Width = GridLength.Auto;
                if ((sender as Button).Content.ToString() == "Добавить")
                {
                    InvListDataGrid.SelectedItem = null;
                }
            }
            else
            {
                InvListChangeColumn.Width = new GridLength(0);
                InvListSplitterColumn.Width = new GridLength(0);
            }
        }

        private void CopyInvListButton_Click(object sender, RoutedEventArgs e)
        {
            if (InvListChangeColumn.Width.Value == 0)
            {
                InvListChangeColumn.Width = new GridLength(250);
                InvListSplitterColumn.Width = GridLength.Auto;
                if (InvListDataGrid.SelectedItem != null)
                {
                    InvListLabel.Content = "Выбранный список накладной";
                    ID_I = InvComboBox.SelectedIndex;
                    ID_M = MedComboBox.SelectedIndex;
                    COUNTS = Convert.ToInt32(COUNTSTextBox.Text);
                    InvListDataGrid.IsHitTestVisible = false;
                    InvListDataGrid.SelectedItem = null;
                    InvComboBox.SelectedIndex = ID_I;
                    MedComboBox.SelectedIndex = ID_M;
                    COUNTSTextBox.Text = COUNTS.ToString();
                }
            }
            else
            {
                InvListChangeColumn.Width = new GridLength(0);
                InvListSplitterColumn.Width = new GridLength(0);
                InvListDataGrid.IsHitTestVisible = true;
            }
        }

        private void EditInvListButton_Click(object sender, RoutedEventArgs e)
        {
            if (InvListChangeColumn.Width.Value == 0)
            {
                InvListChangeColumn.Width = new GridLength(250);
                InvListSplitterColumn.Width = GridLength.Auto;
            }
            else
            {
                InvListChangeColumn.Width = new GridLength(0);
                InvListSplitterColumn.Width = new GridLength(0);
            }
        }

        private void DeleteInvListButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранную запись списка накладной?", "Внимание!", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                try
                {
                    Data.INVOICE_LIST DeletingInvList = (Data.INVOICE_LIST)InvListDataGrid.SelectedItem;
                    if (InvListDataGrid.SelectedIndex < InvListDataGrid.Items.Count - 1)
                    {
                        InvListDataGrid.SelectedIndex++;
                    }
                    else
                    {
                        if (InvListDataGrid.SelectedIndex > 0)
                        {
                            InvListDataGrid.SelectedIndex--;
                        }
                    }
                    Data.INVOICE_LIST SelectingInvList = (Data.INVOICE_LIST)InvListDataGrid.SelectedItem;
                    InvListDataGrid.SelectedItem = DeletingInvList;
                    SourceCore.DB.INVOICE_LIST.Remove(DeletingInvList);
                    SourceCore.DB.SaveChanges();
                    UpdateGrid(SelectingInvList);
                }
                catch
                {
                    MessageBox.Show("Невозможно удалить запись, так как она используется в других справочниках базы данных.",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                }
                InvListDataGrid.Focus();
            }
        }

        private void CloseEdChangeClick(object sender, RoutedEventArgs e)
        {
            InvListChangeColumn.Width = new GridLength(0);
            InvListSplitterColumn.Width = new GridLength(0);
            InvListDataGrid.IsHitTestVisible = true;
        }

        private void CommitButtonInvList(object sender, RoutedEventArgs e)
        {
            var S = new Data.INVOICE_LIST();
            S.INVOICE = (Data.INVOICE)InvComboBox.SelectedItem;
            S.MEDICINE = (Data.MEDICINE)MedComboBox.SelectedItem;
            S.COUNTS = Convert.ToInt32(COUNTSTextBox.Text);
            if (InvListDataGrid.SelectedItem == null)
            {
                SourceCore.DB.INVOICE_LIST.Add(S);
            }
            SourceCore.DB.SaveChanges();
            UpdateGrid(S);
            InvListDataGrid.Focus();
            CloseEdChangeClick(sender, e);
        }

        public void UpdateGrid(Data.INVOICE_LIST InvList)
        {
            if ((InvList == null) && (InvListDataGrid.ItemsSource != null))
            {
                InvList = (Data.INVOICE_LIST)InvListDataGrid.SelectedItem;
            }
            InvListDataGrid.ItemsSource = SourceCore.DB.INVOICE_LIST.ToList();
            InvListDataGrid.SelectedItem = InvList;
        }

        private void FilterInvListComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FilterInvListTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
