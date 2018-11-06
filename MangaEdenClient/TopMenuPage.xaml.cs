using MangaEdenClient.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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


            // Get all Manga
            HTTP.HttpWrapper.HttpGetMangaListAsync(-1, -1, (List<DAO.Manga> mangaList) =>
            {
                if (mangaList == null || mangaList.Count == 0)
                {
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
                    return true;
                }
            });
            Debug.WriteLine("After HttpWrapper");
        }
    }
}
