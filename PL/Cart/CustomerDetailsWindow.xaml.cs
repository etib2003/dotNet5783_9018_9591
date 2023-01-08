﻿using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public BO.Cart Cart
        {
            get { return (BO.Cart)GetValue(cartProperty); }
            set { SetValue(cartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty cartProperty =
            DependencyProperty.Register("Cart", typeof(BO.Cart), typeof(CustomerDetailsWindow));

        public CustomerDetailsWindow(BO.Cart _cart)
        {
            Cart = _cart;
            InitializeComponent();
        }

        private void endOrderbutton_Click(object sender, RoutedEventArgs e) //להוסיף בדיקות שנכנס משהו!
        {
            try
            {
            var order=bl?.Cart.CommitOrder(Cart);
            new CompleteWindow(order).Show();
            this.Close();
            }
            catch (BO.NotValidEmailException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }         
        }
        private void PreviewTextInputDecimal(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
