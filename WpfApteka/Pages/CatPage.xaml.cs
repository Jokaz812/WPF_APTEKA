using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for CatPage.xaml
    /// </summary>
    public partial class CatPage : Page
    {
        string NAME_CAT;
        public CatPage()
        {
            InitializeComponent();
            DataContext = this;
            CatDataGrid.ItemsSource = SourceCore.DB.CATEGORIES.ToList();
        }

        private void AddCatButton_Click(object sender, RoutedEventArgs e)
        {
            if (CatChangeColumn.Width.Value == 0)
            {
                CatChangeColumn.Width = new GridLength(250);
                CatSplitterColumn.Width = GridLength.Auto;
                if ((sender as Button).Content.ToString() == "Добавить")
                {
                    CatDataGrid.SelectedItem = null;
                }
            }
            else
            {
                CatChangeColumn.Width = new GridLength(0);
                CatSplitterColumn.Width = new GridLength(0);
            }
        }

        private void CopyCatButton_Click(object sender, RoutedEventArgs e)
        {
            if (CatChangeColumn.Width.Value == 0)
            {
                CatChangeColumn.Width = new GridLength(250);
                CatSplitterColumn.Width = GridLength.Auto;
                if (CatDataGrid.SelectedItem != null)
                {
                    CatLabel.Content = "Выбранная категория";
                    NAME_CAT = CatNameTextBox.Text;
                    CatDataGrid.SelectedItem = null;
                    CatNameTextBox.Text = NAME_CAT;
                    CatDataGrid.IsHitTestVisible = false;
                }
            }
            else
            {
                CatChangeColumn.Width = new GridLength(0);
                CatSplitterColumn.Width = new GridLength(0);
                CatDataGrid.IsHitTestVisible = true;
            }
        }

        private void EditCatButton_Click(object sender, RoutedEventArgs e)
        {
            if (CatChangeColumn.Width.Value == 0)
            {
                CatChangeColumn.Width = new GridLength(250);
                CatSplitterColumn.Width = GridLength.Auto;
            }
            else
            {
                CatChangeColumn.Width = new GridLength(0);
                CatSplitterColumn.Width = new GridLength(0);
            }
        }

        private void DeleteCatButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранную запись категории?", "Внимание!", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                try
                {
                    Data.CATEGORIES DeletingCat = (Data.CATEGORIES)CatDataGrid.SelectedItem;
                    if (CatDataGrid.SelectedIndex < CatDataGrid.Items.Count - 1)
                    {
                        CatDataGrid.SelectedIndex++;
                    }
                    else
                    {
                        if (CatDataGrid.SelectedIndex > 0)
                        {
                            CatDataGrid.SelectedIndex--;
                        }
                    }
                    Data.CATEGORIES SelectingCat = (Data.CATEGORIES)CatDataGrid.SelectedItem;
                    CatDataGrid.SelectedItem = DeletingCat;
                    SourceCore.DB.CATEGORIES.Remove(DeletingCat);
                    SourceCore.DB.SaveChanges();
                    UpdateGrid(SelectingCat);
                }
                catch
                {
                    MessageBox.Show("Невозможно удалить запись, так как она используется в других справочниках базы данных.",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                }
                CatDataGrid.Focus();
            }
        }

        private void CloseEdChangeClick(object sender, RoutedEventArgs e)
        {
            CatChangeColumn.Width = new GridLength(0);
            CatSplitterColumn.Width = new GridLength(0);
            CatDataGrid.IsHitTestVisible = true;
        }

        private void CommitButtonCat(object sender, RoutedEventArgs e)
        {
            var S = new Data.CATEGORIES();
            S.NAME_CAT = CatNameTextBox.Text;
            if (CatDataGrid.SelectedItem == null)
            {
                SourceCore.DB.CATEGORIES.Add(S);
            }
            SourceCore.DB.SaveChanges();
            UpdateGrid(S);
            CloseEdChangeClick(sender, e);
        }

        public void UpdateGrid(Data.CATEGORIES Cat)
        {
            if ((Cat == null) && (CatDataGrid.ItemsSource != null))
            {
                Cat = (Data.CATEGORIES)CatDataGrid.SelectedItem;
            }
            CatDataGrid.ItemsSource = SourceCore.DB.CATEGORIES.ToList();
            CatDataGrid.SelectedItem = Cat;
        }
    }
}
