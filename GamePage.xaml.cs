using System;
using System.Timers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;
using System.Threading.Tasks;
using System.Diagnostics;
using Windows.System;

namespace The_Missing_Pretzels
{
    public sealed partial class GamePage : Page
    {
        public GamePage()
        {
            this.InitializeComponent();

            Begin_Game();

            Window.Current.CoreWindow.Dispatcher.AcceleratorKeyActivated += (window, e) =>
            {
                switch (e.VirtualKey)
                {
                    case VirtualKey.Escape:
                        this.Frame.Navigate(typeof(MainPage));
                        break;
                }
            };
        }

        int score = 0;
        int pretzelCount = 0;
        int pretzelLoss = 0;

        private async void Begin_Game()
        {

            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {

                while (pretzelCount <= 100 && pretzelLoss <= 50)
                {
                    int left = (new Random()).Next(0, 2000);

                    Button pretzelButton = new Button
                    {
                        Name = "pretzel" + pretzelCount,
                        Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)),
                        Height = 61,
                        Width = 73,
                        Margin = new Thickness
                        {
                            Top = 0,
                            Left = left
                        },
                    };

                    pretzelButton.Click += (sender, e) =>
                    {
                        pretzelCount--;
                        score++;
                        Pretzel_Click(pretzelButton);
                    };

                    Image pretzelImage = new Image
                    {
                        Source = new BitmapImage(new Uri("ms-appx:///Assets/pretzel.png", UriKind.Absolute)),
                        Stretch = Stretch.UniformToFill,
                    };

                    pretzelButton.Content = pretzelImage;

                    Pretzels_Stack.Children.Add(pretzelButton);

                    Debug.WriteLine("Added pretzel #" + pretzelCount);

                    Pretzel_Move(pretzelButton);

                    pretzelCount++;

                    await Task.Delay(500);
                }

                if (pretzelLoss >= 50)
                {
                    Debug.WriteLine("Game over!");

                    ContentDialog deleteFileDialog = new ContentDialog
                    {
                        Title = "Delete file permanently?",
                        Content = "Game over! Your score was: " + score + "! Would you like to play again?",
                        PrimaryButtonText = "Play Again",
                        CloseButtonText = "Return to Menu"
                    };

                    ContentDialogResult result = await deleteFileDialog.ShowAsync();

                    if (result == ContentDialogResult.Primary)
                    {
                        score = 0;
                        pretzelCount = 0;
                        pretzelLoss = 0;

                        Pretzels_Stack.Children.Clear();

                        await Task.Delay(500);

                        Begin_Game();
                    }
                    else
                    {
                        this.Frame.Navigate(typeof(MainPage));
                    }
                }
            });
        }

        private async void Pretzel_Click(Button pretzelButton)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Pretzels_Stack.Children.Remove(pretzelButton);
                Debug.WriteLine("Removed a pretzel!");
            });
        }

        private async void Pretzel_Move(Button pretzelButton)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                while (pretzelButton.Margin.Top <= 50)
                {
                    pretzelButton.Margin = new Thickness
                    {
                        Top = pretzelButton.Margin.Top + 1,
                        Left = pretzelButton.Margin.Left
                    };

                    await Task.Delay(500);
                }

                if (pretzelButton.Margin.Top >= 50)
                {
                    Pretzels_Stack.Children.Remove(pretzelButton);
                    pretzelCount--;
                    pretzelLoss++;

                    Debug.WriteLine("Lost a pretzel! " + pretzelLoss);
                }
            });
        }
    }
}
