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
        private string _getAllSchools = "SELECT * FROM  School";
        private string _getSchool = "SELECT School.Name, School.streetName, School.SchoolType, School.ZipCode, ZipCodeLookup.City FROM School JOIN ZipCodeLookup on School.ZipCode = ZipCodeLookup.ZipCode WHERE SchoolID = @SchoolID";

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
                    command.Parameters.AddWithValue("@SchoolType", (int)input.SchoolType); //Skal lige forhøre mig hos Rosa. //det er præcis sådan det gøres
                    command.Parameters.AddWithValue("@City", input.City);

                    int noOfRowsEffected = await command.ExecuteNonQueryAsync();

                    await connection.CloseAsync();
                }
                catch(SqlException sqlex)
                {
                    throw;
                }
                catch(Exception ex)
                {

                    throw;
                }
            }
        }

        public async Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int toDelete)//Sofie kom til lave den lidt for tidligt
        {
            using(SqlConnection connection = new SqlConnection(Secret.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_deleteSchool, connection);
                    await connection.OpenAsync();

                    command.Parameters.AddWithValue("@SchoolID", toDelete);
                    
                    int noOfRowsEffected = await command.ExecuteNonQueryAsync();

                    await connection.CloseAsync();
                }
                catch(SqlException sqlex)
                {
                    throw;
                }
                catch(Exception ex)
                {
                    throw;
                }

            }

        }

        public async Task<List<School>> GetAllAsync()
        {
            //List<School> schools = new List<School>();
            //using (SqlConnection connection = new SqlConnection())
            //{
            //    SqlCommand command = new SqlCommand(_getAllSchools, connection);
            //    await connection.OpenAsync();
            //    SqlDataReader reader = await command.ExecuteReaderAsync();

            //    while (reader.Read())
            //    {
            //        string name = reader.GetString("Name");
            //        string street = reader.GetString("Street");
            //        string zipCode = reader.GetString("ZipCode");
            //        SchoolType schooltype = Enum.Parse<SchoolType>reader.GetInt32("SchoolType");
            //    }
            //}

            throw new NotImplementedException();
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
                        //int schoolID = reader.GetInt32("SchoolID");
                        school = new School(name, street, city, zipCode, schoolType);
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

        public Task UpdateAsync(School toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
