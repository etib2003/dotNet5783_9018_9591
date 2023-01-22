using BO;
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

namespace Orders
{
    /// <summary>
    /// Interaction logic for OrderListWindow.xaml
    /// </summary>
    public partial class OrderListWindow : Window
    {
        static readonly BlApi.IBl? bl = BlApi.Factory.Get();
        public ObservableCollection<OrderForList>? OrderForList { set; get; }
        private int selectedIndex;
        Action? action;
        //shows all the orders
        public OrderListWindow(Action action)
        {
            try
            {
                this.action = action;
                OrderForList = new ObservableCollection<OrderForList>(bl?.Order.GetOrderListForManager()!);
                InitializeComponent();
            }
            catch (BO.BoDoesNoExistException)
            {
                MessageBox.Show("We could not load the data..\n Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //update an order
        private void Update_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (OrderForListView.SelectedItem is OrderForList orderForList)
                {
                    selectedIndex = OrderForListView.SelectedIndex;
                    int oflId = ((OrderForList)OrderForListView.SelectedItem).Id;
                    new OrderWindow(oflId, (orderId) => OrderForList![selectedIndex] = bl?.Order.GetOrderForList(orderId)!, action!).Show();
                }
            }
            catch(BO.BoDoesNoExistException)
            {
                MessageBox.Show("We could not find the order\n Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
