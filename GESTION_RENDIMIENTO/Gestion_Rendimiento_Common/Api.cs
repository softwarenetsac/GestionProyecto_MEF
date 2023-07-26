using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;
namespace Gestion_Rendimiento_Common
{
    public class Api
    {
        public async Task<T> PostApi<T>(ApiParams param) where T : class
        {
            var entidad = JsonConvert.DeserializeObject<T>("");
            try
            {
                using (var cliente = new HttpClient(new HttpClientHandler { }))
                {
                    cliente.BaseAddress = new Uri(param.EndPoint);
                    var content = new StringContent(JsonConvert.SerializeObject(param.parametros), Encoding.UTF8, "application/json");
                    var response = await cliente.PostAsync(param.Url, content);
                    var json_respuesta = await response.Content.ReadAsStringAsync();
                    entidad = JsonConvert.DeserializeObject<T>(json_respuesta);

                }
            }
            catch (Exception ex)
            {

            }
            return entidad;
        }
        public class ApiParams
        {
            public string EndPoint { get; set; }
            public string Url { get; set; }
            public object Params { get; set; }
            public string UserAD { get; set; }
            public string PassAD { get; set; }
            public object parametros { get; set; }
        }
    }
}
