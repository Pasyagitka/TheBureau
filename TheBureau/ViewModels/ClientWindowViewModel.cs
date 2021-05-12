using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using TheBureau.Repositories;

namespace TheBureau.ViewModels
{
    public class ClientWindowViewModel : ViewModelBase
    {
        private RequestRepository _requestRepository = new RequestRepository();
        private ClientRepository _clientRepository = new ClientRepository();
        private AddressRepository _addressRepository = new AddressRepository();
        
        private ObservableCollection<Request> _requests;
        private RelayCommand sendRequestCommand;
        
        private string findRequestText;
        private string firstname;
        private string surname;
        private string patronymic;
        private decimal contactNumber;
        private string email;
        private string country;
        private string city;
        private string street;
        private int house;
        private int corpus;
        private int flat;
        private string statusCost;
        private string emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        private string contactNumberPattern = @"^((\+7|7|8)+([0-9]){10})$";
        
        private string statusName;

        public RelayCommand SendRequestCommand
        {
            get
            {
                return sendRequestCommand ??= new RelayCommand(obj =>
                {
                    Request request = new Request();
                    Address address = new Address();
                    Client client = new Client();
                    _requestRepository.Add(request);
                    _addressRepository.Add(address);
                    _clientRepository.Add(client);
                });
            }
        }
        
        #region  fields
        public string Firstname
        {
            get => firstname;
            set
            {
                if (value.Length is >= 2 and <= 20)
                {
                    firstname = value;
                    statusName = String.Empty;
                }
                else
                    statusName = "Имя должно быть от 2 до 20 символов";
                OnPropertyChanged("Firstname");
            }
        }

        public string Surname
        {
            get => surname;
            set
            {
                if (value.Length is >= 2 and <= 20)
                {
                    surname = value;
                    statusName = String.Empty;
                }
                else
                    statusName = "Фамилия должна быть от 2 до 20 символов";
                OnPropertyChanged("Surname");
            }
        }

        public string Patronymic
        {
            get => patronymic;
            set
            {
                if (value.Length is >= 2 and <= 20)
                {
                    patronymic = value;
                    statusName = String.Empty;
                }
                else
                    statusName = "Отчество должно быть от 2 до 20 символов";
                OnPropertyChanged("Patronymic");
            }
        }

        public string ContactNumber
        {
           
            get => contactNumber.ToString();
            set
            {
                if (Decimal.TryParse(value, out contactNumber) && value.Length == 12
                    && Decimal.Parse(value) >= 0)
                {
                   contactNumber = Decimal.Parse(value);
                   statusCost = String.Empty;
                }
                else
                {
                    statusCost = "Номер должен быть длиной 12 и содержать лишь цифры";
                }
                OnPropertyChanged("ContactNumber"); 
            }
        }

        [EmailAddress]
        public string Email
        {
            get => email;
            set
            {
                value = email;
                OnPropertyChanged("Email");
            }
        }
        public string Country
        {
            get => country;
            set
            {
                if(value != null)
                value = country;
            }
        }

        public string City
        {
            get => city;
            set => city = value;
        }

        public string Street
        {
            get => street;
            set => street = value;
        }

        public string House
        {
            get => house.ToString();
            set => house = int.Parse(value);
        }
        
        public string Corpus
        {
            get => corpus.ToString();
            set => corpus = int.Parse(value);
        }
        
        public string Flat
        {
            get => flat.ToString();
            set => flat = int.Parse(value);
        }
        #endregion

        public ObservableCollection<Request> Requests
        {
            get => _requests;
            set { _requests = value; OnPropertyChanged("Requests"); }
        }

        public string FindRequestText
        {
            get => findRequestText;
            set
            {
                findRequestText = value;
                SetClientsRequests();
                OnPropertyChanged("FindRequestText");
            }
        }
        void SetClientsRequests()
        {
            Requests = new ObservableCollection<Request>(_requestRepository.GetAll().Where(x => 
                x.Client.surname.ToLower() == (FindRequestText.ToLower()) || 
                x.Client.email.ToLower() == (FindRequestText.ToLower())));
        }

        public ClientWindowViewModel()
        {
           
        }
        
        
        
        
    }
}