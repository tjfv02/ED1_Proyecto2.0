using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ED1_ProyectoCOVID.Models
{
    public class Paciente
    {
        //Datos Personales

        public int Id { get; set; }
        public int Edad { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Identificacion { get; set; } //DPI o Partida de Nacimiento
        public string Departamento { get; set; }
        public string Municipio { get; set; }

        //Datos Salud
        public int Prioridad { get; set; }
        public string Sintomas { get; set; }
        public string DescripcionContagioPosible { get; set; }
        public string Fecha { get; set; }
        public string HoraIngreso { get; set; }

        public string EstadoPaciente { get; set; } //Contagiado o Sospechoso
        public string HospitalAsignado { get; set; }
        public string CamaAsignada { get; set; }

        public string Accion { get; set; }

    }
}