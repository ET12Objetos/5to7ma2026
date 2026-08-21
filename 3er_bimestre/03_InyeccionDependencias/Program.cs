using Microsoft.Extensions.Configuration;

ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

configurationBuilder.SetBasePath(AppContext.BaseDirectory);

configurationBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

IConfiguration configuration = configurationBuilder.Build();

string? connectionString = configuration.GetConnectionString("DefaultConnection");

Console.WriteLine(connectionString);
