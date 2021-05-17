using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using TheBureau.Enums;
using TheBureau.Repositories;

namespace TheBureau.ViewModels
{
    public class ClientWindowViewModel : ViewModelBase
    {
        private RequestRepository _requestRepository;
        private ClientRepository _clientRepository;
        private AddressRepository _addressRepository;
        private RequestEquipmentRepository _requestEquipmentRepository;
        
        private ObservableCollection<Request> _requests;
        private RelayCommand sendRequestCommand;
        
        private string _findRequestText;
        private string _firstname;
        private string _surname;
        private string _patronymic;
        private decimal _contactNumber;
        private string _email;
        private string _city;
        private string _street;
        private int _house;
        private int _corpus;
        private int _flat;
        private string _statusCost;
        private string _emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        private string _contactNumberPattern = @"^((\+7|7|8)+([0-9]){10})$";
        private int _rpQuantity;
        private int _rsQuantity;
        private int _kpQuantity;
        private int _ksQuantity;
        private int _vpQuantity;
        private bool _isRough;
        private bool _isClean;
        private string _comment;
        private DateTime _mountingDate;
        

        public DateTime MountingDate
        {
            get => _mountingDate;
            set
            {
                _mountingDate = value;
                OnPropertyChanged("MountingDate");
            }
        }

        public string Comment
        {
            get => _comment;
            set { _comment = value; OnPropertyChanged("Comment"); }
        }

        public int RpQuantity
        {
            get => _rpQuantity;
            set { _rpQuantity = value; OnPropertyChanged("RpQuantity");}
        }

        public int RsQuantity
        {
            get => _rsQuantity;
            set { _rsQuantity = value; OnPropertyChanged("RsQuantity");}
        }

        public int KpQuantity
        {
            get => _kpQuantity;
            set { _kpQuantity = value; OnPropertyChanged("KpQuantity");}
        }

        public int KsQuantity
        {
            get => _ksQuantity;
            set {  _ksQuantity = value; OnPropertyChanged("KsQuantity");}
        }

        public int VpQuantity
        {
            get => _vpQuantity;
            set { _vpQuantity = value; OnPropertyChanged("VpQuantity");}
        }

        public bool IsRough
        {
            get => _isRough;
            set { _isRough = value; OnPropertyChanged("IsRough");}
        }

        public bool IsClean
        {
            get => _isClean;
            set { _isClean = value; OnPropertyChanged("IsClean");}
        }

        public int Stage
        {
            get
            {
                if (IsRough)
                {
                    return IsClean ? 3 : 1;
                    //Черновая + чистовая - 3, черновая - 1
                }
                if (IsClean) return 2; //только чистовая
                return 0;
            }
        }

        private string statusName;

        public RelayCommand SendRequestCommand
        {
            get
            {
                return sendRequestCommand ??= new RelayCommand(obj =>
                {
                    //todo обновление сразу в поиске по фамилии/почте
                    var address = new Address{city=City, street = Street, house = Int32.Parse(House), 
                        corpus = Corpus, flat = Int32.Parse(Flat)};
                    //todo проверить, существует ли такой адрес
                    _addressRepository.Add(address);
                    _addressRepository.Save();
                    var client = new Client{firstname = Firstname, patronymic = Patronymic, surname = Surname, 
                        email = Email, contactNumber = Decimal.Parse(ContactNumber)};
                    //todo проверить, существует ли такой клиент
                    _clientRepository.Add(client);
                    _clientRepository.Save();
                    var request = new Request
                    {
                        clientId = client.id, addressId = address.id, stage=Stage, status = 1, mountingDate=MountingDate,
                        comment = Comment
                    };
                    
                    _requestRepository.Add(request);
                    _requestRepository.Save();
                    //todo quantity default 0
                    
                    if (RpQuantity != 0) {
                        var requestEquipmentRP = new RequestEquipment  {  requestId = request.id, equipmentId = "RP", quantity = RpQuantity };
                        _requestEquipmentRepository.Add(requestEquipmentRP);
                    }
                    if (RsQuantity != 0)
                    {
                        var requestEquipmentRS = new RequestEquipment { requestId = request.id, equipmentId = "RS", quantity = RsQuantity};
                        _requestEquipmentRepository.Add(requestEquipmentRS);

                    }
                    if (KpQuantity != 0)
                    {
                        var requestEquipmentKP = new RequestEquipment { requestId = request.id, equipmentId = "HP", quantity = KpQuantity };
                        _requestEquipmentRepository.Add(requestEquipmentKP);
                    }

                    if (KsQuantity != 0)
                    {
                        var requestEquipmentKS = new RequestEquipment  { requestId = request.id, equipmentId = "HS", quantity = KsQuantity };
                        _requestEquipmentRepository.Add(requestEquipmentKS);
                    }

                    if (VpQuantity != 0)
                    {
                        var requestEquipmentVP = new RequestEquipment { requestId = request.id, equipmentId = "VP", quantity = VpQuantity  };
                        _requestEquipmentRepository.Add(requestEquipmentVP);
                    }
                   
                    _requestEquipmentRepository.Save();

                    Update();
                    OnPropertyChanged("SendRequestCommand");
                });
            }
        }
        
