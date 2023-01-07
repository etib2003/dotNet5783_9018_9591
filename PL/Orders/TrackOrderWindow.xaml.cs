using BO;
using DocumentFormat.OpenXml.Office.CustomDocumentInformationPanel;
using System.Linq;
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

        public BO.OrderTracking orderTracking
        {
            get { return (BO.OrderTracking)GetValue(orderTrackingProperty); }
            set { SetValue(orderTrackingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for orderTracking.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty orderTrackingProperty =
            DependencyProperty.Register("orderTracking", typeof(BO.OrderTracking), typeof(TrackOrderWindow));


        public string Id
        {
            get { return (string)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Id.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IdProperty =
            DependencyProperty.Register("Id", typeof(string), typeof(TrackOrderWindow));



        public TrackOrderWindow()
        {
            orderTracking = new BO.OrderTracking() {};

            InitializeComponent();
        }

        private void Enter(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    int id = int.Parse(Id);//int.Parse(IdTextBox.Text);                  
                    orderTracking = bl?.Order.TrackingOrder(id);
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
