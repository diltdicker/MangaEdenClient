using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace MangaEdenClient.DAO
{
    class Manga
    {
        public BitmapImage Image { get; set; }
        public byte[] ImageByteArray { get; set; }
        public string Title { get; set; }
        public string Id { get; set; }
        public string Alias { get; set; }
        public string Status { get; set; }
        public List<String> Categories { get; set; }
        public List<String> ChapterIds { get; set; }
        public List<String> ChapterTitles { get; set; }
        public string Completed { get; set; }                // 1: ongoing 2: completed
        public double LastDate { get; set; }               // unix epoch timestamp
        public string Author { get; set; }
        public string Description { get; set; }
        public string CreatedDate { get; set; }
        public int Hits { get; set; }

        public Manga()
        {

        }

        public Manga(string Title, string Id, string Alias, string Completed)
        {
            this.Title = Title;
            this.Id = Id;
            this.Alias = Alias;
            this.Completed = Completed;
        }

        public Manga(dynamic dynamic)
        {

        }
    }
}
