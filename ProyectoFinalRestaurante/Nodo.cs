namespace ProyectoFinalRestaurante
{
    // este es un nodo sencillo para la lista
    // solo guarda el valor y el siguiente nodo

    public class Nodo<T>
    {
        public T Valor;
        public Nodo<T> Siguiente;

        public Nodo(T valor)
        {
            Valor = valor;
            Siguiente = null;
        }
    }
}