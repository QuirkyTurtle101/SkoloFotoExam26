using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using System.Data;

namespace SkoloFotoExam26.Services
{
    public class SchoolSecretaryRepoAsync : IRepoAsync<SchoolSecretary, int>, ILoginableRepo
    {
        #region SQL querys
        private string _addSchoolSecretary = "INSERT INTO SchoolSecretary VALUES(@FirstName, @LastName, @Email, @PhoneNumber, @Initials, @SchoolID)";
        private string _getAllSchoolSecretaries = "SELECT SchoolSecretary.FirstName, SchoolSecretary.LastName, SchoolSecretary.SchoolSecretaryID, SchoolSecretary.Email, SchoolSecretary.PhoneNumber, SchoolSecretary.Initials, School.SchoolID, School.Name, School.StreetName, School.ZipCode, School.SchoolType, ZipCodeLookup.City FROM SchoolSecretary JOIN School on School.SchoolID = SchoolSecretary.SchoolID JOIN ZipCodeLookup ON School.ZipCode = ZipCodeLookup.ZipCode";
        private string _getSchoolSecretary = "SELECT SchoolSecretary.FirstName, SchoolSecretary.SchoolSecretaryID, SchoolSecretary.LastName, SchoolSecretary.Email, SchoolSecretary.PhoneNumber, SchoolSecretary.Initials, School.SchoolID, School.Name, School.StreetName, School.ZipCode FROM SchoolSecretary JOIN School on School.SchoolID = SchoolSecretary.SchoolID WHERE SchoolSecretaryID = @SchoolSecretaryID";
        private string _deleteSchoolSecretary = "DELETE FROM SchoolSecretary WHERE SchoolSecretaryID = @SchoolSecretaryID ";
        private string _updateSchoolSecretary = "UPDATE SchoolSecretary SET FirstName = @FirstName, LastName = @LastName, PhoneNumber = @PhoneNumber, Initials = @Initials, SchoolID = @SchoolSecretaryID WHERE SchoolID = @ID";
        private string _getSchoolSecretaryForLogin = "SELECT * FROM SchoolSecretary WHERE Email = @Email";
        #endregion

        IRepoAsync<School, int> _schoolRepo;
        public SchoolSecretaryRepoAsync(IRepoAsync<School, int> schoolRepo)
        {
            _schoolRepo = schoolRepo;
        }

        public async Task AddAsync(SchoolSecretary input)
        {
            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_addSchoolSecretary, connection);
                    await connection.OpenAsync();

                    command.Parameters.AddWithValue("@FirstName", input.FirstName);
                    command.Parameters.AddWithValue("@LastName", input.LastName);
                    command.Parameters.AddWithValue("@Email", input.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", input.PhoneNumber); 
                    command.Parameters.AddWithValue("@Initials", input.Initials);
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
                    SqlCommand command = new SqlCommand(_deleteSchoolSecretary, connection);
                    await connection.OpenAsync();

                    command.Parameters.AddWithValue("@SchoolSecretaryID", toDelete);

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


        public async Task<List<SchoolSecretary>> GetAllAsync()
        {
            List<SchoolSecretary> secretaries = new List<SchoolSecretary>();
            using(SqlConnection connection = new SqlConnection(Secret.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_getAllSchoolSecretaries, connection);
                    await connection.OpenAsync();

                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while(await reader.ReadAsync())
                    {
                        string firstName = reader.GetString("FirstName");
                        string lastName = reader.GetString("LastName");
                        string initials = reader.GetString("Initials");
                        string phoneNumber = reader.GetString("PhoneNumber");
                        string email = reader.GetString("Email");
                        string name = reader.GetString("Name");
                        string street = reader.GetString("StreetName");
                        int zipCode = reader.GetInt32("ZipCode");
                        string city = reader.GetString("City");
                        int valueType = reader.GetInt32("SchoolType");
                        SchoolType schoolType = (SchoolType)valueType;
                        int schoolID = reader.GetInt32("SchoolID");
                        int schoolSecretaryID = reader.GetInt32("SchoolSecretaryID");

                        secretaries.Add(new SchoolSecretary(schoolSecretaryID, firstName, lastName, initials, phoneNumber, email, new School(schoolID, name, street,city,zipCode, schoolType)));

                    }
                }
                catch(SqlException sqlex)
                {
                    throw;
                }
                catch(Exception ex)
                {
                    throw;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
            return secretaries;
        }

        public async Task<SchoolSecretary> GetAsync(int toGet)
        {
            SchoolSecretary secretary = null;
            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
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
                        int schoolID = reader.GetInt32("SchoolID");

                        secretary = new SchoolSecretary(schoolSecretaryID, firstName, lastName, initials, phoneNumber, email, await _schoolRepo.GetAsync(schoolID));
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

        public async Task<User> GetForLogin(string email)
        {
            SchoolSecretary secretary = null;
            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_getSchoolSecretaryForLogin, connection);
                    await connection.OpenAsync();
                    command.Parameters.AddWithValue("@Email", email);

                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.Read())
                    {
                        string firstName = reader.GetString("FirstName");
                        string lastName = reader.GetString("LastName");
                        string emailResult = reader.GetString("Email");
                        string phoneNumber = reader.GetString("PhoneNumber");
                        int schoolSecretaryID = reader.GetInt32("SchoolSecretaryID");
                        string initials = reader.GetString("Initials");
                        int SchoolID = reader.GetInt32("SchoolID");

                        secretary = new SchoolSecretary(schoolSecretaryID, firstName, lastName, initials, phoneNumber, emailResult, new School());
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

        public async Task UpdateAsync(SchoolSecretary toUpdate)
        {
            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
            using (SqlCommand command = new SqlCommand(_updateSchoolSecretary, connection))
            {
                try
                {
                    await command.Connection.OpenAsync();

                    command.Parameters.AddWithValue("@FirstName", toUpdate.FirstName);
                    command.Parameters.AddWithValue("@LastName", toUpdate.LastName);
                    command.Parameters.AddWithValue("@PhoneNumber", toUpdate.PhoneNumber);
                    command.Parameters.AddWithValue("@Initials", toUpdate.Initials);
                    command.Parameters.AddWithValue("@SchoolID", toUpdate.TheSchool.SchoolID);
                    command.Parameters.AddWithValue("@SchoolSecretaryID", toUpdate.ID);
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
