using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace MangaEdenClient.DAO
{
    class MangaStorageChapter : MangaChapter
    {
        public List<byte[]> ImageByteArrays { get; set; }

        public MangaStorageChapter() : base()
        {
            ImageByteArrays = new List<byte[]>();
            ChapterId = null;
            ChapterNumber = -1;
            Date = null;
            ChapterTitle = null;
        }

        override public List<BitmapImage> GetImages()
        {
            List<BitmapImage> images = new List<BitmapImage>();
            if (ImageByteArrays != null && ImageByteArrays.Count > 0)
            {
                // TODO turn byte arrays into a list of bitmap images
            }
            return images;
        }

        //public int GetImageListCount()
        //{
        //    return ImageByteArrays.Count;
        //}

        //public void SetImages(List<IBuffer> imageBufferArray)
        //{
        //    ImageByteArrays = new List<byte[]>();
        //    foreach (IBuffer imageBuffer in imageBufferArray)
        //    {
        //        ImageByteArrays.Add(imageBuffer.ToArray());
        //    }
        //}
    }
}
