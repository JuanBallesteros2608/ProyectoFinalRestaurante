using System;

namespace ProyectoFinalRestaurante
{
    // esta es una pila sencilla
    // el ultimo que entra es el primero que sale

    public class Pila<T>
    {
        private Nodo<T> superior;

        public Pila()
        {
            superior = null;
        }

        // agrega un elemento arriba
        public void AgregarElemento(T valor)
        {
            Nodo<T> nuevo = new Nodo<T>(valor);
            nuevo.Siguiente = superior;
            superior = nuevo;
        }

        // devuelve el que esta arriba sin quitarlo
        public T ObtenerSuperior()
        {
            if (superior == null)
            {
                return default(T);
            }
            return superior.Valor;
        }

        // quita el que esta arriba
        public void EliminarSuperior()
        {
            if (superior == null) return;
            superior = superior.Siguiente;
        }

        // revisa si no hay elementos
        public bool EstaVacia()
        {
            return superior == null;
        }

        // recorre la pila
        public void Recorrer(Action<T> accion)
        {
            Nodo<T> act = superior;
            while (act != null)
            {
                accion(act.Valor);
                act = act.Siguiente;
            }
        }
    }
}