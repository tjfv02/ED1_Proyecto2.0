using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ED1_ProyectoCOVID.Models
{
    public class SimulacionExamen
    {
        public int Id { get; set; }
        public bool ViajeEuropa { get; set; }
        public bool ConocidoContagiado { get; set; }
        public bool FamiliarContagiado { get; set; }
        public bool ReunionesSociales { get; set; }
    }
}