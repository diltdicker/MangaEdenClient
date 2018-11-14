using MangaEdenClient.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class CategoryPage : Page
    {

        ObservableCollection<DAO.Manga> observableManga = new ObservableCollection<DAO.Manga>(); 

        public CategoryPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is string category)
            {
                Debug.WriteLine("helping serach");
                //Task.Run(() => GetQueryAsync(helper.IdString, helper.Conditional));
                Task.Run(async () => 
                {
                    List<DAO.Manga> mangas = null;
                    mangas = await new DAO.MangaDao().SearchMangaCategoryAsync(category);
                    await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        foreach (DAO.Manga manga in mangas)
                        {
                            observableManga.Add(manga);
                        }
                    });
                });
            }
            else
            {
                Debug.WriteLine("Not searching");
            }
        }

        private void GridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DAO.Manga manga = (sender as GridView).SelectedItem as DAO.Manga;
            if (manga != null)
            {
                Frame.Navigate(typeof(MangaPage), manga.Id);
            }
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
            foreach (Manga manga in mangas)
            {
                observableManga.Add(manga);
            }
        }
    }
}
