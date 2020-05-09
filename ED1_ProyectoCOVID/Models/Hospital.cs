using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ED1_ProyectoCOVID.Models
{
    public class Hospital
    {
        public string Ubicacion { get; set; }
        public int NumeroCamas { get; set; }
        public int CamasLlenas { get; set; }

        public string CadenaUnica { get; set; }
    }
}