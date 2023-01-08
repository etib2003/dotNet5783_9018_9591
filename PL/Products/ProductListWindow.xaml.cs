
using BO;
using DO;
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
        BlApi.IBl? bl = BlApi.Factory.Get();
        public ObservableCollection<BO.ProductForList> ProductsForList { set; get; }
        public Array Categories { set; get; }
        private int selectedIndex;

        public ProductListWindow()
        {
            try
            {
                ProductsForList = new ObservableCollection<BO.ProductForList>(bl?.Product.GetListProductForManagerAndCatalog());
                Categories = Enum.GetValues(typeof(BO.Category));
                InitializeComponent();
            }
            catch(BO.BoDoesNoExistException ex)
            {
                MessageBox.Show("We could not load the data..\n Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void addProductForList(int productId)
        {
            ProductsForList.Add(bl?.Product.GetProductForList(productId));
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
            catch (BO.BoDoesNoExistException ex)
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
                BO.Category category = (BO.Category)selectCategory.SelectedItem;
                if (ProductsForList.Any(p => p.Category == category) == false)
                {
                    restartAndAdd(bl?.Product.GetListProductForManagerAndCatalog(x => (BO.Category)x?.Category == category));
                }
                else
                {
                    List<BO.ProductForList> objects = bl?.Product.GetProductForListByCond(ProductsForList, product => product.Category == category).ToList();
                    if (objects.Any())
                    {
                        restartAndAdd(objects);
                    }
                }
            }
            catch (BO.BoDoesNoExistException ex)//catches the exception from the data layer
            {
                MessageBox.Show("We could not load the data..\n Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)//catches the exception from the data layer
            {
                MessageBox.Show("Error\n Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void restartAndAdd(IEnumerable<BO.ProductForList> objects)
        {
            ProductsForList.Clear();

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
                restartAndAdd(bl?.Product.GetListProductForManagerAndCatalog());
            }
            catch (BO.BoDoesNoExistException ex)//catches the exception from the data layer
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
                if (ProductForListView.SelectedItem is ProductForList productForList)
                {
                    selectedIndex = ProductForListView.SelectedIndex;
                    int pflId = ((ProductForList)ProductForListView.SelectedItem).Id;
                    new ProductWindow(pflId, (productId) => ProductsForList[selectedIndex] = bl?.Product.GetProductForList(productId)).Show();
                    this.Close();
                }
            }
            catch (BO.BoDoesNoExistException ex)//catches the exception from the data layer
            {
                MessageBox.Show("We could not find the product..\n Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
