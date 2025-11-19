using System;

namespace ProyectoFinalRestaurante
{
    // esta clase maneja la info de un plato
    // tambien tiene el menu para platos

    public class Plato
    {
        public string Codigo;
        public string Nombre;
        public string Descripcion;
        public decimal Precio;

        // menu platos
        public static void MenuPlatos(ListaEnlazada<Restaurante> restaurantes)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- PLATOS ---");
                Console.WriteLine("1. Agregar plato");
                Console.WriteLine("2. Editar plato");
                Console.WriteLine("3. Listar platos");
                Console.WriteLine("0. Volver");
                Console.Write("Elija una opcion: ");
                string op = Console.ReadLine();

                if (op == "0") return;

                Restaurante r = Restaurante.SeleccionarRestaurante(restaurantes);
                if (r == null) continue;

                if (op == "1")
                {
                    AgregarPlato(r);
                }
                else if (op == "2")
                {
                    EditarPlato(r);
                }
                else if (op == "3")
                {
                    ListarPlatos(r, true);
                }
                else
                {
                    Console.WriteLine("opcion no valida");
                    Console.ReadLine();
                }
            }
        }

        // agrega un plato al restaurante
        public static void AgregarPlato(Restaurante r)
        {
            Console.Clear();
            Console.WriteLine("--- AGREGAR PLATO ---");

            Plato p = new Plato();

            Console.Write("codigo: ");
            p.Codigo = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(p.Codigo))
            {
                Console.WriteLine("codigo vacio");
                Console.ReadLine();
                return;
            }

            if (BuscarPlatoPorCodigo(r, p.Codigo) != null)
            {
                Console.WriteLine("ya existe un plato con ese codigo");
                Console.ReadLine();
                return;
            }

            Console.Write("nombre: ");
            p.Nombre = Console.ReadLine();

            Console.Write("descripcion: ");
            p.Descripcion = Console.ReadLine();

            Console.Write("precio: ");
            string texto = Console.ReadLine();
            decimal precio;

            if (!decimal.TryParse(texto, out precio) || precio <= 0)
            {
                Console.WriteLine("precio invalido");
                Console.ReadLine();
                return;
            }

            p.Precio = precio;

            r.Platos.Agregar(p);

            Console.WriteLine("plato guardado");
            Console.ReadLine();
        }

        // lista todos los platos del restaurante
        public static void ListarPlatos(Restaurante r, bool esperar)
        {
            Console.Clear();
            Console.WriteLine("--- LISTA DE PLATOS ---");

            Nodo<Plato> act = r.Platos.Cabeza;
            int i = 0;

            if (act == null)
            {
                Console.WriteLine("no hay platos");
            }

            while (act != null)
            {
                Plato p = act.Valor;
                Console.WriteLine((i + 1) + ". " + p.Codigo + " - " + p.Nombre + " - " + p.Descripcion + " - " + p.Precio);
                i++;
                act = act.Siguiente;
            }

            if (esperar)
            {
                Console.WriteLine("enter para seguir");
                Console.ReadLine();
            }
        }

        // permite escoger un plato de la lista
        public static Plato SeleccionarPlato(Restaurante r)
        {
            while (true)
            {
                ListarPlatos(r, false);
                Console.WriteLine("0. Volver");
                Console.Write("Numero del plato: ");

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
                int total = r.Platos.Longitud();

                if (idx < 0 || idx >= total)
                {
                    Console.WriteLine("numero fuera de rango");
                    Console.ReadLine();
                    return null;
                }

                return r.Platos.ObtenerPorIndice(idx);
            }
        }

        // busca un plato por codigo en el restaurante
        public static Plato BuscarPlatoPorCodigo(Restaurante r, string codigo)
        {
            Nodo<Plato> act = r.Platos.Cabeza;
            while (act != null)
            {
                if (act.Valor.Codigo == codigo)
                {
                    return act.Valor;
                }
                act = act.Siguiente;
            }
            return null;
        }

        // permite cambiar los datos del plato
        public static void EditarPlato(Restaurante r)
        {
            Console.Clear();
            Console.WriteLine("--- EDITAR PLATO ---");

            Plato p = SeleccionarPlato(r);
            if (p == null) return;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("plato actual");
                Console.WriteLine(p.Codigo + " - " + p.Nombre + " - " + p.Descripcion + " - " + p.Precio);
                Console.WriteLine();
                Console.WriteLine("1. cambiar nombre");
                Console.WriteLine("2. cambiar descripcion");
                Console.WriteLine("3. cambiar precio");
                Console.WriteLine("0. volver");
                Console.Write("Elija una opcion: ");
                string op = Console.ReadLine();

                if (op == "1")
                {
                    Console.Write("nuevo nombre: ");
                    p.Nombre = Console.ReadLine();
                }
                else if (op == "2")
                {
                    Console.Write("nueva descripcion: ");
                    p.Descripcion = Console.ReadLine();
                }
                else if (op == "3")
                {
                    Console.Write("nuevo precio: ");
                    string texto = Console.ReadLine();
                    decimal precio;

                    if (decimal.TryParse(texto, out precio) && precio > 0)
                    {
                        p.Precio = precio;
                    }
                    else
                    {
                        Console.WriteLine("precio invalido");
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
