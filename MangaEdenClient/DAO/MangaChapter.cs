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
    class MangaChapter
    {
        private List<BitmapImage> Images { get; set; }
        public string ChapterId { get; set; }
        public double ChapterNumber { get; set; }
        public string Date { get; set; }
        public string ChapterTitle { get; set; }

        public MangaChapter()
        {
            Images = new List<BitmapImage>();
            ChapterId = null;
            ChapterNumber = -1;
            Date = null;
            ChapterTitle = null;
        }

        /// <summary>
        /// Method to set the array of images for a chapter
        /// </summary>
        /// <param name="imageBufferArray">Must be in chronological order</param>
        public void SetImages(List<IBuffer> imageBufferArray)
        {
            Images = new List<BitmapImage>();
            foreach (IBuffer imageBuffer in imageBufferArray)
            {
                BitmapImage image = new BitmapImage();
                image.SetSource(imageBuffer.AsStream().AsRandomAccessStream());
                Images.Add(image);
            }
        }

        virtual public List<BitmapImage> GetImages()
        {
            return Images;
        }

        public int GetImageListCount()
        {
            return Images.Count;
        }
    }
}
