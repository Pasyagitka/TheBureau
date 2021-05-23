using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Input;
using TheBureau.Models.DataManipulating;
using TheBureau.Repositories;

namespace TheBureau.ViewModels
{
    public class AddEmployeeViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly EmployeeRepository _employeeRepository = new();
        private readonly BrigadeRepository _brigadeRepository = new();
        private readonly ErrorsViewModel _errorsViewModel= new();
        
        private int _id;
        private string _surname;
        private string _firstname;
        private string _patronymic;
        private string _email;
        private decimal _contactNumber;
        private int? _brigadeid;
        Employee _employee;
        
        private ICommand _addEmployeeCommand;
        private ObservableCollection<Employee> _employees;
        private ObservableCollection<Brigade> _brigades;
        int _selectedBrigadeId;
        
        public int SelectedBrigadeId
        {
            get => _selectedBrigadeId;
            set { _selectedBrigadeId = value; OnPropertyChanged("SelectedBrigadeId"); }
        }
        
        public ObservableCollection<Brigade> Brigades 
        { 
            get => _brigades; 
            set { _brigades = value; OnPropertyChanged("Brigades"); } 
        }

        #region EmployeePropetries
        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged("Id"); }
        }
        
        public string Surname
        {
            get => _surname;
            set
            { 
                _surname = value; 
                _errorsViewModel.ClearErrors("Surname");
                
                if (string.IsNullOrWhiteSpace(_surname))
                {
                    _errorsViewModel.AddError("Surname", ValidationConst.FieldCannotBeEmpty);
                }
                if (_surname?.Length is > 20 or < 2)
                {
                    _errorsViewModel.AddError("Surname", ValidationConst.NameLengthExceeded);
                }
                var regex = new Regex(ValidationConst.LettersHyphenRegex);
                if (!regex.IsMatch(_surname!))
                {
                    _errorsViewModel.AddError("Surname", ValidationConst.IncorrectSurname);
                }
                OnPropertyChanged("Surname");
            }

        }

        public string Firstname
        {
            get => _firstname;
            set
            {
                _firstname = value;
                _errorsViewModel.ClearErrors("Firstname");
                
                if (string.IsNullOrWhiteSpace(_firstname))
                {
                    _errorsViewModel.AddError("Firstname", ValidationConst.FieldCannotBeEmpty);
                }
                if (_firstname?.Length is > 20 or < 2)
                {
                    _errorsViewModel.AddError("Firstname", ValidationConst.NameLengthExceeded);
                }
                var regex = new Regex(ValidationConst.LettersHyphenRegex);
                if (!regex.IsMatch(_firstname!))
                {
                    _errorsViewModel.AddError("Firstname",  ValidationConst.IncorrectFirstname);
                }
                OnPropertyChanged("Firstname");
            }
        }

        public string Patronymic
        {
            get => _patronymic;
            set
            {
                _patronymic = value; 
                _errorsViewModel.ClearErrors("Patronymic");
                
                if (string.IsNullOrWhiteSpace(_patronymic))
                {
                    _errorsViewModel.AddError("Patronymic", ValidationConst.FieldCannotBeEmpty);
                }
                if (_patronymic?.Length is > 20 or < 2)
                {
                    _errorsViewModel.AddError("Patronymic", ValidationConst.NameLengthExceeded);
                }
                var regex = new Regex(ValidationConst.LettersHyphenRegex);
                if (!regex.IsMatch(_patronymic!))
                {
                    _errorsViewModel.AddError("Patronymic", ValidationConst.IncorrectPatronymic);
                }
                OnPropertyChanged("Patronymic");
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value; 
                _errorsViewModel.ClearErrors("Email");
                if (string.IsNullOrWhiteSpace(_email))
                {
                    _errorsViewModel.AddError("Email", ValidationConst.FieldCannotBeEmpty);
                }
                if (_email?.Length > 255)
                {
                    _errorsViewModel.AddError("Email", ValidationConst.EmailLengthExceeded);
                }
                var regex = new Regex(ValidationConst.EmailRegex);
                if (!regex.IsMatch(_email!))
                {
                    _errorsViewModel.AddError("Email", ValidationConst.IncorrectEmailStructure);
                }
                OnPropertyChanged("Email");
            }
        }

        public string ContactNumber
        {
            get => _contactNumber.ToString();
            set
            {
                _contactNumber = decimal.Parse(value); 
                _errorsViewModel.ClearErrors("ContactNumber");
                
                if (string.IsNullOrWhiteSpace(_contactNumber.ToString()))
                {
                    _errorsViewModel.AddError("ContactNumber", ValidationConst.FieldCannotBeEmpty);
                }
                var regex = new Regex(ValidationConst.ContactNumberRegex);
                if (!regex.IsMatch(_contactNumber.ToString()))
                {
                    _errorsViewModel.AddError("ContactNumber", ValidationConst.IncorrectNumberStructure);
                }
                OnPropertyChanged("ContactNumber");
            }
        }
        public int? BrigadeId
        {
            get => _brigadeid;
            set { _brigadeid = value; OnPropertyChanged("BrigadeId"); }
        }
        #endregion

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
                ContactNumber = Employee.contactNumber.ToString();
                BrigadeId = Employee.brigadeId;
                OnPropertyChanged("Employee");
            }
        }
        public AddEmployeeViewModel()
        {
            _errorsViewModel.ErrorsChanged += ErrorsViewModel_ErrorsChanged;
            _errorsViewModel.AddError("Surname", ValidationConst.FieldCannotBeEmpty);
            _errorsViewModel.AddError("Firstname", ValidationConst.FieldCannotBeEmpty);
            _errorsViewModel.AddError("Patronymic", ValidationConst.FieldCannotBeEmpty);
            _errorsViewModel.AddError("Email", ValidationConst.FieldCannotBeEmpty);
            _errorsViewModel.AddError("ContactNumber", ValidationConst.FieldCannotBeEmpty);
            Brigades = new ObservableCollection<Brigade>(_brigadeRepository.GetAll()) {new Brigade {id = 0}};
        }
        
        public ICommand AddEmployeeCommand => _addEmployeeCommand ??= new RelayCommand(AddEmployee, CanAddEmployee);

        private bool CanAddEmployee(object sender) => !HasErrors;

        private void AddEmployee(object sender)
        {
            Employee employee = new Employee()
            {
                firstname = Firstname, surname = Surname, patronymic = Patronymic, email = Email,
                contactNumber = decimal.Parse(ContactNumber),
                brigadeId = SelectedBrigadeId == 0 ? (int?) null : SelectedBrigadeId
            };
            _employeeRepository.Add(employee);
            _employeeRepository.SaveChanges();
        }
        
        #region  Validation
        public IEnumerable GetErrors(string propertyName) => _errorsViewModel.GetErrors(propertyName);
        public bool HasErrors => _errorsViewModel.HasErrors;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        private void ErrorsViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
            OnPropertyChanged("CanAddEmployee");
        }
        
        #endregion
    }
}