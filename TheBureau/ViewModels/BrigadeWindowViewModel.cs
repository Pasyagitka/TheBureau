using System.Collections.ObjectModel;
using System.Linq;
using TheBureau.Repositories;

namespace TheBureau.ViewModels
{
    public class BrigadeWindowViewModel : ViewModelBase
    {
        private RequestRepository _requestRepository = new RequestRepository();
        private BrigadeRepository _brigadeRepository = new BrigadeRepository();

        private ObservableCollection<Request> _requests;
        private ObservableCollection<Brigade> _brigades;
        
        public ObservableCollection<Request> BrigadeRequests
        {
            get => _requests;
            set
            {
                _requests = value;
                OnPropertyChanged("BrigadeRequests");
            }
        }
        public ObservableCollection<Brigade> Brigades
        {
            get => _brigades;
            set { _brigades = value; OnPropertyChanged("Brigades"); }
        }

        public BrigadeWindowViewModel() //int currentId
        {
            BrigadeRequests =
                new ObservableCollection<Request>(_requestRepository.GetRequestsByBrigadeId(1));
            Brigades = new ObservableCollection<Brigade>(_brigadeRepository.GetAll());
        }

    }
}