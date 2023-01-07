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
        BlApi.IBl? bl = BlApi.Factory.Get();
        public ObservableCollection<OrderForList> OrderForList { set; get; }
        private int selectedIndex;

        public OrderListWindow()
        {
            OrderForList = new ObservableCollection<OrderForList>(bl?.Order.GetOrderListForManager());
            InitializeComponent();
        }
     
            private void Update_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (OrderForListView.SelectedItem is OrderForList orderForList)
            {
                selectedIndex = OrderForListView.SelectedIndex;
                int oflId = ((OrderForList)OrderForListView.SelectedItem).Id;
                new OrderWindow(oflId, (orderId) => OrderForList[selectedIndex] = bl?.Order.GetOrderForList(orderId)).Show();
            }
        }       
    }
}
