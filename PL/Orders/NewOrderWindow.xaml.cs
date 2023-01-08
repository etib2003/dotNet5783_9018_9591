using BO;
using Cart;
using DO;
using PL;
using PL.productsWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Products
{
    /// <summary>
    /// Interaction logic for NewOrderWindow.xaml
    /// </summary>
    public partial class NewOrderWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public BO.Cart cart
        {
            get { return (BO.Cart)GetValue(cartProperty); }
            set { SetValue(cartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty cartProperty =
            DependencyProperty.Register("Cart", typeof(BO.Cart), typeof(NewOrderWindow));

        public ObservableCollection<ProductItem> ProductsItems
        {
            get => (ObservableCollection<ProductItem>)GetValue(ProductsDependency);
            private set => SetValue(ProductsDependency, value);
        }

        public static readonly DependencyProperty ProductsDependency
            = DependencyProperty.Register(nameof(ProductsItems), typeof(ObservableCollection<ProductItem>), typeof(NewOrderWindow));
        public Array Categories { set; get; }
        private int selectedIndex;

        public NewOrderWindow(BO.Cart _cart)
        {
            cart = _cart;
            ProductsItems = new ObservableCollection<ProductItem>(bl?.Product.GetListProductForCatalog(cart));
            Categories = Enum.GetValues(typeof(BO.Category));
            InitializeComponent();
     
        }

        private void restartAndAdd(IEnumerable<ProductItem> objects)
        {
            ProductsItems.Clear();

            foreach (var item in objects)
            {
                ProductsItems.Add(item);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Category category = (BO.Category)CategoryComboBox.SelectedItem;

            if (ProductsItems.Any(p => p.Category == category) == false)
                restartAndAdd(bl?.Product.GetListProductForCatalog(cart, x => (BO.Category)x?.Category! == category));
            else
            {
                List<ProductItem> objects = bl?.Product.GetListProductForCatalogView(cart, ProductsItems, x => (BO.Category)x?.Category! == category).ToList();
                if (objects.Any())
                {
                    restartAndAdd(objects);
                }
            }
        }

        /// <summary>
        /// button that shows all the categories
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">the event</param>
        private void ShowAllCategories_Click(object sender, RoutedEventArgs e)
        {
            restartAndAdd(bl?.Product.GetListProductForCatalog(cart));
            Categories.Clone();/////////////////
            Categories = Enum.GetValues(typeof(BO.Category)); //לא מסנן כשזה מסומן כבר על הקטגוריה
        }

        private void CartButton_Click(object sender, RoutedEventArgs e)
        {
            new CartWindow(cart/*, ProductsItems*/).Show();
            this.Close();
        }

        private void AddToCart(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameworkElement frameworkElement = (sender as FrameworkElement)!;
                int productId;
                if (frameworkElement is not null && frameworkElement.DataContext is not null)
                {
                    productId = ((ProductItem)(frameworkElement.DataContext)).Id;
                    bl?.Cart.AddProductToCart(cart, productId);
                    var p = ProductsItems.First(p => p.Id == productId);
                    ProductsItems[ProductsItems.IndexOf(p)] = bl?.Product.GetProductDetailsForCustomer(productId, cart);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Out of stock!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        } 
    }
}
