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

namespace Orders
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        private Action<int>? action;
        Action? _action;

        public BO.Order Order
        {
            get { return (BO.Order)GetValue(OrderProperty); }
            set { SetValue(OrderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Order.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrderProperty =
            DependencyProperty.Register("Order", typeof(BO.Order), typeof(OrderWindow));

        public bool ViewCondition
        {
            get { return (bool)GetValue(ViewConditionProperty); }
            set { SetValue(ViewConditionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewCondition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewConditionProperty =
            DependencyProperty.Register("ViewCondition", typeof(bool), typeof(OrderWindow));

        //show all the orders without an option to update
        public OrderWindow(int ordLId, Action<int> action, Action _action)
        {
            try
            {
                this.action = action;
                this._action = _action;
                Order = bl?.Order.GetOrderDetails(ordLId)!;
                ViewCondition = false;
                InitializeComponent();
            }
            catch(BO.NegativeNumberException)
            {
                MessageBox.Show("Negative ID!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BoDoesNoExistException)
            {
                MessageBox.Show("No Order exists with this ID!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //show all the orders with an option to update
        public OrderWindow(int ordLId)
        {
            try
            {
                Order = bl?.Order.GetOrderDetails(ordLId)!;
                ViewCondition = true;
                InitializeComponent();
            }
            catch (BO.NegativeNumberException)
            {
                MessageBox.Show("Negative ID!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BoDoesNoExistException)
            {
                MessageBox.Show("No Order exists with this ID!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //update the shipDate
        private void ShipCheck_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Order = bl?.Order.UpdateOrderShip(Order.Id)!;
                action!(Order.Id);
                _action!();
            }
            catch (BO.NegativeNumberException)
            {
                MessageBox.Show("Negative ID!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BoDoesNoExistException)
            {
                MessageBox.Show("No Order exists with this ID!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(BO.DateAlreadyUpdatedException ex)
            {
                MessageBox.Show(ex.Message+"\nPlease try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //update the deliveryDate
        private void DeliveryCheck_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Order = bl?.Order.UpdateOrderDelivery(Order.Id)!;
                action!(Order.Id);
                _action!();
            }
            catch (BO.NegativeNumberException)
            {
                MessageBox.Show("Negative ID!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BoDoesNoExistException)
            {
                MessageBox.Show("No Order exists with this ID!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.DateHasNotUpdatedYetException ex)
            {
                MessageBox.Show(ex.Message + "\nPlease try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(BO.DateAlreadyUpdatedException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
