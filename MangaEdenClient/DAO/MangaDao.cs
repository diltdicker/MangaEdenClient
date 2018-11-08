using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            bool flag = true;
            if (manga.Title == null || manga.Id == null || manga.Alias == null || manga.Status == null)
            {
                flag = false;
                return flag;
            }
            using (SqliteConnection db = new SqliteConnection(App.APP_DB_STRING))
            {
                db.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = db;

                command.CommandText = "INSERT INTO " + App.APP_MANGA_TABLE + " (manga_id, manga_title, alias, status, last_chapter_date, hits, image_url) " +
                    "VALUES (@ID, @TITLE, @ALIAS, @STATUS, @DATE, @HITS, @IMAGE);";
                command.Parameters.AddWithValue("@ID", manga.Id);
                command.Parameters.AddWithValue("@TITLE", manga.Title);
                command.Parameters.AddWithValue("@ALIAS", manga.Alias);
                command.Parameters.AddWithValue("@STATUS", manga.Status);
                if (manga.LastDate != null)
                {
                    command.Parameters.AddWithValue("@DATE", manga.LastDate);
                }
                else
                {
                    command.Parameters.AddWithValue("@DATE", DBNull.Value);
                }
                command.Parameters.AddWithValue("@HITS", manga.Hits);
                if (manga.ImageString != null)
                {
                    command.Parameters.AddWithValue("@IMAGE", manga.ImageString);
                }
                else
                {
                    command.Parameters.AddWithValue("@IMAGE", DBNull.Value);
                }

                try
                {
                    await command.ExecuteReaderAsync();
                }
                catch (SqliteException e)
                {
                    Debug.WriteLine(e.StackTrace);
                    Debug.WriteLine("CreateMangaAsync");
                    flag = false;
                }

                // Manga Categories

                foreach(string category in manga.Categories)
                {
                    SqliteCommand categoryCommand = new SqliteCommand();
                    categoryCommand.Connection = db;

                    categoryCommand.CommandText = "INSERT INTO " + App.APP_MANGA_CATEGORY_TABLE + " (manga_id, category) VALUES " +
                        "(@ID, @CATEGORY);";
                    categoryCommand.Parameters.AddWithValue("@ID", manga.Id);
                    categoryCommand.Parameters.AddWithValue("@CATEGORY", category);
                    try
                    {
                        await categoryCommand.ExecuteReaderAsync();
                    }
                    catch (SqliteException e)
                    {
                        Debug.WriteLine(e.StackTrace);
                        Debug.WriteLine("CreateMangaAsync - Category");
                        flag = false;
                    }
                }

                db.Close();
                Debug.WriteLine("CreateMangaAsync: " + flag);
            }
            return flag;
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

        /// <summary>
        /// Method to detect if manga already exists in database.
        /// </summary>
        /// <param name="mangaId">The Id String for the manga in question</param>
        /// <returns></returns>
        public async Task<bool> MangaExistsAsync(string mangaId)
        {
            bool flag = false;
            if (mangaId == null)
            {
                flag = false;
                return flag;
            }
            using (SqliteConnection db = new SqliteConnection(App.APP_DB_STRING))
            {
                db.Open();
                SqliteDataReader reader = null;
                SqliteCommand command = new SqliteCommand
                {
                    Connection = db,
                    CommandText = "SELECT * FROM " + App.APP_MANGA_TABLE + " WHERE manga_id = @MANGA_ID;"
                };
                command.Parameters.AddWithValue("@MANGA_ID", mangaId);
                try
                {
                    reader = await command.ExecuteReaderAsync();
                }
                catch (SqliteException e)
                {
                    Debug.WriteLine(e.StackTrace);
                    flag = false;
                }
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        Debug.WriteLine(reader.ToString());
                        Debug.WriteLine("MangaExistsAsync");
                        flag = true;
                    }
                }
                db.Close();
            }
            Debug.WriteLine("IfExists: " + mangaId + " : " + flag.ToString());
            return flag;
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

        /// <summary>
        /// Method to perform an Update/Insert of a Manga Object.
        /// </summary>
        /// <param name="manga">Manga Object to be updated/inserted into DB.
        /// required to have ID, Title, Alias, and Status</param>
        /// <returns>True if sucess, False if an Error is encountered</returns>
        public async Task<bool> UpdateMangaAsync(Manga manga)
        {
            //Debug.WriteLine("UpdateMangaAsync - Begin");
            bool flag = true;
            if (manga.Id == null || manga.Title == null || manga.Alias == null || manga.Status == null)
            {
                flag = false;
                return flag;
            }
            using (SqliteConnection db = new SqliteConnection(App.APP_DB_STRING))
            {
                db.Open();
                SqliteDataReader reader;
                SqliteCommand existsCommand = new SqliteCommand();
                existsCommand.Connection = db;

                // EXISTS

                existsCommand.CommandText = "SELECT manga_id FROM " + App.APP_MANGA_TABLE + " WHERE manga_id = @ID";
                existsCommand.Parameters.AddWithValue("@ID", manga.Id);

                try
                {
                    reader = await existsCommand.ExecuteReaderAsync();
                }
                catch (SqliteException e)
                {
                    Debug.WriteLine(e.StackTrace);
                    Debug.WriteLine("UpdateMangaAsync - Exists");
                    flag = false;
                    db.Close();
                    return flag;
                }
                bool update = false;
                while (await reader.ReadAsync())
                {
                    update = true;
                }
                //Debug.WriteLine("UpdateMangaAsync - Exists: " + update);
                //Debug.WriteLine("Last date: " + manga.LastDate + " | " + (manga.LastDate != null));

                // UPDATE/INSERT


                if (update)
                {
                    SqliteCommand updateCommand = new SqliteCommand();
                    updateCommand.Connection = db;
                    updateCommand.CommandText = "UPDATE " + App.APP_MANGA_TABLE + " SET manga_title = @TITLE, alias = @ALIAS, status = @STATUS, last_chapter_date = @DATE, hits = @HITS, " + 
                        "image_url = @IMAGE WHERE manga_id = @ID;";
                    updateCommand.Parameters.AddWithValue("@TITLE", manga.Title);
                    updateCommand.Parameters.AddWithValue("@ALIAS", manga.Alias);
                    updateCommand.Parameters.AddWithValue("@STATUS", manga.Status);
                    if (manga.LastDate != null)
                    {
                        updateCommand.Parameters.AddWithValue("@DATE", manga.LastDate);
                    }
                    else
                    {
                        updateCommand.Parameters.AddWithValue("@DATE", DBNull.Value);
                    }
                    updateCommand.Parameters.AddWithValue("@HITS", manga.Hits);
                    updateCommand.Parameters.AddWithValue("@ID", manga.Id);
                    if (manga.ImageString != null)
                    {
                        updateCommand.Parameters.AddWithValue("@IMAGE", manga.ImageString);
                    }
                    else
                    {
                        updateCommand.Parameters.AddWithValue("@IMAGE", DBNull.Value);
                    }

                    //string query = "UPDATE " + App.APP_MANGA_TABLE + " SET manga_title = '" + manga.Title + "', alias = '" + manga.Alias +                // WORKS
                    //    "', status = '" + manga.Status + "', last_chapter_date = '" + manga.LastDate + "', hits = " + manga.Hits + " WHERE " +
                    //    "manga_id = '" + manga.Id + "';";
                    //SqliteCommand updateCommand = new SqliteCommand(query, db);

                    try
                    {
                        await updateCommand.ExecuteReaderAsync();
                    }
                    catch(SqliteException e)
                    {
                        Debug.WriteLine(e.StackTrace);
                        Debug.WriteLine("UpdateMangaAsync - Update");
                        flag = false;
                    }

                    //// Remove Existing categories

                    //SqliteCommand removeCommand = new SqliteCommand();
                    //removeCommand.Connection = db;
                    //removeCommand.CommandText = "DELETE FROM " + App.APP_MANGA_CATEGORY_TABLE + " WHERE manga_id = @ID;";
                    //removeCommand.Parameters.AddWithValue("@ID", manga.Id);
                    //try
                    //{
                    //    await removeCommand.ExecuteReaderAsync();
                    //}
                    //catch(SqliteException e)
                    //{
                    //    Debug.WriteLine(e.StackTrace);
                    //    Debug.WriteLine("UpdateMangaAsync - Remove Category");
                    //    flag = false;
                    //}

                    //// Manga Categories

                    //foreach (string category in manga.Categories)
                    //{
                    //    SqliteCommand categoryCommand = new SqliteCommand();
                    //    categoryCommand.Connection = db;

                    //    categoryCommand.CommandText = "INSERT INTO " + App.APP_MANGA_CATEGORY_TABLE + " (manga_id, category) VALUES " +
                    //        "(@ID, @CATEGORY);";
                    //    categoryCommand.Parameters.AddWithValue("@ID", manga.Id);
                    //    categoryCommand.Parameters.AddWithValue("@CATEGORY", category);
                    //    try
                    //    {
                    //        await categoryCommand.ExecuteReaderAsync();
                    //    }
                    //    catch (SqliteException e)
                    //    {
                    //        Debug.WriteLine(e.StackTrace);
                    //        Debug.WriteLine("UpdateMangaAsync - Category");
                    //        flag = false;
                    //    }
                    //}
                }
                else
                {
                    SqliteCommand insertCommand = new SqliteCommand();
                    insertCommand.Connection = db;

                    insertCommand.CommandText = "INSERT INTO " + App.APP_MANGA_TABLE + " (manga_id, manga_title, alias, status, last_chapter_date, hits, image_url) " +
                        "VALUES (@ID, @TITLE, @ALIAS, @STATUS, @DATE, @HITS, @IMAGE);";
                    insertCommand.Parameters.AddWithValue("@ID", manga.Id);
                    insertCommand.Parameters.AddWithValue("@TITLE", manga.Title);
                    insertCommand.Parameters.AddWithValue("@ALIAS", manga.Alias);
                    insertCommand.Parameters.AddWithValue("@STATUS", manga.Status);
                    if (manga.LastDate != null)
                    {
                        insertCommand.Parameters.AddWithValue("@DATE", manga.LastDate);
                    }
                    else
                    {
                        insertCommand.Parameters.AddWithValue("@DATE", DBNull.Value);
                    }
                    insertCommand.Parameters.AddWithValue("@HITS", manga.Hits);
                    if (manga.ImageString != null)
                    {
                        insertCommand.Parameters.AddWithValue("@IMAGE", manga.ImageString);
                    }
                    else
                    {
                        insertCommand.Parameters.AddWithValue("@IMAGE", DBNull.Value);
                    }

                    try
                    {
                        await insertCommand.ExecuteReaderAsync();
                    }
                    catch (SqliteException e)
                    {
                        Debug.WriteLine(e.StackTrace);
                        Debug.WriteLine("UpdateMangaAsync - Insert");
                        flag = false;
                    }

                    // Manga Categories

                    foreach (string category in manga.Categories)
                    {
                        SqliteCommand categoryCommand = new SqliteCommand();
                        categoryCommand.Connection = db;

                        categoryCommand.CommandText = "INSERT INTO " + App.APP_MANGA_CATEGORY_TABLE + " (manga_id, category) VALUES " +
                            "(@ID, @CATEGORY);";
                        categoryCommand.Parameters.AddWithValue("@ID", manga.Id);
                        categoryCommand.Parameters.AddWithValue("@CATEGORY", category);
                        try
                        {
                            await categoryCommand.ExecuteReaderAsync();
                        }
                        catch (SqliteException e)
                        {
                            Debug.WriteLine(e.StackTrace);
                            Debug.WriteLine("UpdateMangaAsync - Category");
                            flag = false;
                        }
                    }
                }
                db.Close();
                //Debug.WriteLine("UpdateMangaAsync: " + flag + " | Update: " + update);
            }
            return flag;
        }
    }
}
