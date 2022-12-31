
 using BO;
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

       public ObservableCollection<ProductForList> ProductsForList { set; get; }
        public Array Categories { set; get; }

        private int selectedIndex;


        /// <summary>
        /// get the list of products from the logical layer:
        /// </summary>
        public ProductListWindow()
        {
            ProductsForList = new ObservableCollection<ProductForList>(bl?.Product.GetListProductForManagerAndCatalog());
            Categories = Enum.GetValues(typeof(BO.Category));

            DataContext = this; 
            InitializeComponent();           
        }

        private void addProductForList(int productId)
        {
            try
            {
                ProductsForList.Add(bl.Product.GetProductForList(productId));
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        /// <summary>
        ///  comboBox for choosing the wanted category of a product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">the event</param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Category category = (BO.Category)selectCategory.SelectedItem;
            IEnumerable<ProductForList> objects = bl?.Product.GetProductForListByCond(ProductsForList, product => product.Category == category);
            if (objects.Any())
            {
                ProductsForList.Clear();
                foreach (var item in objects)
                {
                    ProductsForList.Add(item);
                }
            }
           

        }

        /// <summary>
        /// button that shows all the categories
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">the event</param>
        private void ShowAllCategories_Click(object sender, RoutedEventArgs e)
        {
            ProductsForList = new ObservableCollection<ProductForList>(bl?.Product.GetListProductForManagerAndCatalog());

        }

        /// <summary>
        /// update a product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">the event</param>
        private void Update_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ProductForListView.SelectedItem is ProductForList productForList)
            {
                selectedIndex = ProductForListView.SelectedIndex;
                int pflId = ((ProductForList)ProductForListView.SelectedItem).Id;
                new ProductWindow(pflId, (productId) => ProductsForList[selectedIndex] = bl.Product.GetProductForList(productId)).Show();
                //ShowAllCategories_Click(sender, e);
            }           
        }

        /// <summary>
        /// add a product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">the event</param>
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow(addProductForList).Show();
            //ShowAllCategories_Click(sender,e);
        }

        /// <summary>
        /// close the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">the event</param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
