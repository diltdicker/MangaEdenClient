using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace MangaEdenClient.DAO
{
    class MangaChapterDao : IMangaChapterDao
    {
        public async Task<bool> CreateMangaChapterAsync(Manga manga)
        {
            using (SqliteConnection db = new SqliteConnection(App.APP_DB_STRING))
            {
                db.Open();
                // TODO Code Goes Here
                db.Close();
            }
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteMangaChapterAsync(string chapterId)
        {
            using (SqliteConnection db = new SqliteConnection(App.APP_DB_STRING))
            {
                db.Open();
                // TODO Code Goes Here
                db.Close();
            }
            throw new NotImplementedException();
        }

        public async Task<List<string>> ReadAllChapterIdsAsync(string mangaId)
        {
            using (SqliteConnection db = new SqliteConnection(App.APP_DB_STRING))
            {
                db.Open();
                // TODO Code Goes Here
                db.Close();
            }
            throw new NotImplementedException();
        }

        public async Task ReadAllMangaChaptersAsync(string mangaId, Func<List<MangaChapter>, bool> callback)
        {
            List<MangaChapter> chapters = new List<MangaChapter>();
            using (SqliteConnection db = new SqliteConnection(App.APP_DB_STRING))
            {
                db.Open();
                SqliteCommand getAllChapters = new SqliteCommand("", db);
                SqliteDataReader query = null;
                try
                {
                    query = await getAllChapters.ExecuteReaderAsync();
                }
                catch (SqliteException e)
                {
                    Debug.WriteLine(e.StackTrace);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.StackTrace);
                }
                while (query.Read())
                {

                }
                db.Close();
                callback.Invoke(chapters);
            }
        }

        public async Task<List<MangaChapter>> ReadAllMangaChaptersAsync(string mangaId)
        {
            List<MangaChapter> chapters = new List<MangaChapter>();
            using (SqliteConnection db = new SqliteConnection(App.APP_DB_STRING))
            {
                db.Open();
                SqliteCommand getAllChapters = new SqliteCommand("", db);
                SqliteDataReader query = null;
                try
                {
                    query = await getAllChapters.ExecuteReaderAsync();
                }
                catch (SqliteException e)
                {
                    Debug.WriteLine(e.StackTrace);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.StackTrace);
                }
                while (query.Read())
                {

                }
                db.Close();
                return chapters;
            }
        }

        public async Task ReadMangaChapterAsync(string chapterId, Func<MangaChapter, bool> callback)
        {
            using (SqliteConnection db = new SqliteConnection(App.APP_DB_STRING))
            {
                db.Open();
                // TODO Code Goes Here
                db.Close();
            }
            throw new NotImplementedException();
        }

        public async Task<MangaChapter> ReadMangaChapterAsync(string chapterId)
        {
            using (SqliteConnection db = new SqliteConnection(App.APP_DB_STRING))
            {
                db.Open();
                // TODO Code Goes Here
                db.Close();
            }
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateMangaChapterAsync(Manga manga)
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
