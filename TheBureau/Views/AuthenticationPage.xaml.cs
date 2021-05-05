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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TheBureau.Models.DataManipulating;

namespace TheBureau.Views
{
    /// <summary>
    /// Логика взаимодействия для AuthenticationPage.xaml
    /// </summary>
    public partial class AuthenticationPage : Page
    {
        public AuthenticationPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            TryLogin();
        }
        private void TryLogin()
        {
            string username = LoginTextBox.Text;
            string password = PasswordHash.CreateHash(PasswordBox.Password);

            MessageBox.Show(password);
            var mainWindow = new MainWindow();
            App.Current.Windows[0].Close();
            mainWindow.Show();
        }
    }
}
