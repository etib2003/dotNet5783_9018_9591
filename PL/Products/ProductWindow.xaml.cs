
using BO;
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
        public Array Categories { set; get; }

        public BO.Product NewPdct
        {
            get { return (BO.Product)GetValue(NewPdctProperty); }
            set { SetValue(NewPdctProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NewPdct.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NewPdctProperty =
            DependencyProperty.Register("NewPdct", typeof(BO.Product), typeof(ProductWindow));

        private Action<int> action;
        private Action update;
        private Action delete;

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
        public ProductWindow(int prtrLId, Action update, Action delete) 
        {
            try
            {
                this.update = update;
                this.delete = delete;
                NewPdct = bl?.Product.GetProductDetailsForManager(prtrLId);
                Categories = Enum.GetValues(typeof(BO.Category));
                InitializeComponent();
                CompleteButton = "Update";
            }
            catch(BO.BoDoesNoExistException ex)
            {
                MessageBox.Show("We could not load the product details..\nPlease try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(BO.NegativeNumberException ex)
            {
                MessageBox.Show("Ivalide Id\nPlease try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.WrongLengthException ex)
            {
                MessageBox.Show("Too short Id number\nPlease try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

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
                    update();                   
                    MessageBox.Show("Updating is done!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
               
                this.Close();
            }
            catch (BO.BoAlreadyExistsException ex)
            {
                MessageBox.Show("Product with this Id already exists!\nPlease try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.NegativeNumberException ex)
            {
                MessageBox.Show(ex.Message+"\nPlease try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.NegativeDoubleNumberException ex)
            {
                MessageBox.Show(ex.Message+"\nPlease try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.WrongLengthException ex)
            {
                MessageBox.Show("Too short id, please try again!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.NotValidFormatNameException ex)
            {
                MessageBox.Show(ex.Message + "\nPlease try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                if (CompleteButton == "Add")
                    MessageBox.Show("Error, you can't add the product!\nPlease try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                if (CompleteButton == "Update")
                    MessageBox.Show("Error, you can't update the product!\nPlease try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try {
                bl?.Product.DeleteProduct(NewPdct.Id);
                this.Close();
                delete();
                MessageBox.Show("Deleting is done!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (BO.NotValidDeleteException ex)
            {
                MessageBox.Show("Product is already in order prosses\nYou can't delete it!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
