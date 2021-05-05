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

namespace TheBureau.Views
{
    /// <summary>
    /// Логика взаимодействия для HelloWindow.xaml
    /// </summary>
    public partial class HelloWindow : Window
    {
        public HelloWindow()
        {
            InitializeComponent();
            AuthFrame.Content = new HelloPage();
        }

        private void ClientButton_Click(object sender, RoutedEventArgs e)
        {
            AuthFrame.Content = new HelloPage();
        }

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            AuthFrame.Content = new AuthenticationPage();
        }
    }
}
