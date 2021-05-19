using System.Windows;

namespace TheBureau.Views
{
    public partial class AddEmployeeWindow : Window
    {
        public AddEmployeeWindow()
        {
            InitializeComponent();
        }
        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}