using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using SkoloFotoExam26.Pages;
using System.Data;

namespace SkoloFotoExam26.Services
{
    public class SchoolRepoAsync : IRepoAsync<School, int>
    {
        #region Query strings
        private string _addSchool = "INSERT INTO School VALUES(@Name, @Street, @ZipCode, @SchoolType)";
        private string _countSchools = "SELECT COUNT(*) FROM School";
        private string _deleteSchool = "Delete FROM School WHERE SchoolID = @SchoolID";
        private string _getAllSchools = "SELECT schoolID, School.Name, School.StreetName, School.ZipCode, School.SchoolType, ZipCodeLookup.City FROM School JOIN ZipCodeLookup ON School.ZipCode = ZipCodeLookup.ZipCode";
        private string _getSchool = "SELECT School.SchoolID, School.Name, School.streetName, School.SchoolType, School.ZipCode, ZipCodeLookup.City FROM School JOIN ZipCodeLookup on School.ZipCode = ZipCodeLookup.ZipCode WHERE SchoolID = @SchoolID";
        private string _updateSchool = "UPDATE School SET Name = @Name, StreetName = @StreetName, ZipCode = @ZipCode, SchoolType = @SchoolType WHERE SchoolID = @SchoolID";

        #endregion

        #region Methods
        /// <summary>
        /// Asynchronously adds a new school record to the database using the specified school information.
        /// </summary>
        /// <remarks>This method opens a new database connection for the operation. The input object's
        /// properties are mapped to the corresponding database fields. The method does not return the newly created
        /// record or its identifier.</remarks>
        /// <param name="input">The school entity containing the details to be added. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous add operation.</returns>
        public async Task AddAsync(School input)
        {
            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_addSchool, connection);
                    await connection.OpenAsync();

                    command.Parameters.AddWithValue("@Name", input.Name);
                    command.Parameters.AddWithValue("@Street", input.Street);
                    command.Parameters.AddWithValue("@ZipCode", input.ZipCode);
                    command.Parameters.AddWithValue("@SchoolType", (int)input.SchoolType);
                    command.Parameters.AddWithValue("@City", input.City);
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
        /// Asynchronously retrieves the total number of schools in the database.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the number of schools found in
        /// the database.</returns>
        public async Task<int> CountAsync()
        {
            int countOfSchools;
            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_countSchools, connection);
                    await connection.OpenAsync();

                    countOfSchools = Convert.ToInt32(await command.ExecuteScalarAsync());


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
                return countOfSchools;
            }
        }

        /// <summary>
        /// Asynchronously deletes the school with the specified identifier, the SchoolID, from the database.  The
        /// operation is performed asynchronously and may throw exceptions if the database is unavailable, if there is dependancies or the query
        /// fails.
        /// </summary>
        /// <param name="toDelete">The unique identifier of the school to delete.</param>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        public async Task DeleteAsync(int toDelete)
        {
            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_deleteSchool, connection);
                    await connection.OpenAsync();

                    command.Parameters.AddWithValue("@SchoolID", toDelete);

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
        /// Asynchronously retrieves all schools from the database.
        /// </summary>
        /// <remarks>This method opens a database connection and queries for all school records. The
        /// operation is performed asynchronously and may throw exceptions if the database is unavailable or the query
        /// fails.</remarks>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of all schools. The list
        /// will be empty if no schools are found.</returns>
        public async Task<List<School>> GetAllAsync()
        {
            List<School> schools = new List<School>();
            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_getAllSchools, connection);
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        string name = reader.GetString("Name");
                        string street = reader.GetString("StreetName");
                        int zipCode = reader.GetInt32("ZipCode");
                        string city = reader.GetString("City");
                        int schoolID = reader.GetInt32("SchoolID");
                        
                        int valueType = reader.GetInt32("SchoolType");
                        SchoolType schoolType = (SchoolType)valueType;
                        schools.Add(new School(schoolID, name, street, city, zipCode, schoolType));
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
            return schools;

        }
        /// <summary>
        /// Asynchronously retrieves a school by SchoolID.
        /// </summary>
        /// <remarks>If no school with the specified identifier exists, the method returns <see
        /// langword="null"/>. The operation opens and closes a database connection for each call. This method is not
        /// thread-safe; concurrent calls should use separate instances.</remarks>
        /// <param name="toGet">The unique identifier of the school to retrieve, the schoolID. Must correspond to an existing school in the database.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="School"/>
        /// instance if found; otherwise, <see langword="null"/>.</returns>

        public async Task<School> GetAsync(int toGet)
        {
            School school = null;
            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_getSchool, connection);
                    await connection.OpenAsync();
                    command.Parameters.AddWithValue("@SchoolID", toGet);

                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.Read())
                    {
                        string name = reader.GetString("Name");
                        string street = reader.GetString("StreetName");
                        int zipCode = reader.GetInt32("ZipCode");
                        string city = reader.GetString("City");
                        int valueType = reader.GetInt32("SchoolType");
                        SchoolType schoolType = (SchoolType)valueType;
                        int schoolID = reader.GetInt32("SchoolID");
                        school = new School(schoolID, name, street, city, zipCode, schoolType);
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
            return school;
        }

        /// <summary>
        /// Asynchronously updates the details of an existing school in the database.
        /// </summary>
        /// <param name="toUpdate">The school entity containing the updated information. The school's identifier must correspond to an existing
        /// record.</param>
        /// <returns>A task that represents the asynchronous update operation.</returns>
        public async Task UpdateAsync(School toUpdate)
        {
            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
            using (SqlCommand command = new SqlCommand(_updateSchool, connection))
            {
                try
                {
                    await command.Connection.OpenAsync();

                    command.Parameters.AddWithValue("@Name", toUpdate.Name);
                    command.Parameters.AddWithValue("@StreetName", toUpdate.Street);
                    command.Parameters.AddWithValue("@ZipCode", toUpdate.ZipCode);
                    command.Parameters.AddWithValue("@SchoolType", (int)toUpdate.SchoolType);
                    command.Parameters.AddWithValue("@SchoolID", toUpdate.SchoolID);
                    await command.ExecuteNonQueryAsync();

                }
                catch (SqlException sqlex)
                {
                    Console.WriteLine("sql fejl: " + sqlex.Message);
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
        #endregion
    }
}
