using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion_Rendimiento_Entity
{
    public class Modulo
    {
        public string DESC_MODULO { get; set; }
        public string DESC_TIPO_MODULO { get; set; }
        public int ID_PERFIL { get; set; }
        public int ID_SISTEMA_MODULO { get; set; }
        public int ID_SISTEMA_MODULO_PADRE { get; set; }
        public int ID_TIPO_MODULO { get; set; }
        public long ID_USUARIO { get; set; }
        public string IMAGEN { get; set; }
        public int NIVEL { get; set; }
        public int ORDEN { get; set; }
        public string URL_MODULO { get; set; }

        public List<Modulo> Modulos { get; set; }
        public List<Modulo> Hijos { get; set; }


    }
}
