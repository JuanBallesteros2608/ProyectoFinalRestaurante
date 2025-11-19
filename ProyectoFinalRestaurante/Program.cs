using System;

namespace ProyectoFinalRestaurante
{
    // aqui empieza el sistema del restaurante
    // este archivo solo muestra el menu principal

    public class Program
    {
        public static void Main(string[] args)
        {
            // aqui creo la lista donde se guardan todos los restaurantes
            ListaEnlazada<Restaurante> restaurantes = new ListaEnlazada<Restaurante>();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- SISTEMA RESTAURANTE ---");
                Console.WriteLine("1. Restaurantes");
                Console.WriteLine("2. Clientes");
                Console.WriteLine("3. Platos");
                Console.WriteLine("4. Pedidos");
                Console.WriteLine("5. Reportes");
                Console.WriteLine("99. Salir");
                Console.Write("Escriba una opcion: ");
                string opcion = Console.ReadLine();

                if (opcion == "99")
                {
                    // aqui termino el programa
                    return;
                }
                else if (opcion == "1")
                {
                    Restaurante.MenuRestaurantes(restaurantes);
                }
                else if (opcion == "2")
                {
                    Cliente.MenuClientes(restaurantes);
                }
                else if (opcion == "3")
                {
                    Plato.MenuPlatos(restaurantes);
                }
                else if (opcion == "4")
                {
                    Pedido.MenuPedidos(restaurantes);
                }
                else if (opcion == "5")
                {
                    Pedido.MenuReportes(restaurantes);
                }
                else
                {
                    Console.WriteLine("opcion no valida");
                    Console.WriteLine("enter para seguir");
                    Console.ReadLine();
                }
            }
        }
    }
}
