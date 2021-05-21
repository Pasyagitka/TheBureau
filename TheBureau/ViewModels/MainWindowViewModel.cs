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
        private ICommand logOutCommand;
        private ICommand closeWindowCommand;
        private ICommand minimizeWindowCommand;
        private ICommand maximizeWindowCommand;

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
        public ICommand LogOutCommand
        {
            get
            {
                return logOutCommand = new RelayCommand(obj =>
                {
                    Application.Current.Properties["User"] = null;
                    var helloWindow = new HelloWindowView();
                    helloWindow.Show();
                    Application.Current.Windows[0]?.Close();
                    OnPropertyChanged("LogOutCommand");
                });
            }
        }

        #region Resize
        private WindowState _windowState;

        public WindowState  WindowState
        {
            get { return _windowState; }
            set
            {
                _windowState = value;
                OnPropertyChanged("WindowState");
            }
        }
        public ICommand CloseWindowCommand
        {
            get
            {
                return closeWindowCommand = new RelayCommand(obj =>
                {
                    Application.Current.Shutdown();
                });
            }
        }
        public ICommand MinimizeWindowCommand
        {
            get
            {
                return minimizeWindowCommand = new RelayCommand(obj =>
                {
                    WindowState = WindowState.Minimized;
                });
            }
        }
        public ICommand MaximizeWindowCommand
        {
            get
            {
                return maximizeWindowCommand = new RelayCommand(obj =>
                {
                    if (WindowState != WindowState.Maximized)
                    {
                        WindowState = WindowState.Maximized;
                    }
                    else
                    {
                        WindowState = WindowState.Normal;
                    }
                });
            }
        }

        #endregion
        public object Content
        {
            get { return content; }
            set
            {
                content = value;
                CountRed = _requestRepository.GetRedRequestsCount();
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
            WindowState = WindowState.Normal;
            //todo только 1 раз
            CountRed = _requestRepository.GetRedRequestsCount();
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