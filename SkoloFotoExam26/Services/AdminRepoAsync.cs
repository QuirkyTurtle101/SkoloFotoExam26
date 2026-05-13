using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Services
{
    public class AdminRepoAsync : IRepoAsync<Administrator, int>, ILoginableRepo
    {
        #region Query strings
        private string _addAdmin = "INSERT INTO Administrator VALUES (FirstName = @FirstName, LastName = @LastName, Email = @Email, PhoneNumber = @PhoneNumber)";

        #endregion


        public async Task AddAsync(Administrator input)
        {
            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_addAdmin, connection);
                    await connection.OpenAsync();

                    command.Parameters.AddWithValue("@FirstName", input.FirstName);
                    command.Parameters.AddWithValue("@LastName", input.LastName);
                    command.Parameters.AddWithValue("@ZipCode", input.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", input.PhoneNumber);
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

        public Task<User> GetForLogin(string email)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Administrator toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
