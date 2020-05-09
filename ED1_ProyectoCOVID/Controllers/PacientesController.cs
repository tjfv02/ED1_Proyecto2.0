using ED1_ProyectoCOVID.Models;
using System;
using ClassLibrary1.Structures;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ED1_ProyectoCOVID.Controllers
{
    public class PacientesController : Controller
    {
        public static List<Paciente> DatosPacientes = new List<Paciente>();

        //Colas 
        private static Cola<int> ColaConfirmados = new Cola<int>();
        private static Cola<string> ColaSospechosos = new Cola<string>();

        // GET: Pacientes
        public ActionResult Index()
        {
            return View(DatosPacientes);
        }

        // GET: Pacientes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Pacientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pacientes/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                
                Paciente AgregarPaciente = new Paciente()
                {
                    //Datos Personales
                    Edad = Convert.ToInt32(collection["Edad"]),
                    Nombre = collection["Nombre"],
                    Apellido = collection["Apellido"],
                    Identificacion = collection["Identificacion"],
                    Departamento = collection["Departamento"],
                    Municipio = collection["Municipio"],

                    //Salud

                    Sintomas = collection["Sintomas"],
                    DescripcionContagioPosible = collection["DescripcionContagioPosible"],
                    Fecha = collection["Fecha"],
                    HoraIngreso = collection["HoraIngreso"],
                    EstadoPaciente = collection["EstadoPaciente"]


                };
               

                ////Datos Generales 
                DatosPacientes.Add(AgregarPaciente);

                //if (AgregarPaciente.EstadoPaciente == "Confirmado")
                //{
                //    PacientesInfectados.Add(AgregarPaciente);
                //    //Se agragaría a la cola de infectados

                //    PacienteActual = PacientesInfectados.Count - 1;
                //    ColaConfirmados.Insertar(AgregarPaciente.Identificacion, AgregarPaciente.Prioridad);


                //}
                //else
                //{
                //    //PacientesSospechosos.Add(AgregarPaciente);
                //    ColaSospechosos.Insertar(AgregarPaciente.Identificacion, AgregarPaciente.Prioridad);
                //    return RedirectToAction("SimularExamen");
                //}

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pacientes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Pacientes/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pacientes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Pacientes/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        void LlenarConfirmados(int Prioridad)
        {
            ColaConfirmados.Insertar(DatosPacientes.Count - 1, Prioridad);

        }

        int CalcularPrioridad (int Edad, bool Confirmado)
        {
            //Definicion de Prioridad 
            if (Confirmado && Edad > 60)
            {
               return  1;
            }
            if (Confirmado && Edad < 1)
            {
                return 2;
            }
            if (Confirmado && Edad > 18 && Edad <= 60)
            {
                return 3;
            }

            if (!Confirmado  && Edad > 60)
            {
                return 4;
            }
            if (Confirmado && Edad >= 1 && Edad <= 18)
            {
                return 5;
            }
            if (!Confirmado && Edad < 1)
            {
                return 6;
            }
            if (!Confirmado && Edad > 18 && Edad <= 60)
            {
                return 7;
            }
            if (!Confirmado && Edad >= 1 && Edad <= 18)
            {
                return 8;
            }
        }

    }
}