        #region  fields
        public string Firstname
        {
            get => _firstname;
            set
            {
                if (value.Length is >= 2 and <= 20)
                {
                    _firstname = value;
                    statusName = String.Empty;
                }
                else
                    statusName = "Имя должно быть от 2 до 20 символов";
                OnPropertyChanged("Firstname");
            }
        }

        public string Surname
        {
            get => _surname;
            set
            {
                if (value.Length is >= 2 and <= 20)
                {
                    _surname = value;
                    statusName = String.Empty;
                }
                else
                    statusName = "Фамилия должна быть от 2 до 20 символов";
                OnPropertyChanged("Surname");
            }
        }

        public string Patronymic
        {
            get => _patronymic;
            set
            {
                if (value.Length is >= 2 and <= 20)
                {
                    _patronymic = value;
                    statusName = String.Empty;
                }
                else
                    statusName = "Отчество должно быть от 2 до 20 символов";
                OnPropertyChanged("Patronymic");
            }
        }

        public string ContactNumber
        {
           
            get => _contactNumber.ToString();
            set
            {
                if (Decimal.TryParse(value, out _contactNumber) && value.Length == 12
                    && Decimal.Parse(value) >= 0)
                {
                   _contactNumber = Decimal.Parse(value);
                   _statusCost = String.Empty;
                }
                else
                {
                    _statusCost = "Номер должен быть длиной 12 и содержать лишь цифры";
                }
                OnPropertyChanged("ContactNumber"); 
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        public string City
        {
            get => _city;
            set => _city = value;
        }

        public string Street
        {
            get => _street;
            set => _street = value;
        }

        public string House
        {
            get => _house.ToString();
            set => _house = int.Parse(value);
        }
        
        public string Corpus
        {
            get => _corpus.ToString();
            set => _corpus = int.Parse(value);
        }
        
        public string Flat
        {
            get => _flat.ToString();
            set => _flat = int.Parse(value);
        }
        #endregion

        public ObservableCollection<Request> Requests
        {
            get => _requests;
            set { _requests = value; OnPropertyChanged("Requests"); }
        }

        public string FindRequestText
        {
            get => _findRequestText;
            set
            {
                _findRequestText = value;
                SetClientsRequests();
                OnPropertyChanged("FindRequestText");
            }
        }
        void SetClientsRequests()
        {
            //todo перенести в репозиторий
            Requests = new ObservableCollection<Request>(_requestRepository.GetAll().Where(x => 
                x.Client.surname.ToLower() == (FindRequestText.ToLower()) || 
                x.Client.email.ToLower() == (FindRequestText.ToLower())));
        }

        public ClientWindowViewModel()
        { 
            Update();
            MountingDate = DateTime.Today;
        }

        public void Update()
        {
            _requestRepository = new RequestRepository();
            _clientRepository = new ClientRepository();
            _addressRepository = new AddressRepository();
            _requestEquipmentRepository = new RequestEquipmentRepository();
        }

    }
}