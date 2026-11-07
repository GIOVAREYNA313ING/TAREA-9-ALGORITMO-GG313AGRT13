using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace ConexionGastos
{
    class Program
    {
        private const string CadenaConexion =
            "Server=(localdb)\\MSSQLLocalDB;Database=GastosDB;Integrated Security=True;TrustServerCertificate=True";

        static void Main(string[] args)
        {
            int opcion = 0;

            while (opcion != 3)
            {
                Console.Clear();

                Console.WriteLine("===== REGISTRO DE GASTOS =====");
                Console.WriteLine("1. Guardar gasto");
                Console.WriteLine("2. Mostrar gastos");
                Console.WriteLine("3. Salir");
                Console.Write("\nElige una opcion: ");

                opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        GuardarGasto();
                        break;

                    case 2:
                        MostrarGastos();
                        break;

                    case 3:
                        Console.WriteLine("Programa finalizado.");
                        break;

                    default:
                        Console.WriteLine("Opcion incorrecta.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void GuardarGasto()
        {
            Console.Clear();

            Console.WriteLine("===== GUARDAR GASTO =====");

            Console.Write("Descripcion: ");
            string descripcion = Console.ReadLine();

            Console.Write("Monto: Q");
            decimal monto = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Categoria: ");
            string categoria = Console.ReadLine();

            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();

                string consulta =
                    "INSERT INTO Gastos (Descripcion, Monto, Categoria) " +
                    "VALUES (@Descripcion, @Monto, @Categoria)";

                SqlCommand comando = new SqlCommand(consulta, conexion);

                comando.Parameters.AddWithValue("@Descripcion", descripcion);
                comando.Parameters.AddWithValue("@Monto", monto);
                comando.Parameters.AddWithValue("@Categoria", categoria);

                comando.ExecuteNonQuery();
            }

            Console.WriteLine("\nGasto guardado correctamente.");
            Console.ReadKey();
        }

        static void MostrarGastos()
        {
            List<Gasto> listaGastos = new List<Gasto>();
            decimal total = 0;

            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();

                string consulta =
                    "SELECT Id, Descripcion, Monto, Categoria FROM Gastos";

                SqlCommand comando = new SqlCommand(consulta, conexion);
                SqlDataReader lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Gasto gasto = new Gasto();

                    gasto.Id = lector.GetInt32(0);
                    gasto.Descripcion = lector.GetString(1);
                    gasto.Monto = lector.GetDecimal(2);
                    gasto.Categoria = lector.GetString(3);

                    listaGastos.Add(gasto);
                    total = total + gasto.Monto;
                }

                lector.Close();
            }

            Console.Clear();

            Console.WriteLine("===== LISTA DE GASTOS =====\n");

            if (listaGastos.Count == 0)
            {
                Console.WriteLine("No hay gastos registrados.");
            }
            else
            {
                foreach (Gasto g in listaGastos)
                {
                    Console.WriteLine(g.ToString());
                }

                Console.WriteLine("\nTOTAL GASTADO: Q" + total.ToString("F2"));
            }

            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }
    }

    public class Gasto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public string Categoria { get; set; }

        public override string ToString()
        {
            return Id + " - " + Descripcion + " - Q" +
                   Monto.ToString("F2") + " - " + Categoria;
        }
    }
}