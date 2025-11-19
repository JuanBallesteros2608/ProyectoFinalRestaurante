using System;

namespace ProyectoFinalRestaurante
{
    // esta clase maneja los pedidos y los reportes

    public class Pedido
    {
        public string IdPedido;
        public Cliente ClientePedido;
        public ListaEnlazada<PlatoPedido> PlatosPedido;
        public decimal Total;
        public DateTime FechaHora;
        public string Estado;

        // plato que se guarda dentro del pedido
        public class PlatoPedido
        {
            public Plato Plato;
            public int Cantidad;
            public decimal Subtotal;
        }

        // cuando se crea el pedido se prepara la lista interna
        public Pedido()
        {
            PlatosPedido = new ListaEnlazada<PlatoPedido>();
        }

        // menu principal de pedidos
        public static void MenuPedidos(ListaEnlazada<Restaurante> restaurantes)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- PEDIDOS ---");
                Console.WriteLine("1. Tomar pedido");
                Console.WriteLine("2. Ver pedidos pendientes");
                Console.WriteLine("3. Despachar pedido");
                Console.WriteLine("0. Volver");
                Console.Write("Elija una opcion: ");
                string op = Console.ReadLine();

                if (op == "0") return;

                Restaurante r = Restaurante.SeleccionarRestaurante(restaurantes);
                if (r == null) continue;

