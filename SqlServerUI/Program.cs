using DataAccessLibrary;
using Microsoft.Extensions.Configuration;

namespace SqlServerUI
{
  class Program
  {
    static void Main(string[] args)
    {
      SqlCrud sql = new SqlCrud(GetConnectionString());

      ReadAllContacts(sql);

      Console.ReadLine();
    }

    private static void ReadAllContacts(SqlCrud sql)
    {
      var rows = sql.GetAllContacts();

      foreach (var row in rows)
      {
        Console.WriteLine($"{row.Id} - {row.FirstName} - {row.LastName}");
      }
    }


    private static string GetConnectionString(string connectionStringName = "Default")
    {
      var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json");
      var config = builder.Build();
      
      string output = config.GetConnectionString(connectionStringName);

      return output;

    }
  }
}