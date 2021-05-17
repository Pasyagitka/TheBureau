﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TheBureau.Repositories;
using TheBureau.Views;

namespace TheBureau.ViewModels
{
    public class RequestViewModel : ViewModelBase
    {
        RequestRepository _requestRepository = new();
        BrigadeRepository _brigadeRepository = new();
        private RequestEquipmentRepository _requestEquipmentRepository = new();

        ObservableCollection<Request> _requests;
        ObservableCollection<Brigade> _brigades;

        private RelayCommand _updateRequest;
        private RelayCommand _hideGreenRequests;
        private RelayCommand _showAllRequests;
        private Request _selectedItem;
        private ObservableCollection<RequestEquipment> _requestEquipments;
        
        public ICommand UpdateRequestCommand => _updateRequest ??= new RelayCommand(OpenEditRequest);
        public ICommand HideGreenRequestsCommand
        {
            get
            {
                return _hideGreenRequests ??= new RelayCommand(o =>
                {
                    Requests = new ObservableCollection<Request>(_requestRepository.GetToDoRequests());
                    SelectedItem = Requests.First();
                });
            }
        }
        public ICommand ShowAllRequestsCommand
        {
            get
            {
                return _showAllRequests ??= new RelayCommand(o =>
                {
                    Requests = new ObservableCollection<Request>(_requestRepository.GetAll());
                    SelectedItem = Requests.First();
                });
            }
        }
        public RequestViewModel()
        {
            Update();
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

        public ObservableCollection<Brigade> Brigades 
        { 
            get => _brigades; 
            set 
            { 
                _brigades = value; 
                OnPropertyChanged("Brigades"); 
            } 
        }
        public ObservableCollection<Request> Requests 
        { 
            get => _requests; 
            set 
            { 
                _requests = value;
                OnPropertyChanged("Requests"); 
            } 
        }
        public void Update()
        {
            _requestRepository = new RequestRepository();
            _brigadeRepository = new BrigadeRepository();
            _requestEquipmentRepository = new RequestEquipmentRepository();
            Requests = new ObservableCollection<Request>(_requestRepository.GetAll());
            Brigades = new ObservableCollection<Brigade>(_brigadeRepository.GetAll());
            SelectedItem = Requests.First();
        }

        public ObservableCollection<RequestEquipment> RequestEquipments
        {
            get => _requestEquipments;
            set { _requestEquipments = value; OnPropertyChanged("RequestEquipments");}
        }

        private void OpenEditRequest(object o)
        {
            EditRequestView window = new(SelectedItem);
            if (window.ShowDialog() == true)
            {
                Update();
            }
        }

        public void SetEquipment()
        {
            RequestEquipments = new ObservableCollection<RequestEquipment>(_requestEquipmentRepository.GetAllByRequestId(SelectedItem.id));
        }
    }
}