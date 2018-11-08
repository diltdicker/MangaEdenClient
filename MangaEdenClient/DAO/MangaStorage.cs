using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace MangaEdenClient.DAO
{
    class MangaStorage : Manga
    {
        //public string ImageString { get; set; }
        public IBuffer ImageBuffer { get; set; }
        //public string Title { get; set; }
        //public string Id { get; set; }
        //public string Alias { get; set; }
        //public string Status { get; protected set; }
        //public List<String> Categories { get; set; }
        public List<MangaChapter> Chapters { get; set; }
        public string Completed { get; set; }                // 1: ongoing 2: completed
        //public string LastDate { get; set; }               // unix epoch timestamp
        public string Author { get; set; }
        public string Description { get; set; }
        public string CreatedDate { get; set; }
        //public int Hits { get; set; }

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
            Completed = null;
            LastDate = null;
            Author = null;
            Description = null;
            CreatedDate = null;
            Hits = 0;
        }

        public void SetCompleted(int status)
        {
            if (status == 0)
            {
                this.Status = "Suspended";
            }
            else if (status == 1)
            {
                this.Status = "Ongoing";
            }
            else
            {
                this.Status = "Completed";
            }
        }
    }
}
