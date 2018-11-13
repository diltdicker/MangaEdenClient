using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaEdenClient.DAO
{
    class CategoryDao
    {

        public async Task<List<string>> GetAllCategories()
        {
            List<string> categories = new List<string>();
            using (SqliteConnection db = new SqliteConnection(App.APP_DB_STRING))
            {
                db.Open();
                SqliteDataReader reader = null;
                SqliteCommand searchCommand = new SqliteCommand();
                searchCommand.Connection = db;
                searchCommand.CommandText = "SELECT DISTINCT category FROM " + App.APP_MANGA_CATEGORY_TABLE + " ORDER BY category ASC;";

                try
                {
                    reader = await searchCommand.ExecuteReaderAsync();
                }
                catch(SqliteException e)
                {
                    Debug.WriteLine(e.StackTrace);
                    Debug.WriteLine(e.TargetSite);
                }
                if ( reader != null)
                {
                    while (reader.Read())
                    {
                        categories.Add(reader.GetString(0));
                    }
                }
                db.Close();
            }
            return categories;
        }
    }
}
