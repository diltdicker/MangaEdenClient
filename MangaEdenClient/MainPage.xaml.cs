using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;
using Microsoft.Data.Sqlite;
using Windows.Web.Http;
using Windows.UI.ViewManagement;
using Windows.UI;
using Windows.ApplicationModel.Core;
using MangaEdenClient.DAO;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MangaEdenClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            Debug.WriteLine("test");
            this.InitializeComponent();

            ApplicationViewTitleBar formattableTitleBar = ApplicationView.GetForCurrentView().TitleBar;
            formattableTitleBar.ButtonBackgroundColor = Colors.Transparent;
            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            //ApplicationView.PreferredLaunchViewSize = new Size(1280, 720);
            //ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            // Default switch frame to top menu
            MangaFrame.Navigate(typeof(TopMenuPage));
        }

        private void BackFrameButton_Click(object sender, RoutedEventArgs e)
        {
            if (MangaFrame.CanGoBack)
            {
                MangaFrame.GoBack();
            }
        }

        private void ForwardFrameButton_Click(object sender, RoutedEventArgs e)
        {
            if (MangaFrame.CanGoForward)
            {
                MangaFrame.GoForward();
            }
        }
    }
}
