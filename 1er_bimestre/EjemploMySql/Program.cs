using MySql.Data.MySqlClient;

string connectionString = "Server=localhost;Port=3306;User=root;Password=pass123;Database=ejemplo";

MySqlConnection mySqlConnection = new MySqlConnection(connectionString);

mySqlConnection.Open();

MySqlCommand command = new MySqlCommand("select * from persona;", mySqlConnection);

MySqlDataReader reader = command.ExecuteReader();

while (reader.Read())
{
    Console.WriteLine($"{reader["id"]} {reader["nombre"]} {reader["dni"]}");
}

reader.Close();

mySqlConnection.Close();