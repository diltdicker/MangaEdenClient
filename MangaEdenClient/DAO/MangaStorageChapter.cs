using System;
using System.Collections.Generic;
using System.IO;
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
        public List<IBuffer> ImageBufferList { get; set; }

        public MangaStorageChapter() : base()
        {
            ImageBufferList = new List<IBuffer>();
            ChapterId = null;
            ChapterNumber = -1;
            Date = null;
            ChapterTitle = null;
        }

        public async void SetImages(List<IBuffer> imageBufferArray)
        {
            Images = new List<BitmapImage>();
            foreach (IBuffer imageBuffer in imageBufferArray)
            {
                BitmapImage image = new BitmapImage();
                await image.SetSourceAsync(imageBuffer.AsStream().AsRandomAccessStream());
                Images.Add(image);
            }
        }

        override public List<BitmapImage> GetImages()
        {
            List<BitmapImage> images = new List<BitmapImage>();
            if (ImageBufferList != null && ImageBufferList.Count > 0)
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
