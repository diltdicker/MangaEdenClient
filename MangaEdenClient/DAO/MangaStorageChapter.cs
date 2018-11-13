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
    }
}
