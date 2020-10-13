using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage.Streams;

namespace The_Missing_Pretzels
{
	public sealed partial class Leaderboard : Page
	{
		public Leaderboard()
		{
			this.InitializeComponent();

			Get_Leaderboard();
		}

		private async void Get_Leaderboard()
		{

		}
	}
}
