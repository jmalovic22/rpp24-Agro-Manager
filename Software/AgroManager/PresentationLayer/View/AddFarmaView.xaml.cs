﻿using System;
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

namespace PresentationLayer.View
{
    /// <summary>
    /// Interaction logic for AddFarmaView.xaml
    /// </summary>
    public partial class AddFarmaView : Window
    {
        public AddFarmaView()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            WindowState = WindowState.Normal;
        }

        private void BtnOdustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnSpremi_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
