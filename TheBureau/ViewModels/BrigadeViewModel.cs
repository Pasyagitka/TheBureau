using System.Collections.ObjectModel;
using MaterialDesignThemes.Wpf;
using TheBureau.Repositories;

namespace TheBureau.ViewModels
{
    public class BrigadeViewModel : ViewModelBase
    {
        BrigadeRepository _repository = new BrigadeRepository();
        private EmployeeRepository _employeeRepository = new EmployeeRepository();
        
        ObservableCollection<Brigade> brigades;
        ObservableCollection<Employee> employees;
        private RelayCommand addBrigade;

        public ObservableCollection<Employee> Employees
        {
            get => employees;
            set
            {
                employees = value;
                OnPropertyChanged("Employees");
            } 
        }
        public RelayCommand AddBrigade
        {
            get
            {
                return addBrigade ??= new RelayCommand(obj =>
                {
                    Brigade newBrigade = new Brigade();
                    _repository.Add(newBrigade);
                    _repository.Save();
                    Brigades = new ObservableCollection<Brigade>(_repository.GetAll());
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
            Brigades = new ObservableCollection<Brigade>(_repository.GetAll());
            Employees = new ObservableCollection<Employee>(_employeeRepository.GetAll());
        }
        
    }
}