using BO;
using PL;
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

        Action<BO.Cart> action;
        public CartWindow(BO.Cart cart, Action<BO.Cart> action)
        {
            this.action = action;
            Cart = cart;
            collectionView = CollectionViewSource.GetDefaultView(Cart.Items);
            InitializeComponent();
        }

        //a button to continue to payment
        private void ContToPayButton_Click(object sender, RoutedEventArgs e)
        {
            new CustomerDetailsWindow(Cart, action).Show();
            this.Close();
        }

        //add a product to the cart
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
                    amount = ((OrderItem)(frameworkElement.DataContext)).Amount;
                    Cart = bl?.Cart.UpdateAmountOfProduct(Cart, productId, amount + 1)!;
                    action(Cart);
                    collectionView.Refresh();
                }
            }
            catch (BO.NotInStockException)
            {
                MessageBox.Show("Out of stock!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BoDoesNoExistException)
            {
                MessageBox.Show("We could not load the data..\n Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
        //remove a product from the cart
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
                    Cart = bl?.Cart.UpdateAmountOfProduct(Cart, productId, amount - 1)!;
                    collectionView.Refresh();
                    action(Cart);
                }
            }
            catch (BO.BoDoesNoExistException)
            {
                MessageBox.Show("We could not load the data..\n Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //delete a product from the cart
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
                    Cart = bl?.Cart.UpdateAmountOfProduct(Cart, productId, 0)!;
                    action(Cart);
                    collectionView.Refresh();
                }
            }
            catch (BO.BoDoesNoExistException)
            {
                MessageBox.Show("We could not load the data..\n Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
