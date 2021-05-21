using System;
using System.Collections;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Input;
using TheBureau.Models.DataManipulating;
using TheBureau.Repositories;

namespace TheBureau.ViewModels
{
    public class EmployeeEditViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private ErrorsViewModel _errorsViewModel;
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
            set { id = value; OnPropertyChanged("Id"); }
        }
        
        public string Surname
        {
            get => surname;
            set
            { 
                surname = value; 
                _errorsViewModel.ClearErrors("Surname");
                
                if (string.IsNullOrWhiteSpace(surname))
                {
                    _errorsViewModel.AddError("Surname", ValidationConst.FieldCannotBeEmpty);
                }
                
                var regex = new Regex(ValidationConst.NameRegex);
                if (!regex.IsMatch(surname))
                {
                    _errorsViewModel.AddError("Surname", ValidationConst.IncorrectSurname);
                }
                OnPropertyChanged("Surname");
            }

        }

        public string Firstname
        {
            get => firstname;
            set
            {
                firstname = value;
                _errorsViewModel.ClearErrors("Firstname");
                
                if (string.IsNullOrWhiteSpace(firstname))
                {
                    _errorsViewModel.AddError("Firstname", ValidationConst.FieldCannotBeEmpty);
                }

                var regex = new Regex(ValidationConst.NameRegex);
                if (!regex.IsMatch(firstname))
                {
                    _errorsViewModel.AddError("Firstname",  ValidationConst.IncorrectFirstname);
                }
                OnPropertyChanged("Firstname");
            }
        }

        public string Patronymic
        {
            get => patronymic;
            set
            {
                patronymic = value; 
                _errorsViewModel.ClearErrors("Patronymic");
                
                if (string.IsNullOrWhiteSpace(patronymic))
                {
                    _errorsViewModel.AddError("Patronymic", ValidationConst.FieldCannotBeEmpty);
                }
                var regex = new Regex(ValidationConst.NameRegex);
                if (!regex.IsMatch(patronymic))
                {
                    _errorsViewModel.AddError("Patronymic", ValidationConst.IncorrectPatronymic);
                }
                OnPropertyChanged("Patronymic");
            }
        }

        public string Email
        {
            get => email;
            set
            {
                email = value; 
                _errorsViewModel.ClearErrors("Email");
                if (string.IsNullOrWhiteSpace(email))
                {
                    _errorsViewModel.AddError("Email", ValidationConst.FieldCannotBeEmpty);
                }
                if (email.Length > 255)
                {
                    _errorsViewModel.AddError("Email", ValidationConst.EmailLengthExceeded);
                }
                var regex = new Regex(ValidationConst.EmailRegex);
                if (!regex.IsMatch(email))
                {
                    _errorsViewModel.AddError("Email", ValidationConst.IncorrectEmailStructure);
                }
                OnPropertyChanged("Email");
            }
        }

        public string ContactNumber
        {
            get => contactNumber.ToString();
            set
            {
                _errorsViewModel.ClearErrors("ContactNumber");
                if (value.Length != 12)
                {
                    _errorsViewModel.AddError("ContactNumber", ValidationConst.IncorrectNumberStructure);
                }
                contactNumber = decimal.Parse(value); 
                OnPropertyChanged("ContactNumber");
            }
        }
        public int? BrigadeId
        {
            get => brigadeid;
            set
            {
                brigadeid = value; 
                OnPropertyChanged("BrigadeId");
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
                ContactNumber = Employee.contactNumber.ToString();
                BrigadeId = Employee.brigadeId;
                OnPropertyChanged("Employee");
            }
        }

        private DelegateCommand<object> editEmployeeCommand;

        public ICommand EditEmployeeCommand
        {
            get
            {
                if (editEmployeeCommand == null)
                {
                    editEmployeeCommand = new DelegateCommand<object>(EditEmployee, CanEditEmployee);
                }
                return editEmployeeCommand;
            }
        }
        public bool CanEditEmployee(object sender)
        {
            return !HasErrors;
        }

        private void EditEmployee(object sender)
        {
            var clientUpdate = _employeeRepository.Get(Id);
            clientUpdate.firstname = Firstname;
            clientUpdate.surname = Surname;
            clientUpdate.patronymic = Patronymic;
            clientUpdate.email = Email;
            clientUpdate.contactNumber = decimal.Parse(ContactNumber);
            clientUpdate.brigadeId = BrigadeId;
            _employeeRepository.Update(clientUpdate);
            _employeeRepository.SaveChanges();
        }

        public EmployeeEditViewModel(Employee selectedEmployee)
        {
            _errorsViewModel = new ErrorsViewModel();
            _errorsViewModel.ErrorsChanged += ErrorsViewModel_ErrorsChanged;
            Employee = selectedEmployee;
        }

        #region Validation
        public IEnumerable GetErrors(string propertyName)
        {
            return _errorsViewModel.GetErrors(propertyName);
        }
        public bool HasErrors => _errorsViewModel.HasErrors;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        
        private void ErrorsViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
            OnPropertyChanged("CanEditEmployee");
        }
        #endregion
    }
}