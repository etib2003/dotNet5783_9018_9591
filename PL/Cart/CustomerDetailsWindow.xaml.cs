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
    /// Interaction logic for CustomerDetailsWindow.xaml
    /// </summary>
    public partial class CustomerDetailsWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public BO.Cart cart
        {
            get { return (BO.Cart)GetValue(cartProperty); }
            set { SetValue(cartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for cart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty cartProperty =
            DependencyProperty.Register("cart", typeof(BO.Cart), typeof(CustomerDetailsWindow));



        //ObservableCollection<BO.OrderItem> productItems;
        public CustomerDetailsWindow(BO.Cart _cart/*, ObservableCollection<BO.OrderItem> _productItems*/)
        {
            //productItems = _productItems;
            cart = _cart;
            InitializeComponent();
        }

        private void endOrderbutton_Click(object sender, RoutedEventArgs e) //להוסיף בדיקות שנכנס משהו!
        {
            try
            {
                //Cart.CustomerName = NameTB.Text;
                //Cart.CustomerAddress = AddressTB.Text;
                //if (EmailTB.Text != null || EmailTB.Text != "")
                //    Cart.CustomerEmail = EmailTB.Text;             
                //bl?.Cart.CommitOrder(Cart);
                //new CompleteWindow(Cart).Show();
            cart.CustomerName = NameTB.Text;
            cart.CustomerAddress = AddressTB.Text;
            if (EmailTB.Text != null || EmailTB.Text != "")
                cart.CustomerEmail = EmailTB.Text;
            var order=bl?.Cart.CommitOrder(cart);
            //productItems = new ObservableCollection<BO.OrderItem>(cart.Items);
            new CompleteWindow(order).Show();
            this.Close();
            }
            catch (BO.NotValidEmailException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }         
        }
        //private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    if (e.Source == NameTB)
        //        NoName.Visibility = Visibility.Hidden;
        //    if (e.Source == AddressTB)
        //      NoAddress.Visibility = Visibility.Hidden;
        //    if (e.Source == CardNumberTB)
        //        NoCardNumber.Visibility = Visibility.Hidden;
        //    if (e.Source == CvvTB)
        //        NoCvv.Visibility = Visibility.Hidden;
        //    if (e.Source == ValidityTB)
        //        NoValidity.Visibility = Visibility.Hidden;
        //}

        //private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    if (e.Source ==  NameTB && string.IsNullOrEmpty(NameTB.Text))
        //        NoName.Visibility = Visibility.Visible;
        //    if (e.Source == AddressTB && string.IsNullOrEmpty(AddressTB.Text))
        //        NoAddress.Visibility = Visibility.Visible;
        //    if (e.Source == CardNumberTB && string.IsNullOrEmpty(CardNumberTB.Text))
        //        NoCardNumber.Visibility = Visibility.Visible;
        //    if (e.Source == CvvTB && string.IsNullOrEmpty(CvvTB.Text))
        //        NoCvv.Visibility = Visibility.Visible;
        //    if (e.Source == ValidityTB && string.IsNullOrEmpty(ValidityTB.Text))
        //        NoValidity.Visibility = Visibility.Visible;
        //}
    }
}
