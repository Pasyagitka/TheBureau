using System;
using System.Collections;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Input;
using TheBureau.Models.DataManipulating;
using TheBureau.Repositories;

namespace TheBureau.ViewModels
{
    public class EditClientViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private ErrorsViewModel _errorsViewModel;
        private ClientRepository _clientRepository = new ClientRepository();
        
        private int _id;
        private string _surname;
        private string _firstname;
        private string _patronymic;
        private string _email;
        private decimal _contactNumber;
        
        Client _client;
        
        private RelayCommand _editClientCommand;
        
        #region propetries

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }
        
        public string Surname
        {
            get => _surname;
            set 
            { 
                _surname = value; 
                
                _errorsViewModel.ClearErrors("Surname");
                var regex = new Regex(ValidationConst.NameRegex);
                if (!regex.IsMatch(_surname))
                {
                    _errorsViewModel.AddError("Surname", "Некорректная фамилия");
                }
                OnPropertyChanged("Surname");
            }

        }

        public string Firstname
        {
            get => _firstname;
            set
            {
                _firstname = value; 
                _errorsViewModel.ClearErrors("Firstname");
                var regex = new Regex(ValidationConst.NameRegex);
                if (!regex.IsMatch(_firstname))
                {
                    _errorsViewModel.AddError("Firstname", "Некорректное имя");
                }
                OnPropertyChanged("Firstname");
            }
        }

        public string Patronymic
        {
            get => _patronymic;
            set
            {
                _patronymic = value; 
                _errorsViewModel.ClearErrors("Patronymic");
                var regex = new Regex(ValidationConst.NameRegex);
                if (!regex.IsMatch(_patronymic))
                {
                    _errorsViewModel.AddError("Patronymic", "Некорректное отчество");
                }
                OnPropertyChanged("Patronymic");
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value; 
                _errorsViewModel.ClearErrors("Email");
                var regex = new Regex(ValidationConst.EmailRegex);
                if (!regex.IsMatch(_email))
                {
                    _errorsViewModel.AddError("Email", "Некорректный адрес электронной почты");
                }
                OnPropertyChanged("Email");
            }
        }

        public decimal ContactNumber
        {
            get => _contactNumber;
            set
            {
                _contactNumber = value; 
                _errorsViewModel.ClearErrors("ContactNumber");
                var regex = new Regex(ValidationConst.ContactNumberRegex);
                if (!regex.IsMatch(_contactNumber.ToString()))
                {
                    _errorsViewModel.AddError("ContactNumber", "Некорректный номер телефона");
                }
                OnPropertyChanged("ContactNumber");
            }
        }
        #endregion propetries

        public Client Client
        {
            get => _client;
            set
            { 
                _client = value;
                Id = Client.id;
                Firstname = Client.firstname;
                Surname = Client.surname;
                Patronymic = Client.patronymic;
                Email = Client.email;
                ContactNumber = Client.contactNumber;
                OnPropertyChanged("Client");
            }
        }

        public ICommand EditClientCommand
        {
            get { return _editClientCommand ??= new RelayCommand(EditClient, CanEditClient); }
        }

        private void EditClient(object sender)
        {
            var clientUpdate = _clientRepository.Get(Id);
            clientUpdate.firstname = Firstname;
            clientUpdate.surname = Surname;
            clientUpdate.patronymic = Patronymic;
            clientUpdate.email = Email;
            clientUpdate.contactNumber = ContactNumber;
            _clientRepository.Update(clientUpdate);
            _clientRepository.SaveChanges();
        }
        public bool CanEditClient(object sender)
        {
            return !HasErrors;
        }
        public EditClientViewModel(Client selectedClient)
        {
            _errorsViewModel = new ErrorsViewModel();
            _errorsViewModel.ErrorsChanged += ErrorsViewModel_ErrorsChanged;
            Client = selectedClient;
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
            OnPropertyChanged("CanEditClient");
        }
        #endregion
    }
}