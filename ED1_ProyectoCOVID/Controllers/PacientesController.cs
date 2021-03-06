﻿using ED1_ProyectoCOVID.Models;
using System;
using ED1_ProyectoCOVID.Structures;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ED1_ProyectoCOVID.Controllers
{
    public class PacientesController : Controller
    {
        public static ABB<string > IndiceNombre = new ABB<string>();
        public static ABB<string> IndiceApellido = new ABB<string>();
        public static ABB<string> IndiceIde = new ABB<string>();


        public static double SospechosoAPositivos;
        public static int Sospechosos;
        public static int Confirmados;
        public static int Recuperados;

        public static HashTable<string, Cama> DatosCama = new HashTable<string, Cama>();

        public static List<Paciente> ResultBusqueda = new List<Paciente>();
        public static List<Departamentos> DatosDepartamentos = new List<Departamentos>();
        public static List<Paciente> DatosPacientes = new List<Paciente>();
        public static List<Cama> Camas = new List<Cama>();

        //Colas 
        private static Cola<int> ColaConfirmados = new Cola<int>();
        private static Cola<string> ColaSospechosos = new Cola<string>();

        public int SospechsoAPositivos { get; private set; }

        // GET: Pacientes
        public ActionResult PaginaPrincipal()
        {

            return View();
        }

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
                    Fecha = DateTime.Now.ToLongDateString(),
                    HoraIngreso = DateTime.Now.ToString("hh:mm:ss"),
                    EstadoPaciente = collection["EstadoPaciente"],
                    Accion = "Examinar"

                };
                if(AgregarPaciente.EstadoPaciente=="Confirmado")
                {
                    Confirmados++;
                }
                else
                {
                    Sospechosos++;     
                }
                //Llenar indices busqueda
                IndiceNombre.Insertar(AgregarPaciente.Nombre, AgregarPaciente.Id);
                IndiceApellido.Insertar(AgregarPaciente.Apellido, AgregarPaciente.Id);
                IndiceIde.Insertar(AgregarPaciente.Identificacion, AgregarPaciente.Id);

                //Llenar lista, cola 
                DatosPacientes.Add(AgregarPaciente);
                LlenarConfirmados(ref AgregarPaciente);
                
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
            PacienteActual.Prioridad = CalcularPrioridad(PacienteActual.Edad, PacienteActual.EstadoPaciente=="Confirmado");
           if (PacienteActual.EstadoPaciente == "Confirmado")
            {
            
                ColaConfirmados.Insertar(PacienteActual.Id, PacienteActual.Prioridad);
                //DatosPacientes[DatosPacientes.Count - 1] = PacienteActual;
                AsignarHospital(PacienteActual.Id);
                 AsignarCama();
            }

        }

        void AsignarHospital(int indice)
        {
            //Asignando Hospital
            for (int i = 0; i < DatosDepartamentos.Count - 1; i++)
            {
                if (DatosPacientes[indice].Departamento == DatosDepartamentos[i].Nombre)
                {
                    //asignar region al paciente 
                    DatosPacientes[indice].HospitalAsignado = DatosDepartamentos[i].Region;
                }
            }
        }

        void AsignarCama( ) //Paciente PacienteCola, int indice)
        {
            bool HayCama = false;
            if (ColaConfirmados.HayDatos())
            {


                int indice = ColaConfirmados.DevolverPrimero();

                AsignarHospital(indice);

                for (int i = 0; i < 50; i++)
                {
                    if (Camas[i].NombreHospital == DatosPacientes[indice].HospitalAsignado && Camas[i].Disponible)
                    {
                        Camas[i].Disponible = false;
                        DatosPacientes[indice].CamaAsignada = Camas[i].Id;
                        HayCama = true;
                        DatosPacientes[indice].Accion = "Recuperado";
                        break;
                    }
                }
                if (HayCama)
                {
                    ColaConfirmados.Eliminar();
                }
            }
           
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
            return 1;
        }
        void LlenarListas()
        {
            //Llenar Camas
            if (Camas.Count == 0)
            {
               

                for (int i = 0; i < 50; i++)
                {
                    string THospital = "";
                    switch ((i + 10) / 10)
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
                        Correlativo=i,
                        NombreHospital = THospital,
                        Disponible = true
                    });

                    DatosCama.Insertar(hash.ToString(), Camas[i]);

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

                var rnd = new Random();

                //for (int i = 0; i < 10; i++)
                //{
                //    DatosPacientes.Add(new Paciente()
                //    {
                //        Id = i,
                //        Edad = rnd.Next(0, 90),
                //        Departamento = "Guatemala",
                //        EstadoPaciente = "Confirmado"
                        
                //    });
                //    Paciente x = DatosPacientes[i];
                //    LlenarConfirmados(ref x);
                //}

            }

        }

        public ActionResult Recuperado(int id)
        {
            Recuperados++;
            DatosPacientes[id].EstadoPaciente = "Sano";
            DatosPacientes[id].Accion = "Examinar";
            Cama Liberar = DatosCama.Get(DatosPacientes[id].CamaAsignada);
            Camas[Liberar.Correlativo].Disponible = true;

            //for (int i = 0; i < 50; i++)
            //{
            //    if ( DatosPacientes[id].CamaAsignada == Camas[i].Id)
            //    {
            //        Camas[i].Disponible = true;

            //        break;
            //    }
            //}

            DatosPacientes[id].CamaAsignada = "";
            AsignarCama();
            return RedirectToAction("Index");
        }

        public ActionResult ResultadoBusqueda()
        {
            return View(ResultBusqueda);
        }

        public ActionResult Busqueda()
        {
            return View();
        }

        // POST: Pacientes/Examinar
        [HttpPost]
        public ActionResult Busqueda(FormCollection collection)
        {
            Paciente BuscarPaciente = new Paciente()
            {
                Nombre=collection["Nombre"],
                Apellido = collection["Apellido"],
                Identificacion = collection["Identificacion"]
            };
            int[] search = IndiceNombre.Buscar(BuscarPaciente.Nombre);
            int[] search2 = IndiceApellido.Buscar(BuscarPaciente.Apellido);
            int[] search3 = IndiceIde.Buscar(BuscarPaciente.Identificacion);

            ResultBusqueda.Clear();
            try
            {
            if (search!=null )
            {
                for (int i = 0; i < search.Length; i++)
                {
                    ResultBusqueda.Add(DatosPacientes[search[i]]);
                }
            }

            }
            catch (Exception)
            {

                return RedirectToAction("ResultadoBusqueda");
            }
            try
            {
            if (search2 != null)
            {
                for (int i = 0; i < search.Length; i++)
                {
                    ResultBusqueda.Add(DatosPacientes[search2[i]]);
                }
            }

            }
            catch (Exception)
            {

                return RedirectToAction("ResultadoBusqueda");
            }
            try
            {
            if (search3 != null)
            {
                for (int i = 0; i < search.Length; i++)
                {
                    ResultBusqueda.Add(DatosPacientes[search3[i]]);
                }
            }

            }
            catch (Exception)
            {

                return RedirectToAction("ResultadoBusqueda");
            }

            return RedirectToAction("ResultadoBusqueda");
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
                if (Porcentaje > 3) SospechosoAPositivos++;
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
        public ActionResult Estadisticas()
        {
            List<Estadisticas> DatosEstadisticas = new List<Estadisticas>();
            double SaP = 0;
            if (Sospechosos>0)
            {
                SaP = SospechosoAPositivos / Sospechosos ;
            }
           
            DatosEstadisticas.Add(new Estadisticas()
            {
                Ingreso_Contagiados = Confirmados,
                Ingreso_Sospechosos=Sospechosos,
                Sospechosos_A_Positivos = SaP,
                Cantidad_Egresados= Recuperados
            
            });
            return View(DatosEstadisticas);
        }
    }
}
