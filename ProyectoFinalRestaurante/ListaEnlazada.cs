using System;

namespace ProyectoFinalRestaurante
{
    // esta es una lista enlazada muy simple hecha a mano

    public class ListaEnlazada<T>
    {
        public Nodo<T> Cabeza;

        // agrega un elemento al final
        public void Agregar(T valor)
        {
            Nodo<T> nuevo = new Nodo<T>(valor);

            if (Cabeza == null)
            {
                Cabeza = nuevo;
                return;
            }

            Nodo<T> act = Cabeza;
            while (act.Siguiente != null)
            {
                act = act.Siguiente;
            }

            act.Siguiente = nuevo;
        }

        // cuenta cuantos nodos hay
        public int Longitud()
        {
            int total = 0;
            Nodo<T> act = Cabeza;
            while (act != null)
            {
                total++;
                act = act.Siguiente;
            }
            return total;
        }

        // obtiene un elemento por posicion
        public T ObtenerPorIndice(int indice)
        {
            int i = 0;
            Nodo<T> act = Cabeza;

            while (act != null)
            {
                if (i == indice)
                {
                    return act.Valor;
                }
                i++;
                act = act.Siguiente;
            }

            return default(T);
        }

        // recorre la lista y ejecuta una accion
        public void Recorrer(Action<T> accion)
        {
            Nodo<T> act = Cabeza;
            while (act != null)
            {
                accion(act.Valor);
                act = act.Siguiente;
            }
        }
    }
}