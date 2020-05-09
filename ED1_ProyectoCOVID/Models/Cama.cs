using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ED1_ProyectoCOVID.Models
{
    public class Cama
    {
        public string Id { get; set; }
        public string NombreHospital { get; set; }
        public int Correlativo { get; set; }
        
        public bool Disponible { get; set; }
    }
}