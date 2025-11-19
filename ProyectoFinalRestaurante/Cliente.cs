using System;

namespace ProyectoFinalRestaurante
{
    // esta clase guarda datos basicos del cliente
    // tambien se usa para manejar el menu de clientes

    public class Cliente
    {
        public string Cedula;
        public string NombreCompleto;
        public string Celular;
        public string Email;

        // revisa de forma sencilla que el correo tenga @ y .
        public static bool EsEmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            if (!email.Contains("@")) return false;
            if (!email.Contains(".")) return false;
            return true;
        }

        // revisa si una cedula se repite en cualquier restaurante
        public static bool ExisteCedulaEnTodos(ListaEnlazada<Restaurante> restaurantes, string cedula)
        {
            Nodo<Restaurante> r = restaurantes.Cabeza;
            while (r != null)
            {
                Nodo<Cliente> c = r.Valor.Clientes.Cabeza;
                while (c != null)
                {
                    if (c.Valor.Cedula == cedula)
                    {
                        return true;
                    }
                    c = c.Siguiente;
                }
                r = r.Siguiente;
            }
            return false;
        }

        // menu clientes
        public static void MenuClientes(ListaEnlazada<Restaurante> restaurantes)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- CLIENTES ---");
                Console.WriteLine("1. Agregar cliente");
                Console.WriteLine("2. Editar cliente");
                Console.WriteLine("3. Listar clientes");
                Console.WriteLine("0. Volver");
                Console.Write("Elija una opcion: ");
                string op = Console.ReadLine();

                if (op == "0") return;

                Restaurante rest = Restaurante.SeleccionarRestaurante(restaurantes);
                if (rest == null) continue;

                if (op == "1")
                {
                    AgregarCliente(restaurantes, rest);
                }
                else if (op == "2")
                {
                    EditarCliente(restaurantes, rest);
                }
                else if (op == "3")
                {
                    ListarClientes(rest, true);
                }
                else
                {
                    Console.WriteLine("opcion no valida");
                    Console.ReadLine();
                }
            }
        }

        // permite escoger un cliente de la lista
        public static Cliente SeleccionarCliente(Restaurante r)
        {
            while (true)
            {
                ListarClientes(r, false);
                Console.WriteLine("0. Volver");
                Console.Write("Numero de cliente: ");
                string t = Console.ReadLine();

                int op;
                if (!int.TryParse(t, out op))
                {
                    Console.WriteLine("dato invalido");
                    Console.ReadLine();
                    return null;
                }

                if (op == 0) return null;

                int idx = op - 1;
                int total = r.Clientes.Longitud();

                if (idx < 0 || idx >= total)
                {
                    Console.WriteLine("numero fuera de rango");
                    Console.ReadLine();
                    return null;
                }

                return r.Clientes.ObtenerPorIndice(idx);
            }
        }

        // agrega un cliente nuevo al restaurante
        public static void AgregarCliente(ListaEnlazada<Restaurante> restaurantes, Restaurante r)
        {
            Console.Clear();
            Console.WriteLine("--- AGREGAR CLIENTE ---");

            Cliente c = new Cliente();

            Console.Write("cedula: ");
            c.Cedula = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(c.Cedula))
            {
                Console.WriteLine("cedula vacia");
                Console.ReadLine();
                return;
            }

            if (ExisteCedulaEnTodos(restaurantes, c.Cedula))
            {
                Console.WriteLine("ya hay un cliente con esa cedula");
                Console.ReadLine();
                return;
            }

            Console.Write("nombre completo: ");
            c.NombreCompleto = Console.ReadLine();

            Console.Write("celular 10 digitos: ");
            c.Celular = Console.ReadLine();

            if (!Restaurante.EsCelularValido(c.Celular))
            {
                Console.WriteLine("celular incorrecto");
                Console.ReadLine();
                return;
            }

            Console.Write("email: ");
            c.Email = Console.ReadLine();

            if (!EsEmailValido(c.Email))
            {
                Console.WriteLine("email incorrecto");
                Console.ReadLine();
                return;
            }

            r.Clientes.Agregar(c);

            Console.WriteLine("cliente guardado");
            Console.ReadLine();
        }

        // muestra todos los clientes del restaurante
        public static void ListarClientes(Restaurante r, bool esperar)
        {
            Console.Clear();
            Console.WriteLine("--- LISTA DE CLIENTES ---");

            Nodo<Cliente> act = r.Clientes.Cabeza;
            int i = 0;

            if (act == null)
            {
                Console.WriteLine("no hay clientes");
            }

            while (act != null)
            {
                Cliente c = act.Valor;
                Console.WriteLine((i + 1) + ". " + c.Cedula + " - " + c.NombreCompleto + " - " + c.Celular);
                i++;
                act = act.Siguiente;
            }

            if (esperar)
            {
                Console.WriteLine("enter para continuar");
                Console.ReadLine();
            }
        }

        // editar datos de un cliente
        public static void EditarCliente(ListaEnlazada<Restaurante> restaurantes, Restaurante r)
        {
            Console.Clear();
            Console.WriteLine("--- EDITAR CLIENTE ---");

            Cliente c = SeleccionarCliente(r);
            if (c == null) return;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("cliente actual");
                Console.WriteLine(c.Cedula + " - " + c.NombreCompleto + " - " + c.Celular + " - " + c.Email);
                Console.WriteLine();
                Console.WriteLine("1. cambiar nombre");
                Console.WriteLine("2. cambiar celular");
                Console.WriteLine("3. cambiar email");
                Console.WriteLine("0. volver");
                Console.Write("Elija una opcion: ");
                string op = Console.ReadLine();

                if (op == "1")
                {
                    Console.Write("nuevo nombre: ");
                    c.NombreCompleto = Console.ReadLine();
                }
                else if (op == "2")
                {
                    Console.Write("nuevo celular: ");
                    string cel = Console.ReadLine();
                    if (Restaurante.EsCelularValido(cel))
                    {
                        c.Celular = cel;
                    }
                    else
                    {
                        Console.WriteLine("celular incorrecto");
                        Console.ReadLine();
                    }
                }
                else if (op == "3")
                {
                    Console.Write("nuevo email: ");
                    string mail = Console.ReadLine();
                    if (EsEmailValido(mail))
                    {
                        c.Email = mail;
                    }
                    else
                    {
                        Console.WriteLine("email incorrecto");
                        Console.ReadLine();
                    }
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
