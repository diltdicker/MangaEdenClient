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
    public sealed partial class ResultPage : Page
    {

        ObservableCollection<DAO.Manga> observableMangas = new ObservableCollection<DAO.Manga>();

        public ResultPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e">Navigation Helper:
        /// If Conditional == true: IdString is the search query for manga titles
        /// If Conditional == false: IdString is the search query for categories</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is NavigationHelper helper)
            {
                Debug.WriteLine("helping serach");
                Task.Run(() => GetQueryAsync(helper.IdString, helper.Conditional));
            }
            else
            {
                Debug.WriteLine("Not searching");
            }
        }

        private async Task GetQueryAsync(string id, bool isMangaTitle)
        {
            List<DAO.Manga> mangas = null;
            if (isMangaTitle)
            {
                Debug.WriteLine("Search title");
                mangas = await new DAO.MangaDao().SearchMangaAsync(id);
            }
            else
            {
                Debug.WriteLine("Search category");
                mangas = await new DAO.MangaDao().SearchMangaCategoryAsync(id);
            }
            if (mangas != null)
            {
                Debug.WriteLine("found them size=" + mangas.Count);
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    foreach(DAO.Manga manga in mangas)
                    {
                        observableMangas.Add(manga);
                    }
                });
            }
        }

        private void MangaResultList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DAO.Manga manga = ((DAO.Manga)((ListView)sender).SelectedItem);
            Frame.Navigate(typeof(MangaPage), manga.Id);
        }
    }
}
