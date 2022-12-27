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

        /// <summary>
        /// constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// a button to the new window-productList
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {
            new ProductListWindow().ShowDialog();
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
            new  NewOrderWindow().Show();
        }
    }
}