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
    class MangaStorage : Manga
    {
        public IBuffer ImageBuffer { get; set; }
        public List<MangaChapter> Chapters { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }

        public MangaStorage() : base()
        {
            ImageString = null;
            ImageBuffer = null;
            Title = null;
            Id = null;
            Alias = null;
            Status = null;
            Categories = new List<string>();
            Chapters = new List<MangaChapter>();
            LastDate = null;
            Author = null;
            Description = null;
            Hits = 0;
        }

        public async void GetImageAsync(Func<BitmapImage, bool> callback)
        {
            BitmapImage image = new BitmapImage();
            if (ImageBuffer != null)
            {
                await image.SetSourceAsync(this.ImageBuffer.AsStream().AsRandomAccessStream());
                //return image;
                callback.Invoke(image);
            }
            else
            {
                //return image;
                callback.Invoke(image);
            }
        }

        public List<string> GetChapterIds()
        {
            List<string> chapterIds = new List<string>();
            foreach(MangaChapter chapter in Chapters)
            {
                chapterIds.Add(chapter.ChapterId);
            }
            return chapterIds;
        }
    }
}
