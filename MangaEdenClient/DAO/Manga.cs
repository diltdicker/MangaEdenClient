using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace MangaEdenClient.DAO
{
    class Manga
    {
        public string ImageString { get; set; }
        public string Title { get; set; }
        public string Id { get; set; }
        public string Alias { get; set; }
        public string Status { get; protected set; }        // 1: ongoing 2: completed
        public List<String> Categories { get; set; }              
        public string LastDate { get; set; }                // unix epoch timestamp
        public int Hits { get; set; }

        public Manga()
        {
            ImageString = null;
            Title = null;
            Id = null;
            Alias = null;
            Status = null;
            Categories = new List<string>();
            LastDate = null;
            Hits = 0;
        }

        public void SetStatus(int status)
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

        public void SetLastDate(string lastDate)
        {
            if (lastDate != null && lastDate.Length > 0)
            {
                this.LastDate = lastDate;
            }
        }

        public BitmapImage getImage()
        {
            if (ImageString != null)
            {
                //Debug.WriteLine(ImageString);
                return new BitmapImage(new Uri(ImageString));
            }
            else
            {
                //(new Uri(AppContext.BaseDirectory + "/Assets/StoreLogo.png")
                return new BitmapImage();
            }
        }
        
        
        override public string ToString()
        {
            return "{\n" +
                "ImageString: " + ImageString + "\n" +
                "Title: " + Title + "\n" +
                "Id: " + Id + "\n" + 
                "Alias: " + Alias + "\n" +
                "Status: " + Status + "\n" + 
                "Categories: " + Categories.ToString() + "\n" +
                "Last Date: " + LastDate + "\n" +
                "Hits: " + Hits + "\n" +
                "}"; 
        }
    }
}
