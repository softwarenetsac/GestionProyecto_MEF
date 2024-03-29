﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Entity.Model
{
    public class EvaluadoModel
    {
        public string ID_PERSONAL { get; set; }
        public string APELLIDO_PATERNO { get; set; }
        public string APELLIDO_MATERNO { get; set; }
        public string NOMBRE { get; set; }
        public string NOMBRE_COMPLETO { get; set; }
        public int ID_OFICINA { get; set; }
        public string NOMBRE_CARGO { get; set; }
        public int ID_SITUACION_LABORAL { get; set; }
        public string NOMBRE_CATEGORIA { get; set; }
        public string CORREO_INSTITUCIONAL { get; set; }
        public string NUMERO_DNI { get; set; }
        public string NOMBRE_OFICINA { get; set; }
        public string NOMBRE_AREA { get; set; }
        public int ID_AREA { get; set; }
        public string FECHA_INGRESO { get; set; }
        public string USUARIO_LOGIN { get; set; }
        public int LOCADOR { get; set; }
        public string ANIO { get; set; }
        public int ID_EVALUADOR { get; set; }
        public string NOMBRE_EVALUADOR { get; set; }
        public string PROYECTO { get; set; }
        public int ID_PROYECTO { get; set; }
        public string ID_EVALUADOR_EVALUADO { get; set; }
        public int NUM_ACTIVIDADES { get; set; }
        
    }
}
