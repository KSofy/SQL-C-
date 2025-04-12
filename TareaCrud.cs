using Microsoft.Data.SqlClient; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqlclase10.CLASES
{
    public class TareaCrud
    {
        string connectionString = "Server=DESKTOP-G1DEPQ9\\SQLEXPRESS;Database=UMG;Integrated Security=True; TrustServerCertificate=True;";

        private bool VerificarCarnet(string carnet)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Tb_Alumnos WHERE Carnet = @carnet"; 
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@carnet", carnet); 
                connection.Open();
                int count = (int)cmd.ExecuteScalar(); 
                return count > 0; 
            }
        }

        
        public void AgregarTareaporCarnet()
        {
            Console.WriteLine("Ingrese el carnet del estudiante:");
            string carnet = Console.ReadLine();

            if (!VerificarCarnet(carnet))
            {
                Console.WriteLine(" El carnet no existe en la tabla de alumnos.");
                return;
            }

            
            Console.Write("Ingrese la Nota 1: ");
            if (!float.TryParse(Console.ReadLine(), out float nota1)) 
            {
                Console.WriteLine(" Nota 1 inválida.");
                return;
            }
            Console.Write("Ingrese la Nota 2: ");
            if (!float.TryParse(Console.ReadLine(), out float nota2))
            {
                Console.WriteLine(" Nota 2 inválida.");
                return;
            }
            Console.Write("Ingrese la Nota 3: ");
            if (!float.TryParse(Console.ReadLine(), out float nota3))
            {
                Console.WriteLine(" Nota 3 inválida.");
                return;
            }
            Console.Write("Ingrese la Nota 4: ");
            if (!float.TryParse(Console.ReadLine(), out float nota4))
            {
                Console.WriteLine(" Nota 4 inválida.");
                return;
            }

           
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "INSERT INTO tareas (Carnet, Nota1, Nota2, Nota3, Nota4) VALUES (@carnet, @nota1, @nota2, @nota3, @nota4)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@carnet", carnet);
                    command.Parameters.AddWithValue("@nota1", nota1);
                    command.Parameters.AddWithValue("@nota2", nota2);
                    command.Parameters.AddWithValue("@nota3", nota3);
                    command.Parameters.AddWithValue("@nota4", nota4);

                    connection.Open();
                    command.ExecuteNonQuery(); 
                    Console.WriteLine(" Tarea agregada correctamente.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" Error al agregar tarea: " + ex.Message);
                }
            }
        }

       
        public void MostrarTareas()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT * FROM tareas ORDER BY id_tarea"; 
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader(); 
                    while (reader.Read()) 
                    {
                        
                        float n1 = Convert.ToSingle(reader["Nota1"]);
                        float n2 = Convert.ToSingle(reader["Nota2"]);
                        float n3 = Convert.ToSingle(reader["Nota3"]);
                        float n4 = Convert.ToSingle(reader["Nota4"]);
                        

                        
                        Console.WriteLine($"ID: {reader["id_tarea"]}, Carnet: {reader["Carnet"]}, Notas: {n1}, {n2}, {n3}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" Error al mostrar tareas: " + ex.Message);
                }
            }
        }


        public void ActualizarTareaPorCarnet()
        {
            Console.Write("Ingrese el carnet del estudiante cuyas tareas desea actualizar: ");
            string carnetActualizar = Console.ReadLine();

            if (!VerificarCarnet(carnetActualizar))
            {
                Console.WriteLine(" El carnet no existe.");
                return;
            }

            try
            {
                Console.Write("Nueva Nota 1 para el carnet {0}: ", carnetActualizar);
                if (!float.TryParse(Console.ReadLine(), out float nota1))
                {
                    Console.WriteLine(" Nota 1 inválida.");
                    return;
                }

                Console.Write("Nueva Nota 2 para el carnet {0}: ", carnetActualizar);
                if (!float.TryParse(Console.ReadLine(), out float nota2))
                {
                    Console.WriteLine(" Nota 2 inválida.");
                    return;
                }

                Console.Write("Nueva Nota 3 para el carnet {0}: ", carnetActualizar);
                if (!float.TryParse(Console.ReadLine(), out float nota3))
                {
                    Console.WriteLine(" Nota 3 inválida.");
                    return;
                }

                Console.Write("Nueva Nota 4 para el carnet {0}: ", carnetActualizar);
                if (!float.TryParse(Console.ReadLine(), out float nota4))
                {
                    Console.WriteLine(" Nota 4 inválida.");
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE tareas SET Nota1 = Nota1 = @nota1, Nota2 = @nota2, Nota3 = @nota3, Nota4 = @nota4 WHERE Carnet = @carnet";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@carnet", carnetActualizar);
                    command.Parameters.AddWithValue("@nota1", nota1);
                    command.Parameters.AddWithValue("@nota2", nota2);
                    command.Parameters.AddWithValue("@nota3", nota3);
                    command.Parameters.AddWithValue("@nota4", nota4);

                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    Console.WriteLine(result > 0 ? $" Se actualizaron {result} tarea(s) para el carnet {carnetActualizar}." : $" No se encontraron tareas para el carnet {carnetActualizar}.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine(" Una de las notas ingresadas no es un número válido.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Error al actualizar tarea: " + ex.Message);
            }
        }

        public void EliminarTareaPorCarnet()
        {
            Console.Write("Ingrese el carnet del estudiante cuyas tareas desea eliminar: ");
            string carnetEliminar = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "DELETE * FROM tareas WHERE Carnet = @carnet";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@carnet", carnetEliminar);

                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    Console.WriteLine(result > 0 ? $" Se eliminaron {result} tarea(s) para el carnet {carnetEliminar}." : $" No se encontraron tareas para el carnet {carnetEliminar}.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" Error al eliminar tareas: " + ex.Message);
                }
            }
        }


        public void BuscarPorId()
        {
            Console.Write("Ingrese el ID de la tarea a buscar: ");
            if (!int.TryParse(Console.ReadLine(), out int idTarea))
            {
                Console.WriteLine(" ID inválido. Debe ser un número.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT * FROM tareas WHERE id_tarea = @idTarea";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idTarea", idTarea);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        float n1 = Convert.ToSingle(reader["Nota1"]);
                        float n2 = Convert.ToSingle(reader["Nota2"]);
                        float n3 = Convert.ToSingle(reader["Nota3"]);
                        float n4 = Convert.ToSingle(reader["Nota4"]);
                       

                        Console.WriteLine($"\nInformación de la Tarea con ID {idTarea}:");
                        Console.WriteLine($"ID: {reader["id_tarea"]}, Carnet: {reader["Carnet"]}, Notas: {n1}, {n2}, {n3}, {n4}");
                    }
                    else
                    {
                        Console.WriteLine($" No se encontró ninguna tarea con el ID: {idTarea}.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" Error al buscar tarea por ID: " + ex.Message);
                }
            }
        }
    }
}