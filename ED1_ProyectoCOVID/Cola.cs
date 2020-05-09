using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1.Structures
{
    class Cola<T>
    {
        private Nodo<T> Primero { get; set; }

        public void Insertar(T valor, int prioridad)
        {
            if (Primero == null)
            {
                Primero = NuevoNodo(valor, prioridad);
            }
            else
            {

            Nodo<T> inicio = Primero;

            Nodo<T> temp = NuevoNodo(valor, prioridad);

            if (Primero.Prioridad > prioridad)
            {
                temp.Siguiente = Primero;
                Primero = temp;
            }
            else
            {
                while (inicio.Siguiente != null && inicio.Siguiente.Prioridad < prioridad)
                {
                    inicio = inicio.Siguiente;
                }
            }
            temp.Siguiente = inicio.Siguiente;
            inicio.Siguiente = temp;
            }
        }

        public T DevolverPrimero()
        {
            var value = Peek();
            return value;
        }

        public T Eliminar()
        {
            var value = Peek();
            EliminarColaPrioridad();
            return value;
        }
        protected void EliminarColaPrioridad()
        {
            if (Primero != null)
            {
                Primero = Primero.Siguiente;
            }
        }
        protected T Peek()// --> Primer valor de la cola 
        {
            return Primero.Valor;
        }

        public static Nodo<T> NuevoNodo(T valor, int prioridad)
        {
            var temp = new Nodo<T>();
            temp.Valor = valor;
            temp.Prioridad = prioridad;
            temp.Siguiente = null;

            return temp;
        }
    }
}
