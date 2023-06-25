using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion_Rendimiento_Common
{
    public class DataCollection<T> where T : class
    {
        public IEnumerable<T> Items { get; set; }
        public int Total { get; set; }
        public int Pages { get; set; }
    }
}