                if (op == "1") TomarPedido(r);
                else if (op == "2") VerPedidosPendientes(r);
                else if (op == "3") DespacharPedido(r);
                else
                {
                    Console.WriteLine("opcion no valida");
                    Console.ReadLine();
                }
            }
        }

        // registrar un pedido nuevo
        public static void TomarPedido(Restaurante r)
        {
            Console.Clear();
            Console.WriteLine("--- TOMAR PEDIDO ---");

            if (r.Clientes.Cabeza == null)
            {
                Console.WriteLine("no hay clientes");
                Console.ReadLine();
                return;
            }

            if (r.Platos.Cabeza == null)
            {
                Console.WriteLine("no hay platos");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("escoja el cliente");
            Cliente cli = Cliente.SeleccionarCliente(r);
            if (cli == null) return;

            Pedido p = new Pedido();
            p.IdPedido = Guid.NewGuid().ToString();
            p.ClientePedido = cli;
            p.FechaHora = DateTime.Now;
            p.Estado = "PENDIENTE";

            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- AGREGAR PLATO AL PEDIDO ---");

                Plato pla = Plato.SeleccionarPlato(r);
                if (pla == null) break;

                Console.Write("cantidad: ");
                string txt = Console.ReadLine();
                int cant;

                if (!int.TryParse(txt, out cant) || cant <= 0)
                {
                    Console.WriteLine("cantidad invalida");
                    Console.ReadLine();
                    continue;
                }

                PlatoPedido pp = new PlatoPedido();
                pp.Plato = pla;
                pp.Cantidad = cant;
                pp.Subtotal = pla.Precio * cant;

                p.PlatosPedido.Agregar(pp);

                Console.Write("agregar otro plato s n: ");
                string s = Console.ReadLine();
                if (s == null || s.ToLower() != "s") break;
            }

            decimal total = 0;
            Nodo<PlatoPedido> act = p.PlatosPedido.Cabeza;
            while (act != null)
            {
                total += act.Valor.Subtotal;
                act = act.Siguiente;
            }
            p.Total = total;

            if (p.Total <= 0)
            {
                Console.WriteLine("pedido sin platos no se guarda");
                Console.ReadLine();
                return;
            }

            Console.Clear();
            Console.WriteLine("--- RESUMEN DEL PEDIDO ---");
            Console.WriteLine("cliente: " + cli.NombreCompleto);

            act = p.PlatosPedido.Cabeza;
            while (act != null)
            {
                Console.WriteLine(act.Valor.Plato.Nombre + " x " + act.Valor.Cantidad + " = " + act.Valor.Subtotal);
                act = act.Siguiente;
            }

            Console.WriteLine("TOTAL: " + p.Total);
            Console.Write("confirmar s n: ");
            string cfm = Console.ReadLine();

            if (cfm == null || cfm.ToLower() != "s")
            {
                Console.WriteLine("pedido cancelado");
                Console.ReadLine();
                return;
            }

            r.ColaPedidos.Agregar(p);

            Console.WriteLine("pedido guardado");
            Console.ReadLine();
        }

        // muestra la cola de pedidos pendientes
        public static void VerPedidosPendientes(Restaurante r)
        {
            Console.Clear();
            Console.WriteLine("--- PEDIDOS PENDIENTES ---");

            if (r.ColaPedidos.EstaVacia())
            {
                Console.WriteLine("no hay pedidos pendientes");
            }
            else
            {
                r.ColaPedidos.Recorrer(p =>
                {
                    Console.WriteLine("id " + p.IdPedido + " cliente " + p.ClientePedido.NombreCompleto + " total " + p.Total);
                });
            }

            Console.WriteLine("enter para seguir");
            Console.ReadLine();
        }

        // saca un pedido de la cola y lo pasa al historial
        public static void DespacharPedido(Restaurante r)
        {
            Console.Clear();
            Console.WriteLine("--- DESPACHAR PEDIDO ---");

            if (r.ColaPedidos.EstaVacia())
            {
                Console.WriteLine("no hay pedidos pendientes");
                Console.ReadLine();
                return;
            }

            Pedido p = r.ColaPedidos.Primero();

            Console.WriteLine("id " + p.IdPedido);
            Console.WriteLine("cliente " + p.ClientePedido.NombreCompleto);
            Console.WriteLine("total " + p.Total);
            Console.Write("confirmar s n: ");
            string conf = Console.ReadLine();

            if (conf == null || conf.ToLower() != "s")
            {
                Console.WriteLine("accion cancelada");
                Console.ReadLine();
                return;
            }

            p.Estado = "DESPACHADO";

            if (p.FechaHora.Date == DateTime.Now.Date)
            {
                r.GananciasDia += p.Total;
            }

            r.HistorialPedidos.AgregarElemento(p);
            r.ColaPedidos.Eliminar();

            Console.WriteLine("pedido despachado");
            Console.ReadLine();
        }

        // menu para los reportes
        public static void MenuReportes(ListaEnlazada<Restaurante> restaurantes)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- REPORTES ---");
                Console.WriteLine("1. Ver platos servidos hoy");
                Console.WriteLine("2. Ver ganancias del dia");
                Console.WriteLine("0. Volver");
                Console.Write("Elija una opcion: ");
                string op = Console.ReadLine();

                if (op == "0") return;

                Restaurante r = Restaurante.SeleccionarRestaurante(restaurantes);
                if (r == null) continue;

                if (op == "1") VerPlatosServidosHoy(r);
                else if (op == "2") VerGananciasDia(r);
                else
                {
                    Console.WriteLine("opcion no valida");
                    Console.ReadLine();
                }
            }
        }

        // lista los platos servidos en el dia
        public static void VerPlatosServidosHoy(Restaurante r)
        {
            Console.Clear();
            Console.WriteLine("--- PLATOS SERVIDOS HOY ---");

            bool hay = false;

            r.HistorialPedidos.Recorrer(p =>
            {
                if (p.FechaHora.Date == DateTime.Now.Date)
                {
                    Nodo<PlatoPedido> act = p.PlatosPedido.Cabeza;
                    while (act != null)
                    {
                        Console.WriteLine(act.Valor.Plato.Nombre + " x " + act.Valor.Cantidad);
                        hay = true;
                        act = act.Siguiente;
                    }
                }
            });

            if (!hay)
            {
                Console.WriteLine("no se han servido platos hoy");
            }

            Console.WriteLine("enter para seguir");
            Console.ReadLine();
        }

        // muestra las ganancias acumuladas del dia
        public static void VerGananciasDia(Restaurante r)
        {
            Console.Clear();
            Console.WriteLine("--- GANANCIAS DEL DIA ---");
            Console.WriteLine(r.GananciasDia);
            Console.WriteLine("enter para seguir");
            Console.ReadLine();
        }
    }
}
