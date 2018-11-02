using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaEdenClient.DAO
{
    class MangaDao : IMangaDao
    {
        public async Task<bool> AddMangaFavoriteAsync(string mangaId)
        {
            using (SqliteConnection db = new SqliteConnection(App.APP_DB_STRING))
            {
                db.Open();
                // TODO Code Goes Here
                db.Close();
            }
            throw new NotImplementedException();
        }

        public async Task<bool> AddMangaHistoryAsync(string mangaId)
        {
            using (SqliteConnection db = new SqliteConnection(App.APP_DB_STRING))
            {
                db.Open();
                // TODO Code Goes Here
                db.Close();
            }
            throw new NotImplementedException();
        }

        public async Task<bool> CreateMangaAsync(Manga manga)
        {
            if (manga.Title == null || manga.Id == null || manga.Alias == null)
            {
                return false;
            }
            using (SqliteConnection db = new SqliteConnection(App.APP_DB_STRING))
            {
                db.Open();
                // TODO Code Goes Here
                db.Close();
            }
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteMangaAsync(string mangaId)
        {
            using (SqliteConnection db = new SqliteConnection(App.APP_DB_STRING))
            {
                db.Open();
                // TODO Code Goes Here
                db.Close();
            }
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteMangaFavoriteAsync(string mangaId)
        {
            using (SqliteConnection db = new SqliteConnection(App.APP_DB_STRING))
            {
                db.Open();
                // TODO Code Goes Here
                db.Close();
            }
            throw new NotImplementedException();
        }

        public async Task<bool> MangaExistsAsync(string mangaId)
        {
            using (SqliteConnection db = new SqliteConnection(App.APP_DB_STRING))
            {
                db.Open();
                // TODO Code Goes Here
                db.Close();
            }
            throw new NotImplementedException();
        }

        public async Task ReadAllMangasAsync(Func<List<Manga>, bool> callback)
        {
            using (SqliteConnection db = new SqliteConnection(App.APP_DB_STRING))
            {
                db.Open();
                // TODO Code Goes Here
                db.Close();
            }
            throw new NotImplementedException();
        }

        public async Task<List<Manga>> ReadAllMangasAsync()
        {
            using (SqliteConnection db = new SqliteConnection(App.APP_DB_STRING))
            {
                db.Open();
                // TODO Code Goes Here
                db.Close();
            }
            throw new NotImplementedException();
        }

        public async Task ReadMangaAsync(string mangaId, Func<Manga, bool> callback)
        {
            using (SqliteConnection db = new SqliteConnection(App.APP_DB_STRING))
            {
                db.Open();
                // TODO Code Goes Here
                db.Close();
            }
            throw new NotImplementedException();
        }

        public async Task<Manga> ReadMangaAsync(string mangaId)
        {
            using (SqliteConnection db = new SqliteConnection(App.APP_DB_STRING))
            {
                db.Open();
                // TODO Code Goes Here
                db.Close();
            }
            throw new NotImplementedException();
        }

        public async Task ReadMangaFavoritesAsync(Func<List<Manga>, bool> callback)
        {
            using (SqliteConnection db = new SqliteConnection(App.APP_DB_STRING))
            {
                db.Open();
                // TODO Code Goes Here
                db.Close();
            }
            throw new NotImplementedException();
        }

        public async Task<List<Manga>> ReadMangaFavoritesAsync()
        {
            using (SqliteConnection db = new SqliteConnection(App.APP_DB_STRING))
            {
                db.Open();
                // TODO Code Goes Here
                db.Close();
            }
            throw new NotImplementedException();
        }

        public async Task ReadMangaHistoryAsync(Func<List<Manga>, bool> callback)
        {
            using (SqliteConnection db = new SqliteConnection(App.APP_DB_STRING))
            {
                db.Open();
                // TODO Code Goes Here
                db.Close();
            }
            throw new NotImplementedException();
        }

        public async Task<List<Manga>> ReadMangaHistoryAsync()
        {
            using (SqliteConnection db = new SqliteConnection(App.APP_DB_STRING))
            {
                db.Open();
                // TODO Code Goes Here
                db.Close();
            }
            throw new NotImplementedException();
        }

        public async Task SearchMangaAsync(string title, Func<List<Manga>, bool> callback)
        {
            using (SqliteConnection db = new SqliteConnection(App.APP_DB_STRING))
            {
                db.Open();
                // TODO Code Goes Here
                db.Close();
            }
            throw new NotImplementedException();
        }

        public async Task<List<Manga>> SearchMangaAsync(string title)
        {
            using (SqliteConnection db = new SqliteConnection(App.APP_DB_STRING))
            {
                db.Open();
                // TODO Code Goes Here
                db.Close();
            }
            throw new NotImplementedException();
        }

        public async Task SearchMangaCategoryAsync(List<string> categories, Func<List<Manga>, bool> callback)
        {
            using (SqliteConnection db = new SqliteConnection(App.APP_DB_STRING))
            {
                db.Open();
                // TODO Code Goes Here
                db.Close();
            }
            throw new NotImplementedException();
        }

        public async Task<List<Manga>> SearchMangaCategoryAsync(List<string> categories)
        {
            using (SqliteConnection db = new SqliteConnection(App.APP_DB_STRING))
            {
                db.Open();
                // TODO Code Goes Here
                db.Close();
            }
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateMangaAsync(Manga manga)
        {
            using (SqliteConnection db = new SqliteConnection(App.APP_DB_STRING))
            {
                db.Open();
                // TODO Code Goes Here
                db.Close();
            }
            throw new NotImplementedException();
        }
    }
}
