using BlApi;
using BlImplementation;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            idEmptyLabel.Visibility = Visibility.Hidden;
            categoryEmptyLabel.Visibility = Visibility.Hidden;
            nameEmptyLabel.Visibility = Visibility.Hidden;
            priceEmptyLabel.Visibility = Visibility.Hidden;
            inStockEmptyLabel.Visibility = Visibility.Hidden;
        }
        public ProductWindow(int prtrLId)
        {
            InitializeComponent();
            CategoryCB.ItemsSource = Enum.GetValues(typeof(BO.Category));
            Complete.Content = "Update";
            BO.Product product = bl.Product.GetProductDetailsForManager(prtrLId);
            IdBox.Text = product.Id.ToString();
            IdBox.IsReadOnly = true;
            CategoryCB.Text = product.Category.ToString();
            NameBox.Text = product.Name;
            PriceBox.Text = product.Price.ToString();
            InStockBox.Text = product.InStock.ToString();
            idEmptyLabel.Visibility = Visibility.Hidden;
            categoryEmptyLabel.Visibility = Visibility.Hidden;
            nameEmptyLabel.Visibility = Visibility.Hidden;
            priceEmptyLabel.Visibility = Visibility.Hidden;
            inStockEmptyLabel.Visibility = Visibility.Hidden;

        }

        //private void CategoryCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}

        private void PreviewTextInputDecimal(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void PreviewTextInputDouble(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
            if (e.Text == "." && PriceBox.Text.Contains("."))
            {
                e.Handled = regex.IsMatch("[.]");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(IdBox.Text.Length == 0 ||
                   CategoryCB.Text.Length == 0 ||
                   NameBox.Text.Length ==0 ||
                   PriceBox.Text.Length == 0 ||
                   InStockBox.Text.Length == 0)
                {
                    if (IdBox.Text.Length == 0)
                        idEmptyLabel.Visibility = Visibility.Visible;
                    else
                       idEmptyLabel.Visibility = Visibility.Hidden;

                    if (CategoryCB.Text.Length == 0)
                        categoryEmptyLabel.Visibility = Visibility.Visible;
                    else
                        categoryEmptyLabel.Visibility = Visibility.Hidden;

                    if (NameBox.Text.Length == 0)
                        nameEmptyLabel.Visibility = Visibility.Visible;
                    else
                        nameEmptyLabel.Visibility = Visibility.Hidden;

                    if (PriceBox.Text.Length == 0)
                        priceEmptyLabel.Visibility = Visibility.Visible;
                    else
                        priceEmptyLabel.Visibility = Visibility.Hidden;

                    if (InStockBox.Text.Length == 0)
                        inStockEmptyLabel.Visibility = Visibility.Visible;
                    else
                        inStockEmptyLabel.Visibility = Visibility.Hidden;
                    return;
                }

                idEmptyLabel.Visibility = Visibility.Hidden;
                categoryEmptyLabel.Visibility = Visibility.Hidden;
                nameEmptyLabel.Visibility = Visibility.Hidden;
                priceEmptyLabel.Visibility = Visibility.Hidden;
                inStockEmptyLabel.Visibility = Visibility.Hidden;

                BO.Product newPdct = new BO.Product() { Id = int.Parse(IdBox.Text), Name = NameBox.Text, Price = double.Parse(PriceBox.Text), Category = (BO.Category)Enum.Parse(typeof(BO.Category), CategoryCB.Text), InStock = int.Parse(InStockBox.Text) };
                if (Complete.Content == "Add")
                {
                    bl.Product.AddProduct(newPdct);
                    MessageBox.Show("Adding is done!");
                }
                else if (Complete.Content == "Update")
                {
                    bl.Product.UpdateProduct(newPdct);
                    MessageBox.Show("Updating is done!");
                }
                this.Close();
            }
            catch(BO.NegativeNumberException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(BO.NegativeDoubleNumberException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.WrongLengthException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.NotValidFormatNameException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                if(Complete.Content=="Add")
                    MessageBox.Show("Error, you cant add the product!");
                if (Complete.Content == "Update")
                    MessageBox.Show("Error, you cant update the product!");
                this.Close();
            }
        }

        private void PWcloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
