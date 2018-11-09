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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MangaEdenClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ChapterPage : Page
    {
        ObservableCollection<ImageHolder> observableImages = new ObservableCollection<ImageHolder>();
        string ChapterId { get; set; }
        bool IsFavorite { get; set; }
        List<string> ChapterIdList { get; set; }

        DAO.MangaStorageChapter Chapter { get; set; }

        public ChapterPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Debug.WriteLine("Navigated");

            NavigationHelper helper = e.Parameter as NavigationHelper;
            ChapterId = helper.IdString;
            IsFavorite = helper.Conditional;
            Debug.WriteLine("after navigate: " + ChapterId);
            ChapterIdList = helper.DataObject as List<string>;

            // IF IS Favorite take from DB

            // ELSE take from internet

            HTTP.HttpWrapper.HttpGetMangaChapterAsync(ChapterId, (DAO.MangaChapter chapter) =>
            {
                if (chapter == null)
                {
                    Debug.WriteLine("Internet failure");
                    return false;
                }
                else
                {
                    Debug.WriteLine("putting images up");
                    List<BitmapImage> images = chapter.GetImages();
                    for (int i = 0; i < images.Count; i++)
                    {
                        //Debug.WriteLine("image: " + i);
                        observableImages.Add(new ImageHolder(images[i], i));
                    }
                    return true;
                }
            });
        }
    }
}
