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
        void SetImages(List<IBuffer> imageBufferArray);

        List<BitmapImage> GetImages();

        int GetImageListCount();
    }
}
