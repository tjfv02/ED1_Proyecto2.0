using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ED1_ProyectoCOVID.Models
{
    public class Estadisticas
    {
        public int Ingreso_Contagiados { get; set; }
        public int Ingreso_Sospechosos{ get; set; }
        public double Sospechosos_A_Positivos { get; set; }
        public int Cantidad_Egresados { get; set; }

    }
}