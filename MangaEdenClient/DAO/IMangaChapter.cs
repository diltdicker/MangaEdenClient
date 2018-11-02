using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace MangaEdenClient.DAO
{
    interface IMangaChapter
    {
        string ChapterId { get; set; }
        int ChapterNumber { get; set; }
        string Date { get; set; }
        string ChapterTitle { get; set; }
        string Title { get; set; }

        void SetImages(List<IBuffer> imageBufferArray);

        List<BitmapImage> GetImages();

        int GetImageListCount();
    }
}
