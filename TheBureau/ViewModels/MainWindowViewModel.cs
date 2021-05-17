using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TheBureau.Repositories;
using TheBureau.Views;

namespace TheBureau.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        RequestRepository _requestRepository = new RequestRepository();

        int selectedIndex;
        private string mainTop;
        object content;
        int countRed;

        private ICommand openSettingsCommand;

        public ICommand OpenSettingsCommand
        {
            get
            {
                return openSettingsCommand = new RelayCommand(obj =>
                {
                    SettingsWindow sw = new SettingsWindow();
                    if (sw.ShowDialog() == true)
                    {
                       
                    }
                    OnPropertyChanged("OpenSettingsCommand");
                });
            }
        }
        public object Content
        {
            get { return content; }
            set
            {
                content = value;
                OnPropertyChanged("Content");
            }
        }

        public string MainTopText
        {
            get => mainTop;
            set
            {
                mainTop = value;
                OnPropertyChanged("MainTopText");
            }
        }
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                setPage(selectedIndex);
                OnPropertyChanged("SelectedIndex");
            }
        }
        public int CountRed
        {
            get { return countRed; }
            set
            {
                countRed = value;
                OnPropertyChanged("CountRed");
            }
        }

  
        public MainWindowViewModel()
        {            
            Content = new StatisticsView(); 
            //todo только 1 раз
            countRed = _requestRepository.GetRedRequestsCount();
            //UserName = CurrentUser.User.firstName + " " + CurrentUser.User.secondName;
            SelectedIndex = 1;
        }
       
        public void setPage(int index)
        {
            switch (index)
            {
                case 0:
                    Content = new StatisticsView();
                    MainTopText = "БЮРО МОНТАЖНИКА";
                    break;
                case 1:
                    Content = new RequestView();
                    MainTopText = "ЗАЯВКИ";
                    break;
                case 2:
                    Content = new BrigadeView();
                    MainTopText = "БРИГАДЫ";
                    break;
                case 3:
                    Content = new ClientView();
                    MainTopText = "КЛИЕНТЫ";
                    break;
                case 4:
                    Content = new EmployeeView();
                    MainTopText = "РАБОТНИКИ";
                    break;
                case 5:
                    Content = new StorageView();
                    MainTopText = "СКЛАД";
                    break;
                default:
                    Content = new StatisticsView();
                    MainTopText = "БЮРО МОНТАЖНИКА";
                    break;                    
            }
            
        }

    }
}