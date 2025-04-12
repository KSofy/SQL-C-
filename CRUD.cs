using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqlclase10.CLASES
{
    public class CRUD
    {
        string connectionString = "Server=DESKTOP-G1DEPQ9\\SQLEXPRESS;Database=UMG;Integrated Security=True; TrustServerCertificate=True; ";

        public void MostrarAlumno()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT * FROM Tb_Alumnos where seccion = 'C' order by carnet";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"Carnet: {reader["carnet"]}  Nombre {reader["Estudiante"]}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Revisa y averigua el error, Error al conectar a la base de datos: " + ex.Message);
                }
                connection.Close();
            }
        }
        public void AregarAlumno()
        {
            Console.WriteLine("Ingrese el carnet del alumno");
            string carnet = Console.ReadLine();
            Console.WriteLine("Ingrese el nombre del alumno");
            string nombre = Console.ReadLine();
            Console.WriteLine("Ingrese la seccion del alumno");
            char seccion = Console.ReadKey().KeyChar;
            Console.WriteLine("Ingrese el Email del alumno");
            string email = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "INSERT INTO Tb_Alumnos (carnet, Estudiante, seccion, email) VALUES (@carnet, @nombre, @seccion, @email)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@carnet", carnet);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@seccion", seccion);
                    command.Parameters.AddWithValue("@email", email);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Alumno agregado correctamente.");
                    }
                    else
                    {
                        Console.WriteLine("No se pudo agregar el alumno.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Revisa y averigua el error, Error al conectar a la base de datos: " + ex.Message);
                }
                connection.Close();
            }
        }
        public void ActualizarAlumno()
        {
            Console.WriteLine("Ingrese el carnet del alumno a actualizar");
            string carnet = Console.ReadLine();
            Console.WriteLine("Ingrese el nuevo nombre del alumno");
            string nombre = Console.ReadLine();
            Console.WriteLine("Ingrese la nueva seccion del alumno");
            char seccion = Console.ReadKey().KeyChar;
            Console.WriteLine("Ingrese el nuevo Email del alumno");
            string email = Console.ReadLine();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "UPDATE Tb_Alumnos SET Estudiante = @nombre, seccion = @seccion, email = @email WHERE carnet = @carnet";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@carnet", carnet);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@seccion", seccion);
                    command.Parameters.AddWithValue("@email", email);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Alumno actualizado correctamente.");
                    }
                    else
                    {
                        Console.WriteLine("No se pudo actualizar el alumno.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Revisa y averigua el error, Error al conectar a la base de datos: " + ex.Message);
                }
                connection.Close();
            }
        }
        public void EliminarAlumno()
        {
            Console.WriteLine("Ingrese el carnet del alumno a eliminar");
            string carnet = Console.ReadLine();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "DELETE FROM Tb_Alumnos WHERE carnet = @carnet";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@carnet", carnet);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Alumno eliminado correctamente.");
                    }
                    else
                    {
                        Console.WriteLine("No se pudo eliminar el alumno.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Revisa y averigua el error, Error al conectar a la base de datos: " + ex.Message);
                }
                connection.Close();
            }
        }
    }

}