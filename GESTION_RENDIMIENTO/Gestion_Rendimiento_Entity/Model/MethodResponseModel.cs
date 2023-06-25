using System.Collections.Generic;
using System.Net;

namespace Gestion_Rendimiento_Entity.Model
{
    public class MethodResponseModel<T>
    {
        public MethodResponseModel()
        {
            Code = (int)HttpStatusCode.OK;
        }

        public MethodResponseModel(int code)
        {
            Code = code;
        }

        public MethodResponseModel(HttpStatusCode statusCode)
        {
            Code = (int)statusCode;
        }

        public int Code { get; set; }
       public int ID { get; set; }
        public object OtherCode { get; set; }
        public string Message { get; set; }
        public string Message2 { get; set; }
        public bool Success { get; set; }
        public T Result { get; set; }
       public List<T> Results { get; set; }
        public List<string> Resultss { get; set; }
        public string ID_ENCRIPTADO { get; set; }
        public long ID_ASISTENCIA { get; set; }
        public string NUMERO { get; set; }
        public int ID_ARCHIVO { get; set; }
        public string NOMBRE { get; set; }
        public string NOMBRE_ESTADO { get; set; }
        public int ID_ESTADO { get; set; }
    }
}
