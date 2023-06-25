using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Frontend.Extensiones.Email
{
    public class EmailFile
    {
        public string url { get; set; }
        public string nombre { get; set; }
        public string nombre2 { get; set; }
        public string extension { get; set; }
        public string nombreCompleto { get; set; }
        public double tamanio { get; set; }
        public byte[] Archivo { get; set; }
        public byte[] Archivo2 { get; set; }
    }
}
