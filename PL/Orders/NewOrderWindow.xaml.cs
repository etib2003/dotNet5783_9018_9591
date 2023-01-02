using BO;
using Cart;
using PL.productsWindows;
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
using System.Windows.Shapes;

namespace Products
{
    /// <summary>
    /// Interaction logic for NewOrderWindow.xaml
    /// </summary>
    public partial class NewOrderWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        BO.Cart cart;

        public ObservableCollection<ProductItem> ProductsItems { set; get; }
        public Array Categories { set; get; }
        private int selectedIndex;

        public NewOrderWindow(BO.Cart _cart)
        {
            cart = _cart;
            ProductsItems = new ObservableCollection<ProductItem>(bl?.Product.GetListProductForCatalog(cart));
            Categories = Enum.GetValues(typeof(BO.Category));
            InitializeComponent();
            //CatalogListView.ItemsSource = bl?.Product.GetListProductForCatalog(cart);
            //CategoryComboBox.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        private void viewProduct_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (CatalogListView.SelectedItem is ProductItem productItem)
            {
                selectedIndex = CatalogListView.SelectedIndex;
                int pflId = ((ProductItem)CatalogListView.SelectedItem).Id;
                new ProductWindow(pflId, cart, (productId) => ProductsItems[selectedIndex] = bl?.Product.GetProductDetailsForCustomer(productId, cart)).Show();
                //CatalogListView.ItemsSource = bl?.Product.GetListProductForCatalog(cart);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Category category = (BO.Category)CategoryComboBox.SelectedItem;
            List<ProductItem> objects = bl?.Product.GetListProductForCatalogView(cart, ProductsItems, x => (BO.Category)x?.Category! == category).ToList();

            if (objects.Any())
            {
                ProductsItems.Clear();

                foreach (var item in objects)
                {
                    ProductsItems.Add(item);
                }
            }
            //CatalogListView.ItemsSource = bl?.Product.GetListProductForCatalog(cart,x => (BO.Category)x?.Category! == category);
        }

        /// <summary>
        /// button that shows all the categories
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">the event</param>
        private void ShowAllCategories_Click(object sender, RoutedEventArgs e)
        {
            CatalogListView.ItemsSource = bl?.Product.GetListProductForCatalogView(cart, ProductsItems);
            CategoryComboBox.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        private void CartButton_Click(object sender, RoutedEventArgs e)
        {
            new CartWindow(cart).Show();
        }

        private void AddToCart(object sender, RoutedEventArgs e)
        {
            FrameworkElement frameworkElement = (sender as FrameworkElement)!;
            if (frameworkElement is not null && frameworkElement.DataContext is not null)
            {
                int productId = ((ProductItem)(frameworkElement.DataContext)).Id;
                bl.Cart.AddProductToCart(cart, productId);
            }

        }
    }
}
