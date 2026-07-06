using Dapper;
using EjemploDapper;
using MySql.Data.MySqlClient;

List<Persona> personas = new List<Persona>();

string connectionString = "Server=localhost;Port=3306;User=root;Password=pass123;Database=ejemplo";

MySqlConnection mySqlConnection = new MySqlConnection(connectionString);

mySqlConnection.Open();

string query = "select * from persona";

personas = mySqlConnection.Query<Persona>(query).ToList();

foreach (var p in personas)
{
    Console.WriteLine($"Id: {p.Id}, Nombre: {p.Nombre}, DNI: {p.Dni}");
}

mySqlConnection.Close();