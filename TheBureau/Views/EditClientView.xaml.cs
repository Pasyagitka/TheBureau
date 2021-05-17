using System.Windows;
using TheBureau.ViewModels;

namespace TheBureau.Views
{
    public partial class EditClientView : Window
    {
        public EditClientView()
        {
            InitializeComponent();
        }

        public EditClientView(Client selected)
        {
            InitializeComponent();
            EditClientViewModel _editClientViewModel = new EditClientViewModel(selected);
            DataContext = _editClientViewModel;
            //_editClientViewModel.Client = client;
        }

        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
           this.DialogResult = true;
        }
        
    }
}