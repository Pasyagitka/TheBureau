using System;
using System.Collections.ObjectModel;
using TheBureau.Repositories;

namespace TheBureau.ViewModels
{
    public class StatisticsViewModel : ViewModelBase
    {
        private ClientRepository _clientRepository = new ClientRepository();
        private RequestRepository _requestRepository = new RequestRepository();
        private BrigadeRepository _brigadeRepository = new BrigadeRepository();

        private ObservableCollection<Client> clients;
        private ObservableCollection<Request> requests;
        private ObservableCollection<Brigade> brigades;
        int countRed;

        public ObservableCollection<Client> Clients
        {
            get => clients;
            set { clients = value; OnPropertyChanged("Clients"); }
        }
        public ObservableCollection<Request> Requests
        {
            get => requests;
            set { requests = value; OnPropertyChanged("Requests"); }
        }
        public ObservableCollection<Brigade> Brigades
        {
            get => brigades;
            set { brigades = value; OnPropertyChanged("Brigades"); }
        }
        
        public string CountRed
        {
            get { return $" Новые заявки ( {countRed} )"; }
            set
            {
                countRed = Int32.Parse(value);
                OnPropertyChanged("CountRed");
            }
        }


        public StatisticsViewModel()
        {
            Clients = new ObservableCollection<Client>(_clientRepository.GetAll());
            Requests = new ObservableCollection<Request>(_requestRepository.GetAll());
            Brigades = new ObservableCollection<Brigade>(_brigadeRepository.GetAll());
            CountRed = _requestRepository.GetRedRequestsCount().ToString();
        }
    }
}