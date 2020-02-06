using HAI.Pages;
using System.Windows;

namespace HAI.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            View.NavigationService.Navigate(new MainPage());
        }
    }
}
