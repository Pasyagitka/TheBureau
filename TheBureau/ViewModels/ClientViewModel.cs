using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TheBureau.Models;
using TheBureau.Repositories;
using TheBureau.Views;

namespace TheBureau.ViewModels
{
    public class ClientViewModel : ViewModelBase
    {
       private AddressRepository _addressRepository = new AddressRepository();
        private RequestEquipmentRepository _requestEquipmentRepository = new RequestEquipmentRepository();
        private ClientRepository _clientRepository = new ClientRepository();
        private RequestRepository _requestRepository = new RequestRepository();
        private ObservableCollection<Client> _clients;
        private ObservableCollection<Request> _clientsRequests;

        private bool readOnly;
        object _selectedItem;
        private string _findClientsText;
        private int selectedIndex;
        private ICommand _deleteCommand;
        private ICommand _updateCommand;
        private ICommand _saveChangesCommand;
        private ICommand openEditClientWindowCommand;

        public ICommand DeleteCommand
        {
            get
            {
                return _deleteCommand ??
                       (_deleteCommand = new RelayCommand(obj =>
                       {
                           int clientid = (SelectedItem as Client).id;
                           var clientRequests = _requestRepository.FindByClientId(clientid);
                           var addresses = clientRequests.Select(x=>x.addressId).ToList();
                           var requestsid = clientRequests.Select(x=>x.id).ToList();
  
                           foreach (var id in requestsid)
                           {
                               _requestEquipmentRepository.DeleteByRequestId(id);
                           }
                           _requestEquipmentRepository.Save();
                           
                           foreach (var request in clientRequests.ToList())
                           {
                               _requestRepository.Delete(request.id);
                           }
                           _requestRepository.Save();
                           
                           foreach (var id in addresses)
                           {
                               _addressRepository.Delete(id);
                           }
                           _addressRepository.Save();
                           _clientRepository.Delete(clientid);
                           _clientRepository.Save();
                           Clients = new ObservableCollection<Client>(_clientRepository.GetAll());
                           SelectedItem = Clients.First();
                       }));
            }
        }
        public ICommand OpenEditClientWindowCommand => openEditClientWindowCommand ??= new RelayCommand(OpenEditClientWindow);

        private void OpenEditClientWindow(object sender)
        {
            var clientToEdit = SelectedItem as Client;
            EditClientView window = new(clientToEdit);
            if (window.ShowDialog() == true)
            {
                _clientRepository = new ClientRepository();
                Clients = new ObservableCollection<Client>(_clientRepository.GetAll());
                SelectedItem = _clientRepository.Get(clientToEdit.id);
            }
        }
        public ICommand EditClientCommand
        {
            get
            {
                return _saveChangesCommand = new RelayCommand(obj =>
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
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                SetClientsRequests();
                OnPropertyChanged("SelectedItem");
            }
        }
        public ObservableCollection<Request> ClientRequests
        {
            get => _clientsRequests;
            set
            {
                if (_selectedItem is Client)
                {
                    _clientsRequests = value;
                    OnPropertyChanged("ClientRequests");
                }
            }
        }
        public string FindClientText
        {
            get => _findClientsText;
            set
            {
                _findClientsText = value;
                Search(_findClientsText); 
                OnPropertyChanged("FindClientText");
            }
        }
        public ClientViewModel()
        {
            Update();
        }
        void SetClientsRequests()
        {
            ClientRequests = new ObservableCollection<Request>(_requestRepository.GetAll().Where(x => x.clientId == (_selectedItem as Client)?.id));
        }
        private void Search(string criteria)
        {
            Clients = new ObservableCollection<Client>(_clientRepository.FindClientsByCriteria(criteria));
            SelectedItem = Clients.First();
        }
        public void Update()
        {
            _clientRepository = new ClientRepository();
            Clients = new ObservableCollection<Client>(_clientRepository.GetAll());
            SelectedItem = Clients.First();
        }
    }
}