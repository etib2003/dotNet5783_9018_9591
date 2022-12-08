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
         private IBl bl = new Bl();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {
            new ProductListWindow().ShowDialog();
        }

        private void MWcloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
