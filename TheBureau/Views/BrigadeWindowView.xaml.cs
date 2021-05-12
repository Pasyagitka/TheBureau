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

namespace TheBureau.Views
{
    /// <summary>
    /// Логика взаимодействия для BrigadeMainWindow.xaml
    /// </summary>
    public partial class BrigadeWindowView : Window
    {
        public BrigadeWindowView()
        {
            InitializeComponent();
        }
        
        private void TopGrid_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void MainWindowClose_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        
        private void MainWindowMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
