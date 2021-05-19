using System.Windows;
using TheBureau.ViewModels;

namespace TheBureau.Views.Controls
{
    public partial class InfoWindow : Window
    {
        private InfoWindowViewModel _viewModel; 
        //todo addviewmodels to default ctor
        public InfoWindow()
        {
            InitializeComponent();
        }
        public InfoWindow(string status, string message)
        {
            InitializeComponent();
            _viewModel = new InfoWindowViewModel(status, message);
            DataContext = _viewModel;
        }
    }
}