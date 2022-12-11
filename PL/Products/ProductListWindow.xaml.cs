using BlApi;
using BlImplementation;
using BO;
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
using System.Windows.Shapes;

namespace PL.productsWindows
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        private IBl _bl = new Bl();

        public ProductListWindow()
        {
            InitializeComponent();
            ProductForListView.ItemsSource = _bl.Product.GetListProductForManagerAndCatalog();
            selectCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Category category = (BO.Category)selectCategory.SelectedItem;
            ProductForListView.ItemsSource = _bl.Product.GetListProductForManagerAndCatalogByCond(x => x.Category == category);
        }

        private void ShowAllCategories_Click(object sender, RoutedEventArgs e)
        {
            ProductForListView.ItemsSource = _bl.Product.GetListProductForManagerAndCatalog();
            selectCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        private void ProductForListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        { 
            int pflId = ((ProductForList)ProductForListView.SelectedItem).Id;
            new ProductWindow(pflId).ShowDialog();
            ShowAllCategories_Click(sender, e);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow().ShowDialog();
            ShowAllCategories_Click(sender,e);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    
    }
}
