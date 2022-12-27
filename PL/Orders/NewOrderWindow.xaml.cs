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

namespace Products
{
    /// <summary>
    /// Interaction logic for NewOrderWindow.xaml
    /// </summary>
    public partial class NewOrderWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        private static BO.Cart _cart = new BO.Cart() { CustomerName = null, CustomerEmail = null, CustomerAddress = null, Items = new List<BO.OrderItem>(), TotalPrice = 0 };

        public NewOrderWindow()
        {
            InitializeComponent();
            CatalogListView.ItemsSource = bl?.Product.GetListProductForCatalog(_cart);
            //selectCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        private void view_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (CatalogListView.SelectedItem is ProductItem productItem)
            {
                int pflId = ((ProductItem)CatalogListView.SelectedItem).Id;
                new ProductWindow(pflId, 1).ShowDialog();
                //CatalogListView.ItemsSource = bl?.Product.GetListProductForCatalog(_cart);//
            }
        }
    }
}
