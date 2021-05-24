using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LiveCharts;
using TheBureau.Models;
using TheBureau.Repositories;

namespace TheBureau.ViewModels
{
    public class StatisticsViewModel : ViewModelBase
    {
        private ClientRepository _clientRepository = new ClientRepository();
        private RequestRepository _requestRepository = new RequestRepository();
        private BrigadeRepository _brigadeRepository = new BrigadeRepository();

        private ObservableCollection<Client> clients;
        private ObservableCollection<Request> requests;
        private ObservableCollection<Brigade> brigades;
        int countRed;
        private int countYellow;
        private int countGreen;

        private ChartValues<int> redValues;
        private ChartValues<int> yellowValues;
        private ChartValues<int> greenValues;

        public ChartValues<int> RedValues
        {
            get => redValues;
            set { redValues = value; OnPropertyChanged("RedValues");}
        }

        public ChartValues<int> YellowValues
        {
            get => yellowValues;
            set { yellowValues = value;  OnPropertyChanged("YellowValues");}
        }

        public ChartValues<int> GreenValues
        {
            get => greenValues;
            set { greenValues = value; OnPropertyChanged("GreenValues");}
        }

        public ObservableCollection<Client> Clients
        {
            get => clients;
            set { clients = value; OnPropertyChanged("Clients"); }
        }
        public ObservableCollection<Request> Requests
        {
            get => requests;
            set { requests = value; OnPropertyChanged("Requests"); }
        }
        public ObservableCollection<Brigade> Brigades
        {
            get => brigades;
            set { brigades = value; OnPropertyChanged("Brigades"); }
        }
        
        public string CountRed
        {
            get { return $" Новые заявки ( {countRed} )"; }
            set
            {
                countRed = Int32.Parse(value);
                OnPropertyChanged("CountRed");
            }
        }

        public int CountRed1
        {
            get => countRed;
            set
            {
                countRed = value;
                OnPropertyChanged("CountRed1");
            }
        }
        public int CountYellow
        {
            get => countYellow;
            set
            {
                countYellow = value;
                OnPropertyChanged("CountYellow");
            }
        }
        public int CountGreen
        {
            get => countGreen;
            set
            {
                countGreen = value;
                OnPropertyChanged("CountGreen");
            }
        }



        public StatisticsViewModel()
        {
            Clients = new ObservableCollection<Client>(_clientRepository.GetAll());
            Requests = new ObservableCollection<Request>(_requestRepository.GetAll());
            Brigades = new ObservableCollection<Brigade>(_brigadeRepository.GetAll());
            CountRed1 = _requestRepository.GetRedRequestsCount();
            CountGreen = _requestRepository.GetGreenRequestsCount();
            CountYellow = _requestRepository.GetYellowRequestsCount();

            GreenValues = new ChartValues<int>();
            GreenValues.Add(CountGreen);
            //GreenValues = new ChartValues<int>(new[] {CountGreen});
            RedValues = new ChartValues<int>(new[] {CountRed1});
            YellowValues = new ChartValues<int>(new[] {CountYellow});
        }
    }
}