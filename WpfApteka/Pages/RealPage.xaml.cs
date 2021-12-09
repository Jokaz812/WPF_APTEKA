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
    /// Interaction logic for RealPage.xaml
    /// </summary>
    public partial class RealPage : Page
    {
        string NAME_REAL;
        public RealPage()
        {
            InitializeComponent();
            DataContext = this;
            RealDataGrid.ItemsSource = SourceCore.DB.RELEASE_FORM.ToList();
        }

        private void AddRealButton_Click(object sender, RoutedEventArgs e)
        {
            if (RealChangeColumn.Width.Value == 0)
            {
                RealChangeColumn.Width = new GridLength(250);
                RealSplitterColumn.Width = GridLength.Auto;
                if ((sender as Button).Content.ToString() == "Добавить")
                {
                    RealDataGrid.SelectedItem = null;
                }
            }
            else
            {
                RealChangeColumn.Width = new GridLength(0);
                RealSplitterColumn.Width = new GridLength(0);
            }
        }

        private void CopyRealButton_Click(object sender, RoutedEventArgs e)
        {
            if (RealChangeColumn.Width.Value == 0)
            {
                RealChangeColumn.Width = new GridLength(250);
                RealSplitterColumn.Width = GridLength.Auto;
                if (RealDataGrid.SelectedItem != null)
                {
                    RealLabel.Content = "Выбранная форма выпуска лекарства";
                    NAME_REAL = RealNameTextBox.Text;
                    RealDataGrid.SelectedItem = null;
                    RealNameTextBox.Text = NAME_REAL;
                    RealDataGrid.IsHitTestVisible = false;
                }
            }
            else
            {
                RealChangeColumn.Width = new GridLength(0);
                RealSplitterColumn.Width = new GridLength(0);
                RealDataGrid.IsHitTestVisible = true;
            }
        }

        private void EditRealButton_Click(object sender, RoutedEventArgs e)
        {
            if (RealChangeColumn.Width.Value == 0)
            {
                RealChangeColumn.Width = new GridLength(250);
                RealSplitterColumn.Width = GridLength.Auto;
            }
            else
            {
                RealChangeColumn.Width = new GridLength(0);
                RealSplitterColumn.Width = new GridLength(0);
            }
        }

        private void DeleteRealButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранную запись форму выпуска лекарства?", "Внимание!", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                try
                {
                    Data.RELEASE_FORM DeletingReal = (Data.RELEASE_FORM)RealDataGrid.SelectedItem;
                    if (RealDataGrid.SelectedIndex < RealDataGrid.Items.Count - 1)
                    {
                        RealDataGrid.SelectedIndex++;
                    }
                    else
                    {
                        if (RealDataGrid.SelectedIndex > 0)
                        {
                            RealDataGrid.SelectedIndex--;
                        }
                    }
                    Data.RELEASE_FORM SelectingReal = (Data.RELEASE_FORM)RealDataGrid.SelectedItem;
                    RealDataGrid.SelectedItem = DeletingReal;
                    SourceCore.DB.RELEASE_FORM.Remove(DeletingReal);
                    SourceCore.DB.SaveChanges();
                    UpdateGrid(SelectingReal);
                }
                catch
                {
                    MessageBox.Show("Невозможно удалить запись, так как она используется в других справочниках базы данных.",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                }
                RealDataGrid.Focus();
            }
        }

        private void CloseEdChangeClick(object sender, RoutedEventArgs e)
        {
            RealChangeColumn.Width = new GridLength(0);
            RealSplitterColumn.Width = new GridLength(0);
            RealDataGrid.IsHitTestVisible = true;
        }

        private void CommitButtonReal(object sender, RoutedEventArgs e)
        {
            var S = new Data.RELEASE_FORM();
            S.NAME_REAL = RealNameTextBox.Text;
            if (RealDataGrid.SelectedItem == null)
            {
                SourceCore.DB.RELEASE_FORM.Add(S);
            }
            SourceCore.DB.SaveChanges();
            UpdateGrid(S);
            CloseEdChangeClick(sender, e);
        }

        public void UpdateGrid(Data.RELEASE_FORM Real)
        {
            if ((Real == null) && (RealDataGrid.ItemsSource != null))
            {
                Real = (Data.RELEASE_FORM)RealDataGrid.SelectedItem;
            }
            RealDataGrid.ItemsSource = SourceCore.DB.RELEASE_FORM.ToList();
            RealDataGrid.SelectedItem = Real;
        }
    }
}
