using System.Windows;

namespace TheBureau.Views
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            //todo event!!
            this.DialogResult = true;
        }
    }
}