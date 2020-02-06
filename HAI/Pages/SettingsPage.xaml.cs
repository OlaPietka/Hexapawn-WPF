using HAI.Pages.Helpers;
using HAI.Resources.Languages;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace HAI.Pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void Button_MouseEnter(object sender, RoutedEventArgs e)
            => SoundManager.PlayHoverSound();

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClickSound();

            NavigationService.Navigate(new MainPage());
        }

        private void ThemeButton_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClickSound();

            var clickedButton = sender as Button;
            var id = clickedButton.GetValue(UidProperty);

            ThemeManager.ApplyBase(isDark: id.Equals("Dark") ? true : false);
        }

        private void StyleButton_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClickSound();

            var clickedButton = sender as Button;
            var id = (string)clickedButton.GetValue(UidProperty);

            var color = ThemeManager.BlueColor;

            if(id == "Red")
                color = ThemeManager.RedColor;
            if(id == "Green")
                color = ThemeManager.GreenColor;

            ThemeManager.ApplyPrimary(color.Primary, color.Secondary);
            ThemeManager.ApplyAccent(color.Accent);
        }

        private void PLLabguage_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClickSound();

            LanguagePack.Culture = new CultureInfo("pl-PL");
            NavigationService.Navigate(new SettingsPage());
        }

        private void ENLabguage_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClickSound();

            LanguagePack.Culture = new CultureInfo("en");
            NavigationService.Navigate(new SettingsPage());
        }
    }
}
