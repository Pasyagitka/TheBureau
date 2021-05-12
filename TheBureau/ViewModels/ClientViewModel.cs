using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TheBureau.Repositories;
using TheBureau.Views;

namespace TheBureau.ViewModels
{
    public class ClientViewModel : ViewModelBase
    {
        private ClientRepository _clientRepository = new ClientRepository();
        private RequestRepository _requestRepository = new RequestRepository();
        private ObservableCollection<Client> _clients;
        private ObservableCollection<Request> _clientsRequests;

        private bool readOnly;
        object selectedItem;
        private string findClientsText;
        private int selectedIndex;

        private RelayCommand deleteCommand;
        private RelayCommand updateCommand;
        private RelayCommand saveChangesCommand;

        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                       (deleteCommand = new RelayCommand(obj =>
                       {
                           _clientRepository.Delete((SelectedItem as Client).id);
                           Clients = new ObservableCollection<Client>(_clientRepository.GetAll());
                           SelectedItem = Clients.First();
                           OnPropertyChanged("DeleteCommand");
                       }));
                
            }
        }
        public RelayCommand UpdateCommand
        {
            get
            {
                return updateCommand ??= new RelayCommand(obj =>
                {
                    var editClientView = new EditClientView(SelectedItem as Client);
                    if (editClientView.ShowDialog() == true )
                    {
                        _clientRepository.SaveChanges();
                        Clients = new ObservableCollection<Client>(_clientRepository.GetAll());
                        SelectedItem = Clients.First();
                        OnPropertyChanged("UpdateCommand");
                    }
                    // _clientRepository.Update((SelectedItem as Client).id, new Client());
                    // Clients = new ObservableCollection<Client>(_clientRepository.GetAll());
                    // OnPropertyChanged("UpdateCommand");
                    
                });
            }
        }
        public ICommand SaveChangesCommand
        {
            get
            {
                return saveChangesCommand = new RelayCommand(obj =>
                {
                    Clients = new ObservableCollection<Client>(_clientRepository.GetAll());
                    SelectedItem = Clients.First();
                    OnPropertyChanged("SaveChangesCommand");
                });
            }
        }
        public ObservableCollection<Client> Clients
        {
            get => _clients;
            set { _clients = value; OnPropertyChanged("Clients"); }
        }
        public object SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                SetClientsRequests();
                OnPropertyChanged("SelectedItem");
            }
        }

        public void Update()
        {
            Clients = new ObservableCollection<Client>(_clientRepository.GetAll());
            SelectedItem = Clients.First();
        }
        public ObservableCollection<Request> ClientRequests
        {
            get => _clientsRequests;
            set
            {
                if (selectedItem is Client)
                {
                    _clientsRequests = value;
                    OnPropertyChanged("ClientRequests");
                }
            }
        }

        public string FindClientText
        {
            get => findClientsText;
            set
            {
                findClientsText = value;
                Search(findClientsText); 
                OnPropertyChanged("FindClientText");
            }
        }
        
        public ClientViewModel()
        {
            Clients = new ObservableCollection<Client>(_clientRepository.GetAll());
            SelectedItem = Clients.First();
        }
        void SetClientsRequests()
        {
            ClientRequests = new ObservableCollection<Request>(_requestRepository.GetAll().Where(x => x.clientId == (selectedItem as Client).id));
        }
        private void Search(string criteria){
        
            Clients = new ObservableCollection<Client>(_clientRepository.FindClientsByCriteria(criteria));
        }
    }
}