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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TheBureau.ViewModels;

namespace TheBureau.Views
{
    /// <summary>
    /// Логика взаимодействия для ClientsMainView.xaml
    /// </summary>
    public partial class ClientView : Page
    {
        // private ClientViewModel _clientViewModel = new ClientViewModel();
        public ClientView()
        {
            InitializeComponent();
            // DataContext = _clientViewModel;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
           // _clientViewModel.Update();
        }
    }
}
