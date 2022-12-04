using BlApi;
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

namespace PL.productsWindows
{
    /// <summary>
    /// Interaction logic for ProductForListWindow.xaml
    /// </summary>
    public partial class ProductForListWindow : Window
    {
        private IBl bl;

        public ProductForListWindow(IBl bl)
        {
            InitializeComponent();
            this.bl = bl;
            PLV.ItemsSource = bl.Product.GetListProductForManagerAndCatalog();
        }
    }
}
