using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1.Structures
{
    class Nodo<T>
    {
        //Valor
        public T Valor { get; set; }

        //Para la cola
        public Nodo<T> Siguiente { get; set; }
        public int Prioridad { get; set; }

        //Para el arbol 
        public Nodo<T> Padre;
        public Nodo<T> Izquierdo;
        public Nodo<T> Derecho;
    }
}
