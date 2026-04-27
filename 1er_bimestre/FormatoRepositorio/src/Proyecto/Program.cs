using Proyecto;
using MySql.Data.MySqlClient;

string connectionString = "server=localhost;database=ejemplo;uid=root;pwd=pass123;";

// ─────────────────────────────────────────────
// Ejemplo 1: Consulta directa a la tabla persona
// ─────────────────────────────────────────────
Console.WriteLine("=== Ejemplo 1: SELECT directo a tabla persona ===");

using (MySqlConnection conn = new MySqlConnection(connectionString))
{
    conn.Open();

    string query = "SELECT id, nombre, dni FROM persona ORDER BY id";
    MySqlCommand cmd = new MySqlCommand(query, conn);

    using MySqlDataReader reader = cmd.ExecuteReader();
    while (reader.Read())
    {
        Persona p = new Persona(
            reader.GetInt32("id"),
            reader.GetString("nombre"),
            reader.GetInt32("dni")
        );
        Console.WriteLine(p);
    }
}

// ─────────────────────────────────────────────
// Ejemplo 2: Stored procedure crear_persona
// ─────────────────────────────────────────────
Console.WriteLine("\n=== Ejemplo 2: SP crear_persona ===");

using (MySqlConnection conn = new MySqlConnection(connectionString))
{
    conn.Open();

    MySqlCommand cmd = new MySqlCommand("crear_persona", conn);
    cmd.CommandType = System.Data.CommandType.StoredProcedure;

    cmd.Parameters.AddWithValue("unNombre", "carlos");
    cmd.Parameters.AddWithValue("unDni", 9999);

    int filasAfectadas = cmd.ExecuteNonQuery();
    Console.WriteLine($"Persona creada. Filas afectadas: {filasAfectadas}");
}

// ─────────────────────────────────────────────
// Ejemplo 3: Stored procedure listar_personas
// ─────────────────────────────────────────────
Console.WriteLine("\n=== Ejemplo 3: SP listar_personas ===");

using (MySqlConnection conn = new MySqlConnection(connectionString))
{
    conn.Open();

    MySqlCommand cmd = new MySqlCommand("listar_personas", conn);
    cmd.CommandType = System.Data.CommandType.StoredProcedure;

    List<Persona> personas = new List<Persona>();

    using MySqlDataReader reader = cmd.ExecuteReader();
    while (reader.Read())
    {
        personas.Add(new Persona(
            reader.GetInt32("id"),
            reader.GetString("nombre"),
            reader.GetInt32("dni")
        ));
    }

    foreach (Persona p in personas)
        Console.WriteLine(p);
}
