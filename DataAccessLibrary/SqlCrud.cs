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

    public FullContactModel GetFullContactById(int ContactId)
    {
      //get basic contact model
      string sql = "SELECT Id, FirstName, LastName FROM dbo.Contacts where Id = @ContactId";
      FullContactModel outup = new();
      outup.BasicInfo = _db.LoadData<BasicContactModel, dynamic>(sql, new { ContactId = ContactId }, _connectionString).FirstOrDefault();

      if (outup.BasicInfo == null) return null;

      string phoneNumbersSql = "SELECT pn.Id, pn.PhoneNumber FROM dbo.PhoneNumbers pn " +
                                "INNER JOIN dbo.ContactPhoneNumbers cpn ON " +
                                "pn.id = cpn.PhoneNumberId " +
                                "WHERE cpn.ContactId = @ContactId";
      outup.PhoneNumbers = _db.LoadData<PhoneNumberModel, dynamic>(phoneNumbersSql, new { ContactId = ContactId }, _connectionString);

      string emailNumbersSql = "SELECT e.Id, e.Email FROM dbo.EmailAddresses e " +
                                "INNER JOIN dbo.ContactEmail ce ON " +
                                "e.id = ce.EmailId " +
                                "WHERE ce.ContactId = @ContactId";
      outup.EmailAddresses = _db.LoadData<EmailAddress, dynamic>(emailNumbersSql, new { ContactId = ContactId }, _connectionString);

      return outup;
    }

    public void CreateContact (FullContactModel contact)
    {
      

    }
  }
}
