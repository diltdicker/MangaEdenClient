using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Web.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml.Media.Imaging;
using System.IO;

namespace MangaEdenClient.HTTP
{
    class HttpWrapper
    {
        private const string API_STRING = "https://www.mangaeden.com/api/";
        public const string API_IMG_STRING = "https://cdn.mangaeden.com/mangasimg/";

        /// <summary>
        /// Depricated test method
        /// Use: HttpGetMangaListAsync( ... )
        /// </summary>
        /// <param name="index"></param>
        /// <param name="number"></param>
        /// <param name="anonFunc"></param>
        public async static void HttpGetMangaTitleList(int index, int number, Func<List<DAO.Manga>, bool> anonFunc)
        {
            List<DAO.Manga> mangas = new List<DAO.Manga>();
            Uri uri = new Uri(API_STRING + "list/0/?p=" + index + "&l=" + number);
            HttpClient client = new HttpClient();
            string response = null;
            try
            {
                response = await client.GetStringAsync(uri);
            } catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }
            if (response != null)
            {
                Debug.WriteLine("got JSON");
                // TODO Parse JSON
                dynamic dynMangas = JsonConvert.DeserializeObject(response);
                DAO.Manga title = new DAO.Manga();
            }
            anonFunc.Invoke(mangas);
        }

