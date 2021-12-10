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
    /// Interaction logic for WorkPage.xaml
    /// </summary>
    public partial class WorkPage : Page
    {
        string FIO;
        string DATE_BIRTHDAY;
        string DATE_ACCEPTANCE;
        string CITY;
        string ADDRESS;
        string TELEPHONE;

        public WorkPage()
        {
            InitializeComponent();
            DataContext = this;
            WorkDataGrid.ItemsSource = SourceCore.DB.WORKERS.ToList();
        }

        private void AddWorkButton_Click(object sender, RoutedEventArgs e)
        {
            if (WorkChangeColumn.Width.Value == 0)
            {
                WorkChangeColumn.Width = new GridLength(250);
                WorkSplitterColumn.Width = GridLength.Auto;
                if ((sender as Button).Content.ToString() == "Добавить")
                {
                    WorkDataGrid.SelectedItem = null;
                }
            }
            else
            {
                WorkChangeColumn.Width = new GridLength(0);
                WorkSplitterColumn.Width = new GridLength(0);
            }
        }

        private void CopyWorkButton_Click(object sender, RoutedEventArgs e)
        {
            if (WorkChangeColumn.Width.Value == 0)
            {
                WorkChangeColumn.Width = new GridLength(250);
                WorkSplitterColumn.Width = GridLength.Auto;
                if (WorkDataGrid.SelectedItem != null)
                {
                    WorkLabel.Content = "Выбран сотрудник";
                    FIO = FIOTextBox.Text;
                    DATE_BIRTHDAY = DATE_BIRTHDAYTextBox.Text;
                    DATE_ACCEPTANCE = DATE_ACCEPTANCETextBox.Text;
                    CITY = CITYTextBox.Text;
                    ADDRESS = ADDRESSTextBox.Text;
                    TELEPHONE = TELEPHONETextBox.Text;
                    WorkDataGrid.IsHitTestVisible = false;
                    WorkDataGrid.SelectedItem = null;
                    FIOTextBox.Text = FIO;
                    DATE_BIRTHDAYTextBox.Text = DATE_BIRTHDAY;
                    DATE_ACCEPTANCETextBox.Text = DATE_ACCEPTANCE;
                    CITYTextBox.Text = CITY;
                    ADDRESSTextBox.Text = ADDRESS;
                    TELEPHONETextBox.Text = TELEPHONE;
                }
            }
            else
            {
                WorkChangeColumn.Width = new GridLength(0);
                WorkSplitterColumn.Width = new GridLength(0);
                WorkDataGrid.IsHitTestVisible = true;
            }
        }

        private void EditWorkButton_Click(object sender, RoutedEventArgs e)
        {
            if (WorkChangeColumn.Width.Value == 0)
            {
                WorkChangeColumn.Width = new GridLength(250);
                WorkSplitterColumn.Width = GridLength.Auto;
            }
            else
            {
                WorkChangeColumn.Width = new GridLength(0);
                WorkSplitterColumn.Width = new GridLength(0);
            }
        }

        private void DeleteWorkButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранную запись о сотруднике?", "Внимание!", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                try
                {
                    Data.WORKERS DeletingWork= (Data.WORKERS)WorkDataGrid.SelectedItem;
                    if (WorkDataGrid.SelectedIndex < WorkDataGrid.Items.Count - 1)
                    {
                        WorkDataGrid.SelectedIndex++;
                    }
                    else
                    {
                        if (WorkDataGrid.SelectedIndex > 0)
                        {
                            WorkDataGrid.SelectedIndex--;
                        }
                    }
                    Data.WORKERS SelectingWork= (Data.WORKERS)WorkDataGrid.SelectedItem;
                    WorkDataGrid.SelectedItem = DeletingWork;
                    SourceCore.DB.WORKERS.Remove(DeletingWork);
                    SourceCore.DB.SaveChanges();
                    UpdateGrid(SelectingWork);
                }
                catch
                {
                    MessageBox.Show("Невозможно удалить запись, так как она используется в других справочниках базы данных.",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                }
                WorkDataGrid.Focus();
            }
        }

        private void CloseEdChangeClick(object sender, RoutedEventArgs e)
        {
            WorkChangeColumn.Width = new GridLength(0);
            WorkSplitterColumn.Width = new GridLength(0);
            WorkDataGrid.IsHitTestVisible = true;
        }

        private void CommitButtonWork(object sender, RoutedEventArgs e)
        {
            var S = new Data.WORKERS();
            S.FIO = FIOTextBox.Text;
            S.DATE_BIRTHDAY = Convert.ToDateTime(DATE_BIRTHDAYTextBox.Text);
            S.DATE_ACCEPTANCE = Convert.ToDateTime(DATE_ACCEPTANCETextBox.Text);
            S.CITY = CITYTextBox.Text;
            S.ADDRESS = ADDRESSTextBox.Text;
            S.TELEPHONE = TELEPHONETextBox.Text;
            if (WorkDataGrid.SelectedItem == null)
            {
                SourceCore.DB.WORKERS.Add(S);
            }
            SourceCore.DB.SaveChanges();
            UpdateGrid(S);
            WorkDataGrid.Focus();
            CloseEdChangeClick(sender, e);
        }

        public void UpdateGrid(Data.WORKERS Work)
        {
            if ((Work == null) && (WorkDataGrid.ItemsSource != null))
            {
                Work = (Data.WORKERS)WorkDataGrid.SelectedItem;
            }
            WorkDataGrid.ItemsSource = SourceCore.DB.WORKERS.ToList();
            WorkDataGrid.SelectedItem = Work;
        }

        private void FilterWorkComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FilterWorkBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
