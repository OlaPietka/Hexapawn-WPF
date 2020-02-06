using HAI.Models;
using HAI.Pages.Helpers;
using HAI.Windows;
using LiveCharts;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace HAI.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void GameButton_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClickSound();

            NavigationService.Navigate(new GamePage());
        }

        private void StatsButton_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClickSound();

            var statsWindow = new StatsWindow();
            statsWindow.Show();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClickSound();

            NavigationService.Navigate(new SettingsPage());
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
            => SoundManager.PlayHoverSound();
    }
}
