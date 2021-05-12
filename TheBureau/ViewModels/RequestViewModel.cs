using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TheBureau.Repositories;

namespace TheBureau.ViewModels
{
    public class RequestViewModel : ViewModelBase
    {
        //todo заявок к исполнению
        RequestRepository _requestRepository = new RequestRepository();
        BrigadeRepository _brigadeRepository = new BrigadeRepository();

        ObservableCollection<Request> requests;
        ObservableCollection<Brigade> _brigades;
        
        private int _requestStatus;
        private RelayCommand updateRequestCommand;
        private object _selectedItem;


        public string RequestStatus
        {
            get => _requestStatus.ToString();
            set
            {
                if (value.Equals("Готово")) 
                _requestStatus = 3; 
                else if (value.Equals("В процессе"))
                    _requestStatus = 2;
                else _requestStatus = 1;
                OnPropertyChanged("RequestStatus");
            }
        }
        
        public object SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                MessageBox.Show(value.ToString());
                OnPropertyChanged("SelectedRequestItem");
            }
        }
        
        public RelayCommand UpdateRequestCommand
        {
            get
            {
                return updateRequestCommand ??= new RelayCommand(obj =>
                {
                    //todo запретить обновнять другие пока этот обновляется
                    MessageBox.Show(SelectedItem.ToString());
                    // var requestUpdate = _requestRepository.Get((SelectedItem as Request).id);
                    // requestUpdate.status = Int32.Parse(RequestStatus);
                    // _requestRepository.Update(requestUpdate);
                    // _requestRepository.SaveChanges();
                    OnPropertyChanged("UpdateRequestCommand");
                });
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
            get => requests; 
            set 
            { 
                requests = value; 
                OnPropertyChanged("Requests"); 
            } 
        }

        public RequestViewModel()
        {
            Requests = new ObservableCollection<Request>(_requestRepository.GetAll());
            Brigades = new ObservableCollection<Brigade>(_brigadeRepository.GetAll());
        }

    }
}