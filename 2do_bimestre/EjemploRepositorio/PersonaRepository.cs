using Dapper;
using MySql.Data.MySqlClient;

namespace EjemploRepositorio;

public class PersonaRepository
{
    private readonly string connectionString;

    public PersonaRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public List<Persona> GetPesonas()
    {
        MySqlConnection mySqlConnection = new MySqlConnection(connectionString);

        mySqlConnection.Open();

        string query = "select * from persona";

        var personas = mySqlConnection.Query<Persona>(query).ToList();

        mySqlConnection.Close();

        return personas;
    }
}