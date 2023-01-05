using BO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;

namespace Orders
{
    /// <summary>
    /// Interaction logic for TrackOrderWindow.xaml
    /// </summary>
    public partial class TrackOrderWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public BO.OrderTracking orderTracking { get; set; }

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
                    int id = int.Parse(IdTextBox.Text);                  
                    orderTracking = bl?.Order.TrackingOrder(id);
                    //OrderTrackingBox.DataContext = orderTracking;
                }
            }
            catch(BoDoesNoExistException ex)
            {
                MessageBox.Show("No Order exists with this ID!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                IdTextBox.Text = "";
            }
        }

        private void ViewOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if(IdTextBox.Text!="")
            {
                new OrderWindow(int.Parse(IdTextBox.Text), 1).Show();
            }
        }

        private void myTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {              
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
