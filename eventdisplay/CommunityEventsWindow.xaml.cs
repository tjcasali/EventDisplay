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
    /// Interaction logic for CommunityEventsWindow.xaml
    /// </summary>
    public partial class CommunityEventsWindow : Window
    {
        public CommunityEventsWindow()
        {
            InitializeComponent();
        }

        private void CEWCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            CommunityEventsWindow1.Close();
        }
    }
}
