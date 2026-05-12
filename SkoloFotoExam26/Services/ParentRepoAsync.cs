using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using System.Data;

namespace SkoloFotoExam26.Services
{
    public class ParentRepoAsync : IRepoAsync<Parent, int>
    {
        private string _addParent = "INSERT INTO Parent Values (@FirstName, @LastName, @Email, @PhoneNumber, @StreetName, @ZipCode)";
        private string _getAllParent = "SELECT p.ParentID, p.FirstName, p.LastName, p.Email, p.PhoneNumber, p.StreetName, p.ZipCode, z.City FROM Parent p JOIN ZipCodeLookup z ON p.ZipCode = z.ZipCode";
        private string _readParent = "SELECT * FROM Parent WHERE ParentID = @ParentID";
        //private string _updateParent = "UPDATE Parent SET FirstName=@FirstName, LastBane=@LastName, Email=@Email, PhoneNumber=@PhoneNumber, StreetName=@StreetName, ZipCode=@ZipCode, City=@City, WHERE ParentID = @ParentID";

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(Parent input)
        {
            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_addParent, connection);
                    await command.Connection.OpenAsync();

                    command.Parameters.AddWithValue("@FirstName", input.FirstName);
                    command.Parameters.AddWithValue("@LastName", input.LastName);
                    command.Parameters.AddWithValue("@Email", input.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", input.PhoneNumber);
                    command.Parameters.AddWithValue("@StreetName", input.Street);
                    command.Parameters.AddWithValue("@ZipCode", input.ZipCode);

                    int noOfRows = await command.ExecuteNonQueryAsync();
                    await command.Connection.CloseAsync();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error: " + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
                finally
                {

                }
            }
        }

        public Task<Parent> GetAsync(int toGet)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Parent>> GetAllAsync()
        {
            List<Parent> parents = new List<Parent>();
            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_getAllParent, connection);
                    await command.Connection.OpenAsync();

                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        int parentID = reader.GetInt32("ParentID");
                        string firstName = reader.GetString("FirstName");
                        string lastName = reader.GetString("LastName");
                        string email = reader.GetString("Email");
                        string phoneNumber = reader.GetString("PhoneNumber");
                        string street = reader.GetString("StreetName");
                        int zipCode = reader.GetInt32("ZipCode");
                        string city = reader.GetString("City");

                        parents.Add(new Parent(firstName, lastName, email, phoneNumber, street, zipCode, city, parentID));
                    }
                }
                catch (SqlException sqlExp)
                {
                    throw;
                }
                catch (Exception exp)
                {
                    throw;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
            return parents;
        }

        public Task UpdateAsync(Parent toUpdate)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int toDelete)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetForLogin(string email)
        {
            throw new NotImplementedException();
        }
    }
}
