using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using System.Data;

namespace SkoloFotoExam26.Services
{
    public class SchoolSecretaryRepoAsync : IRepoAsync<SchoolSecretary, int>, ILoginableRepo
    {
        #region Query strings
        private string _addSchoolSecretary = "INSERT INTO SchoolSecretary VALUES(@FirstName, @LastName, @Email, @PhoneNumber, @Initials, @SchoolID)";
        private string _getAllSchoolSecretaries = "SELECT SchoolSecretary.FirstName, SchoolSecretary.LastName, SchoolSecretary.SchoolSecretaryID, SchoolSecretary.Email, SchoolSecretary.PhoneNumber, SchoolSecretary.Initials, School.SchoolID, School.Name, School.StreetName, School.ZipCode, School.SchoolType, ZipCodeLookup.City FROM SchoolSecretary JOIN School on School.SchoolID = SchoolSecretary.SchoolID JOIN ZipCodeLookup ON School.ZipCode = ZipCodeLookup.ZipCode";
        private string _getSchoolSecretary = "SELECT SchoolSecretary.FirstName, SchoolSecretary.SchoolSecretaryID, SchoolSecretary.LastName, SchoolSecretary.Email, SchoolSecretary.PhoneNumber, SchoolSecretary.Initials, School.SchoolID, School.Name, School.StreetName, School.ZipCode FROM SchoolSecretary JOIN School on School.SchoolID = SchoolSecretary.SchoolID WHERE SchoolSecretaryID = @SchoolSecretaryID";
        private string _deleteSchoolSecretary = "DELETE FROM SchoolSecretary WHERE SchoolSecretaryID = @SchoolSecretaryID ";
        private string _updateSchoolSecretary = "UPDATE SchoolSecretary SET FirstName = @FirstName, LastName = @LastName, PhoneNumber = @PhoneNumber, Initials = @Initials, SchoolID = @SchoolID WHERE SchoolSecretaryID = @ID";
        private string _getSchoolSecretaryForLogin = "SELECT * FROM SchoolSecretary WHERE Email = @Email";
        private string _countSecretaries = "SELECT COUNT(*) FROM SchoolSecretary";
        #endregion

        #region constructor med SchoolRepo
        /// <summary>
        /// Provides access to asynchronous operations for managing School entities in the data store.
        /// </summary>
        IRepoAsync<School, int> _schoolRepo;

        /// <summary>
        /// Initializes a new instance of the SchoolSecretaryRepoAsync class with the specified school repository.
        /// </summary>
        /// <param name="schoolRepo">The asynchronous repository used to access and manage School entities. Cannot be null.</param>
        public SchoolSecretaryRepoAsync(IRepoAsync<School, int> schoolRepo)
        {
            _schoolRepo = schoolRepo;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Asynchronously adds a new school secretary to the data store.
        /// </summary>
        /// <remarks>This method does not return a result. If the operation fails, an exception is thrown.
        /// Ensure that the provided SchoolSecretary object contains all required information before calling this
        /// method.</remarks>
        /// <param name="input">The school secretary to add. Must not be null and must have a valid associated school.</param>
        /// <returns>A task that represents the asynchronous add operation.</returns>
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

        /// <summary>
        /// Asynchronously retrieves the total number of secretary records in the database.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the number of secretary records
        /// found in the database.</returns>
        public async Task<int> CountAsync()
        {
            int countOfSecretary;
            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_countSecretaries, connection);
                    await connection.OpenAsync();

                    countOfSecretary = Convert.ToInt32(await command.ExecuteScalarAsync());


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
                return countOfSecretary;
            }
        }

        /// <summary>
        /// Asynchronously deletes the school secretary record with the specified identifier from the database.
        /// </summary>
        /// <remarks>If no record with the specified identifier exists, the operation completes without
        /// throwing an exception. The method does not return information about whether a record was deleted.</remarks>
        /// <param name="toDelete">The unique identifier of the school secretary to delete.</param>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
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

        /// <summary>
        /// Asynchronously retrieves all school secretary records from the data source.
        /// </summary>
        /// <remarks>This method opens a database connection and queries for all school secretary entries.
        /// The operation is performed asynchronously and may throw exceptions related to database connectivity or query
        /// execution. Callers should handle potential exceptions as appropriate for their context.</remarks>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see
        /// cref="SchoolSecretary"/> objects representing all school secretaries. The list is empty if no records are
        /// found.</returns>
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

        /// <summary>
        /// Asynchronously retrieves a school secretary by the specified identifier.
        /// </summary>
        /// <remarks>If no school secretary exists with the specified identifier, the method returns
        /// <c>null</c>.</remarks>
        /// <param name="toGet">The unique identifier of the school secretary to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see
        /// cref="SchoolSecretary"/> instance if found; otherwise, <c>null</c>.</returns>
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

                    if (await reader.ReadAsync())
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
        
        /// <summary>
        /// Retrieves the user account associated with the specified email address for login purposes.
        /// </summary>
        /// <param name="email">The email address of the user to retrieve. Cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the user associated with the
        /// specified email address, or null if no matching user is found.</returns>
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

        /// <summary>
        /// Asynchronously updates the details of an existing school secretary in the database.
        /// </summary>
        /// <remarks>This method updates the school secretary's information based on the provided object's
        /// properties. The operation is performed asynchronously and does not return a result. Ensure that the <see
        /// cref="SchoolSecretary"/> object contains valid and complete data before calling this method.</remarks>
        /// <param name="toUpdate">The <see cref="SchoolSecretary"/> instance containing the updated information. The <see
        /// cref="SchoolSecretary.ID"/> property must identify an existing record to update. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous update operation.</returns>
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
                    command.Parameters.AddWithValue("@ID", toUpdate.ID);
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
        #endregion
    }
}
