﻿using BO;
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

namespace Cart
{
    /// <summary>
    /// Interaction logic for CompleteWindow.xaml
    /// </summary>
    public partial class CompleteWindow : Window
    {
        public BO.Cart? cart { get; set; }
        public BO.Order order { get; set; }

        public CompleteWindow(BO.Order _order)
        {
            order = _order;
            InitializeComponent();

        }
    }
}
