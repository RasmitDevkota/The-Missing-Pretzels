using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace The_Missing_Pretzels
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            Debug.WriteLine("hello");
        }


        private void Start_Game(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GamePage));
        }

        private void Leaderboard(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Leaderboard));
        }
    }
}