using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1.Structures
{
    class ABB<T>
    {
    //    //----------------------------------Metodos Arbol------------------------------------------------
    //    Nodo<T> NuevoHijo(T valor, ref Nodo<T> hoja, ref Nodo<T> padre, int Altura = 0)
    //    {

    //        if (hoja == null)
    //        {
    //            hoja = new Nodo<T>();
    //            hoja.Valor = valor;
    //            hoja.Padre = padre;
    //            hoja.Altura = Altura;
    //            return hoja;
    //        }
    //        // si es igual
    //        else if (valor == hoja.Linea)
    //        {
    //            return hoja;
    //        }
    //        //Si es mayor
    //        else if (valor > hoja.Linea)
    //        {
    //            return NuevoHijo(valor, ref hoja.Derecho, ref hoja, Altura + 1);
    //        }
    //        else
    //        {
    //            return NuevoHijo(valor, ref hoja.Izquierdo, ref hoja, Altura + 1);
    //        }

    //    }

    //    Nodo<T> BuscarNodo(int linea, Nodo<T> hoja)
    //    {
    //        if (hoja == null)
    //        {
    //            return null;
    //        }
    //        else if (hoja.Linea == linea)
    //        {
    //            return hoja; // Valor encontrado
    //        }
    //        else if (linea > hoja.Linea)
    //        {
    //            return BuscarNodo(linea, hoja.Derecho);
    //        }
    //        else
    //        {
    //            return BuscarNodo(linea, hoja.Izquierdo);
    //        }
    //    }

    //    void ELiminar(int linea)
    //    {
    //        Nodo NodoELiminado;
    //        Nodo auxDerecho;
    //        Nodo auxIzquierdo;
    //        NodoELiminado = BuscarNodo(linea, Raiz); // Encontrado

    //        auxDerecho = NodoELiminado.Derecho;
    //        auxIzquierdo = NodoELiminado.Izquierdo;
    //        if (auxDerecho == null)
    //        {
    //            auxDerecho = auxIzquierdo;
    //            auxIzquierdo = null;
    //        }
    //        NodoELiminado = auxDerecho;

    //        if (auxIzquierdo != null)
    //        {
    //            Nodo auxInferior;
    //            auxInferior = auxDerecho;
    //            while (auxInferior.Izquierdo != null)
    //            {
    //                auxInferior = auxInferior.Izquierdo;
    //            }
    //            auxInferior.Izquierdo = auxIzquierdo;
    //        }
    //        Raiz = LlamarBalanceo(Raiz); //Despues de eliminar el nodo, se debe balancear el arbol
    //    }
    //    Nodo NecesitaReabastecimiento(int linea)
    //    {

    //        return Raiz;
    //    }
    //    //                                      Recorridos
    //    //---------------------------------------------------------------------------------------------------
    //    void listarPreOrden()
    //    {
    //        PreOrden(Raiz, ListaRecorridos);
    //    }
    //    void listarInOrden()
    //    {
    //        InOrden(Raiz, ListaRecorridos);
    //    }
    //    void listarPostOrden()
    //    {
    //        PostOrden(Raiz, ListaRecorridos);
    //    }


    //    void PreOrden(Nodo NodoRecorrido, List<Nodo> Lista)
    //    {
    //        //PreOrden
    //        Lista.Add(NodoRecorrido);
    //        if (NodoRecorrido.Derecho != null)
    //        {
    //            PreOrden(NodoRecorrido.Derecho, Lista);
    //        }
    //        if (NodoRecorrido.Izquierdo != null)
    //        {
    //            PreOrden(NodoRecorrido.Izquierdo, Lista);
    //        }
    //    }
    //    void InOrden(Nodo NodoRecorrido, List<Nodo> Lista)
    //    {
    //        if (NodoRecorrido.Derecho != null)
    //        {
    //            InOrden(NodoRecorrido.Derecho, Lista);
    //        }
    //        Lista.Add(NodoRecorrido);
    //        if (NodoRecorrido.Izquierdo != null)
    //        {
    //            InOrden(NodoRecorrido.Izquierdo, Lista);
    //        }
    //    }
    //    void PostOrden(Nodo NodoRecorrido, List<Nodo> Lista)
    //    {
    //        if (NodoRecorrido.Derecho != null)
    //        {
    //            PostOrden(NodoRecorrido.Derecho, Lista);
    //        }
    //        if (NodoRecorrido.Izquierdo != null)
    //        {
    //            PostOrden(NodoRecorrido.Izquierdo, Lista);
    //        }
    //        Lista.Add(NodoRecorrido);
    //    }
    }
}
