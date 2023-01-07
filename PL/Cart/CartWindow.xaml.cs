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

        public BO.Cart Cart
        {
            get { return (BO.Cart)GetValue(cartProperty); }
            set { SetValue(cartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty cartProperty =
            DependencyProperty.Register("Cart", typeof(BO.Cart), typeof(CartWindow));

        public ObservableCollection<BO.OrderItem?> cartItems { set; get; }

        public CartWindow(BO.Cart _cart, ObservableCollection<ProductItem> ProductItems)
        {
            Cart = _cart;          
            cartItems = new ObservableCollection<BO.OrderItem?>(Cart.Items);
            InitializeComponent();             
        }

        private void ContToPayButton_Click(object sender, RoutedEventArgs e)
        {
            new CustomerDetailsWindow(Cart, cartItems).Show();
            this.Close();
        }

        private void Add1(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameworkElement frameworkElement = (sender as FrameworkElement)!;
                int productId;
                int amount;
                if (frameworkElement is not null && frameworkElement.DataContext is not null)
                {
                    productId = ((OrderItem)(frameworkElement.DataContext)).ProductID;
                    amount= ((OrderItem)(frameworkElement.DataContext)).Amount;
                    Cart = bl?.Cart.UpdateAmountOfProduct(Cart, productId,amount+1);
                    //cartItems = Cart.Items;
                    var p = cartItems.First(p => p.ProductID == productId);
                    cartItems[cartItems.IndexOf(p)] = cartItems.FirstOrDefault(p => p.ProductID == productId);
                    CartItemsView.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Out of stock!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
            private void remove1(object sender, RoutedEventArgs e)
        { 
            try
            {
                FrameworkElement frameworkElement = (sender as FrameworkElement)!;
                int productId;
                int amount;
                if (frameworkElement is not null && frameworkElement.DataContext is not null)
                {                
                    productId = ((OrderItem)(frameworkElement.DataContext)).ProductID;
                    amount = ((OrderItem)(frameworkElement.DataContext)).Amount;
                    Cart =bl?.Cart.UpdateAmountOfProduct(Cart, productId, amount - 1);                
                    var p = cartItems.First(p => p.ProductID == productId);
                    cartItems[cartItems.IndexOf(p)] = cartItems.FirstOrDefault(p => p.ProductID == productId);
                    CartItemsView.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Out of stock!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void deleteProduct(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameworkElement frameworkElement = (sender as FrameworkElement)!;
                int productId;
                int amount;
                if (frameworkElement is not null && frameworkElement.DataContext is not null)
                {
                    productId = ((OrderItem)(frameworkElement.DataContext)).ProductID;
                    amount = ((OrderItem)(frameworkElement.DataContext)).Amount;
                    Cart = bl?.Cart.UpdateAmountOfProduct(Cart, productId, 0);
                    var p = cartItems.First(p => p.ProductID == productId);
                    cartItems.Remove(p);
                    CartItemsView.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Out of stock!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
