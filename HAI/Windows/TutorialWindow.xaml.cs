using HAI.Pages.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HAI.Windows
{
    /// <summary>
    /// Interaction logic for TutorialWindow.xaml
    /// </summary>
    public partial class TutorialWindow : Window
    {
        public TutorialWindow()
        {
            InitializeComponent();
        }

        private void Button_MouseEnter(object sender, RoutedEventArgs e)
            => SoundManager.PlayHoverSound();

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClickSound();

            this.Close();
        }
    }
}
