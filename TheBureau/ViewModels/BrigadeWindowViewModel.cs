using System.Collections.ObjectModel;
using System.Linq;
using System.Web.UI.WebControls.Expressions;
using System.Windows;
using System.Windows.Input;
using TheBureau.Models;
using TheBureau.Repositories;
using TheBureau.Views;

namespace TheBureau.ViewModels
{
    public class BrigadeWindowViewModel : ViewModelBase
    {
        private RequestRepository _requestRepository = new RequestRepository();
        private BrigadeRepository _brigadeRepository = new BrigadeRepository();
        private RequestEquipmentRepository _requestEquipmentRepository = new RequestEquipmentRepository();


        private ObservableCollection<Request> _brigadeRequests;
        private ObservableCollection<RequestEquipment> _requestEquipments; 
        private Brigade _currentBrigade;
        private Request _selectedItem;
        
        private string _findRequestText;

        
        private WindowState _windowState;
        
        private ICommand _logOutCommand;
        private ICommand _updateRequest;
        private ICommand _closeWindowCommand;
        private ICommand _minimizeWindowCommand;
        public ICommand UpdateRequestCommand => _updateRequest ??= new RelayCommand(OpenEditRequest);
        public ICommand CloseWindowCommand => _closeWindowCommand ??= new RelayCommand(obj => { Application.Current.Shutdown(); });
        public ICommand MinimizeWindowCommand => _minimizeWindowCommand ??= new RelayCommand(obj => { WindowState = WindowState.Minimized; });

        public WindowState  WindowState
        {
            get => _windowState;
            set { _windowState = value; OnPropertyChanged("WindowState"); }
        }
        public string FindRequestText
        {
            get => _findRequestText;
            set
            {
                _findRequestText = value;
                Search(_findRequestText);
                OnPropertyChanged("FindRequestText");
            }
        }
        
        public Brigade CurrentBrigade
        {
            get => _currentBrigade;
            set
            {
                _currentBrigade = value;
                OnPropertyChanged("CurrentBrigade");
            }
        }
        
        public ObservableCollection<Request> BrigadeRequests
        {
            get => _brigadeRequests;
            set
            {
                _brigadeRequests = value;
                OnPropertyChanged("BrigadeRequests");
            }
        }
        public Request SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                SetEquipment();
                OnPropertyChanged("SelectedItem");
            }
        }
        private void OpenEditRequest(object o)
        {
            var requestToEdit = SelectedItem;
            EditRequestFromBrigadeView window = new(requestToEdit);
            if (window.ShowDialog() == true)
            {
                _requestRepository = new RequestRepository();
                BrigadeRequests = new ObservableCollection<Request>(_requestRepository.GetRequestsByBrigadeId(CurrentBrigade.id));
                SelectedItem = _requestRepository.Get(requestToEdit.id);
            }
        }
        public BrigadeWindowViewModel()
        {
            WindowState = WindowState.Normal;
            var user = Application.Current.Properties["User"] as User;
            if (user!= null)
            {
                CurrentBrigade = _brigadeRepository.GetAll().FirstOrDefault(x => x.userId == user.id);
                if (CurrentBrigade != null)
                {
                    BrigadeRequests = 
                        new ObservableCollection<Request>(_requestRepository.GetRequestsByBrigadeId(CurrentBrigade.id));
                    SelectedItem = BrigadeRequests.First();
                }
            }
        }
        
        public ObservableCollection<RequestEquipment> RequestEquipments
        {
            get => _requestEquipments;
            set { _requestEquipments = value; OnPropertyChanged("RequestEquipments");}
        }
        public void SetEquipment()
        {
            RequestEquipments = new ObservableCollection<RequestEquipment>(_requestEquipmentRepository.GetAllByRequestId(SelectedItem.id));
        }
        
        public ICommand LogOutCommand
        {
            get
            {
                return _logOutCommand ??= new RelayCommand(obj =>
                {
                    Application.Current.Properties["User"] = null;
                    var helloWindow = new HelloWindowView();
                    helloWindow.Show();
                    Application.Current.Windows[0]?.Close();
                    OnPropertyChanged("LogOutCommand");
                });
            }
        }

        private void Search(string criteria)
        {
            BrigadeRequests = new ObservableCollection<Request>(_requestRepository.FindRequestsForBrigadeByCriteria(FindRequestText, _currentBrigade.id));
            SelectedItem = BrigadeRequests.First();
        }

    }
}