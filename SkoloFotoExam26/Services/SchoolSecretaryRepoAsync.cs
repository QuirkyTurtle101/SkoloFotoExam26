using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using System.Data;

namespace SkoloFotoExam26.Services
{
    public class SchoolSecretaryRepoAsync : SofieConnectionString, ISchoolSecretaryRepoAsync
    {
        #region SQL querys
        private string _addSchoolSecretary = "INSERT INTO SchoolSecretary VALUES(@FirstName, @LastName, @Email, @PhoneNumber, @Initials, @SchoolID)";

        private string _getSchoolSecretary = "SELECT * FROM SchoolSecretary WHERE SchoolSecretaryID = @SchoolSecretaryID";
        #endregion


        public async Task AddAsync(SchoolSecretary input)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_addSchoolSecretary, connection);
                    await connection.OpenAsync();

                    command.Parameters.AddWithValue("@Name", input.FirstName);
                    command.Parameters.AddWithValue("@LastName", input.LastName);
                    command.Parameters.AddWithValue("@Email", input.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", input.PhoneNumber); 
                    command.Parameters.AddWithValue("@Initails", input.Initials);
                    command.Parameters.AddWithValue("@SchoolID", input.TheSchool.SchoolID);
                    int noOfRowsEffected = await command.ExecuteNonQueryAsync();

                    await connection.CloseAsync();
                }
                catch (SqlException sqlex)
                {
                    throw;
                }
                catch (Exception ex)
                {

                    throw;
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

        public Task<List<SchoolSecretary>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<SchoolSecretary> GetAsync(int toGet)
        {
            SchoolSecretary secretary = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_getSchoolSecretary, connection);
                    await connection.OpenAsync();
                    command.Parameters.AddWithValue("@SchoolSecretaryID", toGet);

                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.Read())
                    {
                        string firstName = reader.GetString("FirstName");
                        string lastName = reader.GetString("LastName");
                        string email = reader.GetString("Email");
                        string phoneNumber = reader.GetString("PhoneNumber");
                        int schoolSecretaryID = reader.GetInt32("SchoolSecretaryID");
                        string initials = reader.GetString("Initials");
                        //secretary = new SchoolSecretary(firstName, lastName, initials, phoneNumber, email, );
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
            return secretary;
        }

        public Task UpdateAsync(SchoolSecretary toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
