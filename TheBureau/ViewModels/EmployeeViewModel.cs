using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
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
        
        private bool readOnly;
        object selectedItem;
        private string findEmployeesText;
        private int selectedIndex;
        
        private RelayCommand deleteCommand;
        private RelayCommand updateCommand;
        private RelayCommand saveChangesCommand;
        
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

        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                       (deleteCommand = new RelayCommand(obj =>
                       {
                           int clientid = (SelectedItem as Employee).id;
                           _employeeRepository.Delete(clientid);
                           _employeeRepository.SaveChanges();
                           Employees = new ObservableCollection<Employee>(_employeeRepository.GetAll());
                           SelectedItem = Employees.First();
                           //OnPropertyChanged("DeleteCommand");
                       }));
                
            }
        }
        private DelegateCommand openChangeProductWindowCommand;
        private DelegateCommand openAddEmployeeWindowCommand;
        
        public DelegateCommand OpenChangeProductWindowCommand
        {
            get
            {
                if (openChangeProductWindowCommand == null)
                {
                    openChangeProductWindowCommand = new DelegateCommand(openEditEmployeeWindow);
                }
                return openChangeProductWindowCommand;
            }
        }
        public DelegateCommand OpenAddEmployeeWindowCommand
        {
            get
            {
                if (openAddEmployeeWindowCommand == null)
                {
                    openAddEmployeeWindowCommand = new DelegateCommand(openAddEmployeeWindow);
                }
                return openAddEmployeeWindowCommand;
            }
        }
        
        private void openAddEmployeeWindow()
        {
            AddEmployeeView view = new();
            if (view.ShowDialog() == true)
            {
                Update();
            }
        
        }
        private void openEditEmployeeWindow()
        {
            EditEmployeeView window = new(SelectedItem as Employee);
            if (window.ShowDialog() == true)
            {
                Update();
            }
        
        }
        
        public ICommand SaveChangesCommand
        {
            get
            {
                return saveChangesCommand = new RelayCommand(obj =>
                {
                    Employees = new ObservableCollection<Employee>(_employeeRepository.GetAll());
                    SelectedItem = Employees.First();
                    OnPropertyChanged("SaveChangesCommand");
                });
            }
        }

        public object SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                SetClientsRequests();
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
                if (selectedItem is Employee)
                {
                    _employeeBrigades = value;
                    OnPropertyChanged("EmployeeBrigade");
                }
            }
        }
        
        public string FindEmployeeText
        {
            get => findEmployeesText;
            set
            {
                findEmployeesText = value;
                Search(findEmployeesText); 
                OnPropertyChanged("FindEmployeeText");
            }
        }

        void SetClientsRequests()
        {
            //todo null??
            EmployeeBrigade = new ObservableCollection<Brigade>(_brigadeRepository.GetAll().Where(x => x.id == (selectedItem as Employee)?.brigadeId));
        }
        private void Search(string criteria){
        
            Employees = new ObservableCollection<Employee>(_employeeRepository.GetAll()); //find by criteria
        }
    }
}