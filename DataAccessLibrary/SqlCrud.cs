using DataAccessLibrary.Models;

namespace DataAccessLibrary
{
  public class SqlCrud
  {
    private readonly string _connectionString;
    private SqlDataAccess _db = new SqlDataAccess();

    public SqlCrud(string connectionString)
    {
      _connectionString = connectionString;
    }

    public List<BasicContactModel> GetAllContacts()
    {
      string sql = "SELECT Id, FirstName, LastName FROM dbo.Contacts";
      return _db.LoadData<BasicContactModel, dynamic>(sql, new { }, _connectionString);
    }
  }
}
