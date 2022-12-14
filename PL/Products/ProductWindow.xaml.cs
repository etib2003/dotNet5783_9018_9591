
using BO;
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
        /// <summary>
        ///Object to access the logical layer
        /// </summary>
        BlApi.IBl? bl = BlApi.Factory.Get();

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
            ProductGrid.DataContext = bl.Product.GetProductDetailsForManager(prtrLId);
            IdBox.IsReadOnly = true;

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
    }
}
