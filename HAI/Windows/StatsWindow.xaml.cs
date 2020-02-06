using HAI.Models;
using LiveCharts;
using System;
using System.ComponentModel;
using System.Windows;

namespace HAI.Windows
{
    /// <summary>
    /// Interaction logic for StatsWindow.xaml
    /// </summary>
    public partial class StatsWindow : Window, INotifyPropertyChanged
    {
        public StatsWindow()
        {
            InitializeComponent();

            // Example
            Values = StatsData.Values;

            YFormatter = value => value + "%";

            YourLoses = StatsData.AiVictories;
            YourVictories = StatsData.PlayerVictories;

            AiVictories = StatsData.AiVictories;
            AiLoses = StatsData.PlayerVictories;

            DataContext = this;
        }

        #region Binding properties
        public ChartValues<double> Values { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public int YourVictories
        {
            get { return yourVictories; }
            set { yourVictories = value; NotifyPropertyChanged("YourVictories"); }
        }
        public int YourLoses
        {
            get { return yourLoses; }
            set { yourLoses = value; NotifyPropertyChanged("YourLoses"); }
        }
        public int AiVictories
        {
            get { return aiVictories; }
            set { aiVictories = value; NotifyPropertyChanged("AiVictories"); }
        }
        public int AiLoses
        {
            get { return aiLoses; }
            set { aiLoses = value; NotifyPropertyChanged("AiLoses"); }
        }

        private int yourVictories;
        private int yourLoses;
        private int aiVictories;
        private int aiLoses;
        #endregion

        #region Properties Change Notification
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        #endregion
    }
}
