using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using System.Data;

namespace SkoloFotoExam26.Services
{
    public class ParentRepoAsync : IRepoAsync<Parent, int>, ILoginableRepo
    {
        private string _addParent = "INSERT INTO Parent Values (@FirstName, @LastName, @Email, @PhoneNumber, @StreetName, @ZipCode)";
        private string _getAllParent = "SELECT p.ParentID, p.FirstName, p.LastName, p.Email, p.PhoneNumber, p.StreetName, p.ZipCode, z.City FROM Parent p JOIN ZipCodeLookup z ON p.ZipCode = z.ZipCode";
        private string _getParent = "SELECT * FROM Parent JOIN ZipCodeLookup ON Parent.ZipCode=ZipCodeLookup.ZipCode WHERE ParentID = @ParentID";
        private string _deleteParent = "DELETE FROM Parent WHERE ParentID = @ParentID";
        private string _updateParent = "UPDATE Parent SET " +
            "FirstName=@FirstName, " +
            "LastName=@LastName, " +
            "Email=@Email, " +
            "PhoneNumber=@PhoneNumber, " +
            "StreetName=@StreetName, " +
            "ZipCode=@ZipCode " +
            "WHERE ParentID = @ID";

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

        public async Task<Parent> GetAsync(int toGet)
        {
            Parent parent = null;
            using SqlConnection connection = new SqlConnection(Secret.connectionString); 
            try
            {
                using SqlCommand command = new SqlCommand(_getParent, connection);
                await command.Connection.OpenAsync();
                command.Parameters.AddWithValue("@ParentID", toGet);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.Read())
                {
                    int parentID = reader.GetInt32("ParentID");
                    string firstName = reader.GetString("FirstName");
                    string lastName = reader.GetString("LastName");
                    string email = reader.GetString("Email");
                    string phoneNumber = reader.GetString("PhoneNumber");
                    string city = reader.GetString("City");
                    int zipCode = reader.GetInt32("ZipCode");
                    string street = reader.GetString("StreetName");
                    parent = new Parent(parentID, firstName, lastName, email, phoneNumber, city, zipCode, street);
                }                
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Exception message: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception message: {ex.Message}");
            }
            finally
            {
                await connection.CloseAsync();
            }
            return parent;
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

                        parents.Add(new Parent(parentID,firstName, lastName, email, phoneNumber, street, zipCode, city));
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

        public async Task UpdateAsync(Parent toUpdate)
        {
            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
            using (SqlCommand command = new SqlCommand(_updateParent, connection))
            {
                try
                {
                    await command.Connection.OpenAsync();

                    command.Parameters.AddWithValue("@FirstName", toUpdate.FirstName);
                    command.Parameters.AddWithValue("@LastName", toUpdate.LastName);
                    command.Parameters.AddWithValue("@Email", toUpdate.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", toUpdate.PhoneNumber);
                    command.Parameters.AddWithValue("@ZipCode", toUpdate.ZipCode);
                    command.Parameters.AddWithValue("@StreetName", toUpdate.Street);
                    command.Parameters.AddWithValue("@ID", toUpdate.ID);

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected == 0)
                    {
                        Console.WriteLine("Ingen rækker blev opdateret! Tjek om ID'et findes.");
                    }
                }
                catch (SqlException sqlex)
                {
                    Console.WriteLine("Sql fejl: " + sqlex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
                finally
                {
                    await command.Connection.CloseAsync();
                }
            }
        }

        public async Task DeleteAsync(int toDelete)
        {
            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_deleteParent, connection);
                    await connection.OpenAsync();

                    command.Parameters.AddWithValue("ParentID", toDelete);

                    int noOfRowsEffected = await command.ExecuteNonQueryAsync();
                }
                catch (SqlException sqlex)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }

        public Task<User> GetForLogin(string email)
        {
            throw new NotImplementedException();
        }
    }
}
