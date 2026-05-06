using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using SkoloFotoExam26.Pages;
using System.Data;

namespace SkoloFotoExam26.Services
{
    public class SchoolRepoAsync : SofieConnectionString, ISchoolRepoAsync
    {
        #region QueryStrings
        private string _addSchool = "INSERT INTO School VALUES(@Name, @StreetName, @ZipCode, @SchoolType)";
        private string _countSchools = "SELECT COUNT(*) FROM School";
        private string _deleteSchool = "Delete FROM School WHERE SchoolID = @SchoolID";
        private string _getAllSchools = "SELECT * FROM  school";

        #endregion


        public async Task AddAsync(School input)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_addSchool, connection);
                    await connection.OpenAsync();

                    command.Parameters.AddWithValue("@Name", input.Name);
                    command.Parameters.AddWithValue("@StreetName", input.StreetName);
                    command.Parameters.AddWithValue("@ZipCode", input.ZipCode);
                    command.Parameters.AddWithValue("@SchoolType", (int)input.SchoolType); //Skal lige forhøre mig hos Rosa.
                    //command.Parameters.AddWithValue("@City")

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
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    try
            //    {
            //        SqlCommand command = new SqlCommand(_countSchools, connection);
            //        await connection.OpenAsync();
            //        command.ex
            //    }
            //}
        }

        public async Task DeleteAsync(int toDelete)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
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
            //        string streetName = reader.GetString("StreetName");
            //        string zipCode = reader.GetString("ZipCode");
            //        SchoolType schooltype = Enum.Parse<SchoolType>reader.GetInt32("SchoolType");
            //    }
            //}

            throw new NotImplementedException();
        }

        public Task<School> GetAsync(int toGet)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(School toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
