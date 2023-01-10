using BO;
using Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Cart
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        ICollectionView collectionView;
        public BO.Cart Cart
        {
            get { return (BO.Cart)GetValue(cartProperty); }
            set { SetValue(cartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty cartProperty =
            DependencyProperty.Register("Cart", typeof(BO.Cart), typeof(CartWindow));

        //public ObservableCollection<BO.OrderItem?> CartItems { set; get; }

        public CartWindow(BO.Cart _cart/*, ObservableCollection<ProductItem> ProductItems*/)
        {
            Cart = _cart;
            collectionView = CollectionViewSource.GetDefaultView(Cart.Items);
            //CartItems = new ObservableCollection<BO.OrderItem?>(Cart.Items);
            InitializeComponent();             
        }

        private void ContToPayButton_Click(object sender, RoutedEventArgs e)
        {
            new CustomerDetailsWindow(Cart).Show();
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
                    //var p = Cart.Items.First(p => p.ProductID == productId);
                    //Cart.Items[Cart.Items.IndexOf(p)] = p;
                    ////CartItems[Cart.Items.IndexOf(p)] = Cart.Items[Cart.Items.IndexOf(p)];
                    collectionView.Refresh();
                }
            }
            catch (Exception)
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
                    collectionView.Refresh();
                }
            }
            catch (Exception)
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
                    //var p = Cart.Items.First(p => p.ProductID == productId);
                    //Cart.Items.Remove(p);
                    //CartItems.Remove(p);

                    collectionView.Refresh();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Out of stock!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
