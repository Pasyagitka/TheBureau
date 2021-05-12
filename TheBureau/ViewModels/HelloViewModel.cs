using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TheBureau.Models.DataManipulating;
using TheBureau.Repositories;
using TheBureau.Views;

namespace TheBureau.ViewModels
{
    public class HelloViewModel : ViewModelBase
    {
        //todo переключение страниц в viewmodel
        private string login;
        private string password;
        private string info;

        private UserRepository _userRepository = new UserRepository();
        private const string LoginAndPasswordRegex = "^[a-zA-Z0-9_.-]*$";
        private readonly string PasswordEmpty = "Введите пароль";
        private readonly string PasswordTooLong = "Пароль должен быть до 20 символов";
        private readonly string LoginAndPasswordStructure = "Пароль и имя пользователя могут состоять лишь из цифр и букв";

        public string Info
        {
            get => info;
            set { info = value; OnPropertyChanged("Info");}
        }

        public string Login
        {
            get => login;
            set { login = value; OnPropertyChanged("Login");}
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        private RelayCommand signinCommand;
        public RelayCommand SigninCommand
        {
            get
            {
                return signinCommand ??= new RelayCommand(obj =>
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
                            MessageBox.Show("Что-то пошло не так.");
                        }
                    }
                    else MessageBox.Show(Info);
                });
            }
        }

        
        private bool TryLogin()
        {
            var regex = new Regex(LoginAndPasswordRegex);
            if (!IsLoginValid())
            {
                return false;
            }
            if (string.IsNullOrEmpty(Password))
            {
                Info = PasswordEmpty;
                return false;
            }
            if (!regex.IsMatch(Login) || !regex.IsMatch((Password)))
            {
                Info = LoginAndPasswordStructure;
                return false;
            }
            if (Password.Length > 20)
            {
                Info = PasswordTooLong;
                return false;
            }
            
            var user = _userRepository.Login(Login, Password);
            if (user == null)
            {
                Info = "ErrorMessages.WrongLoginOrPassword";
                return false;
            }
            Application.Current.Properties["User"] = _userRepository.Get(user.id);
            Info = "Success";
            return true;
        }
        
        private bool IsLoginValid()
        {
            if (string.IsNullOrEmpty(Login))
            {
                Info = "LoginIsEmpty";
                return false;
            }
            if (login.Length <= 20) return true;
            Info = "LoginTooLong";
            return false;
        }
    }
}