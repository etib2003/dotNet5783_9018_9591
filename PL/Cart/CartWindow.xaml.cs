using BO;
using Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Cart
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public BO.Cart cart { get; set; }
        public ObservableCollection<BO.OrderItem> cartItems { set; get; }

        public CartWindow(BO.Cart _cart, ObservableCollection<ProductItem> ProductItems)
        {
            cart = _cart;
            cartItems = new ObservableCollection<BO.OrderItem>(cart.Items);
            InitializeComponent();
             
        }

        private void ContToPayButton_Click(object sender, RoutedEventArgs e)
        {
            new CustomerDetailsWindow(cart, cartItems).Show();
            this.Close();
        }

        private void Add1(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameworkElement frameworkElement = (sender as FrameworkElement)!;
                int productId;
                if (frameworkElement is not null && frameworkElement.DataContext is not null)
                {
                    productId = ((OrderItem)(frameworkElement.DataContext)).ProductID;
                    bl?.Cart.AddProductToCart(cart, productId);
                    //cartItems = cart.Items;
                    var p = cartItems.First(p => p.ProductID == productId);
                    cartItems[cartItems.IndexOf(p)] = cartItems.FirstOrDefault(p=> p.ProductID == productId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Out of stock!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void remove1(object sender, RoutedEventArgs e)
        {

        }

        private void deleteProduct(object sender, RoutedEventArgs e)
        {

        }
    }
}
