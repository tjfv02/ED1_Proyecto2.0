using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1.Structures
{
    class AVL
    {
    //    //Función para obtener que rama es mayor
    //    int max(int lhs, int rhs)
    //    {
    //        return lhs > rhs ? lhs : rhs;
    //    }
    //    int Alturas(Nodo Raiz)
    //    {
    //        return Raiz == null ? -1 : Raiz.Altura;
    //    }

    //    //Funcion para obtener la Altura del arbol
    //    int getAltura(Nodo nodoActual)
    //    {
    //        if (nodoActual == null)
    //            return 0;
    //        else
    //            return 1 + Math.Max(getAltura(nodoActual.Izquierdo), getAltura(nodoActual.Derecho));
    //    }

    //    //Balanceo de arbol AVL
    //    public Nodo LlamarBalanceo(Nodo inicio)
    //    {


    //        if (inicio.Izquierdo != null)
    //        {
    //            inicio.Izquierdo = LlamarBalanceo(inicio.Izquierdo);
    //        }

    //        if (inicio.Derecho != null)
    //        {
    //            inicio.Derecho = LlamarBalanceo(inicio.Derecho);
    //        }
    //        int a = NecesitoBalanceo(inicio);
    //        if (a >= -1 && a <= 1)
    //        {
    //            //Nodo balanceado
    //        }
    //        else
    //        {
    //            if (a > 1)
    //            {
    //                int b = NecesitoBalanceo(inicio.Izquierdo);
    //                if (b < 0)
    //                {

    //                    inicio = RotacionDerechoDoble(inicio);
    //                }
    //                else
    //                {
    //                    inicio = RotacionDerechoSimple(inicio);
    //                }

    //            }
    //            else
    //            {
    //                int c = NecesitoBalanceo(inicio.Derecho);
    //                if (c > 0)
    //                {
    //                    inicio = RotacionIzquierdoDoble(inicio);
    //                }
    //                else
    //                {
    //                    inicio = RotacionIzquierdoSimple(inicio);
    //                }
    //            }

    //        }

    //        return inicio;
    //    }
    //    public int NecesitoBalanceo(Nodo padre)
    //    {
    //        padre.calcularAltura();
    //        int a = 0;
    //        if (padre.Izquierdo == null && padre.Derecho == null)
    //        {
    //            a = 0;
    //        }
    //        else if (padre.Izquierdo == null && padre.Derecho != null)
    //        {
    //            a = 0 - padre.Derecho.Altura;
    //        }
    //        else if (padre.Izquierdo != null && padre.Derecho == null)
    //        {
    //            a = padre.Izquierdo.Altura;
    //        }
    //        else if (padre.Izquierdo != null && padre.Derecho != null)
    //        {
    //            a = padre.Izquierdo.Altura - padre.Derecho.Altura;
    //        }

    //        return a;
    //    }

    //    //Rotacion Izquierdo Simple
    //    Nodo RotacionDerechoSimple(Nodo k2)
    //    {
    //        Nodo k1 = k2.Izquierdo;
    //        k2.Izquierdo = k1.Derecho;
    //        k1.Derecho = k2;
    //        k2.Altura = max(Alturas(k2.Izquierdo), Alturas(k2.Derecho)) + 1;
    //        k1.Altura = max(Alturas(k1.Izquierdo), k2.Altura) + 1;
    //        return k1;
    //    }
    //    //Rotacion Derecho Simple
    //    Nodo RotacionIzquierdoSimple(Nodo k1)
    //    {
    //        Nodo k2 = k1.Derecho;
    //        k1.Derecho = k2.Izquierdo;
    //        k2.Izquierdo = k1;
    //        k1.Altura = max(Alturas(k1.Izquierdo), Alturas(k1.Derecho)) + 1;
    //        k2.Altura = max(Alturas(k2.Derecho), k1.Altura) + 1;
    //        return k2;
    //    }
    //    //Doble Rotacion Izquierdo
    //    Nodo RotacionDerechoDoble(Nodo k3)
    //    {
    //        k3.Izquierdo = RotacionIzquierdoSimple(k3.Izquierdo);
    //        return RotacionDerechoSimple(k3);
    //    }
    //    //Doble Rotacion Derecho
    //    Nodo RotacionIzquierdoDoble(Nodo k1)
    //    {
    //        k1.Derecho = RotacionDerechoSimple(k1.Derecho);
    //        return RotacionIzquierdoSimple(k1);
    //    }
    }
}
