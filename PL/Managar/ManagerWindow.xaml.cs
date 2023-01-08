using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Orders;
using PL.productsWindows;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public IEnumerable<OrderStatistics> Statistics
        {
            get { return (IEnumerable<OrderStatistics>)GetValue(StatisticsProperty); }
            set { SetValue(StatisticsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Statistics.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StatisticsProperty =
            DependencyProperty.Register("Statistics", typeof(IEnumerable<OrderStatistics>), typeof(ManagerWindow));

        public ManagerWindow()
        {
            try
            {
                Statistics = bl?.Order.GroupByStatistics();
                InitializeComponent();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message+"\n Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            new OrderListWindow(() => Statistics = Statistics.Select(s => s)).Show();
        }

        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {
            new ProductListWindow().Show();
        }      
    }
}
