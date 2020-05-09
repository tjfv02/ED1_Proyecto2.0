﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Structures
{
    public class HashTable<T, TU>
    { //Comentario comprobando que no de el error otra vez
        
        //                  <Llave, Valor>
        private LinkedList<Tuple<T, TU>>[] items;
        private int factorLlenado = 3; //ayuda a extender el tamaño de la lista en caso de colision 
        private int tamaño;

        public HashTable()
        {
            items = new LinkedList<Tuple<T, TU>>[4];//Tamaño incial de la tabla
        }

        public void Insertar(T key, TU value)
        {
            var pos = PosicionEnHash(key, items.Length);
            if (items[pos] == null)
            {
                items[pos] = new LinkedList<Tuple<T, TU>>();
            }
            if (items[pos].Any(x => x.Item1.Equals(key)))
            {
                throw new ArgumentException("Llave duplicada, no se puede insertar");
            }
            if (tamaño >= factorLlenado)
            {
                ReHashing();
            }
            pos = PosicionEnHash(key, items.Length);
            if (items[pos] == null)
            {
                items[pos] = new LinkedList<Tuple<T, TU>>();
            }
            items[pos].AddFirst(new Tuple<T, TU>(key, value));
            tamaño++;
        }

        public void Eliminar(T key)
        {
            var pos = PosicionEnHash(key, items.Length);
            if (items[pos] != null)
            {
                var objRemover = items[pos].FirstOrDefault(item => item.Item1.Equals(key));
                if (objRemover == null) return;
                items[pos].Remove(objRemover);
                tamaño--;
            }
            else
            {
                throw new Exception("Este valor no se encuentra en la tabla hash.");
            }
        }

        //public TU Get(T key)
        //{
        //    var pos = PosicionEnHash(key, items.Length);
        //    foreach (var item in items[pos].Where(item => item.Item1.Equals(key)))
        //    {
        //        return item.Item2;
        //    }
        //    throw new KeyNotFoundException("La llave no existe en la tabla hash.");
        //}

        private void ReHashing() // Crea nuevamente la tabla e ingresa los datos 
        {
            factorLlenado *= 2;
            var nuevosItems = new LinkedList<Tuple<T, TU>>[items.Length * 2];
            foreach (var item in items.Where(x => x != null))
            {
                foreach (var value in item)
                {
                    var pos = PosicionEnHash(value.Item1, nuevosItems.Length);
                    if (nuevosItems[pos] == null)
                    {
                        nuevosItems[pos] = new LinkedList<Tuple<T, TU>>();
                    }
                    nuevosItems[pos].AddFirst(new Tuple<T, TU>(value.Item1, value.Item2));
                }
            }
            items = nuevosItems;
        }

        private int PosicionEnHash(T key, int length)
        {
            var hash = key.GetHashCode();
            var pos = Math.Abs(hash % length);
            return pos;
        }
    }
}
