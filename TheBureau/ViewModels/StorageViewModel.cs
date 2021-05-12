using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TheBureau.Annotations;
using TheBureau.Repositories;

namespace TheBureau.ViewModels
{
    public class StorageViewModel : ViewModelBase
    {
        ToolRepository _toolRepository = new ToolRepository();
        AccessoryRepository _accessoryRepository = new AccessoryRepository();
        EquipmentRepository _equipmentRepository = new EquipmentRepository();
        
        ObservableCollection<Tool> tools;
        ObservableCollection<Accessory> accessories;
        ObservableCollection<Equipment> equipments;

        public ObservableCollection<Tool> Tools 
        { 
            get => tools; 
            set  {  tools = value;  OnPropertyChanged("Tools"); } 
        }
        public ObservableCollection<Accessory> Accessories 
        { 
            get => accessories; 
            set  {  accessories = value;  OnPropertyChanged("Accessories");  } 
        }

        public ObservableCollection<Equipment> Equipments
        {
            get => equipments;
            set { equipments = value;  OnPropertyChanged("Equipments");  }
        }

        public StorageViewModel()
        {
            Tools = new ObservableCollection<Tool>(_toolRepository.GetAll());
            Accessories = new ObservableCollection<Accessory>(_accessoryRepository.GetAll());
            Equipments = new ObservableCollection<Equipment>(_equipmentRepository.GetAll());
        }
        
    }
}