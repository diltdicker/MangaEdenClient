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
using Windows.Storage.Streams;
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
        ObservableCollection<BitmapImage> observableImages = new ObservableCollection<BitmapImage>();
        string ChapterId { get; set; }
        string chapterName;
        bool IsFavorite { get; set; }
        List<string> ChapterIdList { get; set; }

        DAO.MangaStorageChapter Chapter { get; set; }

        public ChapterPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //Debug.WriteLine("Navigated");

            NavigationHelper helper = e.Parameter as NavigationHelper;
            ChapterId = helper.IdString;
            IsFavorite = helper.Conditional;
            //Debug.WriteLine("after navigate: " + ChapterId);
            ChapterIdList = helper.DataObject as List<string>;

            // IF IS Favorite take from DB

            // ELSE take from internet

            //HTTP.HttpWrapper.HttpGetMangaChapterAsync(ChapterId, (DAO.MangaChapter chapter) =>
            //HTTP.HttpWrapper.HttpGetMangaStorageChapterAsync(ChapterId, (DAO.MangaStorageChapter chapter) =>
            //{
            //    if (chapter == null)
            //    {
            //        Debug.WriteLine("Internet failure");
            //        return false;
            //    }
            //    else
            //    {
            //        Chapter = chapter;
            //        chapterName = chapter.ChapterNumber + ": " + chapter.ChapterTitle;
            //        //ChapterTitle.GetBindingExpression(null).UpdateSource();
            //        //Debug.WriteLine("putting images up");
            //        ChapterProgressBar.Visibility = Visibility.Collapsed;
            //        //List<BitmapImage> images = chapter.GetImages();
            //        //for (int i = 0; i < images.Count; i++)
            //        //{
            //        //    Debug.WriteLine("image: " + i);
            //        //    observableImages.Add(new ImageHolder(images[i], i));
            //        //}
            //        //return true;
            //        Debug.WriteLine("done");
            //        Task.Run(() => AssignImagesAsync());
            //        return true;
            //    }
            //});

            //HTTP.HttpWrapper.HttpGetMangaStorageChapterAsync(ChapterId, (int progressValue, int progressMax) => 
            //{
            //    ChapterProgressBar.Value = progressValue;
            //    ChapterProgressBar.Maximum = progressMax;
            //    return true;
            //}, (DAO.MangaStorageChapter chapter) =>
            //{
            //    double progress = ChapterProgressBar.Value;
            //    foreach (BitmapImage image in chapter.GetImages())
            //    {
            //        observableImages.Add(image);
            //        ChapterProgressBar.Value = ++progress;
            //    }
            //    ChapterProgressBar.Visibility = Visibility.Collapsed;
            //    return true;
            //});

            HTTP.HttpWrapper.HttpGetChapterImagesAsync(ChapterId, (BitmapImage image) =>
            {
                observableImages.Add(image);
                return true;
            });

            //    Task.Run(() => HTTP.HttpWrapper.HttpGetMangaStorageChapterAsync(ChapterId, async (DAO.MangaStorageChapter chapter) =>
            //    {
            //        await chapter.GetImagesAsync();
            //        return true;
            //    }));
            //}
        }

        //private async Task AssignImagesAsync()
        //{
        //    try
        //    {
        //        foreach (IBuffer buffer in Chapter.ImageBufferList)
        //        {
        //            BitmapImage image = new BitmapImage();
        //            await image.SetSourceAsync(buffer.AsStream().AsRandomAccessStream());
        //            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
        //            {
        //                observableImages.Add(new ImageHolder(ref image));
        //            });
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.WriteLine(e.StackTrace);
        //        Debug.WriteLine(e.TargetSite);
        //    }
        //}
    }
}
