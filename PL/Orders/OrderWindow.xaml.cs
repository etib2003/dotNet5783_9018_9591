using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
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

namespace Orders
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        private Action<int> action;


        public OrderWindow(int ordLId, Action<int> action)
        {
            this.action = action;

            InitializeComponent();
            var order = bl?.Order.GetOrderDetails(ordLId);
            OrderGrid.DataContext = order;
            OrderItemGrid.DataContext = order.OrderItems;
            if (order.ShipDate == null)
                ShipCheck.Visibility = Visibility.Visible;
            if (order.DeliveryDate == null)
                DeliveryCheck.Visibility = Visibility.Visible;
        }

        public OrderWindow(int ordLId, int different)
        {
            InitializeComponent();
            var order = bl?.Order.GetOrderDetails(ordLId);
            OrderGrid.DataContext = order;
            OrderItemGrid.DataContext = order.OrderItems;
        }

        private void o_OkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ShipCheck.IsChecked == true)
                    bl?.Order.UpdateOrderShip(int.Parse(IdTextBlock.Text));

                if (DeliveryCheck.IsChecked == true)
                    bl?.Order.UpdateOrderDelivery(int.Parse(IdTextBlock.Text));
            }
            catch(BO.DateHasNotUpdatedYetException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Close();
        }
    }
}
