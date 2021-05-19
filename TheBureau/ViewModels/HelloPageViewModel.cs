using System.Windows.Input;
using TheBureau.Views;

namespace TheBureau.ViewModels
{
    public class HelloPageViewModel : ViewModelBase
    {
        private ICommand _enterAsClientCommand;
        public ICommand EnterAsClientCommand
        {
            get
            {
                return _enterAsClientCommand = new RelayCommand(obj =>
                {
                    var clientMainWindow = new ClientWindowView();
                    App.Current.Windows[0].Close();
                    clientMainWindow.Show();
                });
            }
        }
    }
}