using System;

namespace ProyectoFinalRestaurante
{
    // esta es una cola basica
    // el primero en entrar es el primero en salir

    public class Cola<T>
    {
        private Nodo<T> cabeza;
        private Nodo<T> cola;

        public Cola()
        {
            cabeza = null;
            cola = null;
        }

        // agrega al final
        public void Agregar(T valor)
        {
            Nodo<T> nuevo = new Nodo<T>(valor);

            if (cabeza == null)
            {
                cabeza = nuevo;
                cola = nuevo;
            }
            else
            {
                cola.Siguiente = nuevo;
                cola = nuevo;
            }
        }

        // devuelve el primero sin quitarlo
        public T Primero()
        {
            if (cabeza == null)
            {
                return default(T);
            }
            return cabeza.Valor;
        }

        // saca el primero
        public void Eliminar()
        {
            if (cabeza == null) return;

            cabeza = cabeza.Siguiente;
            if (cabeza == null)
            {
                cola = null;
            }
        }

        // revisa si esta vacia
        public bool EstaVacia()
        {
            return cabeza == null;
        }

        // recorre todos los elementos
        public void Recorrer(Action<T> accion)
        {
            Nodo<T> act = cabeza;
            while (act != null)
            {
                accion(act.Valor);
                act = act.Siguiente;
            }
        }
    }
}