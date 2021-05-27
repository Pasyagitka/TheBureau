using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using TheBureau.Enums;
using TheBureau.Models;
using TheBureau.Repositories;
using TheBureau.Services;

namespace TheBureau.ViewModels
{
    public class BrigadeViewModel : ViewModelBase
    {
        private const string BrigadeLoginBase = "brigade";

        private readonly BrigadeRepository _brigadeRepository = new();
        private readonly RequestRepository _requestRepository = new();
        private readonly EmployeeRepository _employeeRepository = new();
        private readonly UserRepository _userRepository = new();
        
        private ObservableCollection<Brigade> _brigades;
        private ObservableCollection<Employee> _employees;
        
        private ICommand _addBrigade;
        private ICommand _deleteBrigade;
        
        private Brigade _selectedItem;

        public ObservableCollection<Employee> Employees
        {
            get => _employees;
            set { _employees = value; OnPropertyChanged("Employees"); } 
        }
        public Brigade SelectedItem
        {
            get => _selectedItem;
            set { _selectedItem = value; OnPropertyChanged("SelectedItem"); }
        }
        public ICommand AddBrigade
        {
            get
            {
                return _addBrigade ??= new RelayCommand(obj =>
                {
                    Brigade newBrigade = new();
                    newBrigade.userId = null;
                    _brigadeRepository.Add(newBrigade);
                    _brigadeRepository.Save();
                    
                    User newUser = new();
                    newUser.login = BrigadeLoginBase + newBrigade.id;
                    newUser.password = PasswordHash.CreateHash(newUser.login);
                    newUser.role = (int)Roles.brigade;
                    _userRepository.Add(newUser);
                    _userRepository.Save();

                    newBrigade.userId = newUser.id;
                    _brigadeRepository.Update(newBrigade);
                    _brigadeRepository.Save();
                    
                    Brigades = new ObservableCollection<Brigade>(_brigadeRepository.GetAll());
                });
            }
        }
        public ICommand DeleteBrigade
        {
            get
            {
                return _deleteBrigade ??= new RelayCommand(obj =>
                {
                    var id = SelectedItem.id;
                    var userId = _brigadeRepository.Get(id).userId;

                    foreach (var employee in Employees.Where(x => x.brigadeId == id))
                    {
                        employee.brigadeId = null;
                        _employeeRepository.Update(employee);
                    }
                    _employeeRepository.SaveChanges();
                    
                    if (userId != null)
                    {
                        _userRepository.Delete((int) userId);
                    }

                    var r = _requestRepository.GetRequestsByBrigadeId(id);
                    foreach (var rq in r)
                    {
                        rq.brigadeId = null;
                        _requestRepository.Update(rq);
                    }
                    _requestRepository.Save();
                    _brigadeRepository.Delete(id);
                    _brigadeRepository.Save();
                    _userRepository.Save();
                    
                    SelectedItem = Brigades.First();
                    Brigades = new ObservableCollection<Brigade>(_brigadeRepository.GetAll());
                });
            }
        }
        
        public ObservableCollection<Brigade> Brigades 
        { 
            get => _brigades; 
            set 
            { 
                _brigades = value; 
                OnPropertyChanged("Brigades"); 
            } 
        }
        public BrigadeViewModel()
        {
            Brigades = new ObservableCollection<Brigade>(_brigadeRepository.GetAll());
            Employees = new ObservableCollection<Employee>(_employeeRepository.GetAll());
            SelectedItem = Brigades.First();
        }
        
    }
}