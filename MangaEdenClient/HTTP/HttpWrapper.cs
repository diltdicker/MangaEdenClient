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
        private const string _API_STRING = "https://www.mangaeden.com/api/";
        private const string _IMG_STRING = "https://cdn.mangaeden.com/mangasimg/";

        public async static void HttpGetMangaTitleList(int index, int number, Func<List<DAO.MangaTitle>, bool> anonFunc)
        {
            List<DAO.MangaTitle> mangas = new List<DAO.MangaTitle>();
            Uri uri = new Uri(_API_STRING + "list/0/?p=" + index + "&l=" + number);
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
                /// TODO Parse JSON
                dynamic dynmangas = JsonConvert.DeserializeObject(response);
                DAO.MangaTitle title = new DAO.MangaTitle(dynmangas.manga[0]);
            }
            anonFunc.Invoke(mangas);
        }

        public async static void HttpGetImageInBytes(string image, Func<BitmapImage , bool> anonFunc)
        {
            Uri uri = new Uri(_IMG_STRING + image);
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

    class ImgBuffer : Windows.Storage.Streams.IBuffer
    {
        public uint Capacity => throw new NotImplementedException();

        public uint Length { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
