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
    /// Interaction logic for FabPage.xaml
    /// </summary>
    public partial class FabPage : Page
    {
        string NAME_FAB;
        string ADDRESS;
        string CITY;
        string COUNTRY;
        string TELEPHONE;
        public FabPage()
        {
            InitializeComponent();
            DataContext = this;
            FabDataGrid.ItemsSource = SourceCore.DB.FABRICATOR.ToList();
        }

        private void AddFabButton_Click(object sender, RoutedEventArgs e)
        {
            if (FabChangeColumn.Width.Value == 0)
            {
                FabChangeColumn.Width = new GridLength(250);
                FabSplitterColumn.Width = GridLength.Auto;
                if ((sender as Button).Content.ToString() == "Добавить")
                {
                    FabDataGrid.SelectedItem = null;
                }
            }
            else
            {
                FabChangeColumn.Width = new GridLength(0);
                FabSplitterColumn.Width = new GridLength(0);
            }
        }

        private void CopyFabButton_Click(object sender, RoutedEventArgs e)
        {
            if (FabChangeColumn.Width.Value == 0)
            {
                FabChangeColumn.Width = new GridLength(250);
                FabSplitterColumn.Width = GridLength.Auto;
                if (FabDataGrid.SelectedItem != null)
                {
                    FabLabel.Content = "Выбран производитель";
                    NAME_FAB = NAME_FABTextBox.Text;
                    ADDRESS = ADDRESSTextBox.Text;
                    CITY = CITYTextBox.Text;
                    COUNTRY = COUNTRYTextBox.Text;
                    TELEPHONE = TELEPHONETextBox.Text;
                    FabDataGrid.IsHitTestVisible = false;
                    FabDataGrid.SelectedItem = null;
                    NAME_FABTextBox.Text = NAME_FAB;
                    ADDRESSTextBox.Text = ADDRESS;
                    CITYTextBox.Text = CITY;
                    COUNTRYTextBox.Text = COUNTRY;
                    TELEPHONETextBox.Text = TELEPHONE;
                }
            }
            else
            {
                FabChangeColumn.Width = new GridLength(0);
                FabSplitterColumn.Width = new GridLength(0);
                FabDataGrid.IsHitTestVisible = true;
            }
        }

        private void EditFabButton_Click(object sender, RoutedEventArgs e)
        {
            if (FabChangeColumn.Width.Value == 0)
            {
                FabChangeColumn.Width = new GridLength(250);
                FabSplitterColumn.Width = GridLength.Auto;
            }
            else
            {
                FabChangeColumn.Width = new GridLength(0);
                FabSplitterColumn.Width = new GridLength(0);
            }
        }

        private void DeleteFabButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранную запись о производителе?", "Внимание!", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                try
                {
                    Data.FABRICATOR DeletingFab = (Data.FABRICATOR)FabDataGrid.SelectedItem;
                    if (FabDataGrid.SelectedIndex < FabDataGrid.Items.Count - 1)
                    {
                        FabDataGrid.SelectedIndex++;
                    }
                    else
                    {
                        if (FabDataGrid.SelectedIndex > 0)
                        {
                            FabDataGrid.SelectedIndex--;
                        }
                    }
                    Data.FABRICATOR SelectingFab = (Data.FABRICATOR)FabDataGrid.SelectedItem;
                    FabDataGrid.SelectedItem = DeletingFab;
                    SourceCore.DB.FABRICATOR.Remove(DeletingFab);
                    SourceCore.DB.SaveChanges();
                    UpdateGrid(SelectingFab);
                }
                catch
                {
                    MessageBox.Show("Невозможно удалить запись, так как она используется в других справочниках базы данных.",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                }
                FabDataGrid.Focus();
            }
        }

        private void CloseEdChangeClick(object sender, RoutedEventArgs e)
        {
            FabChangeColumn.Width = new GridLength(0);
            FabSplitterColumn.Width = new GridLength(0);
            FabDataGrid.IsHitTestVisible = true;
        }

        private void CommitButtonFab(object sender, RoutedEventArgs e)
        {
            var S = new Data.FABRICATOR();
            S.NAME_FAB = NAME_FABTextBox.Text;
            S.ADDRESS = ADDRESSTextBox.Text;
            S.CITY = CITYTextBox.Text;
            S.COUNTRY = CITYTextBox.Text;
            S.TELEPHONE = TELEPHONETextBox.Text;
            if (FabDataGrid.SelectedItem == null)
            {
                SourceCore.DB.FABRICATOR.Add(S);
            }
            SourceCore.DB.SaveChanges();
            UpdateGrid(S);
            FabDataGrid.Focus();
            CloseEdChangeClick(sender, e);
        }

        public void UpdateGrid(Data.FABRICATOR Fab)
        {
            if ((Fab == null) && (FabDataGrid.ItemsSource != null))
            {
                Fab = (Data.FABRICATOR)FabDataGrid.SelectedItem;
            }
            FabDataGrid.ItemsSource = SourceCore.DB.FABRICATOR.ToList();
            FabDataGrid.SelectedItem = Fab;
        }

        private void FilterFabComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FilterFabTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;
            switch (FilterFabComboBox.SelectedIndex)
            {
                case 0:
                    FabDataGrid.ItemsSource = SourceCore.DB.FABRICATOR.Where(filtercase => filtercase.NAME_FAB.Contains(textbox.Text)).ToList();
                    break;
                case 1:
                    FabDataGrid.ItemsSource = SourceCore.DB.FABRICATOR.Where(filtercase => filtercase.ADDRESS.Contains(textbox.Text)).ToList();
                    break;
                case 2:
                    FabDataGrid.ItemsSource = SourceCore.DB.FABRICATOR.Where(filtercase => filtercase.CITY.Contains(textbox.Text)).ToList();
                    break;
                case 3:
                    FabDataGrid.ItemsSource = SourceCore.DB.FABRICATOR.Where(filtercase => filtercase.COUNTRY.Contains(textbox.Text)).ToList();
                    break;
                case 4:
                    FabDataGrid.ItemsSource = SourceCore.DB.FABRICATOR.Where(filtercase => filtercase.TELEPHONE.Contains(textbox.Text)).ToList();
                    break;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<String> Columns = new List<string>();
            for (int I = 0; I < 5; I++)
            {
                Columns.Add(FabDataGrid.Columns[I].Header.ToString());
            }
            FilterFabComboBox.ItemsSource = Columns;
            FilterFabComboBox.SelectedIndex = 0;
            foreach (DataGridColumn Column in FabDataGrid.Columns)
            {
                Column.CanUserSort = false;
            }
        }
    }
}
