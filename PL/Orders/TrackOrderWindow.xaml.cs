using BO;
using PL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Orders
{
    /// <summary>
    /// Interaction logic for TrackOrderWindow.xaml
    /// </summary>
    public partial class TrackOrderWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public TrackOrderWindow()
        {
            InitializeComponent();
        }

        private void Enter(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    int.TryParse(IdTextBox.Text, out int id);
                    var orderTracking = bl?.Order.TrackingOrder(id);
                    OrderTrackingBox.DataContext = orderTracking;
                }
            }
            catch(BoDoesNoExistException ex)
            {
                MessageBox.Show("No order exists with this ID!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
