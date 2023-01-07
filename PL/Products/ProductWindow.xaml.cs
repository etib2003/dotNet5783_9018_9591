
using DocumentFormat.OpenXml.Vml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Printing.IndexedProperties;
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
        BO.Cart cart;
        public Array Categories { set; get; }
        //public BO.Product NewPdct { get; set; }



        public BO.Product NewPdct
        {
            get { return (BO.Product)GetValue(NewPdctProperty); }
            set { SetValue(NewPdctProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NewPdct.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NewPdctProperty =
            DependencyProperty.Register("NewPdct", typeof(BO.Product), typeof(ProductWindow));






        private Action<int> action;

        public string CompleteButton
        {
            get { return (string)GetValue(CompleteButtonProperty); }
            set { SetValue(CompleteButtonProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CompleteButton.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CompleteButtonProperty =
            DependencyProperty.Register("CompleteButton", typeof(string), typeof(ProductWindow));


        /// <summary>
        /// constructor, bruild the labels
        /// </summary>
        public ProductWindow(Action<int> action) 
        {
            this.action = action;
            NewPdct = new BO.Product();
            Categories = Enum.GetValues(typeof(BO.Category));
            InitializeComponent();
            CompleteButton = "Add";
        }

        /// <summary>
        /// get the wanted product from the logical layer and put its detailes in the boxes
        /// </summary>
        /// <param name="prtrLId">id of a product</param>
        public ProductWindow(int prtrLId, Action<int> action) 
        {
            this.action = action;
            NewPdct = bl?.Product.GetProductDetailsForManager(prtrLId);
            Categories = Enum.GetValues(typeof(BO.Category));
            InitializeComponent();
            CompleteButton = "Update";
        }

        //public ProductWindow(int prtrLId, BO.Cart _cart, Action<int> action)
        //{
        //    this.action = action;
        //    NewPdct = bl?.Product.GetProductDetailsForManager(prtrLId);
        //   // DataContext = this;
        //    InitializeComponent();
        //    Complete.Content = "Add to cart";
        //    cart = _cart;
        //    CategoryCB.Visibility = Visibility.Collapsed;
        //    CategoryBox.Visibility = Visibility.Visible;
        //    CategoryBox.Text = NewPdct!.Category.ToString();
        //    IdBox.IsReadOnly = true;
        //    NameBox.IsReadOnly = true;
        //    PriceBox.IsReadOnly = true;
        //    InStockBox.IsReadOnly = true;
        //    AmountAddBox.Visibility = Visibility.Visible;
        //}

        /// <summary>
        /// makes sure the id is valid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">the event</param>
        private void PreviewTextInputDecimal(object sender, TextCompositionEventArgs e)
        {
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
            if (e.Text == "." && PriceBox.Text.Contains(".")) //$
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
            //if (e.Source == IdBox && string.IsNullOrEmpty(IdBox.Text))
            //    idEmptyLabel.Visibility = Visibility.Visible;
            //if (e.Source == NameBox && string.IsNullOrEmpty(NameBox.Text))
            //    nameEmptyLabel.Visibility = Visibility.Visible;
            //if (e.Source == PriceBox && string.IsNullOrEmpty(PriceBox.Text))
            //    priceEmptyLabel.Visibility = Visibility.Visible;
            //if (e.Source == InStockBox && string.IsNullOrEmpty(InStockBox.Text))
            //    inStockEmptyLabel.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Makes sure that when the user doesnt stands on the box the warning shows if needed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            //if (e.Source == IdBox)
            //    idEmptyLabel.Visibility = Visibility.Hidden;
            //if (e.Source == NameBox)
            //    nameEmptyLabel.Visibility = Visibility.Hidden;
            //if (e.Source == PriceBox)
            //    priceEmptyLabel.Visibility = Visibility.Hidden;
            //if (e.Source == InStockBox)
            //    inStockEmptyLabel.Visibility = Visibility.Hidden;
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
                if (!(NewPdct.Id > 0 && NewPdct.Category != null &&
                    NewPdct.Name.Length > 0 && NewPdct.Price > 0 &&
                    NewPdct.InStock >= 0)) { 
                    //if (IdBox.Text.Length == 0 || IdBox.Text == "0" ||
                    //   CategoryCB.Text.Length == 0 ||
                    //   NameBox.Text.Length == 0 ||
                    //   PriceBox.Text.Length == 0 || PriceBox.Text == "0" ||
                    //   InStockBox.Text.Length == 0)
                    //{
                    //    //if (IdBox.Text.Length == 0 || IdBox.Text=="0")
                    //    //    idEmptyLabel.Visibility = Visibility.Visible;
                    //    //else
                    //    //    idEmptyLabel.Visibility = Visibility.Hidden;

                    //    //if (CategoryCB.Text.Length == 0)
                    //    //    categoryEmptyLabel.Visibility = Visibility.Visible;
                    //    //else
                    //    //    categoryEmptyLabel.Visibility = Visibility.Hidden;

                    //    //if (NameBox.Text.Length == 0)
                    //    //    nameEmptyLabel.Visibility = Visibility.Visible;
                    //    //else
                    //    //    nameEmptyLabel.Visibility = Visibility.Hidden;

                    //    //if (PriceBox.Text.Length == 0 || PriceBox.Text=="0")
                    //    //    priceEmptyLabel.Visibility = Visibility.Visible;
                    //    //else
                    //    //    priceEmptyLabel.Visibility = Visibility.Hidden;

                    //    if (InStockBox.Text.Length == 0)
                    //        inStockEmptyLabel.Visibility = Visibility.Visible;
                    //    else
                    //        inStockEmptyLabel.Visibility = Visibility.Hidden;
                    return;
                }
                
                //יש מצב שלא צריך את זה
                //idEmptyLabel.Visibility = Visibility.Hidden; 
                //categoryEmptyLabel.Visibility = Visibility.Hidden;
                //nameEmptyLabel.Visibility = Visibility.Hidden;
                //priceEmptyLabel.Visibility = Visibility.Hidden;
                //inStockEmptyLabel.Visibility = Visibility.Hidden;

                //BO.Product newPdct = new BO.Product() { Id = int.Parse(IdBox.Text), Name = NameBox.Text, Price = double.Parse(PriceBox.Text), Category = (BO.Category)Enum.Parse(typeof(BO.Category), CategoryCB.Text), InStock = int.Parse(InStockBox.Text) };
                if (CompleteButton == "Add")
                {
                    bl?.Product.AddProduct(NewPdct);
                    action(NewPdct.Id);
                    MessageBox.Show("Adding is done!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (CompleteButton == "Update")
                {
                    bl?.Product.UpdateProduct(NewPdct);
                    action(NewPdct.Id);                   
                    MessageBox.Show("Updating is done!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                //else if (Complete.Content == "Add to cart") //לבדוק מה הולך פה
                //{
                //    //if (AmountAddBox.Text != "0") //מומלץ לעשות שלא יוכל להכניס 0
                //    //{
                //        bl?.Cart.AddProductToCart(cart, NewPdct.Id); 
                //        if (AmountAddBox.Text != "Enter amount") //יש בעיה כי אם אין מספיק במלאי הוא עדיין יעשה הוספה של אחד ותכלס זה בעיה
                //            bl?.Cart.UpdateAmountOfProduct(cart, NewPdct.Id, int.Parse(AmountAddBox.Text));

                //    action(NewPdct.Id);
                //    MessageBox.Show("Adding to cart is done!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                //    //}
                //}
                this.Close();
            }
            catch (BO.BoAlreadyExistsException ex)
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
                if (CompleteButton == "Add")
                    MessageBox.Show("Error, you cant add the product!");
                if (CompleteButton == "Update")
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

        ////זה בשביל הוספה לסל, לוודא אם צריך בסוף או לא
        //private void OnTextBoxGotFocus(object sender, RoutedEventArgs e)
        //{
        //    if (AmountAddBox.Text == "Enter amount")
        //        AmountAddBox.Text = "";
        //}

        //private void OnTextBoxLostFocus(object sender, RoutedEventArgs e)
        //{
        //    if(AmountAddBox.Text== "" || AmountAddBox.Text =="0")
        //        AmountAddBox.Text = "Enter amount";
        //}
 
    }
}
