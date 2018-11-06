﻿using System;
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
        public string Status { get; set; }
        public List<String> Categories { get; set; }
        public string Completed { get; set; }                // 1: ongoing 2: completed
        public string LastDate { get; set; }               // unix epoch timestamp
        public int Hits { get; set; }

        public Manga()
        {
            ImageString = null;
            Title = null;
            Id = null;
            Alias = null;
            Status = null;
            Categories = new List<string>();
            Completed = null;
            LastDate = null;
            Hits = 0;
        }
    }
}
