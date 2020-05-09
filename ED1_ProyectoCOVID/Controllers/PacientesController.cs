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
        public static List<Departamentos> DatosDepartamentos = new List<Departamentos>();
        public static List<Paciente> DatosPacientes = new List<Paciente>();
        public static List<Cama> Camas = new List<Cama>();

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
            LlenarListas();
            try
            {

                Paciente AgregarPaciente = new Paciente()
                {
                    Id = DatosPacientes.Count,
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
                    Prioridad = CalcularPrioridad(Convert.ToInt32(collection["Edad"]), collection["EstadoPaciente"]=="Confirmado"),
                    Fecha = collection["Fecha"],
                    HoraIngreso = collection["HoraIngreso"],
                    EstadoPaciente = collection["EstadoPaciente"],
                    Accion = "Examinar"

                };
                
                LlenarConfirmados(ref AgregarPaciente);
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
        //Llena cola 
        void LlenarConfirmados(ref Paciente PacienteActual)
        {

            // Paciente PacienteActual = DatosPacientes[DatosPacientes.Count - 1];

            if (PacienteActual.EstadoPaciente == "Confirmado")
            {
                ColaConfirmados.Insertar(DatosPacientes.Count, PacienteActual.Prioridad);
                //DatosPacientes[DatosPacientes.Count - 1] = PacienteActual;

                //Asignando Hospital
                for (int i = 0; i < DatosDepartamentos.Count - 1; i++)
                {
                    if (PacienteActual.Departamento == DatosDepartamentos[i].Nombre)
                    {
                        //asignar region al paciente 
                        PacienteActual.HospitalAsignado = DatosDepartamentos[i].Region;
                    }
                }

                int indice = ColaConfirmados.Eliminar();

                if (indice == DatosPacientes.Count)
                {
                    AsignarCama(ref PacienteActual,indice);

                }
                else
                {
                    Paciente PacienteTemp = DatosPacientes[indice];
                    AsignarCama(ref PacienteTemp,indice);
                }


            }

        }
        void AsignarCama(ref Paciente PacienteCola, int indice)
        {
            bool HayCama = false;

            for (int i = 0; i < 50; i++)
            {
                if (Camas[i].NombreHospital == PacienteCola.HospitalAsignado && Camas[i].Disponible)
                {
                    Camas[i].Disponible = false;
                    PacienteCola.CamaAsignada = Camas[i].Id;
                    HayCama = true;
                    PacienteCola.Accion = "Recuperado";
                    break;
                }
            }
            if (!HayCama)
            {
                ColaConfirmados.Insertar(indice, PacienteCola.Prioridad);
            }
           
        }

        int CalcularPrioridad (int Edad, bool Confirmado)
        {
            //Definicion de Prioridad 
            if (Confirmado && Edad > 60)
            {
               return  8;
            }
            if (Confirmado && Edad < 1)
            {
                return 7;
            }
            if (Confirmado && Edad > 18 && Edad <= 60)
            {
                return 6;
            }

            if (!Confirmado  && Edad > 60)
            {
                return 5;
            }
            if (Confirmado && Edad >= 1 && Edad <= 18)
            {
                return 4;
            }
            if (!Confirmado && Edad < 1)
            {
                return 3;
            }
            if (!Confirmado && Edad > 18 && Edad <= 60)
            {
                return 2;
            }
            if (!Confirmado && Edad >= 1 && Edad <= 18)
            {
                return 1;
            }
            return 1;
        }
        void LlenarListas()
        {
            //Llenar Camas
            for (int i = 0; i < 50; i++)
            {
                string THospital = "";
                switch ((i+10)/10)
                {
                    case 1:
                        THospital = "Centro";
                        break;
                    case 2:
                        THospital = "Norte";
                        break;
                    case 3:
                        THospital = "Sur";
                        break;
                    case 4:
                        THospital = "Oriente";
                        break;
                    case 5:
                        THospital = "Occidente";
                        break;

                    default:
                        break;
                }
                var hash = i.ToString().GetHashCode().ToString("x");
                Camas.Add(new Cama()
                {
                    Id = hash.ToString(),
                    NombreHospital = THospital,
                    Disponible = true 
                });

                //Llenar Departamentos 
                DatosDepartamentos.Add(new Departamentos()
                {
                    Id = 1,
                    Nombre = "Alta Verapaz",
                    Region = "Norte"
                });

                DatosDepartamentos.Add(new Departamentos()
                {

                    Id = 2,
                    Nombre = "Baja Verapaz",
                    Region = "Norte"
                });

                DatosDepartamentos.Add(new Departamentos()
                {

                    Id = 3,
                    Nombre = "Chimaltenango",
                    Region = "Centro"

                });
                DatosDepartamentos.Add(new Departamentos()
                {
                    Id = 4,
                    Nombre = "Chiquimula",
                    Region = "Oriente"

                });
                DatosDepartamentos.Add(new Departamentos()
                {
                    Id = 5,
                    Nombre = "Peten",
                    Region = "Norte"

                }); DatosDepartamentos.Add(new Departamentos()
                {
                    Id = 6,
                    Nombre = "El Progreso",
                    Region = "Oriente"

                }); DatosDepartamentos.Add(new Departamentos()
                {
                    Id = 7,
                    Nombre = "Quiche",
                    Region = "Occidente"

                }); DatosDepartamentos.Add(new Departamentos()
                {
                    Id = 8,
                    Nombre = "Escuintla",
                    Region = "Sur"
                });
                DatosDepartamentos.Add(new Departamentos()
                {
                    Id = 9,
                    Nombre = "Guatemala",
                    Region = "Centro"

                });
                DatosDepartamentos.Add(new Departamentos()
                {
                    Id = 10,
                    Nombre = "Huehuetenango",
                    Region = "Occidente"

                });
                DatosDepartamentos.Add(new Departamentos()
                {
                    Id = 11,
                    Nombre = "Izabal",
                    Region = "Oriente"

                });
                DatosDepartamentos.Add(new Departamentos()
                {
                    Id = 12,
                    Nombre = "Jalapa",
                    Region = "Oriente"

                });
                DatosDepartamentos.Add(new Departamentos()
                {

                    Id = 13,
                    Nombre = "Jutiapa",
                    Region = "Oriente"

                });
                DatosDepartamentos.Add(new Departamentos()
                {
                    Id = 14,
                    Nombre = "Quetzaltenango",
                    Region = "Occidente"

                });
                DatosDepartamentos.Add(new Departamentos()
                {
                    Id = 15,
                    Nombre = "Retalhuleu",
                    Region = "Occidente"

                });
                DatosDepartamentos.Add(new Departamentos()
                {
                    Id = 16,
                    Nombre = "Sacatepequez",
                    Region = "Centro"
                });
                DatosDepartamentos.Add(new Departamentos()
                {
                    Id = 17,
                    Nombre = "San Marcos",
                    Region = "Occidente"

                });
                DatosDepartamentos.Add(new Departamentos()
                {
                    Id = 18,
                    Nombre = "Santa Rosa",
                    Region = "Oriente"

                });
                DatosDepartamentos.Add(new Departamentos()
                {
                    Id = 19,
                    Nombre = "Solola",
                    Region = "Occidente"


                });
                DatosDepartamentos.Add(new Departamentos()
                {
                    Id = 20,
                    Nombre = "Suchitepequez",
                    Region = "Occidente"


                });
                DatosDepartamentos.Add(new Departamentos()
                {
                    Id = 21,
                    Nombre = "Totonicapan",
                    Region = "Occidente"

                });
                DatosDepartamentos.Add(new Departamentos()
                {
                    Id = 22,
                    Nombre = "Zacapa",
                    Region = "Oriente"

                });

                
            }

        }









        public ActionResult Examinar(int id)
        {
            return View();
        }

        // POST: Pacientes/Examinar
        [HttpPost]
        public ActionResult Examinar(int id, FormCollection collection)
        {
            try
            {

                var rand = new Random();
                SimulacionExamen PruebaContagio = new SimulacionExamen()
                {
                    ViajeEuropa = collection["ViajeEuropa"] != "false",
                    ConocidoContagiado = collection["ConocidoContagiado"] != "false",
                    FamiliarContagiado = collection["FamiliarContagiado"] != "false",
                    ReunionesSociales = collection["ReunionesSociales"] != "false"

                };

                int Porcentaje = 5;

                if (PruebaContagio.ViajeEuropa)
                {
                    Porcentaje += 10;
                }
                if (PruebaContagio.ConocidoContagiado)
                {
                    Porcentaje += 15;
                }
                if (PruebaContagio.FamiliarContagiado)
                {
                    Porcentaje += 30;
                }
                if (PruebaContagio.ReunionesSociales)
                {
                    Porcentaje += 5;
                }


                Paciente PacienteSimulado = DatosPacientes[id];

                PacienteSimulado.EstadoPaciente = Porcentaje > 34 ? "Confirmado" : "Sospechoso";

                PacienteSimulado.Prioridad = CalcularPrioridad(PacienteSimulado.Edad, PacienteSimulado.EstadoPaciente=="Confirmado");

                LlenarConfirmados(ref PacienteSimulado);


                if (PacienteSimulado.EstadoPaciente == "Sospechoso")
                {
                    DatosPacientes[id].EstadoPaciente = "Sano";
                }

                return RedirectToAction("Index");
            }
            catch (Exception x)
            {
                return View();
            }

        }
    }
}
