using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion_Rendimiento_Entity
{
   public  class BaseResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }

        public long ID_ASISTENCIA { get; set; }
        public string NUMERO { get; set; }
        public int ID_ARCHIVO { get; set; }
        public string NOMBRE { get; set; }
        public string NOMBRE_ESTADO { get; set; }
        public int  ID_ESTADO { get; set; }
        public int ID { get; set; }

        public string Extra { get; set; }
        public string Extra2 { get; set; }
        public string Extra3 { get; set; }
        public string Extra4 { get; set; }
        public string HORA { get; set; }
        public string Otros { get; set; }
        public string NameFile { get; set; }
        public string RutaOrigen { get; set; }
        public string RutaDestino { get; set; }
        public string ID_ENCRIPTADO { get; set; }
        public byte[] File { get; set; }

        public DateTime? Fecha { get; set; }
    }

}
