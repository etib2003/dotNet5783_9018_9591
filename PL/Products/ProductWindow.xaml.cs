
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
        //    Complete.Content = "Add to Cart";
        //    Cart = _cart;
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
        /// Shows warnings if needed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">the event</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
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
                //else if (Complete.Content == "Add to Cart") //לבדוק מה הולך פה
                //{
                //    //if (AmountAddBox.Text != "0") //מומלץ לעשות שלא יוכל להכניס 0
                //    //{
                //        bl?.Cart.AddProductToCart(Cart, NewPdct.Id); 
                //        if (AmountAddBox.Text != "Enter amount") //יש בעיה כי אם אין מספיק במלאי הוא עדיין יעשה הוספה של אחד ותכלס זה בעיה
                //            bl?.Cart.UpdateAmountOfProduct(Cart, NewPdct.Id, int.Parse(AmountAddBox.Text));

                //    action(NewPdct.Id);
                //    MessageBox.Show("Adding to Cart is done!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    MessageBox.Show("Error, you can't add the product!");
                if (CompleteButton == "Update")
                    MessageBox.Show("Error, you can't update the product!");
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

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try {
                bl?.Product.DeleteProduct(NewPdct.Id);
                MessageBox.Show("Deleting is done!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, you can't delete the product!\n It is already in the process of buying");
            }
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
