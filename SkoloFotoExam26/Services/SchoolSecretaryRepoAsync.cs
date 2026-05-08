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
        #endregion


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

        public Task DeleteAsync(int toDelete)
        {
            throw new NotImplementedException();
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

                    while(reader.Read())
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

                        secretaries.Add(new SchoolSecretary(firstName, lastName, initials, phoneNumber, email, new School(schoolID, name, street,city,zipCode, schoolType), schoolSecretaryID));

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
                        int SchoolID = reader.GetInt32("SchoolID");

                        secretary = new SchoolSecretary(firstName, lastName, initials, phoneNumber, email, new School(), schoolSecretaryID);
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

        public Task<User> GetForLogin(string email)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(SchoolSecretary toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
