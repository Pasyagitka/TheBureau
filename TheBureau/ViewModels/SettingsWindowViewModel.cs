using System.Windows.Input;
using TheBureau.Repositories;

namespace TheBureau.ViewModels
{
    public class SettingsWindowViewModel : ViewModelBase
    {
        private CompanyRepository _companyRepository = new();
        private string _email;
        private string _password;
        //todo валидация
        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged("Login");}
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
        
        public SettingsWindowViewModel()
        {
            Email = _companyRepository.Get().email;
            Password = _companyRepository.Get().password;
        }
        
        private ICommand _editSettingsCommand;
        public ICommand EditSettingsCommand
        {
            get { return _editSettingsCommand ??= new RelayCommand(EditClient); }
        }
        
        private void EditClient(object sender)
        {
            var company = _companyRepository.Get();
            company.email = Email;
            company.password = Password;
            _companyRepository.Update();
            _companyRepository.SaveChanges();
        }
    }
}