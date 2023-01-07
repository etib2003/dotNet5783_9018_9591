using Managar;
using Orders;
using PL.productsWindows;
using Products;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        ///Object to access the logical layer.
        /// </summary>
         BlApi.IBl? bl= BlApi.Factory.Get();
        //BO.Cart Cart = new BO.Cart() { CustomerName = null, CustomerEmail = null, CustomerAddress = null, Items = new List<BO.OrderItem>(), TotalPrice = 0 };


        public BO.Cart cart
        {
            get { return (BO.Cart)GetValue(cartProperty); }
            set { SetValue(cartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty cartProperty =
            DependencyProperty.Register("Cart", typeof(BO.Cart), typeof(MainWindow));


    int i=1;

        /// <summary>
        /// constructor
        /// </summary>
        public MainWindow()
        {
            cart = new BO.Cart() { CustomerName = null, CustomerEmail = null, CustomerAddress = null, Items = new List<BO.OrderItem>(), TotalPrice = 0 };

            InitializeComponent();
        }

        /// <summary>
        /// close the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">the event</param>
        private void MWcloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void managerButton_Click(object sender, RoutedEventArgs e)
        {
            new ManagerAccessWindow().Show();
        }

        private void TrackingOrderButton_Click(object sender, RoutedEventArgs e)
        {
            new TrackOrderWindow().Show();
        }

        private void NewOrderButton_click(object sender, RoutedEventArgs e)
        {
            new  NewOrderWindow(cart).Show();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            i--;
            if(i<1)
            {
                i = 4;
            }
            PictureHolder.Source = new BitmapImage(new System.Uri("PictursForMain/" + i + ".jpg",System.UriKind.Relative));
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            i++;
            if (i > 4)
            {
                i = 1;
            }
            PictureHolder.Source = new BitmapImage(new System.Uri("PictursForMain/" + i + ".jpg", System.UriKind.Relative));
        }
    }
}