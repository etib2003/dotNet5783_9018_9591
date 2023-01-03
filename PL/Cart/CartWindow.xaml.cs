using BO;
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

        public CartWindow(BO.Cart _cart)
        {
            cart = _cart;
            cartItems = new ObservableCollection<BO.OrderItem>(cart.Items);
            InitializeComponent();
            //CartGrid.DataContext = cart;
            //CartItemsView.ItemsSource = cart.Items;
        }

        private void ContToPayButton_Click(object sender, RoutedEventArgs e)
        {
            new CustomerDetailsWindow(cart).Show();
        }
    }
}
