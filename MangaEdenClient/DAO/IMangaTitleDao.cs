using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaEdenClient.DAO
{
    interface IMangaTitleDao
    {
        List<MangaTitle> GetAllMangaTitles();

        MangaTitle GetMangaTitle(string id);

        Boolean SaveMangaTile(MangaTitle mangaTitle);

        Boolean DeleteMangaTitle(string id);
    }
}
