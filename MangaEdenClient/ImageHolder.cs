using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace MangaEdenClient
{
    class ImageHolder
    {
        public BitmapImage Image { get; set; }
        public int Index { get; set; }

        public ImageHolder()
        {

        }

        public ImageHolder(BitmapImage image, int index)
        {
            Image = image;
            Index = index;
        }

        public BitmapImage GetImage()
        {
            return Image;
        }
    }
}
