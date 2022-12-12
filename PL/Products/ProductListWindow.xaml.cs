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
        //Object to access the logical layer
        private IBl _bl = new Bl();

        /// <summary>
        /// get the list of products from the logical layer:
        /// </summary>
        public ProductListWindow()
        {
            InitializeComponent();        
            ProductForListView.ItemsSource = _bl.Product.GetListProductForManagerAndCatalog();
            selectCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));

        }
        /// <summary>
        ///  comboBox for choosing the wanted category of a product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">the event</param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Category category = (BO.Category)selectCategory.SelectedItem;
            ProductForListView.ItemsSource = _bl.Product.GetListProductForManagerAndCatalogByCond(x => x.Category == category);
        }

        /// <summary>
        /// button that shows all the categories
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">the event</param>
        private void ShowAllCategories_Click(object sender, RoutedEventArgs e)
        {
            ProductForListView.ItemsSource = _bl.Product.GetListProductForManagerAndCatalog();
            selectCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        /// <summary>
        /// update a product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">the event</param>
        private void Update_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ProductForListView.SelectedItem is ProductForList productForList)
            {
                int pflId = ((ProductForList)ProductForListView.SelectedItem).Id;
                new ProductWindow(pflId).ShowDialog();
                ShowAllCategories_Click(sender, e);
            }           
        }

        /// <summary>
        /// add a product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">the event</param>
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow().ShowDialog();
            ShowAllCategories_Click(sender,e);
        }

        /// <summary>
        /// close the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">the event</param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
