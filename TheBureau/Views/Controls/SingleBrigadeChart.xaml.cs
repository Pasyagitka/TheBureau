using System;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;

namespace TheBureau.Views.Controls
{
    public partial class SingleBrigadeChart : UserControl
    {
        public SingleBrigadeChart()
        {
            InitializeComponent();
            SeriesCollection = new SeriesCollection
            {
                new StackedRowSeries
                {
                    Values = new ChartValues<double> {8},
                    StackMode = StackMode.Values, // this is not necessary, values is the default stack mode
                    DataLabels = true
                },
                new StackedRowSeries
                {
                    Values = new ChartValues<double> {7},
                    StackMode = StackMode.Values,
                    DataLabels = true
                }
            };
 
            //adding series updates and animates the chart
            SeriesCollection.Add(new StackedRowSeries
            {
                Values = new ChartValues<double> {6},
                StackMode = StackMode.Values
            });
 
            //adding values also updates and animates
            // SeriesCollection[2].Values.Add(4d);
 
            Labels = new[] {"Chrome"};
            Formatter = value => value + " Mill";
 
            DataContext = this;
        }
 
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
        
    }
}