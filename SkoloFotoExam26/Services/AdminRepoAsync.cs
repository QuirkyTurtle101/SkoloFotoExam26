using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using System.Data;

namespace SkoloFotoExam26.Services
{
    public class AdminRepoAsync : IRepoAsync<Administrator, int>, ILoginableRepo
    {
        #region Query strings
        private string _addAdmin = "INSERT INTO Administrator VALUES (@FirstName, @LastName, @Email, @PhoneNumber)";
        private string _getAllAdmins = "SELECT * FROM Administrator";
        private string _getAdmin = "SELECT * FROM Administrator WHERE AdministratorID = @AdministratorID";
        private string _deleteAdmin = "DELETE FROM Administrator WHERE AdministratorID = @AdministratorID";
        private string _updateAdmin = " UPDATE Administrator SET FirstName = @FirstName, LastName = @LastName, PhoneNumber = @PhoneNumber WHERE AdministratorID = @AdminID";
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
                    command.Parameters.AddWithValue("@Email", input.Email);
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

        public async Task DeleteAsync(int toDelete)
        {
            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_deleteAdmin, connection);
                    await connection.OpenAsync();

                    command.Parameters.AddWithValue("@AdministratorID", toDelete);

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

        public async Task<List<Administrator>> GetAllAsync()
        {
            List<Administrator> administrators = new List<Administrator>();
            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_getAllAdmins, connection);
                    await connection.OpenAsync();

                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        string firstName = reader.GetString("FirstName");
                        string lastName = reader.GetString("LastName");
                        string phoneNumber = reader.GetString("PhoneNumber");
                        string email = reader.GetString("Email");
                        int id = reader.GetInt32("AdministratorID");

                        administrators.Add(new Administrator(id, firstName,lastName,phoneNumber,email));
                    }
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
            return administrators;


        }

        public async Task<Administrator> GetAsync(int toGet)
        {
            Administrator admin = null;
            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_getAdmin, connection);
                    await connection.OpenAsync();
                    command.Parameters.AddWithValue("@AdministratorID", toGet);

                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.Read())
                    {
                        string firstName = reader.GetString("FirstName");
                        string lastName = reader.GetString("LastName");
                        string email = reader.GetString("Email");
                        string phoneNumber = reader.GetString("PhoneNumber");
                        int id = reader.GetInt32("AdministratorID");

                        admin = new Administrator(id, firstName, lastName, phoneNumber, email);
                    }
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
            return admin;
        }
        

        public Task<User> GetForLogin(string email)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Administrator toUpdate)
        {
            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
            using (SqlCommand command = new SqlCommand(_updateAdmin, connection))
            {
                try
                {
                    await command.Connection.OpenAsync();

                    command.Parameters.AddWithValue("@FirstName", toUpdate.FirstName);
                    command.Parameters.AddWithValue("@LastName", toUpdate.LastName);
                    command.Parameters.AddWithValue("@PhoneNumber", toUpdate.PhoneNumber);
                    command.Parameters.AddWithValue("@AdminID", toUpdate.ID);
                    await command.ExecuteNonQueryAsync();

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
                    await command.Connection.CloseAsync();
                }
            }
        }
    }
}
