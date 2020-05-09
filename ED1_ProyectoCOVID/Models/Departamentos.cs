using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ED1_ProyectoCOVID.Models
{
    public class Departamentos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Region { get; set; }

        //public static List<Departamentos> DatosDepartamentos = new List<Departamentos>();

        public int DepartamentoAleatorio()
        {
            Random DepRand = new Random();
            //LlenandoDepartamentos();
            return DepRand.Next(0, 21);
        }
    }
}