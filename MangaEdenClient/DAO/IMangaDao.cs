using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaEdenClient.DAO
{
    interface IMangaDao
    {
        Task ReadAllMangasAsync(Func<List<Manga>, bool> callback);

        Task<List<Manga>> ReadAllMangasAsync();

        Task ReadMangaAsync(string mangaId, Func<Manga, bool> callback);

        Task<Manga> ReadMangaAsync(string mangaId);

        Task SearchMangaAsync(string title, Func<List<Manga>, bool> callback);

        Task<List<Manga>> SearchMangaAsync(string title);

        Task<Boolean> CreateMangaAsync(Manga manga);

        Task<Boolean> DeleteMangaAsync(string mangaId);

        Task<Boolean> UpdateMangaAsync(Manga manga);

        Task<Boolean> MangaExistsAsync(string mangaId);

        Task ReadMangaHistoryAsync(Func<List<Manga>, bool> callback);

        Task<List<Manga>> ReadMangaHistoryAsync();

        Task<Boolean> AddMangaHistoryAsync(string mangaId);

        Task ReadMangaFavoritesAsync(Func<List<Manga>, bool> callback);

        Task<List<Manga>> ReadMangaFavoritesAsync();

        Task<Boolean> AddMangaFavoriteAsync(string mangaId);

        Task<Boolean> DeleteMangaFavoriteAsync(string mangaId);

        Task SearchMangaCategoryAsync(List<String> categories, Func<List<Manga>, bool> callback);

        Task<List<Manga>> SearchMangaCategoryAsync(List<String> categories);
    }
}
