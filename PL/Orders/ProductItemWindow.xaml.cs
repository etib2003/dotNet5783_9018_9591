using BO;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using PL.productsWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Orders
{
    /// <summary>
    /// Interaction logic for ProductItemWindow.xaml
    /// </summary>
    public partial class ProductItemWindow : Window
    {

        BlApi.IBl? bl = BlApi.Factory.Get();

        public BO.Cart Cart
        {
            get { return (BO.Cart)GetValue(CartProperty); }
            set { SetValue(CartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CartProperty =
            DependencyProperty.Register("Cart", typeof(BO.Cart), typeof(ProductItemWindow));


        public BO.ProductItem NewPdctItem
        {
            get { return (BO.ProductItem)GetValue(NewPdctProperty); }
            set { SetValue(NewPdctProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NewPdct.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NewPdctProperty =
            DependencyProperty.Register("NewPdctItem", typeof(BO.ProductItem), typeof(ProductItemWindow));

        private Action? action;


        public ProductItemWindow(int id, BO.Cart _cart, Action action)
        {
            try
            {
                this.action = action;
                Cart = _cart;
                NewPdctItem = bl?.Product.GetProductDetailsForCustomer(id, Cart)!;
                InitializeComponent();
            }
            catch (BO.NegativeNumberException)
            {
                MessageBox.Show("Negative ID!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.WrongLengthException)
            {
                MessageBox.Show("Wrong length ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BoDoesNoExistException)
            {
                MessageBox.Show("No Order exists with this ID!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
