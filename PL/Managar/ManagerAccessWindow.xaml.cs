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

namespace Managar
{
    /// <summary>
    /// Interaction logic for ManagerAccessWindow.xaml
    /// </summary>
    public partial class ManagerAccessWindow : Window
    {
        string password = "1";
        public ManagerAccessWindow()
        {
            InitializeComponent();
        }

        private void Enter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (PasswordBox.Text == password)
                {
                    new ManagerWindow().Show();
                    this.Close();
                }
            }

        }
    }
}
