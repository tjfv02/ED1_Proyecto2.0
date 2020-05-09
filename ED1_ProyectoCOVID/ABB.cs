using System;
using System.Collections.Generic;
using System.Text;

namespace ED1_ProyectoCOVID.Structures
{
    public class ABB<T>
    {

        private  Nodo<T> Raiz = new Nodo<T>();

        //----------------------------------Metodos Arbol------------------------------------------------
        public Nodo<T> Insertar(T valor, int id)
        {
            Nodo<T> Temp;
            if (Raiz.Valor == null)
            {
                Raiz.Valor = valor;
                Raiz.Altura = 0;
                Raiz.Referencia = id;
                Temp = Raiz;
            }
            else
            {

                // si es igual
                if (valor.ToString().CompareTo(Raiz.Valor.ToString()) == 0)
                {
                    Temp = Raiz;
                }
                //Si es mayor
                else if (valor.ToString().CompareTo(Raiz.Valor.ToString()) > 0)
                {
                    Raiz.Derecho = NuevoHijo(valor, ref Raiz.Derecho, ref Raiz, 1, id);
                    Temp = Raiz.Derecho;
                }
                else
                {
                    Raiz.Izquierdo = NuevoHijo(valor, ref Raiz.Izquierdo, ref Raiz, 1, id);
                    Temp = Raiz.Izquierdo;
                }

            }
            return Temp;
        }



        private Nodo<T> NuevoHijo(T valor, ref Nodo<T> hoja, ref Nodo<T> padre, int Altura, int id)
        {
            Nodo<T> Temp;

            if (hoja == null)
            {
                hoja = new Nodo<T>();
                hoja.Valor = valor;
                hoja.Padre = padre;
                hoja.Altura = Altura;
                hoja.Referencia = id;
                Temp = hoja;
            }
            else
            {
                // si es igual
                if (valor.ToString().CompareTo(hoja.Valor.ToString()) == 0)
                {
                    Temp = hoja;
                }
                //Si es mayor
                else if (valor.ToString().CompareTo(hoja.Valor.ToString()) > 0)
                {
                    hoja.Derecho = NuevoHijo(valor, ref hoja.Derecho, ref hoja, 1, id);
                    Temp = hoja.Derecho;
                }
                else
                {
                    hoja.Izquierdo = NuevoHijo(valor, ref hoja.Izquierdo, ref hoja, 1, id);
                    Temp = hoja.Izquierdo;
                }
            }

            return Temp;

        }

        public int Buscar(T valor)
        {
            Nodo<T> Temp = BuscarNodo(valor, Raiz);
            return Temp.Referencia;
        }

        private Nodo<T> BuscarNodo(T valor, Nodo<T> hoja)
        {
            if (hoja == null)
            {
                return null; // no existe el elemento
            }
            else if (valor.ToString().CompareTo(hoja.Valor.ToString()) == 0)
            {
                return hoja; // Valor encontrado
            }
            else if (valor.ToString().CompareTo(hoja.Valor.ToString()) > 0)
            {
                return BuscarNodo(valor, hoja.Derecho);
            }
            else
            {
                return BuscarNodo(valor, hoja.Izquierdo);
            }
        }

        public void ELiminar(T valor)
        {
            Nodo<T> NodoELiminado;
            Nodo<T> auxDerecho;
            Nodo<T> auxIzquierdo;
            NodoELiminado = BuscarNodo(valor, Raiz); // Encontrado

            auxDerecho = NodoELiminado.Derecho;
            auxIzquierdo = NodoELiminado.Izquierdo;
            if (auxDerecho == null)
            {
                auxDerecho = auxIzquierdo;
                auxIzquierdo = null;
            }
            NodoELiminado = auxDerecho;

            if (auxIzquierdo != null)
            {
                Nodo<T> auxInferior;
                auxInferior = auxDerecho;
                while (auxInferior.Izquierdo != null)
                {
                    auxInferior = auxInferior.Izquierdo;
                }
                auxInferior.Izquierdo = auxIzquierdo;
            }
            //Raiz = LlamarBalanceo(Raiz); //Despues de eliminar el nodo, se debe balancear el arbol
        }
        //Nodo<T> NecesitaReabastecimiento(int linea)
        //{

        //    return Raiz;
        //}
        //                                      Recorridos
        //---------------------------------------------------------------------------------------------------
        //void listarPreOrden()
        //{
        //    PreOrden(Raiz, ListaRecorridos);
        //}
        //void listarInOrden()
        //{
        //    InOrden(Raiz, ListaRecorridos);
        //}
        //void listarPostOrden()
        //{
        //    PostOrden(Raiz, ListaRecorridos);
        //}


        //void PreOrden(Nodo NodoRecorrido, List<Nodo> Lista)
        //{
        //    //PreOrden
        //    Lista.Add(NodoRecorrido);
        //    if (NodoRecorrido.Derecho != null)
        //    {
        //        PreOrden(NodoRecorrido.Derecho, Lista);
        //    }
        //    if (NodoRecorrido.Izquierdo != null)
        //    {
        //        PreOrden(NodoRecorrido.Izquierdo, Lista);
        //    }
        //}
        //void InOrden(Nodo NodoRecorrido, List<Nodo> Lista)
        //{
        //    if (NodoRecorrido.Derecho != null)
        //    {
        //        InOrden(NodoRecorrido.Derecho, Lista);
        //    }
        //    Lista.Add(NodoRecorrido);
        //    if (NodoRecorrido.Izquierdo != null)
        //    {
        //        InOrden(NodoRecorrido.Izquierdo, Lista);
        //    }
        //}
        //void PostOrden(Nodo NodoRecorrido, List<Nodo> Lista)
        //{
        //    if (NodoRecorrido.Derecho != null)
        //    {
        //        PostOrden(NodoRecorrido.Derecho, Lista);
        //    }
        //    if (NodoRecorrido.Izquierdo != null)
        //    {
        //        PostOrden(NodoRecorrido.Izquierdo, Lista);
        //    }
        //    Lista.Add(NodoRecorrido);
        //}
    }
}
