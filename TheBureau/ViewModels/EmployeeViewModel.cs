using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TheBureau.Models;
using TheBureau.Repositories;
using TheBureau.Views;

namespace TheBureau.ViewModels
{
    public class EmployeeViewModel : ViewModelBase
    {
        //todo поиск не работает
        private EmployeeRepository _employeeRepository;
        private BrigadeRepository _brigadeRepository;
        
        private ObservableCollection<Employee> _employees;
        private ObservableCollection<Brigade> _employeeBrigades;
        
        private bool _readOnly;
        private Employee _selectedItem;
        private string _findEmployeesText;
        private int _selectedIndex;
        
        private ICommand _deleteCommand;
        private ICommand _updateCommand;
        private ICommand _saveChangesCommand;
        private ICommand _openEditEmployeeWindowCommand;
        private ICommand _openAddEmployeeWindowCommand;

        
        public ObservableCollection<Employee> Employees
        {
            get => _employees;
            set { _employees = value; OnPropertyChanged("Employees"); }
        }
        public EmployeeViewModel()
        {
            _employeeRepository = new EmployeeRepository();
            _brigadeRepository = new BrigadeRepository();
            Employees = new ObservableCollection<Employee>(_employeeRepository.GetAll());
            SelectedItem = Employees.First();
        }

        public ICommand DeleteCommand
        {
            get
            {
                return _deleteCommand ??
                       (_deleteCommand = new RelayCommand(obj =>
                       {
                           var empl = SelectedItem as Employee;
                           if (empl != null)
                           {
                               int clientid = empl.id;
                               _employeeRepository.Delete(clientid);
                               _employeeRepository.SaveChanges();
                               Employees.Remove(SelectedItem as Employee); //todo REMOVE?? or new
                               SelectedItem = Employees.First();
                               OnPropertyChanged("Employees");
                           }
                       }));
            }
        }
        
        public ICommand OpenEditEmployeeWindowCommand =>
            _openEditEmployeeWindowCommand ??= new RelayCommand(OpenEditEmployeeWindow);

        public ICommand OpenAddEmployeeWindowCommand =>
            _openAddEmployeeWindowCommand ??= new RelayCommand(OpenAddEmployeeWindow);

        private void OpenAddEmployeeWindow(object sender)
        {
            AddEmployeeView view = new();
            if (view.ShowDialog() == true)
            {
                Update();
            }
        }
        
        private void OpenEditEmployeeWindow(object sender)
        {
            var employeeToEdit = SelectedItem;
            EditEmployeeView window = new(employeeToEdit);
            if (window.ShowDialog() == true)
            {
                Update();
                SelectedItem = _employeeRepository.Get(employeeToEdit.id);
            }
        }
        
        public ICommand SaveChangesCommand =>
            _saveChangesCommand = new RelayCommand(obj =>
            {
                Employees = new ObservableCollection<Employee>(_employeeRepository.GetAll());
                SelectedItem = Employees.First();
                OnPropertyChanged("SaveChangesCommand");
            });

        public Employee SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                SetEmployeeBrigade();
                OnPropertyChanged("SelectedItem");
            }
        }
        
        public void Update()
        {
            _employeeRepository = new EmployeeRepository();
            Employees = new ObservableCollection<Employee>(_employeeRepository.GetAll());
            EmployeeBrigade = new ObservableCollection<Brigade>(_brigadeRepository.GetAll());
            SelectedItem = Employees.First();
        }
        
        public ObservableCollection<Brigade> EmployeeBrigade
        {
            get => _employeeBrigades;
            set
            {
                _employeeBrigades = value;
                OnPropertyChanged("EmployeeBrigade");
            }
        }
        
        public string FindEmployeeText
        {
            get => _findEmployeesText;
            set
            {
                _findEmployeesText = value;
                Search(_findEmployeesText); 
                OnPropertyChanged("FindEmployeeText");
            }
        }

        void SetEmployeeBrigade()
        {
            EmployeeBrigade = new ObservableCollection<Brigade>(_brigadeRepository.GetAll().Where(x => x.id == _selectedItem?.brigadeId));
        }
        private void Search(string criteria)
        {
            Employees = new ObservableCollection<Employee>(_employeeRepository.FindEmployeesByCriteria(criteria));
            SelectedItem = Employees.First();
        }
    }
}