using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaEdenClient.DAO
{
    interface IMangaChapterDao
    {
        Task ReadAllMangaChaptersAsync(string mangaId, Func<List<MangaChapter>, bool> callback);

        Task<List<MangaChapter>> ReadAllMangaChaptersAsync(string mangaId);

        Task ReadMangaChapterAsync(string chapterId, Func<MangaChapter, bool> callback);

        Task<MangaChapter> ReadMangaChapterAsync(string chapterId);

        Task<List<string>> ReadAllChapterIdsAsync(string mangaId);

        Task<Boolean> CreateMangaChapterAsync(Manga manga);

        Task<Boolean> DeleteMangaChapterAsync(string chapterId);

        Task<Boolean> UpdateMangaChapterAsync(Manga manga);
    }
}
