using System.Collections.ObjectModel;
using System.Windows.Input;
using TheBureau.Repositories;

namespace TheBureau.ViewModels
{
    public class AddEmployeeViewModel : ViewModelBase
    {
        private EmployeeRepository _employeeRepository = new EmployeeRepository();
        private int id;
        private string surname;
        private string firstname;
        private string patronymic;
        private string email;
        private decimal? contactNumber;
        private int? brigadeid;
        private RelayCommand addEmployee;
        private ObservableCollection<Employee> _employees;

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
        public ObservableCollection<Employee> Employees 
        { 
            get => _employees; 
            set 
            { 
                _employees = value; 
                OnPropertyChanged("Employees"); 
            } 
        }

        public RelayCommand AddEmployee
        {
            get
            {
                return addEmployee ??= new RelayCommand(obj =>
                {
                    Employee employee = new();
                    employee.firstname = Firstname;
                    employee.surname = Surname;
                    employee.patronymic = Patronymic;
                    employee.email = Email;
                    employee.contactNumber = ContactNumber;
                    employee.brigadeId = BrigadeId;
                    _employeeRepository.Add(employee);
                    _employeeRepository.SaveChanges();
                });
            }
        }
    }
}