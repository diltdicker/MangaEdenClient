using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace MangaEdenClient.DAO
{
    class MangaStorage
    {
        public string ImageString { get; set; }
        public IBuffer ImageBuffer { get; set; }
        public string Title { get; set; }
        public string Id { get; set; }
        public string Alias { get; set; }
        public string Status { get; set; }
        public List<String> Categories { get; set; }
        public List<String> ChapterIds { get; set; }
        public List<String> ChapterTitles { get; set; }
        public string Completed { get; set; }                // 1: ongoing 2: completed
        public string LastDate { get; set; }               // unix epoch timestamp
        public string Author { get; set; }
        public string Description { get; set; }
        public string CreatedDate { get; set; }
        public int Hits { get; set; }

        public MangaStorage()
        {
            ImageString = null;
            ImageBuffer = null;
            Title = null;
            Id = null;
            Alias = null;
            Status = null;
            Categories = new List<string>();
            ChapterIds = new List<string>();
            ChapterTitles = new List<string>();
            Completed = null;
            LastDate = null;
            Author = null;
            Description = null;
            CreatedDate = null;
            Hits = 0;
        }
    }
}
