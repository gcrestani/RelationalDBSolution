using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;

namespace SqlServerUI
{
  class Program
  {
    static void Main(string[] args)
    {
      SqlCrud sql = new SqlCrud(GetConnectionString());

      //ReadAllContacts(sql);
      ReadFullContact(sql, 1002);

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

    private static void ReadFullContact(SqlCrud sql, int contactId)
    {
      var contact = sql.GetFullContactById(contactId);

      if (contact == null)
      {
        Console.WriteLine($"Contact {contactId} not found.");
        return;
      }

      Console.Write("Contact Info: \n");
      Console.Write($"{contact.BasicInfo.FirstName} - {contact.BasicInfo.LastName} \n");

      Console.Write("Phone Info: \n");
      foreach (PhoneNumberModel phone in contact.PhoneNumbers)
      {
        Console.WriteLine($"{phone.Id} - {phone.PhoneNumber}");
      }

      Console.Write("Email Info: \n");
      foreach (EmailAddress email in contact.EmailAddresses)
      {
        Console.WriteLine($"{email.Id} - {email.Email}");
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