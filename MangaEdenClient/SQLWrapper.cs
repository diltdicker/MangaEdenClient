using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace MangaEdenClient
{
    class SQLWrapper
    {
        private static SQLWrapper wrapper = null;

        private SQLWrapper()
        {

        }

        public static SQLWrapper GetWrapper()
        {
            if (wrapper == null)
            {
                wrapper = new SQLWrapper();
            }
            return wrapper;
        }


    }
}
