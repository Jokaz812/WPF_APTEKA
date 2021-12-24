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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApteka.Pages
{
    /// <summary>
    /// Interaction logic for CheckPage.xaml
    /// </summary>
    public partial class CheckPage : Page
    {
        int ID_W;
        string TIME_SALE;
        string DATE_SALE;
        public CheckPage()
        {
            InitializeComponent();
            DataContext = this;
            CheckDataGrid.ItemsSource = SourceCore.DB.CHECKS.ToList();
            listBox1.ItemsSource = SourceCore.DB.CHECKS.ToList();
           // WorkComboBox.ItemsSource = SourceCore.DB.WORKERS.ToList();
        }

        //private void AddCheckButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (CheckChangeColumn.Width.Value == 0)
        //    {
        //        CheckSplitterColumn.Width = GridLength.Auto;
        //        if ((sender as Button).Content.ToString() == "Добавить")
        //        {
        //            CheckDataGrid.SelectedItem = null;
        //        }
        //    }
        //    else
        //    {
        //        CheckSplitterColumn.Width = new GridLength(0);
        //    }
        //}

        //private void CopyCheckButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (CheckChangeColumn.Width.Value == 0)
        //    {
        //        CheckChangeColumn.Width = new GridLength(250);
        //        CheckSplitterColumn.Width = GridLength.Auto;
        //        if (CheckDataGrid.SelectedItem != null)
        //        {
        //            CheckLabel.Content = "Выбран чек";
        //            ID_W = WorkComboBox.SelectedIndex;
        //            TIME_SALE = TIME_SALETextBox.Text;
        //            DATE_SALE = DATE_SALETextBox.Text;
        //            CheckDataGrid.IsHitTestVisible = false;
        //            CheckDataGrid.SelectedItem = null;
        //            WorkComboBox.SelectedIndex = ID_W;
        //            TIME_SALETextBox.Text = TIME_SALE;
        //            DATE_SALETextBox.Text = DATE_SALE;
        //        }
        //    }
        //    else
        //    {
        //        CheckChangeColumn.Width = new GridLength(0);
        //        CheckSplitterColumn.Width = new GridLength(0);
        //        CheckDataGrid.IsHitTestVisible = true;
        //    }
        //}

        //private void EditCheckButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (CheckChangeColumn.Width.Value == 0)
        //    {
        //        CheckChangeColumn.Width = new GridLength(250);
        //        CheckSplitterColumn.Width = GridLength.Auto;
        //    }
        //    else
        //    {
        //        CheckChangeColumn.Width = new GridLength(0);
        //        CheckSplitterColumn.Width = new GridLength(0);
        //    }
        //}

        private void DeleteCheckButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранную запись о чеке?", "Внимание!", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                try
                {
                    Data.CHECKS DeletingCheck = (Data.CHECKS)CheckDataGrid.SelectedItem;
                    if (CheckDataGrid.SelectedIndex < CheckDataGrid.Items.Count - 1)
                    {
                        CheckDataGrid.SelectedIndex++;
                    }
                    else
                    {
                        if (CheckDataGrid.SelectedIndex > 0)
                        {
                            CheckDataGrid.SelectedIndex--;
                        }
                    }
                    Data.CHECKS SelectingCheck = (Data.CHECKS)CheckDataGrid.SelectedItem;
                    CheckDataGrid.SelectedItem = DeletingCheck;
                    SourceCore.DB.CHECKS.Remove(DeletingCheck);
                    SourceCore.DB.SaveChanges();
                    UpdateGrid(SelectingCheck);
                }
                catch
                {
                    MessageBox.Show("Невозможно удалить запись, так как она используется в других справочниках базы данных.",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                }
                CheckDataGrid.Focus();
            }
        }

        private void CloseEdChangeClick(object sender, RoutedEventArgs e)
        {
            //CheckChangeColumn.Width = new GridLength(0);
            //CheckSplitterColumn.Width = new GridLength(0);
            //CheckDataGrid.IsHitTestVisible = true;
        }

        private void CommitButtonCheck(object sender, RoutedEventArgs e)
        {
            var S = new Data.CHECKS();
            //S.WORKERS = (Data.WORKERS)WorkComboBox.SelectedItem;
            //S.TIME_SALE = TimeSpan.Parse(TIME_SALETextBox.Text);
            //S.DATE_SALE = Convert.ToDateTime(DATE_SALETextBox.Text);
            if (CheckDataGrid.SelectedItem == null)
            {
                SourceCore.DB.CHECKS.Add(S);
            }
            SourceCore.DB.SaveChanges();
            UpdateGrid(S);
            CheckDataGrid.Focus();
            CloseEdChangeClick(sender, e);
        }

        public void UpdateGrid(Data.CHECKS Check)
        {
            if ((Check == null) && (CheckDataGrid.ItemsSource != null))
            {
                Check = (Data.CHECKS)CheckDataGrid.SelectedItem;
            }
            CheckDataGrid.ItemsSource = SourceCore.DB.CHECKS.ToList();
            CheckDataGrid.SelectedItem = Check;

            if ((Check == null) && (listBox1.ItemsSource != null))
            {
                Check = (Data.CHECKS)listBox1.SelectedItem;
            }
            listBox1.ItemsSource = SourceCore.DB.CHECKS.ToList();
            listBox1.SelectedItem = Check;
        }

        private void FilterCheckComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FilterCheckTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;
            switch (FilterCheckComboBox.SelectedIndex)
            {
                case 0:
                    CheckDataGrid.ItemsSource = SourceCore.DB.CHECKS.Where(filtercase => filtercase.WORKERS.FIO.Contains(textbox.Text)).ToList();
                    listBox1.ItemsSource = SourceCore.DB.CHECKS.Where(filrercase => filrercase.WORKERS.FIO.Contains(textbox.Text)).ToList();
                    break;
                case 1:
                    CheckDataGrid.ItemsSource = SourceCore.DB.CHECKS.Where(filtercase => filtercase.TIME_SALE.ToString().Contains(textbox.Text)).ToList();
                    listBox1.ItemsSource = SourceCore.DB.CHECKS.Where(filtercase => filtercase.TIME_SALE.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 2:
                    CheckDataGrid.ItemsSource = SourceCore.DB.CHECKS.Where(filtercase => filtercase.DATE_SALE.ToString().Contains(textbox.Text)).ToList();
                    listBox1.ItemsSource = SourceCore.DB.CHECKS.Where(filtercase => filtercase.DATE_SALE.ToString().Contains(textbox.Text)).ToList();
                    break;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<String> Columns = new List<string>();
            for (int I = 0; I < 3; I++)
            {
                Columns.Add(CheckDataGrid.Columns[I].Header.ToString());
            }
            FilterCheckComboBox.ItemsSource = Columns;
            FilterCheckComboBox.SelectedIndex = 0;
            foreach (DataGridColumn Column in CheckDataGrid.Columns)
            {
                Column.CanUserSort = false;
            }
        }

        private void AddShopListButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditShopListButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteShopListButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddNewCheck_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CheckDlgPage());
        }

        private void NewCheck_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CheckDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listBox1.SelectedItem = CheckDataGrid.SelectedItem;
            listBox1.Focus();
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckDataGrid.SelectedItem = listBox1.SelectedItem;
            CheckDataGrid.Focus();
        }
    }
}
