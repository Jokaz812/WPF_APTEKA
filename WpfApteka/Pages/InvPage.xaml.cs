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
    /// Interaction logic for InvPage.xaml
    /// </summary>
    public partial class InvPage : Page
    {
        string NAME;
        string BDATE;
        int ID_S;
        public InvPage()
        {
            InitializeComponent();
            DataContext = this;
            InvDataGrid.ItemsSource = SourceCore.DB.INVOICE.ToList();
            SupComboBox.ItemsSource = SourceCore.DB.SUPPLIER.ToList();
        }

        private void AddInvButton_Click(object sender, RoutedEventArgs e)
        {
            if (InvChangeColumn.Width.Value == 0)
            {
                InvChangeColumn.Width = new GridLength(250);
                InvSplitterColumn.Width = GridLength.Auto;
                if ((sender as Button).Content.ToString() == "Добавить")
                {
                    InvDataGrid.SelectedItem = null;
                }
            }
            else
            {
                InvChangeColumn.Width = new GridLength(0);
                InvSplitterColumn.Width = new GridLength(0);
            }
        }

        private void CopyInvButton_Click(object sender, RoutedEventArgs e)
        {
            if (InvChangeColumn.Width.Value == 0)
            {
                InvChangeColumn.Width = new GridLength(250);
                InvSplitterColumn.Width = GridLength.Auto;
                if (InvDataGrid.SelectedItem != null)
                {
                    InvLabel.Content = "Выбранная накладная";
                    NAME = NAMETextBox.Text;
                    BDATE = BDATETextBox.Text;
                    ID_S = SupComboBox.SelectedIndex;
                    InvDataGrid.IsHitTestVisible = false;
                    InvDataGrid.SelectedItem = null;
                    NAMETextBox.Text = NAME;
                    BDATETextBox.Text = BDATE;
                    SupComboBox.SelectedIndex = ID_S;
                }
            }
            else
            {
                InvChangeColumn.Width = new GridLength(0);
                InvSplitterColumn.Width = new GridLength(0);
                InvDataGrid.IsHitTestVisible = true;
            }
        }

        private void EditInvButton_Click(object sender, RoutedEventArgs e)
        {
            if (InvChangeColumn.Width.Value == 0)
            {
                InvChangeColumn.Width = new GridLength(250);
                InvSplitterColumn.Width = GridLength.Auto;
            }
            else
            {
                InvChangeColumn.Width = new GridLength(0);
                InvSplitterColumn.Width = new GridLength(0);
            }
        }

        private void DeleteInvButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранную запись о накладной?", "Внимание!", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                try
                {
                    Data.INVOICE DeletingInv = (Data.INVOICE)InvDataGrid.SelectedItem;
                    if (InvDataGrid.SelectedIndex < InvDataGrid.Items.Count - 1)
                    {
                        InvDataGrid.SelectedIndex++;
                    }
                    else
                    {
                        if (InvDataGrid.SelectedIndex > 0)
                        {
                            InvDataGrid.SelectedIndex--;
                        }
                    }
                    Data.INVOICE SelectingInv = (Data.INVOICE)InvDataGrid.SelectedItem;
                    InvDataGrid.SelectedItem = DeletingInv;
                    SourceCore.DB.INVOICE.Remove(DeletingInv);
                    SourceCore.DB.SaveChanges();
                    UpdateGrid(SelectingInv);
                }
                catch
                {
                    MessageBox.Show("Невозможно удалить запись, так как она используется в других справочниках базы данных.",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                }
                InvDataGrid.Focus();
            }
        }

        private void CloseEdChangeClick(object sender, RoutedEventArgs e)
        {
            InvChangeColumn.Width = new GridLength(0);
            InvSplitterColumn.Width = new GridLength(0);
            InvDataGrid.IsHitTestVisible = true;
        }

        private void CommitButtonInv(object sender, RoutedEventArgs e)
        {
            var S = new Data.INVOICE();
            S.NAME = NAMETextBox.Text;
            S.BDATE = Convert.ToDateTime(BDATETextBox.Text);
            S.SUPPLIER = (Data.SUPPLIER)SupComboBox.SelectedItem;
            if (InvDataGrid.SelectedItem == null)
            {
                SourceCore.DB.INVOICE.Add(S);
            }
            SourceCore.DB.SaveChanges();
            UpdateGrid(S);
            InvDataGrid.Focus();
            CloseEdChangeClick(sender, e);
        }

        public void UpdateGrid(Data.INVOICE Inv)
        {
            if ((Inv == null) && (InvDataGrid.ItemsSource != null))
            {
                Inv = (Data.INVOICE)InvDataGrid.SelectedItem;
            }
            InvDataGrid.ItemsSource = SourceCore.DB.INVOICE.ToList();
            InvDataGrid.SelectedItem = Inv;
        }
    }
}
