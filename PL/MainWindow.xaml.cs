using Managar;
using Orders;
using PL.productsWindows;
using Products;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        ///Object to access the logical layer.
        /// </summary>
         BlApi.IBl? bl= BlApi.Factory.Get();

        private string _path = Environment.CurrentDirectory + @$"\PicturesForMain\";
 

        public BO.Cart Cart
        {
            get { return (BO.Cart)GetValue(cartProperty); }
            set { SetValue(cartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty cartProperty =
            DependencyProperty.Register("Cart", typeof(BO.Cart), typeof(MainWindow));



        public ImageSource PictureHolderSource
        {
            get { return (ImageSource)GetValue(PictureHolderProperty); }
            set { SetValue(PictureHolderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PictureHolderProperty =
            DependencyProperty.Register("PictureHolderSource", typeof(ImageSource), typeof(MainWindow));

        BackgroundWorker backgroundWorker;
        int i=1;

        /// <summary>
        /// constructor
        /// </summary>
        public MainWindow()
        {
            Cart = new BO.Cart() { Items = new List<BO.OrderItem?>(), TotalPrice=0 };
          
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.RunWorkerAsync();
            InitializeComponent();
        }

        private void BackgroundWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            PictureHolderSource = new BitmapImage(new System.Uri(_path + $"{e.ProgressPercentage}.jpg"));
        }

        private void BackgroundWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            int i = 1;
            while (true)
            {
                backgroundWorker.ReportProgress(i++);
                i = i < 7 ? i : 1;
                Thread.Sleep(2000);
            }
        }

        /// <summary>
        /// close the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">the event</param>
        private void MWcloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void managerButton_Click(object sender, RoutedEventArgs e)
        {
            new ManagerAccessWindow().Show();
        }

        private void TrackingOrderButton_Click(object sender, RoutedEventArgs e)
        {
            new TrackOrderWindow().Show();
        }
        private void initCart(BO.Cart cart) => Cart = cart;
        private void NewOrderButton_click(object sender, RoutedEventArgs e)
        {
            new  NewOrderWindow(Cart, initCart).ShowDialog();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            i--;
            if(i<1)
            {
                i = 7;
            }
            PictureHolderSource = new BitmapImage(new System.Uri(_path + $"{i}.jpg"));
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            i++;
            if (i > 7)
            {
                i = 1;
            }
            
           PictureHolderSource = new BitmapImage(new System.Uri(_path + $"{i}.jpg"));
                    
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new SimulatorWindow().Show();
        }

        //private void replaceImages()
        //{
        //    int i = 1;
        //    while (true)
        //    {
        //        i = i < 8 ? i : 1;
        //        PictureHolderSource = new BitmapImage(new System.Uri(_path + $"{i}.jpg"));
        //        i++;
        //        Thread.Sleep(2300);
        //        i = i < 8 ? i : 1;
        //        PictureHolderSource = new BitmapImage(new System.Uri(_path + $"{i}.jpg"));
        //    }
        //}
    }
}