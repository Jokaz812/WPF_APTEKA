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
    /// Interaction logic for MedPage.xaml
    /// </summary>
    public partial class MedPage : Page
    {
        string NAME_MED;
        int ID_C;
        string DESCRIPTION;
        int ID_R;
        int ID_F;
        public MedPage()
        {
            InitializeComponent();
            DataContext = this;
            MedDataGrid.ItemsSource = SourceCore.DB.MEDICINE.ToList();
            CatComboBox.ItemsSource = SourceCore.DB.CATEGORIES.ToList();
            RealComboBox.ItemsSource = SourceCore.DB.RELEASE_FORM.ToList();
            FabComboBox.ItemsSource = SourceCore.DB.FABRICATOR.ToList();
        }

        private void AddMedButton_Click(object sender, RoutedEventArgs e)
        {
            if (MedChangeColumn.Width.Value == 0)
            {
                MedChangeColumn.Width = new GridLength(250);
                MedSplitterColumn.Width = GridLength.Auto;
                if ((sender as Button).Content.ToString() == "Добавить")
                {
                    MedDataGrid.SelectedItem = null;
                }
            }
            else
            {
                MedChangeColumn.Width = new GridLength(0);
                MedSplitterColumn.Width = new GridLength(0);
            }
        }

        private void CopyMedButton_Click(object sender, RoutedEventArgs e)
        {
            if (MedChangeColumn.Width.Value == 0)
            {
                MedChangeColumn.Width = new GridLength(250);
                MedSplitterColumn.Width = GridLength.Auto;
                if (MedDataGrid.SelectedItem != null)
                {
                    MedLabel.Content = "Выбрано лекарство";
                    NAME_MED = NAME_MEDTextBox.Text;
                    ID_C = CatComboBox.SelectedIndex;
                    DESCRIPTION = DESCRIPTIONTextBox.Text;
                    ID_R = RealComboBox.SelectedIndex;
                    ID_F = FabComboBox.SelectedIndex;
                    MedDataGrid.IsHitTestVisible = false;
                    MedDataGrid.SelectedItem = null;

                    NAME_MEDTextBox.Text = NAME_MED;
                    CatComboBox.SelectedIndex = ID_C;
                    DESCRIPTIONTextBox.Text = DESCRIPTION;
                    RealComboBox.SelectedIndex = ID_R;
                    FabComboBox.SelectedIndex = ID_F;
                }
            }
            else
            {
                MedChangeColumn.Width = new GridLength(0);
                MedSplitterColumn.Width = new GridLength(0);
                MedDataGrid.IsHitTestVisible = true;
            }
        }

        private void EditMedButton_Click(object sender, RoutedEventArgs e)
        {
            if (MedChangeColumn.Width.Value == 0)
            {
                MedChangeColumn.Width = new GridLength(250);
                MedSplitterColumn.Width = GridLength.Auto;
            }
            else
            {
                MedChangeColumn.Width = new GridLength(0);
                MedSplitterColumn.Width = new GridLength(0);
            }
        }

        private void DeleteMedButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранную запись о лекарстве?", "Внимание!", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                try
                {
                    Data.MEDICINE DeletingMed = (Data.MEDICINE)MedDataGrid.SelectedItem;
                    if (MedDataGrid.SelectedIndex < MedDataGrid.Items.Count - 1)
                    {
                        MedDataGrid.SelectedIndex++;
                    }
                    else
                    {
                        if (MedDataGrid.SelectedIndex > 0)
                        {
                            MedDataGrid.SelectedIndex--;
                        }
                    }
                    Data.MEDICINE SelectingMed = (Data.MEDICINE)MedDataGrid.SelectedItem;
                    MedDataGrid.SelectedItem = DeletingMed;
                    SourceCore.DB.MEDICINE.Remove(DeletingMed);
                    SourceCore.DB.SaveChanges();
                    UpdateGrid(SelectingMed);
                }
                catch
                {
                    MessageBox.Show("Невозможно удалить запись, так как она используется в других справочниках базы данных.",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                }
                MedDataGrid.Focus();
            }
        }

        private void CloseEdChangeClick(object sender, RoutedEventArgs e)
        {
            MedChangeColumn.Width = new GridLength(0);
            MedSplitterColumn.Width = new GridLength(0);
            MedDataGrid.IsHitTestVisible = true;
        }

        private void CommitButtonMed(object sender, RoutedEventArgs e)
        {
            var S = new Data.MEDICINE();
            S.NAME_MED = NAME_MEDTextBox.Text;
            S.CATEGORIES = (Data.CATEGORIES)CatComboBox.SelectedItem;
            S.DESCRIPTION = DESCRIPTIONTextBox.Text;
            S.RELEASE_FORM = (Data.RELEASE_FORM)RealComboBox.SelectedItem;
            S.FABRICATOR = (Data.FABRICATOR)FabComboBox.SelectedItem;
            if (MedDataGrid.SelectedItem == null)
            {
                SourceCore.DB.MEDICINE.Add(S);
            }
            SourceCore.DB.SaveChanges();
            UpdateGrid(S);
            MedDataGrid.Focus();
            CloseEdChangeClick(sender, e);
        }

        public void UpdateGrid(Data.MEDICINE Med)
        {
            if ((Med == null) && (MedDataGrid.ItemsSource != null))
            {
                Med = (Data.MEDICINE)MedDataGrid.SelectedItem;
            }
            MedDataGrid.ItemsSource = SourceCore.DB.MEDICINE.ToList();
            MedDataGrid.SelectedItem = Med;
        }

        private void FilterMedComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FilterMedBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;
            switch (FilterMedComboBox.SelectedIndex)
            {
                case 0:
                    MedDataGrid.ItemsSource = SourceCore.DB.MEDICINE.Where(filtercase => filtercase.NAME_MED.Contains(textbox.Text)).ToList();
                    break;
                case 1:
                    MedDataGrid.ItemsSource = SourceCore.DB.MEDICINE.Where(filtercase => filtercase.CATEGORIES.NAME_CAT.Contains(textbox.Text)).ToList();
                    break;
                case 2:
                    MedDataGrid.ItemsSource = SourceCore.DB.MEDICINE.Where(filtercase => filtercase.DESCRIPTION.Contains(textbox.Text)).ToList();
                    break;
                case 3:
                    MedDataGrid.ItemsSource = SourceCore.DB.MEDICINE.Where(filtercase => filtercase.RELEASE_FORM.NAME_REAL.Contains(textbox.Text)).ToList();
                    break;
                case 4:
                    MedDataGrid.ItemsSource = SourceCore.DB.MEDICINE.Where(filtercase => filtercase.FABRICATOR.NAME_FAB.Contains(textbox.Text)).ToList();
                    break;
                case 5:
                    MedDataGrid.ItemsSource = SourceCore.DB.MEDICINE.Where(filtercase => filtercase.DATE_DELIVERY.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 6:
                    MedDataGrid.ItemsSource = SourceCore.DB.MEDICINE.Where(filtercase => filtercase.PRICE.ToString().Contains(textbox.Text)).ToList();
                    break;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<String> Columns = new List<string>();
            for (int I = 0; I < 7; I++)
            {
                Columns.Add(MedDataGrid.Columns[I].Header.ToString());
            }
            FilterMedComboBox.ItemsSource = Columns;
            FilterMedComboBox.SelectedIndex = 0;
            foreach (DataGridColumn Column in MedDataGrid.Columns)
            {
                Column.CanUserSort = false;
            }
        }
    }
}
