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

            //TestSqlLite.InitializeDatabase();
            HTTP.HttpWrapper.HttpGetMangaTitleList(0, 50, (mangaList) =>
            {
                Debug.WriteLine("inside delegate");
                return true;
            });
            Debug.WriteLine("After 2nd delegate");
            HTTP.HttpWrapper.HttpGetImageInBytes("89/895a2f7c551df340121483918440668e585c8a6de6b2300cc6fb2e9d.png", (bitmap) =>
            {
                Debug.WriteLine("inside image delegate");
                //TestImage.Source = bitmap;
                return true;
            });
            Debug.WriteLine("After 2nd delegate");
        }
        
    }
}
