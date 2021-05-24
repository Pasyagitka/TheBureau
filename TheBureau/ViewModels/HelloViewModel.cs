using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TheBureau.Models;
using TheBureau.Repositories;
using TheBureau.Services;
using TheBureau.Views;

namespace TheBureau.ViewModels
{
    public class HelloViewModel : ViewModelBase
    {
        private UserRepository _userRepository = new();
        private ErrorsViewModel _errorsViewModel = new();

        private string _login;
        private string _password;
        private string _info;

        private ICommand _closeWindowCommand;
        private ICommand _clientPageSetCommand;
        private ICommand _authPageSetCommand;
        private object _frameContent;
        
        public HelloViewModel()
        {
            FrameContent = new HelloPageView();
        }

        public object FrameContent
        {
            get => _frameContent;
            set
            {
                _frameContent = value;
                OnPropertyChanged("FrameContent");
            }
        }
        public ICommand ClientViewSetCommand => _clientPageSetCommand ??= 
            new RelayCommand(obj => { FrameContent = new HelloPageView(); });
        public ICommand AuthViewSetCommand => _authPageSetCommand ??= 
            new RelayCommand(obj => { FrameContent = new AuthenticationPageView(); });
        public ICommand CloseWindowCommand => _closeWindowCommand ??= 
            new RelayCommand(obj => { Application.Current.Shutdown(); });
        
        public string Info
        {
            get => _info;
            set { _info = value; OnPropertyChanged("Info");}
        }

        public string Login
        {
            get => _login;
            set
            {
                _login = value; 
                _errorsViewModel.ClearErrors("Login");
                
                if (string.IsNullOrWhiteSpace(_login))
                {
                    _errorsViewModel.AddError("Login", ValidationConst.LoginEmpty);
                }
                if (_login?.Length > 20)
                {
                    _errorsViewModel.AddError("Login", ValidationConst.LoginLengthExceeded);
                }
                var regex = new Regex(ValidationConst.LoginRegex);
                if (!regex.IsMatch(_login!))
                {
                    _errorsViewModel.AddError("Login", ValidationConst.IncorrectLoginStructure);
                }
                OnPropertyChanged("Login");
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        private ICommand _signinCommand;
        public ICommand SigninCommand
        {
            get
            {
                return _signinCommand ??= new RelayCommand(obj =>
                {
                    var passwordBox = obj as PasswordBox;
                    if (passwordBox == null)
                        return;
                    Password = passwordBox.Password;
                    
                    if (TryLogin())
                    {
                        var user = Application.Current.Properties["User"] as User;
                        if (user?.role == 1)
                        {
                            var mainWindow = new MainWindowView();
                            mainWindow.Show();
                            Application.Current.Windows[0]?.Close();
                        }
                        else if (user?.role == 2)
                        {
                            var brigadeWindow = new BrigadeWindowView();
                            brigadeWindow.Show();
                            Application.Current.Windows[0]?.Close();
                        }
                        else
                        {
                           Info = ValidationConst.SomethingWentWrong;
                        }
                    }
                });
            }
        }

        
        private bool TryLogin()
        {
            var regex = new Regex(ValidationConst.LoginRegex);
            if (!IsLoginValid())
            {
                return false;
            }
            if (string.IsNullOrEmpty(Password))
            {
                Info = ValidationConst.PasswordEmpty;
                return false;
            }
            if (!regex.IsMatch(Login))
            {
                Info = ValidationConst.LoginAndPasswordStructure;
                return false;
            }
            if (Password.Length > 70)
            {
                Info = ValidationConst.PasswordTooLong;
                return false;
            }
            
            var user = _userRepository.Login(Login, Password);
            if (user == null)
            {
                Info = ValidationConst.WrongLoginOrPassword;
                return false;
            }
            Application.Current.Properties["User"] = _userRepository.Get(user.id);
            return true;
        }
        
        private bool IsLoginValid()
        {
            if (string.IsNullOrEmpty(Login))
            {
                Info = ValidationConst.LoginEmpty;
                return false;
            }
            if (_login.Length <= 20) return true;
            Info = ValidationConst.LoginLengthExceeded;
            return false;
        }
    }
}