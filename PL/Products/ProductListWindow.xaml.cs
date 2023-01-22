
using BO;
using DO;
using Products;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace PL.productsWindows
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        //Object to access the logical layer
        static readonly BlApi.IBl bl = BlApi.Factory.Get()!;
        public ObservableCollection<BO.ProductForList?>? ProductsForList { set; get; }
        public Array? Categories { set; get; }
        private int selectedIndex { set; get; }

        public BO.Category? CategorySelected
        {
            get { return (BO.Category?)GetValue(CategorySelectedProperty); }
            set { SetValue(CategorySelectedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CategorySelected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CategorySelectedProperty =
            DependencyProperty.Register("CategorySelected", typeof(BO.Category?), typeof(ProductListWindow));

        //shows the products
        public ProductListWindow()
        {
            try
            {
                ProductsForList = new ObservableCollection<BO.ProductForList?>(bl.Product.GetListProductForManagerAndCatalog());
                Categories = Enum.GetValues(typeof(BO.Category));
                restartAndAdd(bl.Product.GetListProductForManagerAndCatalog());
                CategorySelected = null;
                InitializeComponent();
            }
            catch(BO.BoDoesNoExistException)
            {
                MessageBox.Show("We could not load the data..\n Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        //add a product to the store
        private void addProductForList(int productId)
        {
            ProductsForList!.Add(bl?.Product.GetProductForList(productId));
        }

        /// <summary>
        /// add a product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">the event</param>
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new ProductWindow(addProductForList).Show();
            }
            catch (BO.BoDoesNoExistException)
            {
                MessageBox.Show("Can't add the product!\n Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        ///  comboBox for choosing the wanted category of a product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">the event</param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                 if (ProductsForList!.Any(p => p!.Category == CategorySelected) == false)
                {
                    restartAndAdd(bl.Product.GetListProductForManagerAndCatalog(x => (BO.Category)x?.Category! == CategorySelected));
                }
                else
                {
                    List<BO.ProductForList?> objects = bl?.Product.GetProductForListByCond(ProductsForList!, product => product!.Category == CategorySelected).ToList();
                    if (objects!.Any())
                    {
                        restartAndAdd(objects!);
                    }
                }
            }
            catch (BO.BoDoesNoExistException)//catches the exception from the data layer
            {
                MessageBox.Show("We could not load the data..\n Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)//catches the exception from the data layer
            {
                MessageBox.Show("Error\n Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void restartAndAdd(IEnumerable<BO.ProductForList?> objects)
        {
            ProductsForList!.Clear();

            foreach (var item in objects)
            {
                ProductsForList.Add(item);
            }
        }

        /// <summary>
        /// button that shows all the categories
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">the event</param>
        private void ShowAllCategories_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CategorySelected = null;

                restartAndAdd(bl.Product.GetListProductForManagerAndCatalog());

            }
            catch (BO.BoDoesNoExistException)//catches the exception from the data layer
            {
                MessageBox.Show("We could not load the data..\n Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// update a product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">the event</param>
        private void Update_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var lv = (sender as ListViewItem)!;
                var product = (BO.ProductForList)(sender as ListViewItem)!.DataContext;

                int pflId = product.Id;
                new ProductWindow(prtrLId: pflId,
                                  update: () => ProductsForList![ProductsForList!.IndexOf(product)] = bl.Product.GetProductForList(pflId),
                                  delete: () => ProductsForList!.RemoveAt(ProductsForList!.IndexOf(product))).Show();
            }
            catch (BO.BoDoesNoExistException)//catches the exception from the data layer
            {
                MessageBox.Show("We could not find the product..\n Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
