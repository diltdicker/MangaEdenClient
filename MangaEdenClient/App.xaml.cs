using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace MangaEdenClient
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        public static bool FULL_UPDATE_FLAG = true;
        public static int DB_UPDATE_PROGRESS_MAX = 0;
        public static int DB_UPDATE_PROGRESS_VALUE = 0;

        //public static ObservableCollection<Manga> observableManga = new ObservableCollection<Manga>();
        public const string APP_DB_STRING = "Filename=MangaEdenClient.db.sqlite3";
        public const string APP_MANGA_TABLE = "manga_table";
        public const string APP_MANGA_CHAPTER_TABLE = "manga_chapter_table";
        public const string APP_MANGA_CHAPTER_IMAGE_TABLE = "manga_chapter_image_table";
        public const string APP_FAVORITE_TABLE = "favorite_table";
        public const string APP_HISTORY_TABLE = "history_table";
        public const string APP_MANGA_CATEGORY_TABLE = "manga_catagory_table";
        public const string TEST_TEXT_TABLE = "test_text_table";
        public const string TEST_IMAGE_TABLE = "test_image_table";
        const string INIT_MANGA_TABLE = "CREATE TABLE IF NOT EXISTS " + APP_MANGA_TABLE + " (" + 
            "manga_id TEXT PRIMARY KEY," +
            "manga_title TEXT NOT NULL," +
            "alias TEXT NOT NULL," +
            "image BLOB," +
            "image_url TEXT," +
            "author TEXT," +
            "hits INTEGER," +
            "description TEXT," +
            "created_date TEXT," +
            "last_chapter_date TEXT," +
            "status TEXT NOT NULL CHECK (status IN ('Suspended', 'Ongoing', 'Completed'))" +
            ") WITHOUT ROWID;";
        const string INIT_MANGA_CHAPTER_TABLE = "CREATE TABLE IF NOT EXISTS " + APP_MANGA_CHAPTER_TABLE + " (" +
            "chapter_id TEXT PRIMARY KEY," +
            "chapter_number REAL NOT NULL," +
            "manga_id TEXT NOT NULL," +
            "date TEXT," +
            "chapter_title TEXT," +
            "FOREIGN KEY (manga_id) REFERENCES " + APP_MANGA_TABLE + "(manga_id)" +
            ") WITHOUT ROWID;";
        const string INIT_MANGA_CHAPTER_IMAGE_TABLE = "CREATE TABLE IF NOT EXISTS " + APP_MANGA_CHAPTER_IMAGE_TABLE + " (" +
            "chapter_id TEXT NOT NULL," +
            "image_number INTEGER NOT NULL," +
            "image BLOB," +
            "PRIMARY KEY (chapter_id, image_number)," +
            "FOREIGN KEY (chapter_id) REFERENCES " + APP_MANGA_CHAPTER_TABLE + "(chapter_id)" +
            ") WITHOUT ROWID;";
        const string INIT_FAVORITE_TABLE = "CREATE TABLE IF NOT EXISTS " + APP_FAVORITE_TABLE + " (" +
            "manga_id TEXT PRIMARY KEY," +
            "FOREIGN KEY (manga_id) REFERENCES " + APP_MANGA_TABLE + "(manga_id)" +
            ") WITHOUT ROWID;";
        const string INIT_HISTORY_TABLE = "CREATE TABLE IF NOT EXISTS " + APP_HISTORY_TABLE + " (" +
            "manga_id TEXT PRIMARY KEY," +
            "number INTEGER NOT NULL," +
            "FOREIGN KEY (manga_id) REFERENCES " + APP_MANGA_TABLE + "(manga_id)" +
            ") WITHOUT ROWID;";
        const string INIT_MANGA_CATEGORY_TABLE = "CREATE TABLE IF NOT EXISTS " + APP_MANGA_CATEGORY_TABLE + " (" +
            "manga_id TEXT NOT NULL," +
            "category TEXT NOT NULL," +
            "PRIMARY KEY(manga_id, category)," +
            "FOREIGN KEY (manga_id) REFERENCES " + APP_MANGA_TABLE + "(manga_id)" +
            ") WITHOUT ROWID;";
        const string INIT_TEST_TEXT_TABLE = "CREATE TABLE IF NOT EXISTS " + TEST_TEXT_TABLE + " (" +
            "test_id INTEGER PRIMARY KEY," +
            "test_text TEXT NOT NULL" +
            ") WITHOUT ROWID;";
        const string INIT_TEST_IMAGE_TABLE = "CREATE TABLE IF NOT EXISTS " + TEST_IMAGE_TABLE + " (" +
            "test_id INTEGER PRIMARY KEY," +
            "test_image BLOB NOT NULL" +
            ") WITHOUT ROWID;";

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;

            //
            //  SqLite Setup
            //
            using (SqliteConnection db = new SqliteConnection(APP_DB_STRING))
            {
                db.Open();
                SqliteCommand createMangaTable = new SqliteCommand(INIT_MANGA_TABLE, db);
                SqliteCommand createMangaChapterTable = new SqliteCommand(INIT_MANGA_CHAPTER_TABLE, db);
                SqliteCommand createMangaChapterImageTable = new SqliteCommand(INIT_MANGA_CHAPTER_IMAGE_TABLE, db);
                SqliteCommand createFavoriteTable = new SqliteCommand(INIT_FAVORITE_TABLE, db);
                SqliteCommand createHistoryTable = new SqliteCommand(INIT_HISTORY_TABLE, db);
                SqliteCommand createMangaCategoryTable = new SqliteCommand(INIT_MANGA_CATEGORY_TABLE, db);
                try
                {
                    //Debug.WriteLine("Manga Table");
                    createMangaTable.ExecuteReader();
                    //Debug.WriteLine("Manga Chapter Table");
                    createMangaChapterTable.ExecuteReader();
                    //Debug.WriteLine("Manga C Image Table");
                    createMangaChapterImageTable.ExecuteReader();
                    //Debug.WriteLine("Manga F Table");
                    createFavoriteTable.ExecuteReader();
                    //Debug.WriteLine("Manga H Table");
                    createHistoryTable.ExecuteReader();
                    //Debug.WriteLine("Manga Category Table");
                    createMangaCategoryTable.ExecuteReader();
                }
                catch (SqliteException e)
                {
                    Debug.WriteLine(e.StackTrace);
                    Debug.WriteLine("SQLite write failed");
                    Exit();
                }
                finally
                {
                    db.Close();
                }
            }

            //
            //  Aplication Resizing
            //
            ApplicationView.PreferredLaunchViewSize = new Size(1024, 768);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
