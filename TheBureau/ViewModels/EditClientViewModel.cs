using System.Windows.Documents.DocumentStructures;
using TheBureau.Repositories;

namespace TheBureau.ViewModels
{
    public class EditClientViewModel : ViewModelBase
    {
        private ClientRepository _clientRepository = new ClientRepository();

        private int id;
        private string surname;
        private string firstname;
        private string patronymic;
        private string email;
        private decimal contactNumber;

        private RelayCommand updateCommand;

        #region propetries

        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        
        public string Surname
        {
            get => surname;
            set { surname = value; OnPropertyChanged("Surname");}

        }

        public string Firstname
        {
            get => firstname;
            set { firstname = value; OnPropertyChanged("Firstname");}
        }

        public string Patronymic
        {
            get => patronymic;
            set { patronymic = value; OnPropertyChanged("Patronymic");}
        }

        public string Email
        {
            get => email;
            set { email = value; OnPropertyChanged("Email");}
        }

        public decimal ContactNumber
        {
            get => contactNumber;
            set
            {
                contactNumber = value; OnPropertyChanged("ContactNumber");
                
            }
        }
        #endregion propetries
        
        Client client;
        public Client Client
        {
            get => client;
            set
            { 
                client = value;
                Id = Client.id;
                Firstname = Client.firstname;
                Surname = Client.surname;
                Patronymic = Client.patronymic;
                email = Client.email;
                contactNumber = Client.contactNumber;
                OnPropertyChanged("Client");
            }
        }
        
        public RelayCommand EditCommand
        {
            get
            {
                return updateCommand ??= new RelayCommand(obj =>
                {
                    var clientUpdate = _clientRepository.Get(Id);
                    clientUpdate.firstname = Firstname;
                    clientUpdate.surname = Surname;
                    clientUpdate.patronymic = Patronymic;
                    clientUpdate.email = Email;
                    clientUpdate.contactNumber = ContactNumber;
                    _clientRepository.Update(clientUpdate);
                    _clientRepository.SaveChanges();

                    OnPropertyChanged("EditCommand");
                });
            }
        }
    }
}