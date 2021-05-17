using System.Windows.Input;
using TheBureau.Repositories;

namespace TheBureau.ViewModels
{
    public class EmployeeEditViewModel : ViewModelBase
    {
        private EmployeeRepository _employeeRepository = new EmployeeRepository();
        private int id;
        private string surname;
        private string firstname;
        private string patronymic;
        private string email;
        private decimal? contactNumber;
        private int? brigadeid;

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

        public decimal? ContactNumber
        {
            get => contactNumber;
            set
            {
                contactNumber = value; OnPropertyChanged("ContactNumber");
                
            }
        }
        public int? BrigadeId
        {
            get => brigadeid;
            set
            {
                brigadeid = value; OnPropertyChanged("BrigadeId");
            }
        }
        #endregion propetries
        
        Employee _employee;
        public Employee Employee
        {
            get => _employee;
            set
            { 
                _employee = value;
                Id = Employee.id;
                Firstname = Employee.firstname;
                Surname = Employee.surname;
                Patronymic = Employee.patronymic;
                Email = Employee.email;
                ContactNumber = Employee.contactNumber;
                BrigadeId = Employee.brigadeId;
                OnPropertyChanged("Employee");
            }
        }

        private DelegateCommand<object> changeProductCommand;

        public ICommand ChangeProductCommand
        {
            get
            {
                if (changeProductCommand == null)
                {
                    changeProductCommand = new DelegateCommand<object>(EditClient);
                }
                return changeProductCommand;
            }
        }

        private void EditClient(object sender)
        {
            var clientUpdate = _employeeRepository.Get(Id);
            clientUpdate.firstname = Firstname;
            clientUpdate.surname = Surname;
            clientUpdate.patronymic = Patronymic;
            clientUpdate.email = Email;
            clientUpdate.contactNumber = ContactNumber;
            clientUpdate.brigadeId = BrigadeId;
            _employeeRepository.Update(clientUpdate);
            _employeeRepository.SaveChanges();
        }

        public EmployeeEditViewModel(Employee selectesEmployee)
        {
            Employee = selectesEmployee;
        }
    }
}