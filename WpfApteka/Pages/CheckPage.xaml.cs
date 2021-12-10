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
            WorkComboBox.ItemsSource = SourceCore.DB.WORKERS.ToList();
        }

        private void AddCheckButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckChangeColumn.Width.Value == 0)
            {
                CheckChangeColumn.Width = new GridLength(250);
                CheckSplitterColumn.Width = GridLength.Auto;
                if ((sender as Button).Content.ToString() == "Добавить")
                {
                    CheckDataGrid.SelectedItem = null;
                }
            }
            else
            {
                CheckChangeColumn.Width = new GridLength(0);
                CheckSplitterColumn.Width = new GridLength(0);
            }
        }

        private void CopyCheckButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckChangeColumn.Width.Value == 0)
            {
                CheckChangeColumn.Width = new GridLength(250);
                CheckSplitterColumn.Width = GridLength.Auto;
                if (CheckDataGrid.SelectedItem != null)
                {
                    CheckLabel.Content = "Выбран чек";
                    ID_W = WorkComboBox.SelectedIndex;
                    TIME_SALE = TIME_SALETextBox.Text;
                    DATE_SALE = DATE_SALETextBox.Text;
                    CheckDataGrid.IsHitTestVisible = false;
                    CheckDataGrid.SelectedItem = null;
                    WorkComboBox.SelectedIndex = ID_W;
                    TIME_SALETextBox.Text = TIME_SALE;
                    DATE_SALETextBox.Text = DATE_SALE;
                }
            }
            else
            {
                CheckChangeColumn.Width = new GridLength(0);
                CheckSplitterColumn.Width = new GridLength(0);
                CheckDataGrid.IsHitTestVisible = true;
            }
        }

        private void EditCheckButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckChangeColumn.Width.Value == 0)
            {
                CheckChangeColumn.Width = new GridLength(250);
                CheckSplitterColumn.Width = GridLength.Auto;
            }
            else
            {
                CheckChangeColumn.Width = new GridLength(0);
                CheckSplitterColumn.Width = new GridLength(0);
            }
        }

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
            CheckChangeColumn.Width = new GridLength(0);
            CheckSplitterColumn.Width = new GridLength(0);
            CheckDataGrid.IsHitTestVisible = true;
        }

        private void CommitButtonCheck(object sender, RoutedEventArgs e)
        {
            var S = new Data.CHECKS();
            S.WORKERS = (Data.WORKERS)WorkComboBox.SelectedItem;
            S.TIME_SALE = TimeSpan.Parse(TIME_SALETextBox.Text);
            S.DATE_SALE = Convert.ToDateTime(DATE_SALETextBox.Text);
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
        }

        private void FilterCheckComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FilterCheckTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
