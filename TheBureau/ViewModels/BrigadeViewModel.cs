using System.Collections.ObjectModel;
using System.Linq;
using MaterialDesignThemes.Wpf;
using TheBureau.Models.DataManipulating;
using TheBureau.Repositories;

namespace TheBureau.ViewModels
{
    public class BrigadeViewModel : ViewModelBase
    {
        BrigadeRepository _brigadeRepository = new BrigadeRepository();
        private RequestRepository _requestRepository = new RequestRepository();
        private EmployeeRepository _employeeRepository = new EmployeeRepository();
        private UserRepository _userRepository = new UserRepository();

        private string _brigadeLogin = "brigade";
        ObservableCollection<Brigade> brigades;
        ObservableCollection<Employee> employees;
        private RelayCommand addBrigade;
        private RelayCommand deleteBrigade;
        //todo не обновляет бригады в employee изменениях
        object selectedItem;

        
        public ObservableCollection<Employee> Employees
        {
            get => employees;
            set
            {
                employees = value;
                OnPropertyChanged("Employees");
            } 
        }
        public object SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }
        public RelayCommand AddBrigade
        {
            get
            {
                return addBrigade ??= new RelayCommand(obj =>
                {
                    Brigade newBrigade = new();
                    newBrigade.userId = null;
                    _brigadeRepository.Add(newBrigade);
                    _brigadeRepository.Save();
                    
                    User newUser = new();
                    newUser.login = _brigadeLogin + newBrigade.id;
                    newUser.password = PasswordHash.CreateHash(newUser.login);
                    newUser.role = 2; //todo enums for roles and so on
                    _userRepository.Add(newUser);
                    _userRepository.Save();

                    newBrigade.userId = newUser.id;
                    _brigadeRepository.Update(newBrigade);
                    _brigadeRepository.Save();
                    
                    Brigades = new ObservableCollection<Brigade>(_brigadeRepository.GetAll());
                });
            }
        }
        public RelayCommand DeleteBrigade
        {
            get
            {
                return deleteBrigade ??= new RelayCommand(obj =>
                {
                    //todo selected item криво выделяет обычно
                    int id = (SelectedItem as Brigade).id;
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
                    
                    Brigades = new ObservableCollection<Brigade>(_brigadeRepository.GetAll());
                });
            }
        }
        


        public ObservableCollection<Brigade> Brigades 
        { 
            get => brigades; 
            set 
            { 
                brigades = value; 
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