
using BO;
using DocumentFormat.OpenXml.Vml;
using System;
using System.Collections.Generic;
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
        /// <summary>
        ///Object to access the logical layer
        /// </summary>
        BlApi.IBl? bl = BlApi.Factory.Get();
        private static BO.Cart _cart = new BO.Cart() { CustomerName = null, CustomerEmail = null, CustomerAddress = null, Items = new List<BO.OrderItem>(), TotalPrice = 0 };


        /// <summary>
        /// constructor, bruild the labels
        /// </summary>
        public ProductWindow()
        {
            InitializeComponent();
            CategoryCB.ItemsSource = Enum.GetValues(typeof(BO.Category));
            Complete.Content = "Add";
  
        }

        /// <summary>
        /// get the wanted product from the logical layer and put its detailes in the boxes
        /// </summary>
        /// <param name="prtrLId">id of a product</param>
        public ProductWindow(int prtrLId)
        {
            InitializeComponent();
            CategoryCB.ItemsSource = Enum.GetValues(typeof(BO.Category));
            Complete.Content = "Update";
            deleteButton.Visibility = Visibility.Visible;
            ProductGrid.DataContext = bl?.Product.GetProductDetailsForManager(prtrLId);
            IdBox.IsReadOnly = true;

        }

        public ProductWindow(int prtrLId, int different)
        {
            InitializeComponent();
            Complete.Content = "Add to cart";
            var product= bl?.Product.GetProductDetailsForManager(prtrLId);
            ProductGrid.DataContext = product;
            CategoryCB.Visibility = Visibility.Collapsed;
            CategoryBox.Visibility = Visibility.Visible;
            CategoryBox.Text = product!.Category.ToString();
            IdBox.IsReadOnly = true;
            NameBox.IsReadOnly = true;
            PriceBox.IsReadOnly = true;
            InStockBox.IsReadOnly = true;
            AmountAddBox.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// makes sure the id is valid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">the event</param>
        private void PreviewTextInputDecimal(object sender, TextCompositionEventArgs e)
        {
            if (e.Source == IdBox)
                if (IdBox.Text.Length == 6)
                {
                    e.Handled = true;
                    return;
                }
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        /// makes sure the price is valid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">the event</param>
        private void PreviewTextInputDouble(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
            if (e.Text == "." && PriceBox.Text.Contains("."))
            {
                e.Handled = regex.IsMatch("[.]");
            }
        }

        /// <summary>
        /// Makes sure that when the user stands on the box the warning disappears
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">the event</param>
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (e.Source == IdBox && string.IsNullOrEmpty(IdBox.Text))
                idEmptyLabel.Visibility = Visibility.Visible;
            if (e.Source == NameBox && string.IsNullOrEmpty(NameBox.Text))
                nameEmptyLabel.Visibility = Visibility.Visible;
            if (e.Source == PriceBox && string.IsNullOrEmpty(PriceBox.Text))
                priceEmptyLabel.Visibility = Visibility.Visible;
            if (e.Source == InStockBox && string.IsNullOrEmpty(InStockBox.Text))
                inStockEmptyLabel.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Makes sure that when the user doesnt stands on the box the warning shows if needed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (e.Source == IdBox)
                idEmptyLabel.Visibility = Visibility.Hidden;
            if (e.Source == NameBox)
                nameEmptyLabel.Visibility = Visibility.Hidden;
            if (e.Source == PriceBox)
                priceEmptyLabel.Visibility = Visibility.Hidden;
            if (e.Source == InStockBox)
                inStockEmptyLabel.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Shows warnings if needed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">the event</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IdBox.Text.Length == 0 ||
                   CategoryCB.Text.Length == 0 ||
                   NameBox.Text.Length == 0 ||
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
                    bl?.Product.AddProduct(newPdct);
                    Console.Beep(1500, 100);
                    MessageBox.Show("Adding is done!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (Complete.Content == "Update")
                {
                    bl?.Product.UpdateProduct(newPdct);
                    Console.Beep(1500, 100);
                    MessageBox.Show("Updating is done!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (Complete.Content == "Add to cart")
                {
                    if (AmountAddBox.Text != "0") //מומלץ לעשות שלא יוכל להכניס 0
                    {
                        bl?.Cart.AddProductToCart(_cart, newPdct.Id);
                        if (AmountAddBox.Text != "Enter amount")
                            bl?.Cart.UpdateAmountOfProduct(_cart, newPdct.Id, int.Parse(AmountAddBox.Text));

                        Console.Beep(1500, 100);
                        MessageBox.Show("Adding to cart is done!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                this.Close();
            }
            catch (BoAlreadyExistsException ex)
            {
                MessageBox.Show("Product with this barcode already exists!\nPlease try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.NegativeNumberException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.NegativeDoubleNumberException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.WrongLengthException ex)
            {
                MessageBox.Show("Too short id, please try again!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.NotValidFormatNameException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                if (Complete.Content == "Add")
                    MessageBox.Show("Error, you cant add the product!");
                if (Complete.Content == "Update")
                    MessageBox.Show("Error, you cant update the product!");
                this.Close();
            }
        }
        /// <summary>
        /// close the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">the event</param>
        private void PWcloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            //AmountAddBox.Text = "";
        }

        private void OnTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            //AmountAddBox.Text = "Enter amount";
        }
    }
}
