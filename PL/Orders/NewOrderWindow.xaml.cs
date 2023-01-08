﻿using BO;
using Cart;
using DO;
using DocumentFormat.OpenXml.ExtendedProperties;
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
        //private CollectionView? view;

        private string groupName = "Category";
        PropertyGroupDescription propertyGroupDescription;
        public ICollectionView CollectionViewProductItemList { set; get; }
        public BO.Cart cart
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
        private int selectedIndex;

        public NewOrderWindow(BO.Cart _cart)
        {
            //view = null;
            

            cart = _cart;
            ProductsItems = new ObservableCollection<ProductItem>(bl?.Product.GetListProductForCatalog(cart));
            Categories = Enum.GetValues(typeof(BO.Category));
            CollectionViewProductItemList = CollectionViewSource.GetDefaultView(ProductsItems);
            propertyGroupDescription = new PropertyGroupDescription(groupName);
            //CollectionViewProductItemList.GroupDescriptions.Clear();
            InitializeComponent();

        }

        private void restartAndAdd(IEnumerable<ProductItem> objects)
        {
            ProductsItems.Clear();

            foreach (var item in objects)
            {
                ProductsItems.Add(item);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CategorySelected = (BO.Category)CategoryComboBox.SelectedItem;


            if (ProductsItems.Any(p => p.Category == CategorySelected) == false)
                restartAndAdd(bl?.Product.GetListProductForCatalog(cart, x => (BO.Category)x?.Category! == CategorySelected));
            else
            {
                List<ProductItem> objects = bl?.Product.GetListProductForCatalogView(cart, ProductsItems, x => (BO.Category)x?.Category! == CategorySelected).ToList();
                if (objects.Any())
                {
                    restartAndAdd(objects);
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
            CollectionViewProductItemList.GroupDescriptions.Clear();

            //view = null;
            restartAndAdd(bl?.Product.GetListProductForCatalog(cart));
            Categories = Enum.GetValues(typeof(BO.Category)); //לא מסנן כשזה מסומן כבר על הקטגוריה
        }

        private void CartButton_Click(object sender, RoutedEventArgs e)
        {
            new CartWindow(cart).Show();
            this.Close();
        }

        private void AddToCart(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameworkElement frameworkElement = (sender as FrameworkElement)!;
                int productId;
                if (frameworkElement is not null && frameworkElement.DataContext is not null)
                {
                    productId = ((ProductItem)(frameworkElement.DataContext)).Id;
                    bl?.Cart.AddProductToCart(cart, productId);
                    var p = ProductsItems.First(p => p.Id == productId);
                    ProductsItems[ProductsItems.IndexOf(p)] = bl?.Product.GetProductDetailsForCustomer(productId, cart);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Out of stock!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void grouping_Click(object sender, RoutedEventArgs e)
        {
            CollectionViewProductItemList.GroupDescriptions.Add(propertyGroupDescription);

            //if (view==null)
            //{
            //    view = (CollectionView)CollectionViewSource.GetDefaultView(ProductsItems);
            //    PropertyGroupDescription groupDescription=new PropertyGroupDescription("Category");
            //    view.GroupDescriptions.Add(groupDescription);
            //}
            //var objects = (from item in bl?.Product.GetListProductForCatalog(cart)
            //               orderby item.Category
            //               select item);
            //restartAndAdd(objects);
        }
    }
}

/*
  <ListView.GroupStyle>
                <GroupStyle>
                    <GroupStyle.ContainerStyle>
                        <Style TargetType="{x:Type GroupItem}">
                            <Setter Property="Template" >
                                <Setter.Value>
                                    <ControlTemplate>
                                        <Expander IsExpanded="True" >
                                            <Expander.Background>
                                                <LinearGradientBrush EndPoint="0.5,1" StartPoint="0.5,0">
                                                    <GradientStop  Offset="0.15"/>
                                                    <GradientStop  Offset="1"/>
                                                    <GradientStop  Offset="0.342"/>
                                                </LinearGradientBrush>
                                            </Expander.Background>
                                            <Expander.Header>
                                                <StackPanel Orientation="Horizontal"  >
                                                    <TextBlock Text="{Binding Name}" FontWeight="DemiBold" Foreground="Black" FontSize="14" VerticalAlignment="Bottom" />
                                                    <TextBlock Text="{Binding ItemCount}" FontSize="14" Foreground="Gray" FontWeight="Bold" FontStyle="Italic" Margin="10,0,0,0" VerticalAlignment="Bottom" />
                                                    <TextBlock Text=" record(s)" FontSize="14" Foreground="Gray" FontStyle="Italic" VerticalAlignment="Bottom" />
                                                </StackPanel>
                                            </Expander.Header>
                                            <ItemsPresenter />
                                        </Expander>
                                    </ControlTemplate>
                                </Setter.Value>
                            </Setter>
                        </Style>
                    </GroupStyle.ContainerStyle>
                </GroupStyle>
            </ListView.GroupStyle>
 */
