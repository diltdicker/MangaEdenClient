using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MangaEdenClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MangaPage : Page
    {
        private ObservableCollection<DAO.MangaChapter> observableMangaChapters = new ObservableCollection<DAO.MangaChapter>();
        private string MangaId { get; set; }
        private DAO.MangaStorage StoredManga { get; set; }
        private bool IsFavorite { get; set; }

        private CancellationTokenSource tokenSource;

        public MangaPage()
        {

            //Debug.WriteLine("start of manga page");
            this.InitializeComponent();
            //Debug.WriteLine("testing new page");
            //MainPage.mainPage.TestChange();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)                            // Happens after MangaPage()
        {
            MangaId = e.Parameter as string;
            //Debug.WriteLine("Set param");

            // TODO Check if Favorite
            IsFavorite = false;
            // ELSE

            HTTP.HttpWrapper.HttpGetMangaAsync(MangaId, (DAO.MangaStorage mangaStorage) => {
                if (mangaStorage == null)
                {
                    Debug.WriteLine("Unable to get Manga From internet");
                    return false;
                }
                else
                {
                    StoredManga = mangaStorage;
                    MangaTitle.Text = mangaStorage.Title;
                    //Debug.WriteLine("Description: " + mangaStorage.Description);
                    MangaDescription.Text = mangaStorage.Description;
                    //Debug.WriteLine("got the manga");
                    mangaStorage.GetImageAsync((BitmapImage image) =>
                    {
                        //Debug.WriteLine("setting image");
                        MangaImage.Source = image;
                        return true;
                    });
                    foreach(DAO.MangaChapter chapter in mangaStorage.Chapters)
                    {
                        observableMangaChapters.Add(chapter);
                    }
                    return true;
                }
            });
        }

        private void MangaChapterList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView view = sender as ListView;
            DAO.MangaChapter mangaChapter = view.SelectedItem as DAO.MangaChapter;
            //Debug.WriteLine("ChapterID: " + mangaChapter.ChapterId);
            Frame.Navigate(typeof(ChapterPage), new NavigationHelper(mangaChapter.ChapterId, IsFavorite, StoredManga.GetChapterIds()));
        }

        private void FavoriteButton_Click(object sender, RoutedEventArgs e)
        {
            IsFavorite = !IsFavorite;
            if (IsFavorite)
            {
                //SavingMangaProgress.IsActive = true;
                SavingMangaProgress.Visibility = Visibility.Visible;
                EmptyHeart.Visibility = Visibility.Collapsed;
                FavoriteHeart.Visibility = Visibility.Visible;

                // TODO Start download process

                if (tokenSource != null)
                {
                    tokenSource.Dispose();
                }
                tokenSource = new CancellationTokenSource();
                Task.Run(() => TestFuncAsync(100, tokenSource.Token));
            }
            else
            {
                //SavingMangaProgress.IsActive = false;
                SavingMangaProgress.Visibility = Visibility.Collapsed;
                EmptyHeart.Visibility = Visibility.Visible;
                FavoriteHeart.Visibility = Visibility.Collapsed;

                tokenSource.Cancel();
            }
        }

        private async Task TestFuncAsync(int val, CancellationToken token)
        {
            for (int i = 0; i < val; i++)
            {
                await Task.Delay(500);
                Debug.WriteLine("i: " + i);
                if (token.IsCancellationRequested)
                {
                    return;
                }
            }
        }
    }
}
