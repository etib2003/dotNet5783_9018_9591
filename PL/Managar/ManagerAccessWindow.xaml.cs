using PL;
using PL.productsWindows;
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

namespace Managar
{
    /// <summary>
    /// Interaction logic for ManagerAccessWindow.xaml
    /// </summary>
    public partial class ManagerAccessWindow : Window
    {
        string password = "1";

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Password.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(ManagerAccessWindow));

        public ManagerAccessWindow()
        {
            InitializeComponent();
        }

        //a window to access manager
        private void Enter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Password == password)
                {
                    new ManagerWindow().Show();
                    this.Close();
                }
            }
        }
    }
}
