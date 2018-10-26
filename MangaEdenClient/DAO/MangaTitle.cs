using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaEdenClient.DAO
{
    class MangaTitle
    {
        private string ImageString { get; set; }
        private string Title { get; set; }
        private string Id { get; set; }
        private string Alias { get; set; }
        private string Status { get; set; }
        private List<String> Categories { get; set; }
        private bool Completed { get; set; }                // 1: ongoing 2: completed
        private double LastDate { get; set; }               // unix epoch timestamp


        public MangaTitle(string ImageString, string Title, string Id, string Alias,
            string Status, List<String> Categories, bool Completed, double LastDate)
        {
            this.ImageString = ImageString;
            this.Title = Title;
            this.Id = Id;
            this.Alias = Alias;
            this.Status = Status;
            this.Categories = Categories;
            this.Completed = Completed;
            this.LastDate = LastDate;
        }

        public MangaTitle(dynamic manga)
        {
            if (manga.a is String)
            {
                Debug.WriteLine("it exists");
            } else
            {
                Debug.WriteLine("doesn't exist");
            }
            this.Alias = manga.a;
            Debug.WriteLine(this.Alias + " : alias");
        }
    }
}
