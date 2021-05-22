using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TheBureau.Repositories;
using TheBureau.Views;

namespace TheBureau.ViewModels
{
    public class BrigadeWindowViewModel : ViewModelBase
    {
        private RequestRepository _requestRepository = new RequestRepository();
        private BrigadeRepository _brigadeRepository = new BrigadeRepository();

        private ObservableCollection<Request> _brigadeRequests;
        private Brigade _currentBrigade;
        private Request _selectedItem;
        
        private ICommand logOutCommand;
        private RelayCommand _updateRequest;
        public ICommand UpdateRequestCommand => _updateRequest ??= new RelayCommand(OpenEditRequest);


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
                OnPropertyChanged("SelectedItem");
            }
        }
        private void OpenEditRequest(object o)
        {
            EditRequestFromBrigadeView window = new(SelectedItem);
            if (window.ShowDialog() == true)
            {
                _requestRepository = new RequestRepository();
                BrigadeRequests = new ObservableCollection<Request>(_requestRepository.GetRequestsByBrigadeId(CurrentBrigade.id));
            }
        }
        public BrigadeWindowViewModel() //todo int currentId
        {
            var user = Application.Current.Properties["User"] as User;
            CurrentBrigade = _brigadeRepository.GetAll().FirstOrDefault(x => x.userId == user.id); 
            //todo all to repo
            if (user != null && CurrentBrigade != null)
                BrigadeRequests = new ObservableCollection<Request>(_requestRepository.GetRequestsByBrigadeId(CurrentBrigade.id));
        }
        
        public ICommand LogOutCommand
        {
            get
            {
                return logOutCommand = new RelayCommand(obj =>
                {
                    Application.Current.Properties["User"] = null;
                    var helloWindow = new HelloWindowView();
                    helloWindow.Show();
                    Application.Current.Windows[0]?.Close();
                    OnPropertyChanged("LogOutCommand");
                });
            }
        }
    }
}