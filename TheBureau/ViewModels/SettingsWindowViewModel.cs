using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Input;
using TheBureau.Repositories;

namespace TheBureau.ViewModels
{
    public class SettingsWindowViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly ErrorsViewModel _errorsViewModel;
        private CompanyRepository _companyRepository = new CompanyRepository();
        private string _email;
        private string _password;
        
        private ICommand _editSettingsCommand;
        
        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged("Email"); }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                
                _errorsViewModel.ClearErrors("Password");
                
                if (_password.Length  < 8 || _password.Length > 40)
                {
                    _errorsViewModel.AddError("Password", "Password length < 8 > 40");
                }
                OnPropertyChanged("Password");
            }
        }
        public SettingsWindowViewModel()
        {
            _errorsViewModel = new ErrorsViewModel();
            _errorsViewModel.ErrorsChanged += ErrorsViewModel_ErrorsChanged;

            Email = _companyRepository.Get().email;
            Password = _companyRepository.Get().password;
        }
        
        public ICommand EditSettingsCommand
        {
            get { return _editSettingsCommand ??= new RelayCommand(EditSettings, CanEditSettings); }
        }
        private bool CanEditSettings(object sender)
        {
            return !HasErrors;
        }
        private void EditSettings(object sender)
        {
            var company = _companyRepository.Get();
            company.password = Password;
            _companyRepository.Update();
            _companyRepository.SaveChanges();
        }

        #region Validation
        public IEnumerable GetErrors(string propertyName)
        {
            return _errorsViewModel.GetErrors(propertyName);
        }
        public bool HasErrors => _errorsViewModel.HasErrors;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        private void ErrorsViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
            OnPropertyChanged("CanEditSettings");
        }
        #endregion
    }

}