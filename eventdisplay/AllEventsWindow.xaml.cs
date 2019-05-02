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

namespace EventDisplay
{
    /// <summary>
    /// Interaction logic for AllEventsWindow.xaml
    /// </summary>
    public partial class AllEventsWindow : Window
    {
        public AllEventsWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void AEWCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            AllEventsWindow1.Close();
        }
    }
}
