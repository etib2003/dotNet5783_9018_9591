using Managar;
using Orders;
using PL.productsWindows;
using Products;
using System.Collections.Generic;
using System.Windows;


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
        private static BO.Cart cart = new BO.Cart() { CustomerName = null, CustomerEmail = null, CustomerAddress = null, Items = new List<BO.OrderItem>(), TotalPrice = 0 };


        /// <summary>
        /// constructor
        /// </summary>
        public MainWindow()
        {
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

        private void MWcloseButton_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void Image_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void managerButton_Click(object sender, RoutedEventArgs e)
        {
            new ManagerAccessWindow().ShowDialog();
        }

        private void TrackingOrderButton_Click(object sender, RoutedEventArgs e)
        {
            new TrackOrderWindow().Show();
        }

        private void NewOrderButton_click(object sender, RoutedEventArgs e)
        {
            new  NewOrderWindow(cart).Show();
        }
    }
}