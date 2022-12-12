using BlApi;
using BlImplementation;
using BO;
using PL.productsWindows;
using System.Windows;


namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        ///Object to access the logical layer
        /// </summary>
        private IBl bl = new Bl();

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
    }
}