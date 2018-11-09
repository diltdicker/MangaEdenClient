using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaEdenClient
{
    class NavigationHelper
    {
        public string IdString { get; set; }
        public bool Conditional { get; set; }
        public Object DataObject { get; set; }

        public NavigationHelper(String id, bool conditional, Object data)
        {
            IdString = id;
            Conditional = conditional;
            DataObject = data;
        }
    }
}
