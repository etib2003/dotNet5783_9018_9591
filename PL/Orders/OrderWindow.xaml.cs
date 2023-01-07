using BO;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
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

namespace Orders
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        private Action<int> action;

        public BO.Order Order
        {
            get { return (BO.Order)GetValue(OrderProperty); }
            set { SetValue(OrderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Order.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrderProperty =
            DependencyProperty.Register("Order", typeof(BO.Order), typeof(OrderWindow));

        
        public OrderWindow(int ordLId, Action<int> action)
        {
            this.action = action;
            Order = bl?.Order.GetOrderDetails(ordLId);
            InitializeComponent();
        }

        public OrderWindow(int ordLId, int different)
        {
            Order = bl?.Order.GetOrderDetails(ordLId);
            InitializeComponent();
            //OrderGrid.DataContext = Order;
            //OrderItemGrid.DataContext = Order.OrderItems;
        }


        private void ShipCheck_Checked(object sender, RoutedEventArgs e)
        {
            Order = bl?.Order.UpdateOrderShip(Order.Id);
            action(Order.Id);
        }

        private void DeliveryCheck_Checked(object sender, RoutedEventArgs e)
        {
            Order = bl?.Order.UpdateOrderDelivery(Order.Id);
            action(Order.Id);
        }
    }
}
