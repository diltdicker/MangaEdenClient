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

            Debug.WriteLine("Top Menu");

            //Task.Run(() => TestProgress());

            //TestProgress2(100, (int val, int max) =>
            //{
            //    //progress_Max = max;

            //    DBUpdateProgressBar.Maximum = max;
            //    DBUpdateProgressBar.Value = val;
            //    Debug.WriteLine("max: " + max + " val: " + val);
            //    return true;
            //});

            //TestProgress();

            //progress_Max = 100;
            //progress_Value = 0;
            //while (progress_Value < progress_Max + 1)
            //{
            //    Debug.WriteLine(TEST_STRING);
            //    //await Task.Delay(500);
            //    TEST_STRING = "progress = " + progress_Value;
            //    progress_Value++;
            //}



            if (App.APP_FULL_FLAG)
            {
                App.APP_FULL_FLAG = false;
                // Get all Manga
                HTTP.HttpWrapper.HttpGetMangaListAsync(-1, -1, (List<DAO.Manga> mangaList) =>
                {
                    if (mangaList == null || mangaList.Count == 0)
                    {
                        Debug.WriteLine("Unable to Connect to internet");
                        return false;
                    }
                    else
                    {
                        foreach (Manga manga in mangaList)
                        {
                            manga.ImageString = HTTP.HttpWrapper.API_IMG_STRING + manga.ImageString;
                            observableManga.Add(manga);
                        }
                        Debug.WriteLine("Finished");
                        DBUpdateProgressBar.Visibility = Visibility.Visible;
                        DBUpdateProgressBar.Maximum = mangaList.Count - 1;
                        Task.Run(() => UpdateMangaDB(mangaList));                                       // Runs in Background

                        return true;
                    }
                });
            }
            
            //Debug.WriteLine("After HttpWrapper");
        }
        
        private async Task UpdateMangaDB(List<Manga> mangas)
        {
            //progress_Max = mangas.Count;
            //progress_Value = 0;
            for (int i = 0; i < mangas.Count; i++)
            {
                await new MangaDao().UpdateMangaAsync(mangas[i]);
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    DBUpdateProgressBar.Value = i;
                    if (DBUpdateProgressBar.Value == DBUpdateProgressBar.Maximum)
                    {
                        DBUpdateProgressBar.Visibility = Visibility.Collapsed;
                    }
                });
            }
            Debug.WriteLine("After DB Update Loop");
        }

        //private async Task TestProgress()
        //{
        //    progress_Max = 100;
        //    progress_Value = 0;
        //    DBUpdateProgressBar.Visibility = Visibility.Visible;
        //    while (progress_Value < progress_Max + 1)
        //    {
        //        Debug.WriteLine(TEST_STRING);
        //        await Task.Delay(500);
        //        TEST_STRING = "progress = " + progress_Value;
        //        progress_Value++;
        //        if (progress_Value % 5 == 0)
        //        {
        //            //ProgressBar_ValueChanged(null, null);
        //        }
        //    }    
        //}

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
            Debug.WriteLine("Test Clicked");
            GridView grid = (GridView)sender;
            Manga manga = (Manga)grid.SelectedItem;
            Debug.WriteLine(manga.Title);
            Debug.WriteLine(manga.ToString());

            //new DAO.MangaDao().CreateMangaAsync(manga);
            new DAO.MangaDao().UpdateMangaAsync(manga);
        }
    }
}
