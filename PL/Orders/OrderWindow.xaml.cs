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

        public bool ShipCheckVar
        {
            get { return (bool)GetValue(ShipCheckVarProperty); }
            set { SetValue(ShipCheckVarProperty, value);
                if (ShipCheckVar)
                {
                    Order = bl?.Order.UpdateOrderShip(Order.Id);
                    action(Order.Id);
                }             
            }
        }

        // Using a DependencyProperty as the backing store for ShipCheckVar.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShipCheckVarProperty =
            DependencyProperty.Register("ShipCheckVar", typeof(bool?), typeof(OrderWindow));

        public bool DeliveryCheckVar
        {
            get { return (bool)GetValue(DeliveryCheckVarProperty); }
            set { SetValue(DeliveryCheckVarProperty, value);
                if (DeliveryCheckVar)
                {
                    Order = bl?.Order.UpdateOrderDelivery(Order.Id);
                    action(Order.Id); 
                }
            }
        }

        // Using a DependencyProperty as the backing store for ShipCheckVar.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeliveryCheckVarProperty =
            DependencyProperty.Register("DeliveryCheckVar", typeof(bool?), typeof(OrderWindow));

        public OrderWindow(int ordLId, Action<int> action)
        {
            this.action = action;
            Order = bl?.Order.GetOrderDetails(ordLId);
            InitializeComponent();

            //if (Order.ShipDate == null)
            //    ShipCheck.Visibility = Visibility.Visible;
            //if (Order.DeliveryDate == null)
            //    DeliveryCheck.Visibility = Visibility.Visible;
        }

        public OrderWindow(int ordLId, int different)
        {
            Order = bl?.Order.GetOrderDetails(ordLId);
            InitializeComponent();
            o_OkButton.Visibility = Visibility.Collapsed;
            //OrderGrid.DataContext = Order;
            //OrderItemGrid.DataContext = Order.OrderItems;
        }

        private void o_OkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                

            }
            catch (BO.DateHasNotUpdatedYetException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //this.Close();
        }
    }
}
