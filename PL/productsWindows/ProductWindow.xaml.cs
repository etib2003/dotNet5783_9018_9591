using BlApi;
using BlImplementation;
using BO;
using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace PL.productsWindows
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private IBl bl = new Bl();
        public ProductWindow()
        {
            InitializeComponent();
            CategoryCB.ItemsSource = Enum.GetValues(typeof(BO.Category));
            Complete.Content = "Add";

        }
        public ProductWindow(int prtrLId)
        {
            InitializeComponent();
            CategoryCB.ItemsSource = Enum.GetValues(typeof(BO.Category));
            Complete.Content = "Update";
            BO.Product product = bl.Product.GetProductDetailsForManager(prtrLId);
            IdBox.Text = product.Id.ToString();
            IdBox.IsReadOnly=true;
            CategoryCB.Text = product.Category.ToString();
            NameBox.Text = product.Name;
            PriceBox.Text = product.Price.ToString();
            InStockBox.Text = product.InStock.ToString();
        }

        private void CategoryCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Product newPdct = new BO.Product() { Id = int.Parse(IdBox.Text), Name = NameBox.Text, Price = double.Parse(PriceBox.Text), Category = (BO.Category)Enum.Parse(typeof(BO.Category), CategoryCB.Text), Color = (BO.Color)Enum.Parse(typeof(BO.Color), "white"), InStock = int.Parse(InStockBox.Text) };
                if (Complete.Content == "Add")
                {
                    bl.Product.AddProduct(newPdct);
                    MessageBox.Show("adding is done!");
                }
                else if (Complete.Content == "Update")
                {
                    bl.Product.UpdateProduct(newPdct);
                    MessageBox.Show("updating is done!");
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, you cant add/update the product!");
                this.Close();
            }
        }

        private void PWcloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
