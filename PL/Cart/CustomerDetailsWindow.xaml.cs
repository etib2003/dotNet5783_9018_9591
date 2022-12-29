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

namespace Cart
{
    /// <summary>
    /// Interaction logic for CustomerDetailsWindow.xaml
    /// </summary>
    public partial class CustomerDetailsWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        BO.Cart cart;
        public CustomerDetailsWindow(BO.Cart _cart)
        { 
            cart = _cart;
            InitializeComponent();
        }

        private void endOrderbutton_Click(object sender, RoutedEventArgs e)
        {
            new CompleteWindow(cart).Show();
        }
        private void  GotFocus(object sender, RoutedEventArgs e)
        {
            if (e.Source == NameTB)
                NoName.Visibility = Visibility.Hidden;
            if (e.Source == AddressTB)
              NoAddress.Visibility = Visibility.Hidden;
            if (e.Source == CardNumberTB)
                NoCardNumber.Visibility = Visibility.Hidden;
            if (e.Source == CvvTB)
                NoCvv.Visibility = Visibility.Hidden;
            if (e.Source == ValidityTB)
                NoValidity.Visibility = Visibility.Hidden;
        }

        private void LoseFocus(object sender, RoutedEventArgs e)
        {


            if (e.Source ==  NameTB && string.IsNullOrEmpty(NameTB.Text))
                NoName.Visibility = Visibility.Visible;
            if (e.Source == AddressTB && string.IsNullOrEmpty(AddressTB.Text))
                NoAddress.Visibility = Visibility.Visible;
            if (e.Source == CardNumberTB && string.IsNullOrEmpty(CardNumberTB.Text))
                NoCardNumber.Visibility = Visibility.Visible;
            if (e.Source == CvvTB && string.IsNullOrEmpty(CvvTB.Text))
                NoCvv.Visibility = Visibility.Visible;
            if (e.Source == ValidityTB && string.IsNullOrEmpty(ValidityTB.Text))
                NoValidity.Visibility = Visibility.Visible;
        }
    }
}
