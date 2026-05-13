using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using SkoloFotoExam26.Pages;
using System.Data;

namespace SkoloFotoExam26.Services
{
    public class SchoolRepoAsync : IRepoAsync<School, int>
    {
        #region QueryStrings
        private string _addSchool = "INSERT INTO School VALUES(@Name, @Street, @ZipCode, @SchoolType)";
        private string _countSchools = "SELECT COUNT(*) FROM School";
        private string _deleteSchool = "Delete FROM School WHERE SchoolID = @SchoolID";
        private string _getAllSchools = "SELECT schoolID, School.Name, School.StreetName, School.ZipCode, School.SchoolType, ZipCodeLookup.City FROM School JOIN ZipCodeLookup ON School.ZipCode = ZipCodeLookup.ZipCode";
        private string _getSchool = "SELECT School.SchoolID, School.Name, School.streetName, School.SchoolType, School.ZipCode, ZipCodeLookup.City FROM School JOIN ZipCodeLookup on School.ZipCode = ZipCodeLookup.ZipCode WHERE SchoolID = @SchoolID";
        private string _updateSchool = "UPDATE School SET Name = @Name, StreetName = @StreetName, ZipCode = @ZipCode, SchoolType = @SchoolType WHERE SchoolID = @SchoolID";

        #endregion


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
                    command.Parameters.AddWithValue("@SchoolType", (int)input.SchoolType); //Skal lige forhøre mig hos Rosa. //det er præcis sådan det gøres //Det var godt :-)
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
                return countOfSchools;
            }
        }

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
                        int valueType = reader.GetInt32("SchoolType");
                        int schoolID = reader.GetInt32("SchoolID");
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
    }
}
