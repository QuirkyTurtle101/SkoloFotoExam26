using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Services
{
    public class LoginRepoAsync : IRepoAsync<LoginInfo, string>
    {
        private string _addLogin = "INSERT INTO LoginInfo VALUES (@Email, @PasswordHash, @TheUserType)";
        private string _getLogin = "SELECT * FROM LoginInfo WHERE LoginInfo.Email = @Email";

        public async Task AddAsync(LoginInfo input)
        {
            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
            using (SqlCommand command = new SqlCommand(_addLogin, connection))
            {
                try
                {
                    Task connected = connection.OpenAsync();

                    command.Parameters.AddWithValue("@Email", input.Email);
                    command.Parameters.AddWithValue("@PasswordHash", input.PasswordHash);
                    command.Parameters.AddWithValue("TheUserType", (int)input.TheUserType);

                    await connected;

                    int noOfRows = await command.ExecuteNonQueryAsync();
                }
                catch (Exception e)
                {

                }
                finally
                {
                    await command.Connection.CloseAsync();
                }
            }
        }

        public async Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(string toDelete)
        {
            throw new NotImplementedException();
        }

        public async Task<List<LoginInfo>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<LoginInfo> GetAsync(string toGet)
        {
            LoginInfo output = null;

            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
            using (SqlCommand command = new SqlCommand(_addLogin, connection))
            {
                try
                {
                    Task connected = connection.OpenAsync();

                    command.Parameters.AddWithValue("@Email", toGet);

                    await connected;

                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        string loginInfoEmail = reader.GetString(0);
                        byte[] loginInfoPwHash = (byte[])reader.GetSqlBinary(1);
                        UserType loginInfoUserType = (UserType)reader.GetInt32(2);
                        output = new LoginInfo(loginInfoEmail, loginInfoPwHash, loginInfoUserType);
                    }
                    await reader.CloseAsync();

                }
                catch(Exception e)
                {

                }
                finally
                {
                    await connection.CloseAsync();
                }
                return output;
            }
        }

        public async Task UpdateAsync(LoginInfo toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
