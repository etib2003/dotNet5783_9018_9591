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

        Action _action;
        public OrderWindow(int ordLId, Action<int> action, Action _action)
        {
            this.action = action;
            this._action = _action;
            Order = bl?.Order.GetOrderDetails(ordLId);
            InitializeComponent();
        }

        public OrderWindow(int ordLId)
        {
            Order = bl?.Order.GetOrderDetails(ordLId);
            InitializeComponent();
        }


        private void ShipCheck_Checked(object sender, RoutedEventArgs e)
        {
            Order = bl?.Order.UpdateOrderShip(Order.Id);
            action(Order.Id);
            _action();
        }

        private void DeliveryCheck_Checked(object sender, RoutedEventArgs e)
        {
            Order = bl?.Order.UpdateOrderDelivery(Order.Id);
            action(Order.Id);
            _action();
        }
    }
}
