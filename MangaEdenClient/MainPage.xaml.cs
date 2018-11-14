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
using System.Collections.ObjectModel;
using System.Threading.Tasks;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MangaEdenClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage mainPage;
        //private bool categoryClearFlag = true;
        private ObservableCollection<Manga> observableSearchResults = new ObservableCollection<Manga>();
        private ObservableCollection<String> observableCategories = new ObservableCollection<string>();
        private List<string> categoryList = new List<string>();

        public MainPage()
        {
            mainPage = this;
            //Debug.WriteLine("test");
            this.InitializeComponent();

            ApplicationViewTitleBar formattableTitleBar = ApplicationView.GetForCurrentView().TitleBar;
            formattableTitleBar.ButtonBackgroundColor = Colors.Transparent;
            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            //ApplicationView.PreferredLaunchViewSize = new Size(1280, 720);
            //ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            // Default switch frame to top menu
            MangaFrame.Navigate(typeof(TopMenuPage));

            Task.Run(() => SetCategories());
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

        public void InitProgressBar(int maxValue)
        {
            DBUpdateProgress.Visibility = Visibility.Visible;
            DBUpdateProgress.Maximum = maxValue - 1;
            DBUpdateProgress.Value = 0;
        }

        public void UpdateProgress(int value)
        {
            DBUpdateProgress.Value = value;
            if (value >= DBUpdateProgress.Maximum)
            {
                DBUpdateProgress.Visibility = Visibility.Collapsed;
            }
        }

        private void SearchResultList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("selected result");
            Manga manga = (sender as ListView).SelectedItem as Manga;
            if (manga != null)
            {
                MangaFrame.Navigate(typeof(MangaPage), manga.Id);
                SearchFlyout.Hide();
            }
            
        }

        private void SearchBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            string query = ((AutoSuggestBox)sender).Text;
            Debug.WriteLine("Entered:" + query);
            MangaFrame.Navigate(typeof(ResultPage), new NavigationHelper(query, true, null));
            SearchFlyout.Hide();
        }

        public async Task SetCategories()
        {
            List<string> categories = await new CategoryDao().GetAllCategories();
            observableCategories.Clear();
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                foreach(string category in categories)
                {
                    observableCategories.Add(category);
                }
            });
        }

        private void CategoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ListView)sender).SelectedItem is string category)
            {
                Debug.WriteLine("Entered:" + category);
                MangaFrame.Navigate(typeof(CategoryPage), category);
                SearchFlyout.Hide();
                ((ListView)sender).SelectedIndex = -1;
            }
        }

        private void SearchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            observableSearchResults.Clear();
            Debug.WriteLine("tc:" + sender.Text);
            string query = sender.Text;
            if (sender.Text.Length > 2)
            {
                Debug.WriteLine("execute search:" + query);
                Task.Run(async () =>
                {
                    List<Manga> mangas = await new MangaDao().SearchMangaAsync(query);
                    Debug.WriteLine("mangasize= " + mangas.Count);
                    await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        observableSearchResults.Clear();
                        foreach (Manga manga in mangas)
                        {
                            observableSearchResults.Add(manga);
                        }
                    });
                });
            }
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            //while (MangaFrame.CanGoBack)
            //{
            //    MangaFrame.GoBack();
            //}

            MangaFrame.Navigate(typeof(TopMenuPage));
        }
    }
}
