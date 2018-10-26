using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaEdenClient.DAO
{
    interface IMangaDao
    {
        List<Manga> GetAllMangaTitles();

        Manga GetManga(string id);

        Boolean SaveManga(Manga manga);

        Boolean DeleteManga(string id);

        Boolean UpdateManga(Manga manga);

        List<Manga> SearchMangaTitles(string title);
    }
}
