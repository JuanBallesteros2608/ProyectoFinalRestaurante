using System;

namespace ProyectoFinalRestaurante
{
    // esta clase tiene la info del restaurante
    // tambien maneja el menu para crear y editar

    public class Restaurante
    {
        public string Nit;
        public string Nombre;
        public string Dueno;
        public string Celular;
        public string Direccion;

        public ListaEnlazada<Cliente> Clientes;
        public ListaEnlazada<Plato> Platos;
        public Cola<Pedido> ColaPedidos;
        public Pila<Pedido> HistorialPedidos;
        public decimal GananciasDia;

        // cuando se crea el restaurante se preparan las listas
        public Restaurante()
        {
            Clientes = new ListaEnlazada<Cliente>();
            Platos = new ListaEnlazada<Plato>();
            ColaPedidos = new Cola<Pedido>();
            HistorialPedidos = new Pila<Pedido>();
            GananciasDia = 0;
        }

        // revisa que el celular tenga 10 numeros
        public static bool EsCelularValido(string celular)
        {
            if (string.IsNullOrWhiteSpace(celular)) return false;
            if (celular.Length != 10) return false;

            foreach (char c in celular)
            {
                if (!char.IsDigit(c)) return false;
            }

            return true;
        }

        // busca un restaurante por nit dentro de la lista
        public static Restaurante BuscarPorNit(ListaEnlazada<Restaurante> restaurantes, string nit)
        {
            Nodo<Restaurante> act = restaurantes.Cabeza;
            while (act != null)
            {
                if (act.Valor.Nit == nit)
                {
                    return act.Valor;
                }
                act = act.Siguiente;
            }
            return null;
        }

        // menu principal de restaurantes
        public static void MenuRestaurantes(ListaEnlazada<Restaurante> restaurantes)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- RESTAURANTES ---");
                Console.WriteLine("1. Crear restaurante");
                Console.WriteLine("2. Editar restaurante");
                Console.WriteLine("3. Listar restaurantes");
                Console.WriteLine("0. Volver");
                Console.Write("Elija una opcion: ");
                string op = Console.ReadLine();

                if (op == "0")
                {
                    return;
                }

                if (op == "1")
                {
                    CrearRestaurante(restaurantes);
                }
                else if (op == "2")
                {
                    EditarRestaurante(restaurantes);
                }
                else if (op == "3")
                {
                    ListarRestaurantes(restaurantes, true);
                }
                else
                {
                    Console.WriteLine("opcion no valida");
                    Console.ReadLine();
                }
            }
        }

        // crear un restaurante nuevo
        public static void CrearRestaurante(ListaEnlazada<Restaurante> restaurantes)
        {
            Console.Clear();
            Console.WriteLine("--- CREAR RESTAURANTE ---");

            Restaurante r = new Restaurante();

            Console.Write("nit: ");
            r.Nit = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(r.Nit))
            {
                Console.WriteLine("nit vacio");
                Console.ReadLine();
                return;
            }

            if (BuscarPorNit(restaurantes, r.Nit) != null)
            {
                Console.WriteLine("ya existe un restaurante con ese nit");
                Console.ReadLine();
                return;
            }

            Console.Write("nombre: ");
            r.Nombre = Console.ReadLine();

            Console.Write("dueño: ");
            r.Dueno = Console.ReadLine();

            Console.Write("celular: ");
            r.Celular = Console.ReadLine();

            if (!EsCelularValido(r.Celular))
            {
                Console.WriteLine("celular incorrecto");
                Console.ReadLine();
                return;
            }

            Console.Write("direccion: ");
            r.Direccion = Console.ReadLine();

            restaurantes.Agregar(r);

            Console.WriteLine("restaurante guardado");
            Console.ReadLine();
        }

        // muestra todos los restaurantes
        public static void ListarRestaurantes(ListaEnlazada<Restaurante> restaurantes, bool esperar)
        {
            Console.Clear();
            Console.WriteLine("--- LISTA DE RESTAURANTES ---");

            Nodo<Restaurante> act = restaurantes.Cabeza;
            int i = 0;

            if (act == null)
            {
                Console.WriteLine("no hay restaurantes registrados");
            }

            while (act != null)
            {
                Restaurante r = act.Valor;
                Console.WriteLine((i + 1) + ". " + r.Nit + " - " + r.Nombre + " - " + r.Dueno + " - " + r.Celular);
                i++;
                act = act.Siguiente;
            }

            if (esperar)
            {
                Console.WriteLine("enter para continuar");
                Console.ReadLine();
            }
        }

        // permite escoger un restaurante de la lista
        public static Restaurante SeleccionarRestaurante(ListaEnlazada<Restaurante> restaurantes)
        {
            while (true)
            {
                ListarRestaurantes(restaurantes, false);
                Console.WriteLine("0. Volver");
                Console.Write("Numero de restaurante: ");

                string t = Console.ReadLine();
                int op;

                if (!int.TryParse(t, out op))
                {
                    Console.WriteLine("dato invalido");
                    Console.ReadLine();
                    return null;
                }

                if (op == 0)
                {
                    return null;
                }

                int idx = op - 1;
                int total = restaurantes.Longitud();

                if (idx < 0 || idx >= total)
                {
                    Console.WriteLine("numero fuera de rango");
                    Console.ReadLine();
                    return null;
                }

                return restaurantes.ObtenerPorIndice(idx);
            }
        }

        // editar datos del restaurante
        public static void EditarRestaurante(ListaEnlazada<Restaurante> restaurantes)
        {
            Console.Clear();
            Console.WriteLine("--- EDITAR RESTAURANTE ---");

            Restaurante r = SeleccionarRestaurante(restaurantes);
            if (r == null) return;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("restaurante actual");
                Console.WriteLine(r.Nit + " - " + r.Nombre + " - " + r.Dueno + " - " + r.Celular + " - " + r.Direccion);
                Console.WriteLine();
                Console.WriteLine("1. cambiar nombre");
                Console.WriteLine("2. cambiar dueño");
                Console.WriteLine("3. cambiar celular");
                Console.WriteLine("4. cambiar direccion");
                Console.WriteLine("0. volver");
                Console.Write("Elija una opcion: ");
                string op = Console.ReadLine();

                if (op == "1")
                {
                    Console.Write("nuevo nombre: ");
                    r.Nombre = Console.ReadLine();
                }
                else if (op == "2")
                {
                    Console.Write("nuevo dueño: ");
                    r.Dueno = Console.ReadLine();
                }
                else if (op == "3")
                {
                    Console.Write("nuevo celular: ");
                    string cel = Console.ReadLine();
                    if (EsCelularValido(cel))
                    {
                        r.Celular = cel;
                    }
                    else
                    {
                        Console.WriteLine("celular incorrecto");
                        Console.ReadLine();
                    }
                }
                else if (op == "4")
                {
                    Console.Write("nueva direccion: ");
                    r.Direccion = Console.ReadLine();
                }
                else if (op == "0")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("opcion no valida");
                    Console.ReadLine();
                }
            }
        }
    }
}
