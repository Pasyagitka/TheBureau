using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TheBureau.Models.DataManipulating;
using TheBureau.Repositories;

namespace TheBureau.ViewModels
{
    public class EditRequestFromBrigadeViewModel : ViewModelBase
    {
        private RequestRepository _requestRepository = new RequestRepository();
        private BrigadeRepository _brigadeRepository = new();
        private int _requestStatus;
        private RelayCommand _updateRequest;
        private Request _requestForEdit;
        public bool sendEmail;

        public bool SendEmail
        {
            get => sendEmail;
            set { sendEmail = value; OnPropertyChanged("SendEmail"); }
        }
        public EditRequestFromBrigadeViewModel(Request request)
        {
            RequestForEdit = request;
        }
        public Request RequestForEdit
        {
            get => _requestForEdit;
            set
            {
                _requestForEdit = value;
                OnPropertyChanged("RequestForEdit");
            }
        }
        //todo command parameter

        public ICommand UpdateRequestCommand => _updateRequest ??= new RelayCommand(UpdateRequest);

        private void UpdateRequest(object o)
        {
            bool isStatusChanged = false;
            var request = _requestRepository.Get(RequestForEdit.id);
            if (Int32.Parse(RequestStatus) == 1 || Int32.Parse(RequestStatus) == 2 ||Int32.Parse(RequestStatus)== 3)
            {
                if (Int32.Parse(RequestStatus) != request.status) isStatusChanged = true;
                request.status = Int32.Parse(RequestStatus);
            }
            _requestRepository.Update(request);
            _requestRepository.Save();

            if (isStatusChanged && SendEmail)
            {
                Notifications.SendRequestStatusChanged(request);
            }
            OnPropertyChanged("Requests");
        }

        public string RequestStatus
        {
            get => _requestStatus.ToString();
            set
            {
                if (value.Contains("Готово"))
                    _requestStatus = 3; 
                else if (value.Contains("В процессе"))
                    _requestStatus = 2;
                else _requestStatus = 1;
                OnPropertyChanged("RequestStatus");
            }
        }
    }
}