        /// <summary>
        /// Method call to grab manga list via the online api
        /// Status: 0 = suspended, 1 = ongoing, 2 = completed.
        /// Last date: unix epoch time.
        /// </summary>
        /// <param name="index">Index to start list at. Use -1 to get entire list.</param>
        /// <param name="listSize">Size of the list to get. Use -1 to get entire list.</param>
        /// <param name="callback">Callback method to executed afterwards (Ideally on UI). Recieves a List of DAO.Manga as a parameter.</param>
        public async static void HttpGetMangaListAsync(int index, int listSize, Func<List<DAO.Manga>, bool> callback)
        {
            List<DAO.Manga> mangas = new List<DAO.Manga>();
            Uri uri = new Uri(API_STRING + "list/0/?p=" + index + "&l=" + listSize);
            HttpClient client = new HttpClient();
            String response = null;
            if (index < 0 || listSize < 0)
            {
                uri = new Uri(API_STRING + "list/0/");
            }
            try
            {
                response = await client.GetStringAsync(uri);
                //Debug.WriteLine(response);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }
            finally
            {
                if (response != null)
                {
                    dynamic jsonMangas = JsonConvert.DeserializeObject(response);
                    for (int i = 0; i < jsonMangas.manga.Count; i++)
                    {
                        //Debug.WriteLine("json: i: " + i);
                        DAO.Manga manga = new DAO.Manga
                        {
                            Id = jsonMangas.manga[i].i,
                            Title = jsonMangas.manga[i].t,
                            Alias = jsonMangas.manga[i].a,
                            ImageString = jsonMangas.manga[i].im,
                            Hits = jsonMangas.manga[i].h,
                        };
                        manga.SetLastDate("" + jsonMangas.manga[i].ld);
                        manga.SetStatus((int)jsonMangas.manga[i].s);
                        if (manga.ImageString != null)
                        {
                            manga.ImageString = API_IMG_STRING + manga.ImageString;
                        }
                        for (int k = 0; k < jsonMangas.manga[i].c.Count; k++)
                        {
                            manga.Categories.Add((string)jsonMangas.manga[i].c[k]);
                        }
                        mangas.Add(manga);
                    }
                }
            }
            Debug.WriteLine("mangasSize: " + mangas.Count);
            callback.Invoke(mangas);
        }

        /// <summary>
        /// Method call to get a single manga
        /// </summary>
        /// <param name="mangaId"></param>
        /// <param name="callback"></param>
        public async static void HttpGetMangaAsync(string mangaId, Func<DAO.MangaStorage, bool> callback)
        {
            Debug.WriteLine("Get Manga Async");
            DAO.MangaStorage manga = null;
            Uri uri = new Uri(API_STRING + "manga/" + mangaId);
            HttpClient client = new HttpClient();
            String response = null;
            try
            {
                response = await client.GetStringAsync(uri);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }
            finally
            {
                if (response != null)
                {
                    dynamic jsonManga = JsonConvert.DeserializeObject(response);
                    manga = new DAO.MangaStorage
                    {
                        Id = mangaId,
                        Title = jsonManga.title,
                        Alias = jsonManga.alias,
                        Author = jsonManga.author,
                        Description = jsonManga.description
                    };
                    manga.SetLastDate("" + jsonManga.last_chapter_date);
                    string text = "chapter count: " + jsonManga.chapters.Count;
                    Debug.WriteLine(text);
                    if (jsonManga.image != null)
                    {
                        manga.ImageString = API_IMG_STRING + jsonManga.image;
                    }
                    for(int i = 0; i < jsonManga.categories.Count; i++)
                    {
                        manga.Categories.Add((string)jsonManga.categories[i]);
                    }
                    for(int i = 0; i < jsonManga.chapters.Count; i++)
                    {
                        /*
                         *  0 - chapter # (double)
                         *  1 - upload date
                         *  2 - title (string or null)
                         *  3 - id (string)
                         */
                        for(int k = 0; k < jsonManga.chapters[i].Count; k++)
                        {
                            text = "i: " + i + " k: " + k + " data: " + jsonManga.chapters[i][k];
                            Debug.WriteLine(text);
                        }
                    }
                }
                else
                {
                    Debug.WriteLine("failure we'll get em next time");
                }
            }
            callback.Invoke(manga);
        }

        /// <summary>
        /// Method call to get all the image from a manga chapter
        /// (This will be all the image strings for the manga chapter)
        /// </summary>
        /// <param name="mangaChapter"></param>
        /// <param name="callback"></param>
        public async static void HttpGetMangaChapterAsync(DAO.MangaChapter mangaChapter, Func<DAO.MangaChapter, bool> callback)
        {
            if (mangaChapter == null || mangaChapter.ChapterId == null)
            {
                callback.Invoke(null);
                return;
            }
            Uri uri = new Uri(API_STRING + "chapter/" + mangaChapter.ChapterId);
            HttpClient client = new HttpClient();
            String response = null;
            try
            {
                response = await client.GetStringAsync(uri);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }
            finally
            {
                if (response != null)
                {
                    // TODO JSON to MangaChapter
                }
            }
            callback.Invoke(mangaChapter);
        }

        /// <summary>
        /// Easy call method for getting a bitmap image from a uri using the image string from a previous api call
        /// Use this method for getting images that are NOT going to be stored in the database
        /// </summary>
        /// <param name="imageString"></param>
        /// <param name="callback"></param>
        public async static void HttpGetImageBitmapAsync(string imageString, Func<BitmapImage, bool> callback)
        {
            BitmapImage image = null;
            Uri uri = new Uri(API_IMG_STRING + imageString);
            HttpClient client = new HttpClient();
            IBuffer imageBuffer = await client.GetBufferAsync(uri);
            image = new BitmapImage();
            await image.SetSourceAsync(imageBuffer.AsStream().AsRandomAccessStream());
            
            callback.Invoke(image);
        }

        /// <summary>
        /// Method call for getting the byte array of an image
        /// Use this to store images in the database as a blob
        /// </summary>
        /// <param name="imageString"></param>
        /// <param name="callback"></param>
        public async static void HttpGetImageByteArrayAsync(string imageString, Func<byte[], bool> callback)
        {
            byte[] byteArray = new byte[0];
            Uri uri = new Uri(API_IMG_STRING + imageString);
            HttpClient client = new HttpClient();

            //  Clean way of doing it
            //
            IBuffer imageBuffer = await client.GetBufferAsync(uri);
            byteArray = imageBuffer.ToArray();

            //  messy way of doing it
            //
            //var imageStream = await client.GetInputStreamAsync(uri);
            //byteArray = new byte[imageStream.AsStreamForRead().Length];
            //await imageStream.AsStreamForRead().ReadAsync(byteArray, 0, (int)imageStream.AsStreamForRead().Length);

            callback.Invoke(byteArray);
        }

        /// <summary>
        /// Method to get an image buffer that can then be sent to be stored as a byte[].
        /// Can also be set as the source for a bitmap image.
        /// </summary>
        /// <param name="imageString"></param>
        /// <param name="fullUrl">True if imageString is the full URL, False if it is only the image path</param>
        /// <param name="callback"></param>
        public async static void HttpGetImageBufferAsync(string imageString, bool fullUrl, Func<IBuffer, bool> callback)
        {
            IBuffer imageBuffer = null;
            Uri uri;
            if (!fullUrl)
            {
                uri = new Uri(API_IMG_STRING + imageString);
            }
            else
            {
                uri = new Uri(imageString);
            }
            HttpClient client = new HttpClient();
            imageBuffer = await client.GetBufferAsync(uri);

            callback.Invoke(imageBuffer);
        }

        /// <summary>
        /// Decpricated test method 
        /// DO NOT USE
        /// </summary>
        /// <param name="image"></param>
        /// <param name="anonFunc"></param>
        public async static void HttpGetImageInBytes(string image, Func<BitmapImage , bool> anonFunc)
        {
            Uri uri = new Uri(API_IMG_STRING + image);
            HttpClient client = new HttpClient();
            HttpResponseMessage responseMessage = await client.GetAsync(uri);
            byte[] bytes = new byte[0];
            IBuffer buffer = await responseMessage.Content.ReadAsBufferAsync();
            BitmapImage bitmap = new BitmapImage();
            await bitmap.SetSourceAsync(buffer.AsStream().AsRandomAccessStream());
            Debug.WriteLine("got IMG");
            await Task.Delay(5000);
            //BitmapImage bitmap = new BitmapImage();
            //IInputStream stream = await responseMessage.Content.ReadAsInputStreamAsync();

            //IRandomAccessStream random = buffer;
            ////await bitmap.SetSourceAsync(random);

            //////bitmap.SetSourceAsync(uri)
            ////await new BitmapImage(uri);
            ////BitmapSource source;
            ////source.
            //bytes = buffer.ToArray();
            //await random.ReadAsync(buffer, buffer.Length, InputStreamOptions.None);

            anonFunc.Invoke(bitmap);
        }
    }
}
