using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using System.Data;

namespace SkoloFotoExam26.Services
{
    public class AdminRepoAsync : IRepoAsync<Administrator, int>, ILoginableRepo
    {
        private string _getAdminForLogin = "SELECT * FROM Administrator WHERE Email = @Email";

        public Task AddAsync(Administrator input)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int toDelete)
        {
            throw new NotImplementedException();
        }

        public Task<List<Administrator>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Administrator> GetAsync(int toGet)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetForLogin(string email)
        {
            Administrator admin = null;
            using SqlConnection connection = new SqlConnection(Secret.connectionString);
            try
            {
                using SqlCommand command = new SqlCommand(_getAdminForLogin, connection);
                await command.Connection.OpenAsync();
                command.Parameters.AddWithValue("@Email", email);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.Read())
                {
                    int adminID = reader.GetInt32("PhotographerID");
                    string firstName = reader.GetString("FirstName");
                    string lastName = reader.GetString("LastName");
                    string emailResult = reader.GetString("Email");
                    string phoneNumber = reader.GetString("PhoneNumber");

                    admin = new Administrator(adminID, firstName, lastName, phoneNumber, emailResult);
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
            return admin;
        }

        public Task UpdateAsync(Administrator toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
