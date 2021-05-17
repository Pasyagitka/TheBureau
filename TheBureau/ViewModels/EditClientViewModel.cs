using System;
using System.Windows;
using System.Windows.Documents.DocumentStructures;
using System.Windows.Input;
using TheBureau.Repositories;

namespace TheBureau.ViewModels
{
    public class EditClientViewModel : ViewModelBase
    {
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
            set { _surname = value; OnPropertyChanged("Surname");}

        }

        public string Firstname
        {
            get => _firstname;
            set { _firstname = value; OnPropertyChanged("Firstname");}
        }

        public string Patronymic
        {
            get => _patronymic;
            set { _patronymic = value; OnPropertyChanged("Patronymic");}
        }

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged("Email");}
        }

        public decimal ContactNumber
        {
            get => _contactNumber;
            set
            {
                _contactNumber = value; OnPropertyChanged("ContactNumber");
                
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
                _email = Client.email;
                _contactNumber = Client.contactNumber;
                OnPropertyChanged("Client");
            }
        }

        public ICommand EditClientCommand
        {
            get { return _editClientCommand ??= new RelayCommand(EditClient, CanEdit); }
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
        public bool CanEdit(object sender)
        {
            return true;
        }
        public EditClientViewModel(Client selectedClient)
        {
            Client = selectedClient;
        }
    }
}