using System;
using System.Collections.Generic;
using System.Text;

namespace ED1_ProyectoCOVID.Structures
{
    public class Nodo<T>
    {
        //Valor
        public T Valor { get; set; }

        //Para la cola
        public Nodo<T> Siguiente { get; set; }
        public int Prioridad { get; set; }

        //Para el arbol 
        public int [] Referencia{ get; set; } //id 
        public Nodo<T> Padre;
        public Nodo<T> Izquierdo;
        public Nodo<T> Derecho;
        public int Altura { get; set; }

        public void Expandir (int x)
        {
            int[] Temp = new int[x];
            Array.Copy(Referencia, Temp,x-1);
            Referencia = Temp;
            //Array.Resize(ref Referencia, 10);
        }
    }
    
}
