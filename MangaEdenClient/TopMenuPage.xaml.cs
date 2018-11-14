using MangaEdenClient.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MangaEdenClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TopMenuPage : Page
    {
        ObservableCollection<Manga> observableManga = new ObservableCollection<Manga>();

        public TopMenuPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)                            // Happens after TopMenuPage()
        {
            HTTP.HttpWrapper.HttpGetMangaListAsync(-1, -1, (List<Manga> mangaList) =>
            {
                if (mangaList == null || mangaList.Count == 0)
                {
                    Debug.WriteLine("Unable to Connect to internet");
                    return false;
                }
                else
                {
                    InsertAndSortbyPopular(mangaList);
                    SortByPopular();
                    //Debug.WriteLine("Finished");
                    if (App.FULL_UPDATE_FLAG)
                    {
                        App.FULL_UPDATE_FLAG = false;
                        //DBUpdateProgressBar.Visibility = Visibility.Visible;
                        //DBUpdateProgressBar.Maximum = mangaList.Count - 1;
                        MainPage.mainPage.InitProgressBar(mangaList.Count);
                        Task.Run(() => UpdateMangaDB(mangaList));                               // Runs in Background
                    }

                    return true;
                }
            });
        }

        private async Task UpdateMangaDB(List<Manga> mangas)
        {
            //progress_Max = mangas.Count;
            //progress_Value = 0;
            //Debug.WriteLine("Worker thread");
            for (int i = 0; i < mangas.Count; i++)
            {
                //Debug.WriteLine("Worker thread i: " + i);
                await new MangaDao().UpdateMangaAsync(mangas[i]);
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    MainPage.mainPage.UpdateProgress(i);
                });
            }
            //Debug.WriteLine("After DB Update Loop");
            await MainPage.mainPage.SetCategories();
        }

        private async void TestProgress2(int max, Func<int, int, bool> calllback)
        {
            int i = 0;
            while ( i < max)
            {
                await Task.Delay(250);
                i++;
            }
        }

        private void MangaGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Debug.WriteLine("Test Clicked");
            GridView grid = (GridView)sender;
            Manga manga = (Manga)grid.SelectedItem;
            //Debug.WriteLine(manga.Title);
            //Debug.WriteLine(manga.ToString());

            //new DAO.MangaDao().CreateMangaAsync(manga);
            //new DAO.MangaDao().UpdateMangaAsync(manga);
            Frame.Navigate(typeof(MangaPage), manga.Id);
        }

        private void SortByRecent()
        {
            List<Manga> mangas = observableManga.OrderByDescending(manga => manga.LastDate).ToList();
            observableManga.Clear();
            foreach (Manga manga in mangas)
            {
                observableManga.Add(manga);
            }
        }

        private void SortByAlphabet()
        {
            List<Manga> mangas = observableManga.OrderBy(manga => manga.Alias).ToList();
            observableManga.Clear();
            foreach (Manga manga in mangas)
            {
                observableManga.Add(manga);
            }
        }

        private void SortByPopular()
        {
            List<Manga> mangas = observableManga.OrderByDescending(manga => manga.Hits).ToList();
            observableManga.Clear();
            foreach(Manga manga in mangas)
            {
                observableManga.Add(manga);
            }
        }

        private void InsertAndSortbyPopular(List<Manga> mangas)
        {
            foreach(Manga manga in mangas)
            {
                observableManga.Add(manga);
            }
            SortByPopular();
        }

        private void SortPopular_Click(object sender, RoutedEventArgs e)
        {
            SortByPopular();
        }

        private void SortAlphabetical_Click(object sender, RoutedEventArgs e)
        {
            SortByAlphabet();
        }

        private void SortRecent_Click(object sender, RoutedEventArgs e)
        {
            SortByRecent();
        }
    }
}
