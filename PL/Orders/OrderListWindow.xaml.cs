using BO;
using PL.productsWindows;
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
    /// Interaction logic for OrderListWindow.xaml
    /// </summary>
    public partial class OrderListWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public OrderListWindow()
        {
            InitializeComponent();
            OrderForListView.ItemsSource = bl?.Order.GetOrderListForManager();
        }
     
            private void Update_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (OrderForListView.SelectedItem is OrderForList orderForList)
            {
                int oflId = ((OrderForList)OrderForListView.SelectedItem).Id;
                new OrderWindow(oflId).ShowDialog();
                OrderForListView.ItemsSource = bl?.Order.GetOrderListForManager();//רענון המסך
            }
        }
    }
}
