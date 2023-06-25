using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Frontend.Extensiones.Email
{
    public class Mensaje
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Cc { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public List<EmailFile> FileAttach { get; set; }
        public string ToDisplayName { get; set; }
    }
}
