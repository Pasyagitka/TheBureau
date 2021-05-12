using System.Windows;
using TheBureau.ViewModels;

namespace TheBureau.Views
{
    public partial class EditClientView : Window
    {
        readonly EditClientViewModel _editClientViewModel = new EditClientViewModel();
        public EditClientView(Client client)
        {
            InitializeComponent();
            DataContext = _editClientViewModel;
            _editClientViewModel.Client = client;
        }

        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
           this.DialogResult = true;
        }
        
    }
}