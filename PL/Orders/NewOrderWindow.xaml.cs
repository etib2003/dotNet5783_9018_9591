using BO;
using Cart;
using DO;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Orders;
using PL;
using PL.productsWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;


namespace Products
{
    /// <summary>
    /// Interaction logic for NewOrderWindow.xaml
    /// </summary>

    public partial class NewOrderWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        private string groupName = "Category";
        PropertyGroupDescription propertyGroupDescription;
        public ICollectionView CollectionViewProductItemList { set; get; }
        public BO.Cart Cart
        {
            get { return (BO.Cart)GetValue(cartProperty); }
            set { SetValue(cartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty cartProperty =
            DependencyProperty.Register("Cart", typeof(BO.Cart), typeof(NewOrderWindow));

        public BO.Category? CategorySelected
        {
            get { return (BO.Category?)GetValue(CategorySelectedProperty); }
            set { SetValue(CategorySelectedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CategorySelected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CategorySelectedProperty =
            DependencyProperty.Register("CategorySelected", typeof(BO.Category?), typeof(NewOrderWindow));

        public ObservableCollection<ProductItem> ProductsItems
        {
            get => (ObservableCollection<ProductItem>)GetValue(ProductsDependency);
            private set => SetValue(ProductsDependency, value);
        }

        public static readonly DependencyProperty ProductsDependency
            = DependencyProperty.Register(nameof(ProductsItems), typeof(ObservableCollection<ProductItem>), typeof(NewOrderWindow));

        public Array Categories { set; get; }
        private int selectedIndex { set; get; }

        Action<BO.Cart> action;
        //a window that shows the catalog
        public NewOrderWindow(BO.Cart _cart, Action<BO.Cart> action)
        {
            try
            {
                Cart = _cart;
                this.action = action;
                var pList = bl?.Product.GetListProductForCatalog(Cart)!;
                ProductsItems = new ObservableCollection<ProductItem>(pList);
                Categories = Enum.GetValues(typeof(BO.Category));
                CollectionViewProductItemList = CollectionViewSource.GetDefaultView(ProductsItems);
                //grouping
                propertyGroupDescription = new PropertyGroupDescription(groupName);
                CollectionViewProductItemList.GroupDescriptions.Clear();
                restartAndAdd(pList);
                CategorySelected = null;
                InitializeComponent();
            }
            catch(BO.BoDoesNoExistException)
            {
                MessageBox.Show("We could not load the data..\n Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.NegativeNumberException)
            {
                MessageBox.Show("Ivalide Id\nPlease try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.WrongLengthException)
            {
                MessageBox.Show("Too short Id number\nPlease try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Error\n Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //put in productItem the objects
        private void restartAndAdd(IEnumerable<ProductItem> objects)
        {
            ProductsItems.Clear();

            foreach (var item in objects)
            {
                ProductsItems.Add(item);
            }
        }

        //a comboBox to choose a certain category of products
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (ProductsItems.Any(p => p.Category == CategorySelected) == false)
                    restartAndAdd(bl?.Product.GetListProductForCatalog(Cart, x => (BO.Category)x?.Category! == CategorySelected)!);
                else
                {
                    List<ProductItem> objects = bl?.Product.GetListProductForCatalogView(Cart, ProductsItems, x => (BO.Category)x?.Category! == CategorySelected).ToList()!;
                    if (objects.Any())
                    {
                        restartAndAdd(objects);
                    }
                }
            }
            catch(BO.BoDoesNoExistException)
            {
                MessageBox.Show("We could not load the data..\n Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.NegativeNumberException)
            {
                MessageBox.Show("Ivalide Id\nPlease try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.WrongLengthException)
            {
                MessageBox.Show("Too short Id number\nPlease try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// button that shows all the categories
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">the event</param>
        private void ShowAllCategories_Click(object sender, RoutedEventArgs e)
        {
            CollectionViewProductItemList.GroupDescriptions.Clear();
            CategorySelected = null;

            restartAndAdd(bl?.Product.GetListProductForCatalog(Cart)!);
            Categories = Enum.GetValues(typeof(BO.Category));
        }

        //go to cart
        private void CartButton_Click(object sender, RoutedEventArgs e)
        {
            new CartWindow(Cart, action).ShowDialog() ;
            this.Close();
        }
        //sort the products into groups
        private void grouping_Click(object sender, RoutedEventArgs e)
        {
            ShowAllCategories_Click(sender, e);
            CollectionViewProductItemList.GroupDescriptions.Add(propertyGroupDescription);
        }

        //add a product to cart
        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameworkElement frameworkElement = (sender as FrameworkElement)!;
                int productId;
                if (frameworkElement is not null && frameworkElement.DataContext is not null)
                {
                    productId = ((ProductItem)(frameworkElement.DataContext)).Id;
                    
                    Cart= bl?.Cart.AddProductToCart(Cart, productId)!;
                    var p = ProductsItems.First(p => p.Id == productId);
                    ProductsItems[ProductsItems.IndexOf(p)] = bl?.Product.GetProductDetailsForCustomer(productId, Cart)!;
                }
            }      
            catch(BO.BoDoesNoExistException)
            {
                MessageBox.Show("No Order exists with this ID!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.NotInStockException)
            {
                MessageBox.Show("Out of stock!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.NegativeNumberException)
            {
                MessageBox.Show("Negative ID!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.WrongLengthException)
            {
                MessageBox.Show("Wrong length ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //open a window that shows details of a choosen product
        private void ShowItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var lv = (sender as ListViewItem)!;
                var product = (BO.ProductItem)lv!.DataContext;
                int piId = product.Id;

                new ProductItemWindow(piId, Cart, () => ProductsItems![ProductsItems!.IndexOf(product)] = bl.Product.GetProductDetailsForCustomer(piId, Cart)).Show();

            }
            catch (BO.BoDoesNoExistException)//catches the exception from the data layer
            {
                MessageBox.Show("We could not find the product..\n Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.NegativeNumberException)
            {
                MessageBox.Show("Negative ID!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.WrongLengthException)
            {
                MessageBox.Show("Wrong length ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

